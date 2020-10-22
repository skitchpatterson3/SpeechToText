using DotNetCoreSpeechToTextDemo.Entities;
using DotNetCoreSpeechToTextDemo.Models;
using DotNetCoreSpeechToTextDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSpeechToTextDemo.Controllers
{
    [ApiController]
    [Route("api/audio")]
    public class AudioController : ControllerBase
    {
        private readonly IAudioFileInfoRepository _audioFileInfoRepository;
        private readonly ILogger<AudioController> _logger;
        private readonly IMailService _mailService;

        public AudioController(IAudioFileInfoRepository audioFileInfoRepository,
            ILogger<AudioController> logger, IMailService mailService)
        {
            _audioFileInfoRepository = audioFileInfoRepository ?? throw new ArgumentNullException(nameof(audioFileInfoRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        [HttpGet]
        public IActionResult GetAudioFiles()
        {
            var audioFileEntities = _audioFileInfoRepository.GetAudioFiles();

            var results = new List<AudioFileDto>();

            foreach (var audioFileEntity in audioFileEntities)
            {
                results.Add(new AudioFileDto
                {
                    Id = audioFileEntity.Id,
                    Filename = audioFileEntity.Filename,
                    Description = audioFileEntity.Description,
                    CreatedOn = audioFileEntity.CreatedOn
                });
            }

            return Ok(results);

            //return Ok(_mapper.Map<IEnumerable<AudioFileDto>>(audioFileEntities); // use this line if using AutoMapper to avoid manually mapping the properties... see corresponding Pluralsight course for implementation details https://app.pluralsight.com/course-player?clipId=c55b6c06-f014-4595-bc29-603e39e3ae4e

            //return Ok(AudioFilesDataStore.Current.AudioFiles); // use if pulling data from in-memory datastore, AudioFilesDataStore.cs
        }

        [HttpGet("{id}", Name = "GetAudioFile")]
        public IActionResult GetAudioFile(int id)
        {
            try
            {
                // find audio file
                var audioFile = _audioFileInfoRepository.GetAudioFile(id);

                if (audioFile == null)
                {
                    _logger.LogInformation($"Audio file with id: {id} wasn't found when accessing audio files.");
                    return NotFound();
                }

                var audioFileResult = new AudioFileDto()
                {
                    Id = audioFile.Id,
                    Filename = audioFile.Filename,
                    Description = audioFile.Description,
                    CreatedOn = audioFile.CreatedOn
                };

                return Ok(audioFileResult);

                //return Ok(_mapper.Map<AudioFileDto>(audioFile); // use this line if using AutoMapper to avoid manually mapping the properties... see corresponding Pluralsight course for implementation details https://app.pluralsight.com/course-player?clipId=c55b6c06-f014-4595-bc29-603e39e3ae4e
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting audio file with id: {id}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }

            //try // use if pulling data from in-memory datastore, AudioFilesDataStore.cs
            //{
            //    // find audio file
            //    var fileToReturn = AudioFilesDataStore.Current.AudioFiles.FirstOrDefault(f => f.Id == id);

            //    if (fileToReturn == null)
            //    {
            //        _logger.LogInformation($"Audio file with id: {id} wasn't found when accessing audio files.");
            //        return NotFound();
            //    }

            //    return Ok(fileToReturn);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogCritical($"Exception while getting audio file with id: {id}.", ex);
            //    return StatusCode(500, "A problem happened while handling your request.");
            //}
        }

        [HttpPost]
        public IActionResult CreateAudioFile([FromBody] AudioFileForUploadDto audioFile)
        {
            if (audioFile.Description == audioFile.Filename)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the file name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var files = _audioFileInfoRepository.GetAudioFiles();
            //var files = AudioFilesDataStore.Current.AudioFiles; // use if pulling data from in-memory datastore, AudioFilesDataStore.cs

            // TO-DO: check that user has chosen a valid audio file
            //if (audioFile format is bad)
            //{
            //    return BadRequest();
            //}

            // demo purposes - id value determination can be improved
            var maxAudioFileId = _audioFileInfoRepository.GetAudioFiles().Max(f => f.Id); // needs improvement because persistent database should use the primary key to track and manage next ID value
            //var maxAudioFileId = AudioFilesDataStore.Current.AudioFiles.Max(f => f.Id); // use if pulling data from in-memory datastore, AudioFilesDataStore.cs

            var newAudioFile = new AudioFile() // was new AudioFileDto()
            {
                //Id = ++maxAudioFileId,
                Filename = audioFile.Filename,
                Description = audioFile.Description,
                CreatedOn = DateTime.Now
            };
            //var newAudioFile = _mapper.Map<Entities.AudioFile>(audioFile); // use this line if using AutoMapper to avoid manually mapping the properties... see corresponding Pluralsight course for implementation details https://app.pluralsight.com/course-player?clipId=c55b6c06-f014-4595-bc29-603e39e3ae4e

            _audioFileInfoRepository.AddAudioFile(newAudioFile);
            _audioFileInfoRepository.Save();
            //files.Add(newAudioFile); // old method to add the new audio file to the in-memory datastore

            // need to map the saved entity back to the dto so we can return the newly-saved audio file (not the entity) along with the "201 Created" status code
            var createdAudioFileToReturn = new AudioFileDto()
            {
                Id = newAudioFile.Id,
                Filename = newAudioFile.Filename,
                Description = newAudioFile.Description,
                CreatedOn = newAudioFile.CreatedOn
            };
            // var createdAudioFileToReturn = _mapper.Map<Models.AudioFileDto>(newAudioFile); // use this line if using AutoMapper to avoid manually mapping the properties... see corresponding Pluralsight course for implementation details https://app.pluralsight.com/course-player?clipId=c55b6c06-f014-4595-bc29-603e39e3ae4e

            // send mail notifying of audio file upload attempt for transcription
            _mailService.Send($"Audio file uploaded for transcription - {DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}",
                $"Audio file was uploaded for text transcription on {DateTime.Now.ToShortDateString()} at {DateTime.Now.ToLongTimeString()}.\r\n\r\n" +
                $"File details:\r\n" +
                $"  Id: {newAudioFile.Id.ToString()}\r\n" +
                $"  Filename: {newAudioFile.Filename}\r\n" +
                $"  Description: {newAudioFile.Description}\r\n" +
                $"  Date Created: {newAudioFile.CreatedOn.ToShortDateString()} {newAudioFile.CreatedOn.ToLongTimeString()}");

            return CreatedAtRoute(
                "GetAudioFile",
                new { createdAudioFileToReturn.Id},
                createdAudioFileToReturn);

            //return CreatedAtRoute( // old method to return a "201 Created" status code
            //    "GetAudioFile",
            //    new { newAudioFile.Id },
            //    newAudioFile);
        }
    }
}
