using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Core.DataTable.Model
{
    public class JsonRepsonse<T> where T : class
    {
        public JsonRepsonse(string draw, int recordsFiltered, int recordsTotal, List<T> data)
        {
            Draw = draw;
            RecordsFiltered = recordsFiltered;
            RecordsTotal = recordsTotal;
            Data = data;
        }

        [JsonProperty("draw")]
        public string Draw { get; set; }

        [JsonProperty("recordsFiltered")]
        public int RecordsFiltered { get; set; }

        [JsonProperty("recordsTotal")]
        public int RecordsTotal { get; set; }

        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}
