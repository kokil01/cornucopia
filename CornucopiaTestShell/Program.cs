using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CornucopiaLib;

namespace CornucopiaTestShell
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary dictionary = new Dictionary();
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                if (dictionary.Find(line))
                {
                    int i = 0;
                    string meaning;
                    while ((meaning = dictionary.Next()) != null)
                    {
                        Console.WriteLine("{0:00}\t{1}", i + 1, meaning);
                        i += 1;
                    }
                }
                else
                {
                    Console.WriteLine("Word not found!");
                }
            }
        }
    }
}
