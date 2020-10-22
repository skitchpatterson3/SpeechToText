using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSpeechToTextDemo.Models
{
    public class AudioFileForUploadDto
    {
        [Required(ErrorMessage = "You must provide a name value.")]
        [MaxLength(100)]
        public string Filename { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
