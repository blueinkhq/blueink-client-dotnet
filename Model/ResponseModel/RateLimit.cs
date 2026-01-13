using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueink.Client.Net.v2.ResponseModel
{
    public class RateLimit
    {
        [Newtonsoft.Json.JsonPropertyAttribute("limit")]
        public virtual int Limit { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("remaining")]
        public virtual int Remaining { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("reset")]
        public virtual int Reset { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("window")]
        public virtual int Window { get; set; }

    }
}
