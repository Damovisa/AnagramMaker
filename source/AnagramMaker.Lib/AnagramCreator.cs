using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramMaker.Lib
{
    public class AnagramCreator
    {
        public const int Limit = 100;
        private readonly Trie<Object> lexicon;

        public AnagramCreator()
        {
            lexicon = new Trie<object>();
            using (Stream stream = typeof(AnagramCreator).Assembly.GetManifestResourceStream("AnagramMaker.Lib.words.txt"))
            using (StreamReader reader = new StreamReader(stream))
            {
                string word;
                while ((word = reader.ReadLine()) != null)
                {
                    if (word.Length == 1 && word != "i" && word != "a")
                        continue;

                    if (lexicon.ContainsKey(word))
                        continue;
                    lexicon.Add(word, null);
                }
            }            
        }

        public IEnumerable<string> GenerateAnagrams(string source)
        {
            var sanitisedSource = source.ToLower().Replace(" ", "");

            return GenerateAnagrams(lexicon, "", sanitisedSource, new List<string>())
                .Take(Limit)
                .Select(x => string.Join(" ", x))
                .ToArray();
        }

        private static IEnumerable<IList<string>> GenerateAnagrams(Trie<object> lexicon, string prefix, string remaining, IList<string> wordsSoFar)
        {
            if (prefix == "" && remaining == "")
            {
                yield return wordsSoFar;
            }

            if (prefix != "" && !lexicon.GetByPrefix(prefix).Any())
                yield break;

            if (lexicon.ContainsKey(prefix))
            {
                foreach (
                    var anagram in GenerateAnagrams(lexicon, "", remaining, wordsSoFar.Concat(new[] { prefix }).ToList()))
                    yield return anagram;
            }

            foreach (var c in remaining.Distinct())
            {
                var nowRemaining = string.Join("", remaining.Split(new[] { c }, 2));
                foreach (var anagram in GenerateAnagrams(lexicon, prefix + c, nowRemaining, wordsSoFar))
                    yield return anagram;
            }
        }
    }
}
