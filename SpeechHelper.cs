using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Speech.AudioFormat;
using System.Speech.Recognition.SrgsGrammar;
using System.Speech.Synthesis.TtsEngine;
using System.IO;
using System.Threading;

namespace DiscordBot2._0
{
    class SpeechStreamerHelper
    {
        SpeechSynthesizer Reader = new SpeechSynthesizer();

        public Prompt GetCurrentlySpokenPrompt { get { return Reader.GetCurrentlySpokenPrompt(); } }

        public int Rate { get { return Reader.Rate; } }
        
        public SpeechStreamerHelper()
        {
            Reader.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Teen);
            //Reader.Rate = -10;
            Reader.Rate = -1;
        }

        public void KillIT()
        {
            Reader.SpeakAsyncCancelAll();
        }

        public MemoryStream BeginRead(string toRead)
        {
            MemoryStream Bufferers = new MemoryStream();
            Reader.SetOutputToAudioStream(Bufferers, new SpeechAudioFormatInfo(44000, AudioBitsPerSample.Sixteen, AudioChannel.Stereo));
            if (toRead != null && toRead.Trim(' ') != "")
            {
                // await for a stream
                Reader.Speak(toRead);
            }
            //this took me longer than i thought it would but ya.
            // if it is on another thread will come back null so ?? and we need to spin and let others have a turn (light vers)
            return Bufferers;
        }

        public void Dispose()
        {
            Reader.Dispose();
        }
    }
}
