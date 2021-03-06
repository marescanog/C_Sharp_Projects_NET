﻿namespace BackendForTranscriptionChecker
{
    class Constants
    {
        public const string delimiter = "(.*)";
        public const string space = " ";
        public const char s = ' ';
        public const string timedOutRegex = "Subsequence Validator operation timed out: ";
        public const double timeOutTime = 0.25;
        public const string stringError = "Error on string: ";
        public const string programError = "Application Encountered Error: ";
        public const string subValidator = "SUBSEQUENCE VALIDATOR: ";
        public const string subProcessor = "SUBSEQUENCE PROCESSOR: ";
        public const string regexPatternBuildStart = "\\b(";
        public const string regexPatternBuildEnd = ")\\b";
        //public const string regexCaseInsensitive = "(?i)";
    }
}
