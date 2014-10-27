using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaLib
{
    public class Dictionary
    {
        private string[] meaningIds;
        private int currentMeaning;

        public bool Find(string word)
        {
            string wordLine = Lookup(word, "word_index.txt", "word_buckets");
            if (wordLine == null)
            {
                currentMeaning = -1;
                meaningIds = null;
                return false;
            }
            else
            {
                string[] tokens = wordLine.Split('\t');
                meaningIds = tokens[1].Split(' ');
                currentMeaning = 0;
                return true;
            }
        }

        public string Next()
        {
            if (currentMeaning < meaningIds.Length)
            {
                string meaningId = meaningIds[currentMeaning];
                currentMeaning += 1;
                string meaningLine = Lookup(meaningId, "meaning_index.txt", "meaning_buckets");
                return meaningLine.Split('\t')[1];
            }
            else
            {
                return null;
            }
        }

        private bool FindMeaning(string meaning)
        {
            return Lookup(meaning, "meaning_index.txt", "meaning_buckets") != null;
        }

        private string Lookup(string key, string indexFile, string bucketDirectory)
        {
            // Opening the word index text file
            using (StreamReader indexReader = new StreamReader(indexFile))
            {
                string indexLine;
                while (true)
                {
                    indexLine = indexReader.ReadLine();
                    if (indexLine == null)
                        break;
                    string[] tokens = indexLine.Split('\t');
                    if (key.CompareTo(tokens[0]) <= 0)
                    {
                        using (StreamReader bucketReader = new StreamReader(bucketDirectory + "/" + tokens[1]))
                        {
                            string bucketLine;
                            while (true)
                            {
                                bucketLine = bucketReader.ReadLine();
                                if (bucketLine == null)
                                    return null;
                                tokens = bucketLine.Split('\t');
                                if (key.CompareTo(tokens[0]) == 0)
                                {
                                    return bucketLine;
                                }
                            }
                        }
                    }
                }
                return null;
            }
        }
    }
}
