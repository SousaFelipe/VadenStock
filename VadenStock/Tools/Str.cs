﻿using System;



namespace VadenStock.Tools
{
    public static class Str
    {
        public static string Currency(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "0,00";

            string output = Sanitize(input, 3);
            char[] vecout = output.ToCharArray();

            Array.Reverse(vecout);

            string reversedOutput = new(vecout);
            int reversedOutLength = reversedOutput.Length;

            int i,
                dotCount = 0;

            for (i = 0; i < reversedOutLength; i++)
            {
                if (i == 2)
                    reversedOutput = reversedOutput.Insert(i, ",");
                else if (i == 5 || i == 8 || i == 11 || i == 14 || i == 17 || i == 20)
                {
                    dotCount++;
                    reversedOutput = reversedOutput.Insert(i + dotCount, ".");
                }
            }

            char[] subversedOutput = reversedOutput.ToCharArray();

            Array.Reverse(subversedOutput);

            return new string(subversedOutput);
        }



        public static string Sanitize(string value, int fill = 3)
        {
            string input = Number(value);
            char[] output = new char[(input.Length >= fill) ? input.Length : fill];

            if (input.Length < output.Length)
            {
                int pos;

                for (int i = output.Length - 1; i >= 0; i--)
                {
                    pos = (i - (output.Length - input.Length));
                    output[i] = (pos >= 0) ? input[pos] : '0';
                }
            }
            else
            {
                for (int i = output.Length - 1; i >= 0; i--)
                    output[i] = input[i];
            }

            return new string(output);
        }



        public static string Number(string dirty)
        {
            string clean = string.Empty;

            for (int i = 0; i < dirty.Length; i++)
            {
                if (Char.IsDigit(dirty[i]))
                    clean += dirty[i];
            }

            return clean;
        }



        public static string ZeroFill(int number, string? concat = "")
        {
            string zero = (number > 0 && number < 10)
                ? $"0{ number }"
                : number.ToString();

            return string.Concat(zero, concat);
        }
    }
}