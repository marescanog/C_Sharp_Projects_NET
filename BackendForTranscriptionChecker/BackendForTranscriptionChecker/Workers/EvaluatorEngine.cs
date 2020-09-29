using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendForTranscriptionChecker.Workers
{
    class EvaluatorEngine
    {

    }

    /*
    private bool IsMissingWord(int index, List<string> original, List<string> transcript)
    {
        string wordtocheck = original[index];

        if (!transcript.Contains(wordtocheck))
        {
            return true;
        }

        //Check if the next words are missing
        //transcript.IndexOf(wordtocheck)
        //index = 1
        //indexWhereWordIsFoundInTranscript = 4

        int indexWhereWordIsFoundInTranscript = transcript.IndexOf(wordtocheck);

        return true;
    }
    */
}
