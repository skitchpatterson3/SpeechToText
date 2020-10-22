using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSpeechToTextDemo.Models
{
    public class AudioFileDto
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
