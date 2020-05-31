using ECommerceSystem.DomainLayer.SystemManagement.spell_checker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace ECommerceSystem.DomainLayer.SystemManagement
{
    public class SpellChecker : ISpellChecker
    {
        public readonly static string AppRoot = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
        private static readonly string SPELL_CHECK_FILE_NAME = Path.Combine(AppRoot, "spell_checker.bin");
        private static readonly string SPELL_CHECK_WORDS_TXT = Path.Combine(AppRoot, "words_alpha.txt");
        private HashSet<string> _dictionary;
        private static Regex _wordRegex = new Regex("[a-z]+", RegexOptions.Compiled);

        public SpellChecker()
        {
            _dictionary = File.Exists(SPELL_CHECK_FILE_NAME) ?
                Deserialize<HashSet<string>>(File.Open(SPELL_CHECK_FILE_NAME, FileMode.Open)) : readDictionaryFromText();
        }

        public HashSet<string> readDictionaryFromText()
        {
            if (!File.Exists(SPELL_CHECK_WORDS_TXT))
            {
                SystemLogger.logger.Error("Missing spell checker dictionary file!");
                return null;
            }
            var dictionary = new HashSet<string>();
            string fileContent = File.ReadAllText(SPELL_CHECK_WORDS_TXT);
            List<string> wordList = fileContent.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var word in wordList)
            {
                string trimmedWord = word.Trim().ToLower();
                if (_wordRegex.IsMatch(trimmedWord))
                {
                    dictionary.Add(trimmedWord);
                }
            }
            Serialize(dictionary, File.Open(SPELL_CHECK_FILE_NAME, FileMode.Create));
            return dictionary;
        }

        public List<string> Correct(string word)
        {
            if (string.IsNullOrEmpty(word) || _dictionary == null)
                return new List<string>();
            word = word.ToLower();
            // no spelling error
            if (_dictionary.Contains(word))
                return new List<string>();

            var variations = Variations(word);
            var candidates = new HashSet<string>();

            foreach (string wordVariation in variations)
            {
                if (_dictionary.Contains(wordVariation) && !candidates.Contains(wordVariation))
                    candidates.Add(wordVariation);
            }

            if (candidates.Count > 0)
                return candidates.OrderBy(w => w).ToList();

            // did not find, try variation on current variations
            foreach (string variation in variations)
            {
                foreach (string wordVariation in Variations(variation))
                {
                    if (_dictionary.Contains(wordVariation) && !candidates.Contains(wordVariation))
                        candidates.Add(wordVariation);
                }
            }
            return (candidates.Count > 0) ? candidates.OrderBy(w => w).ToList() : null;
        }

        private List<string> Variations(string word)
        {
            var splits = new List<(string, string)>();
            var transposes = new List<string>();
            var deletes = new List<string>();
            var replaces = new List<string>();
            var inserts = new List<string>();

            // Splits
            for (int i = 0; i < word.Length; i++)
            {
                var tuple = (word.Substring(0, i), word.Substring(i));
                splits.Add(tuple);
            }

            // Deletes
            for (int i = 0; i < splits.Count; i++)
            {
                string a = splits[i].Item1;
                string b = splits[i].Item2;
                if (!string.IsNullOrEmpty(b))
                {
                    deletes.Add(a + b.Substring(1));
                }
            }

            // Transposes
            for (int i = 0; i < splits.Count; i++)
            {
                string a = splits[i].Item1;
                string b = splits[i].Item2;
                if (b.Length > 1)
                {
                    transposes.Add(a + b[1] + b[0] + b.Substring(2));
                }
            }

            // Replaces
            for (int i = 0; i < splits.Count; i++)
            {
                string a = splits[i].Item1;
                string b = splits[i].Item2;
                if (!string.IsNullOrEmpty(b))
                {
                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        replaces.Add(a + c + b.Substring(1));
                    }
                }
            }

            // Inserts
            for (int i = 0; i < splits.Count; i++)
            {
                string a = splits[i].Item1;
                string b = splits[i].Item2;
                for (char c = 'a'; c <= 'z'; c++)
                {
                    inserts.Add(a + c + b);
                }
            }

            return deletes.Union(transposes).Union(replaces).Union(inserts).ToList();
        }

        public static void Serialize<Object>(Object dictionary, Stream stream)
        {
            try // try to serialize the collection to a file
            {
                using (stream)
                {
                    // create BinaryFormatter
                    BinaryFormatter bin = new BinaryFormatter();
                    // serialize the collection (EmployeeList1) to file (stream)
                    bin.Serialize(stream, dictionary);
                }
            }
            catch (IOException e)
            {
                SystemLogger.LogError("Serialize spell checker file failed: " + e.Message + "\n" + "Path: " + SPELL_CHECK_FILE_NAME);
            }
        }

        public static Object Deserialize<Object>(Stream stream) where Object : new()
        {
            Object ret = CreateInstance<Object>();
            try
            {
                using (stream)
                {
                    // create BinaryFormatter
                    BinaryFormatter bin = new BinaryFormatter();
                    // deserialize the collection (Employee) from file (stream)
                    ret = (Object)bin.Deserialize(stream);
                }
            }
            catch (IOException e)
            {
                SystemLogger.LogError("Deseriazlize spell checker file failed: " + e.Message + "\n" + "Path: " + SPELL_CHECK_FILE_NAME);
            }
            return ret;
        }

        // function to create instance of T
        public static Object CreateInstance<Object>() where Object : new()
        {
            return (Object)Activator.CreateInstance(typeof(Object));
        }
    }
}