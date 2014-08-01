using Dell.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dell.Models {
    public class Asset {
        //public ExtensionDataObject ExtensionData { get; set; }

        public Asset() {
        }

        [JsonProperty("ServiceTag")]
        public string Svctag { get; set; }

        [JsonProperty("MachineDescription")]
        public string Description { get; set; }
    }
}
