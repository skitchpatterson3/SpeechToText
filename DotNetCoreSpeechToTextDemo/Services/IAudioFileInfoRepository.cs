using DotNetCoreSpeechToTextDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSpeechToTextDemo.Services
{
    public interface IAudioFileInfoRepository
    {
        IEnumerable<AudioFile> GetAudioFiles();

        AudioFile GetAudioFile(int audioFileId);

        void AddAudioFile(AudioFile audioFile);

        bool Save();
    }
}
