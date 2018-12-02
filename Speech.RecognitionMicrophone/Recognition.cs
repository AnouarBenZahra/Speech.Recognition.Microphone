using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speech.RecognitionMicrophone
{
    public class Recognition
    {
        //config = SpeechConfig.FromSubscription("YourSubscriptionKey", "YourServiceRegion");
        public async Task<string> GetTextFromMicrophone(SpeechConfig config)
        {           
            string result = string.Empty;     
            using (var recognizer = new SpeechRecognizer(config))
            {
                var recognizerAsync = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

                if (recognizerAsync.Reason == ResultReason.RecognizedSpeech)
                    result = recognizerAsync.Text;

                else if (recognizerAsync.Reason == ResultReason.NoMatch)
                    result = "Error";

                else if (recognizerAsync.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(recognizerAsync);                    
                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        result = "Cancellation Error";                       
                    }
                }
            }
            return result;
        }
    }
}
