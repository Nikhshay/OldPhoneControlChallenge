using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OldPhoneControlChallenge
{
    /**
     * Program to Control Old Phone Key presses
     * 
     * Assumption and limitations
     * 
     * Assume # at the end, means also if there are multiple in between, take that as the end so substring to the first occurance
     * Multiple * means keep deleting till no character left
     */
    public static class Program
    {
        /**
         * Key mapping for old phone keypad
         */
        private static readonly Dictionary<char, string> PhoneKey = new()
        {
            { '0', " " },
            { '1', "&'(" },
            { '2', "ABC" },
            { '3', "DEF" },
            { '4', "GHI" },
            { '5', "JKL" },
            { '6', "MNO" },
            { '7', "PQRS" },
            { '8', "TUV" },
            { '9', "WXYZ" }, 
            { '*', "*" },
            { '#', "#" }
        };

        /**
         * Removes the character before each '*' in the StringBuilder.
         * @param word The StringBuilder containing the text to process.
         * @return A new StringBuilder with the previous characters removed.
         */
        private static StringBuilder RemovePreviousChar(StringBuilder word)
        {
            StringBuilder newWord = new StringBuilder(word.Length);

            foreach (char c in word.ToString())
            {
                if (c == '*')
                {
                    if (newWord.Length > 0) // Remove previous character if available
                        newWord.Length--;
                }
                else
                    newWord.Append(c);
            }

            return newWord;
        }

        /**
         * Simulates old phone keypad input to text conversion.
         * @param input A string representing the sequence of key presses.
         * @return The resulting text after processing the input.
         */
        public static String OldPhonePad(string input)
        {
            input = input.Trim(); //remove leading and trailing whitespaves

            if (string.IsNullOrEmpty(input))
                return "INVALID";

            const string allowed = "0123456789#* "; //check valid characters
            if (input.Any(c => !allowed.Contains(c)))
                return "INVALID";

            int index = input.IndexOf('#'); //remove everything after the first occurance of #
            if (index != -1)
                input = input.Substring(0, index+1); //required to keep hashtag


            Regex pattern = new Regex(@"([\d#*])\1*"); //Divide into group of "non unique" consecutives
            MatchCollection matches = pattern.Matches(input); // Get all matches

            var result = new StringBuilder();

            foreach (Match match in matches)
            {
                string letters = PhoneKey[match.Value[0]]; //Take first character for each of the group and find in dictionary
                int letterCount = letters.Length;
                int pressCount = match.Length;
                int letterIndex = (pressCount - 1) % letterCount; // Wrap around using modulo
                result.Append(letters[letterIndex].ToString()); // Append the corresponding letter
            }

            // handle special characters at the end
            if (result.ToString().Contains('*'))
                result =  RemovePreviousChar(result);



            string returnVal = result.ToString();

            return returnVal.Substring(0, returnVal.Length - 1); //finally remove # == send
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Input: ");
            string input = Console.ReadLine();
            Console.WriteLine(OldPhonePad(input));
        }

    }
}









