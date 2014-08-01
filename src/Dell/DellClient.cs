using Dell.Models;
using Dell.Net.Http;
using Dell.Net.Http.Configurators;
using Dell.Net.Http.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dell {
    public class DellClient {
        static DellClient() {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, error) => true;
        }

        private HttpClient _client;

        private MediaTypeFormatter _formatter = new DellMediaTypeFormatter();
        private string _apikey;

        private DellClient() {
            _client = new HttpClient(new DellDelegatingHandler()) {
                BaseAddress = new Uri("https://api.dell.com/support/v2/assetinfo/")
            };
            _client.DefaultRequestHeaders.Clear();
        }

        public DellClient(string apikey = "1adecee8a60444738f280aad1cd87d0e") : this() {
            _apikey = apikey;
        }

        public async Task<Asset> GetAssetAsync(string svctag) {
            return await _client.SendAsync(x => x
                .Method(HttpMethod.Get)
                .Address("detail/tags.json?svctags={0}&apikey={1}", svctag, _apikey)
            ).ReadAsAsync<Asset>(_formatter);
        }
    }

    public static partial class Extensions {
        private static TResult Configure<TSource, TResult>(Action<TSource> configure) where TResult : TSource, new() {
            var result = new TResult();
            configure(result);
            return result;
        }
        internal static Task<HttpResponseMessage> SendAsync(this HttpClient client, Action<IHttpRequestMessageConfigurator> configure) {
            var request = Configure<IHttpRequestMessageConfigurator, HttpRequestMessageConfigurator>(configure).Build();
            return client.SendAsync(request);
        }

        internal static IHttpRequestMessageConfigurator Address(this IHttpRequestMessageConfigurator self, string format, params object[] args) {
            return self.Address(format.FormatWith(args));
        }

        internal static IHttpRequestMessageConfigurator Content(this IHttpRequestMessageConfigurator self, object value, MediaTypeFormatter formatter) {
            return self.Content(new ObjectContent(value.GetType(), value, formatter));
        }

        internal static string FormatWith(this string format, params object[] args) {
            return string.Format(format, args);
        }

        internal static Task<T> ReadAsAsync<T>(this Task<HttpResponseMessage> message, params MediaTypeFormatter[] formatters) {
            var response = message
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<T>(formatters);
        }
    }
}

//https://api.dell.com/support/v2/assetinfo/detail/tags.json?svctags=7K8SPY1&apikey=1adecee8a60444738f280aad1cd87d0e