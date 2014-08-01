using Dell;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox {
    class Program {
        static void Main(string[] args) {
            var results = new List<Result>();
            var dell = new DellClient();
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var line = (string)null;
            using (var reader = new StreamReader(Path.Combine(documents, "dell.txt"))) {
                while (null != (line = reader.ReadLine())) {
                    line = line.Trim();
                    try {
                        var assest = dell.GetAssetAsync(line).Result;
                        results.Add(new Result { Svctag = assest.Svctag, Description = assest.Description });
                    }
                    catch { }
                }
            }

            using(var writer = new StreamWriter(Path.Combine(documents, "dell.csv"))) {
                foreach(var result in results) {
                    writer.WriteLine(string.Format("{0},{1}", result.Svctag, result.Description));
                }
                writer.Flush();
            }
        }
    }

    public class Result {
        public string Svctag { get; set; }
        public string Description { get; set; }
    }
}
