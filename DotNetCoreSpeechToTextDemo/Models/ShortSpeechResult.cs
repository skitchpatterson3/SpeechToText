using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSpeechToTextDemo.Models
{
    public class ShortSpeechResult
    {
        public ResultReason Reason { get; set; }

        public TimeSpan Duration { get; set; }

        public long OffsetInTicks { get; set; }

        public string Text { get; set; }
    }
}
