using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreSpeechToTextDemo.Models;
using Microsoft.CognitiveServices.Speech;

namespace DotNetCoreSpeechToTextDemo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> GetShortSampleRecording()
        {
            var config = SpeechConfig.FromSubscription("2efdde4787904073bc0febfdeac28465", "westus2");

            // If you get a error stating "Unable to load DLL 'Microsoft.CognitiveServices.Speech.core.dll'" it may be due to
            // the machine not having the latest "Microsoft Visual C++ Redistributable for Visual Studio 2015, 2017 and 2019"
            // because the Speech SDK versions 1.13 and later depend on VS2019 runtimes. See links below for more information
            // https://github.com/Azure-Samples/cognitive-services-speech-sdk/issues/780
            // https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads

            ShortSpeechResult r = new ShortSpeechResult();

            r.Text = "";

            using (var recognizer = new SpeechRecognizer(config))
            {

                var result = await recognizer.RecognizeOnceAsync();

                r.Reason = result.Reason;
                r.Duration = result.Duration;
                r.OffsetInTicks = result.OffsetInTicks;

                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    r.Text = result.Text;
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    r.Text = ($"No speech recognized");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellationDetails = CancellationDetails.FromResult(result);
                    r.Text = ($"Speech recognition canceled: {cancellationDetails.Reason}");

                    if (cancellationDetails.Reason == CancellationReason.Error)
                    {
                        r.Text = ($"ErrorCode {cancellationDetails.ErrorCode}");
                        r.Text += Environment.NewLine;
                        r.Text += ($"ErrorDetails {cancellationDetails.ErrorDetails}");
                    }
                }
                
                return PartialView("_ShortSampleRecording", r);
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // Original speech recognizer method for Console app

        //public static async Task RecognizeSpeechAsync()
        //{
        //    var config = SpeechConfig.FromSubscription("2efdde4787904073bc0febfdeac28465", "westus2");

        //    using (var recognizer = new SpeechRecognizer(config))
        //    {
        //        Console.WriteLine("Say something...");

        //        var result = await recognizer.RecognizeOnceAsync();

        //        if (result.Reason == ResultReason.RecognizedSpeech)
        //        {
        //            Console.WriteLine($"Text recognized: {result.Text}");

        //        }
        //        else if (result.Reason == ResultReason.NoMatch)
        //        {
        //            Console.WriteLine($"No speech recognized");
        //        }
        //        else if (result.Reason == ResultReason.Canceled)
        //        {
        //            var cancellationDetails = CancellationDetails.FromResult(result);
        //            Console.WriteLine($"Speech recognition canceled: {cancellationDetails.Reason}");

        //            if (cancellationDetails.Reason == CancellationReason.Error)
        //            {
        //                Console.WriteLine($"ErrorCode {cancellationDetails.ErrorCode}");
        //                Console.WriteLine($"ErrorDetails {cancellationDetails.ErrorDetails}");
        //            }
        //        }
        //    }
        //}
    }
}
