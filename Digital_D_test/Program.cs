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
            string outputTxt = @"C:\Users\Anast\OneDrive\Рабочий стол\Digital_D_test\output2.txt";


            Dictionary<string, int> words = new Dictionary<string, int>();
            //Regex regex = new Regex(@"\b[a-zа-яё'-]+\b", RegexOptions.IgnoreCase); //"output1.txt"
            Regex regex = new Regex(@"\b[а-яё]+(-[а-яё]+)*\b", RegexOptions.IgnoreCase); //"output2.txt"

            using (StreamReader sr = new StreamReader(inputTxt))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    foreach (Match match in regex.Matches(line))
                    {
                        string word = match.Value.ToLower();

                        if (words.ContainsKey(word))
                            words[word]++;
                        else
                            words[word] = 1;
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

    }

}