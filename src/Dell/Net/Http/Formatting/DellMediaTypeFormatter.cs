using Dell.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace Dell.Net.Http.Formatting {
    internal class DellMediaTypeFormatter : JsonMediaTypeFormatter { 
        public DellMediaTypeFormatter() {
            SerializerSettings.Converters.Add(new AssestConverter());
        }
    }
}
