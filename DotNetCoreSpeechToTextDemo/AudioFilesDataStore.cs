using DotNetCoreSpeechToTextDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSpeechToTextDemo
{
    public class AudioFilesDataStore
    {
        public static AudioFilesDataStore Current { get; } = new AudioFilesDataStore();

        public List<AudioFileDto> AudioFiles { get; set; }

        public AudioFilesDataStore()
        {
            // init dummy data
            DateTime currentDateTime = new DateTime();
            currentDateTime = DateTime.Now;

            AudioFiles = new List<AudioFileDto>()
            {
                new AudioFileDto()
                {
                    Id = 1,
                    Filename = "Rec01",
                    Description = "First audio file from DataStore",
                    CreatedOn = currentDateTime.AddDays(-7).AddHours(-5).AddMinutes(-8).AddSeconds(35)
                },
                new AudioFileDto()
                {
                    Id = 2,
                    Filename = "Rec02",
                    Description = "Second audio file from DataStore",
                    CreatedOn = currentDateTime.AddDays(-3).AddHours(-1).AddMinutes(-42).AddSeconds(-9)
                },
                new AudioFileDto()
                {
                    Id = 3,
                    Filename = "Rec03",
                    Description = "Third audio file from DataStore",
                    CreatedOn = currentDateTime
                }
            };
        }
    }
}
