using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAF1
{
 public   class top_apps
    {

        public string Id { get; set; }

        [JsonProperty(PropertyName = "topappname")]
        public string topappname { get; set; }

        [JsonProperty(PropertyName = "topapplink")]
        public string topapplink { get; set; }

        [JsonProperty(PropertyName = "topappsection")]
        public string topappsection { get; set; }

        [JsonProperty(PropertyName = "topapptype")]
        public string topapptype { get; set; }



        [JsonProperty(PropertyName = "topapps")]
        public int topapps { get; set; }

        [JsonProperty(PropertyName = "topapplogo")]
        public string topapplogo { get; set; }

        [JsonProperty(PropertyName = "topdevelopername")]
        public string topdevelopername { get; set; }

        [JsonProperty(PropertyName = "topappdescription")]
        public string topappdescription { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }
    }
}
