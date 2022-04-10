using ARINLAB.Models;
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

        public static MailImageModel GetImage(this string msg)
        {
            MailImageModel result = new MailImageModel();
            List<string> imgs = new List<string>();
            int x = msg.IndexOf("src=\""); ;
            while (x != -1)
            {
                int end = msg.IndexOf('\"', x + 5);
                if (end != -1)
                {
                    imgs.Add(msg.Substring(x, (end - x) + 1));
                }
                x = msg.IndexOf("src=\"", x + 1);
            }
            int i = 1;
            foreach (string s in imgs)
            {
                result.ImageSrc.Add(s.Substring(5, s.Length - 6));
                result.Cid.Add($"image{i}");
                msg = msg.Replace(s, $"src=\"cid:image{i}\"");
                i++;
            }
            result.MessageBody = msg;
            return result;

        } 
    }
}
