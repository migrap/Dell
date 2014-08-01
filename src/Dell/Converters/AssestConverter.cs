using Dell.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dell.Converters {
    internal class AssestConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return typeof(Asset).Equals(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var jobject = JObject.Load(reader);
            var token = jobject.SelectToken("GetAssetDetailResponse.GetAssetDetailResult.Response.DellAsset");
            return token.ToObject(objectType);         
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override bool CanRead {
            get { return true; }
        }

        public override bool CanWrite {
            get { return false; }
        }        
    }    
    
}
