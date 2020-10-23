using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSpeechToTextDemo.Models
{
    public class RESTSpeechToTextResult
    {
        public RESTSpeechToTextResult()
        {
            this.NBest = new NBest();
        }

        [JsonProperty("RecognitionStatus")]
        public string RecognitionStatus { get; set; }

        [JsonProperty("Offset")]
        public long Offset { get; set; }

        [JsonProperty("Duration")]
        public TimeSpan Duration { get; set; }

        [JsonProperty("NBest")]
        public NBest NBest { get; set; }
    }

    public class NBest
    {
        [JsonProperty("Confidence")]
        public double Confidence { get; set; }

        [JsonProperty("Lexical")]
        public string Lexical { get; set; }

        [JsonProperty("ITN")]
        public string Itn { get; set; }

        [JsonProperty("MaskedITN")]
        public string MaskedItn { get; set; }

        [JsonProperty("Display")]
        public string Display { get; set; }
    }
}
