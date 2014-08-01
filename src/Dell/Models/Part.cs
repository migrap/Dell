using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dell.Models {
    public class Part : IExtensibleDataObject {
        public ExtensionDataObject ExtensionData { get; set; }

        [JsonProperty("PartDescription")]
        public string Description { get; set; }

        [JsonProperty("PartNumber")]
        public string Number { get; set; }

        [JsonProperty("Quantity")]
        public string Quantity { get; set; }

        [JsonProperty("SkuNumber")]
        public string Sku { get; set; }
    }
}
