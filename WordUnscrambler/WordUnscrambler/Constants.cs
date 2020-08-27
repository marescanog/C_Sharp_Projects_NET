namespace WordUnscrambler
{
    class Constants
    {
        public const string OptionsOnHowToEnterScrabledWords = "Enter scrambled word(s) manually or as a file: F- file / M- manual";
        public const string OptionsOnContinuingTheProgram = "Would you like to continue? Y/N";

        public const string EnterScrambledWordsViaFile = "Enter Full Path including the File Name: ";
        public const string EnterScrambledWordsManually = "Enter word(s) manually (separared by commas if multiple): ";
        public const string EnterScrambledWordsOptionNotRecognized = "The option was not recognized.";

        public const string ErrorScrambledWordsCannotBeLoaded = "Scrambled words were not loaded because there was an error: ";
        public const string ErrorProgramWillBeTerminated = "The program will be terminated: ";

        public const string MatchFound = "MATCH FOUND FOR {0}: {1}";
        public const string MatchNotFound = "NO MATCHES HAVE BEEN FOUND";

        public const string Yes = "Y";
        public const string No = "N";
        public const string File = "F";
        public const string Manual = "N";
    }
}
