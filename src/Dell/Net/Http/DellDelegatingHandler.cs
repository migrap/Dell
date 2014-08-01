using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dell.Net.Http {
    internal class DellDelegatingHandler : DelegatingHandler {
        public DellDelegatingHandler() {
            InnerHandler = new HttpClientHandler() {
                AllowAutoRedirect = true,
                ClientCertificateOptions = ClientCertificateOption.Automatic,
            };
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            return base.SendAsync(request, cancellationToken);
        }
    }
}
