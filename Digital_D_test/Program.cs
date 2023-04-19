using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DD
{
    public class Program
    {

        static void Main(string[] args)
        {
            string inputTxt = @"C:\Users\Anast\OneDrive\Рабочий стол\Digital_D_test\tolstoj_lew_nikolaewich-text_0040.fb2";
            string outputTxt = @"C:\Users\Anast\OneDrive\Рабочий стол\Digital_D_test\output.txt";


            Dictionary<string, int> words = new Dictionary<string, int>();
            Regex regex = new Regex(@"\b[a-zа-яё'-]+\b", RegexOptions.IgnoreCase);
            int startIndex;
            int endIndex;

            using (StreamReader sr = new StreamReader(inputTxt))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    startIndex = line.IndexOf('<');
                    while (startIndex >= 0)
                    {
                        endIndex = line.IndexOf('>', startIndex);
                        if (endIndex > startIndex)
                        {
                            string tag = line.Substring(startIndex, endIndex - startIndex + 1);
                            line = line.Replace(tag, "");
                        }
                        startIndex = line.IndexOf('<');
                    }
                    foreach (Match match in regex.Matches(line))
                    {
                        string word = match.Value.ToLower();
                        if (IsRussianOrEnglishWord(word) || word.Contains("-") || word.Contains("'"))
                        {
                            if (words.ContainsKey(word))
                                words[word]++;
                            else
                                words[word] = 1;
                        }
                    }
                }

            }
            using (StreamWriter sw = new StreamWriter(outputTxt))
            {
                sw.WriteLine($"Всего уникальных слов: {words.Count}\n");

                foreach (var item in words.OrderByDescending(k => k.Value))
                {
                    sw.WriteLine($"{item.Key}\t{item.Value}");
                }
            }
        }
        private static bool IsRussianOrEnglishWord(string word)
        {
            return Regex.IsMatch(word, @"^[\p{IsCyrillic}\p{IsBasicLatin}]+$");
        }
    }

}