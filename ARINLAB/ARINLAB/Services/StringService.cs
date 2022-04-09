using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services
{
    public static class StringService
    {
        public static string ReverseOnlyNumbers(this string word)
        {
            char[] result = new char[word.Length];
            List<char> temp = new List<char>();
            int pos = 0;
            foreach(char c in word)
            {
                if (!char.IsDigit(c))
                {
                    if (temp.Count > 0)
                    {
                        temp.Reverse();
                        foreach (var item in temp)
                        {
                            result[pos] = item;
                            pos++;
                        }
                        temp.Clear();
                    }
                    result[pos] = c;
                    pos++;
                }
                else
                {
                    temp.Add(c);
                }
            }
            return new string(result);
        }
        public static string ReverseWithNumber(this string word)
        {
            char[] result = new char[word.Length];
            int pos = word.Length - 1;
            
            List<char> temp = new List<char>();
            foreach (var ch in word)
            {
                if (!char.IsDigit(ch))
                {
                    if (temp.Count > 0)
                    {                        
                        temp.Reverse();
                        foreach (var item in temp)
                        {
                            result[pos] = item;
                            pos--;
                        }
                        temp.Clear(); 
                    }
                    result[pos] = ch;
                    pos--;
                }
                else
                {
                    temp.Add(ch);                    
                }
            }
            return new string(result);
        }
    }
}
