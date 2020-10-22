using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSpeechToTextDemo.Entities
{
    public class AudioFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Filename { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
