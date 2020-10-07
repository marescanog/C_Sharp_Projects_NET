using BackendForTranscriptionChecker.Objects;
using BackendForTranscriptionChecker.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BackendForTranscriptionChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /*
                FileReader fileReader = new FileReader();

                List<string> referenceText = fileReader.Read("Reference.txt");
                List<string> evaluatedText = fileReader.Read("Evaluated.txt");

                EvaluatorEngine evaluatorEngine = new EvaluatorEngine(new RegExPatternCreator());
                evaluatorEngine.EvaluateText(referenceText, evaluatedText);
                */


                //string[] refArray = { "A", "A", "A", "B", "A", "A", "A", "A", "B" };
                //string[] evalArray = { "B", "A", "F", "F", "A", "B" };

                //string[] refArray = { "A", "B", "C", "D", "E" };
                //string[] evalArray = { "A", "B", "C", "D", "E" };

                //string[] refArray = { "A", "B", "C", "C", "D", "E", "F", "G", "H" };
                //string[] evalArray = { "A", "B", "F", "D", "E", "G", "G", "G", "H" };

                ////string[] refArray = { "A", "B", "F", "F", "F", "F", "G", "H", "I", "J", "K", "L" };
                ////string[] evalArray = { "A", "B", "O", "G", "H", "I", "J", "K", "L", "F" };

                //string expectedOutCome = "(.*) A A B (.*) (.*) (.*) (.*) (.*)";
                //string expectedOutComeCrossCheck = "\nA\nA B\n";

                //string[] arrayOFCorrectWords = _crossChecker.GetCorrectWords(refArray, evalArray);
                //string regexPattern = _regExPatternCreator.CreateRegexPattern(refArray, evalArray);

                //Console.WriteLine("ExpctPattern: {0}", expectedOutComeCrossCheck);
                //Console.WriteLine("ActualPattern: ");

                //string[] refArray = { "A", "B", "F", "F", "F", "F", "G", "H", "I", "J", "K", "L" };
                //string[] evalArray = { "A", "B", "B", "B", "B", "I", "I", "I", "I", "F" };

                /* Test this string
                string[] refArray = { "G", "H", "I", "J", "K", "L", "O"};
                string[] evalArray = { "E", "O", "G", "H", "I", "J", "K", "L", "F" };
                */

                //string[] refArray = { "G", "H", "I", "J", "K", "L", "O", "P", "Q" };
                //string[] evalArray = { "E", "O", "G", "H", "I", "J", "K", "L", "F" };
                //string[] refArray = { "G", "H", "I", "J", "K", "L", "O", "P", "Q", "E", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };
                // string[] evalArray = { "E", "O", "G", "H", "I", "J", "K", "L", "F", "G", "Q", "P", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };

                //string[] refArray = { "The","quick","brown","fox","jumped","over","the","lazy","dog", "The", "quick", "brown", "fox", "jumped", "over", "the", "lazy", "dog" };
                //string[] evalArray = { "blah", "blah", "The", "quick", "brown", "fox","jumped", "over", "the", "lazy", "fox", "jumped", "over", "the", "lazy", "pig" };

                //string[] refArray = { "The", "quick", "brown", "fox", "jumped" };
               //string[] evalArray = { "blah", "blah", "The", "quick", "brown", "fox", "jumped", "over", "the", "lazy", "fox", "jumped", "over", "the", "lazy", "pig", "The", "quick", "brown", "fox", "jumped" };

                EvaluatorEngine _evaluatorEngine = new EvaluatorEngine();

                string[] refArray = { "A", "B", "A", "F", "A", "B"};
                string[] evalArray = { "D", "B", "A", "B", "A", "A", "B", "F" };

                //string[] refArray = {"The","quick","brown","fox","jumped","over","the","lazy","dog" };
                //string[] evalArray = { "The", "quick", "brown", "fox", "jumped", "over", "the", "lazy", "fox"};


                /*
                string[] refArray = {
                    "Look", "again", "at", "that", "dot",
                    "That's", "here", "That's", "home", "That's", "us",
                    "On", "it", "everyone", "you", "love",
                    "everyone", "you", "know", "everyone", "you",
                    "ever", "heard", "of", "every", "human",
                    "being", "who", "ever", "was", "lived",
                    "out", "their", "lives"
                };
                string[] evalArray = {
                    "Look", "again", "at", "that", "dot",
                    "That's", "here", "That's", "home", "That's", "is",
                    "Ap", "it", "everyone", "he", "love",
                    "everyone", "you", "know", "you",
                    "ever", "heard", "of", "every", "human",
                    "being", "who", "ever", "has", "lived",
                    "out", "their", "lives"
                };
                */
                
                
                //string[] refArray = { "D", "B", "A", "B", "A", "A", "B", "F" };
                //string[] evalArray = { "A", "B", "A", "F", "A", "B" };

                Console.WriteLine("The original text");
                Console.WriteLine("{0} \n", String.Join(" ", refArray));
                Console.WriteLine("The modified text");
                Console.WriteLine("{0} \n", String.Join(" ", evalArray));


                Console.WriteLine("\nThe evaluated pattern");
                List<string> listofAllsequences = _evaluatorEngine.Evaluate(refArray, evalArray);
                foreach (var item in listofAllsequences)
                {
                    Console.WriteLine(item);
                }



                /*
                Console.WriteLine("\n\nThe pattern of correct words:");
                List<string> expectedList = new List<string>();

                expectedList.AddRange(new List<string>() {
                    "B C D E"
                });
                */

                /*
                expectedList.AddRange(new List<string>() {
                        "Z B C D E",
                        "Z B C D",
                        "A B C",
                        "A B",
                        "A",
                        "B C D E",
                        "B C D",
                        "B C",
                        "B",
                        "C D E",
                        "C D",
                        "C",
                        "D E",
                        "D",
                        "E",

                        "A B C D",
                        "a B C D"
                });
                */

                //expectedList.OrderBy(X=>X).ToList().ForEach(Console.WriteLine);
                //expectedList.Sort();

                //expectedList.Sort((x, y) => y.Split(Constants.s).Length - x.Split(Constants.s).Length);

                /*
                //Sorts Alphabetically by the first word in the string
                expectedList.Sort((x, y) => 
                    string.Compare(
                        x.Split(Constants.s)[0], 
                        y.Split(Constants.s)[0]));

                //Sorts By Number of words in String
                expectedList.Sort((x, y) => y.Split(Constants.s).Length - x.Split(Constants.s).Length);
                */

                /*
                Sort(expectedList);
                expectedList.ForEach(Console.WriteLine);

                if (listofAllsequences.SequenceEqual(expectedList))
                {
                    Console.WriteLine("\nEqual");
                }
                else
                {
                    Console.WriteLine("\nNot Equal");
                }
                */

                    Console.WriteLine("\n\nEnd");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }


            void Sort(List<string> expectedList)
            {
                //Sorts Alphabetically by the first word in the string
                expectedList.Sort((x, y) =>
                    string.Compare(
                        x.Split(Constants.s)[0],
                        y.Split(Constants.s)[0]));

                //Sorts By Number of words in String
                expectedList.Sort((x, y) => y.Split(Constants.s).Length - x.Split(Constants.s).Length);
            }


        }
    }
}


