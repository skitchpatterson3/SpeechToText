using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreSpeechToTextDemo.Contexts;
using DotNetCoreSpeechToTextDemo.Entities;

namespace DotNetCoreSpeechToTextDemo.Services
{
    public class AudioFileInfoRepository : IAudioFileInfoRepository
    {
        private readonly AudioFileInfoContext _context;

        public AudioFileInfoRepository(AudioFileInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<AudioFile> GetAudioFiles()
        {
            return _context.AudioFiles.OrderBy(f => f.Filename).ToList();
        }

        public AudioFile GetAudioFile(int audioFileId)
        {
            return _context.AudioFiles.Where(f => f.Id == audioFileId).FirstOrDefault();
        }

        public void AddAudioFile(AudioFile audioFile)
        {
            _context.AudioFiles.Add(audioFile);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
