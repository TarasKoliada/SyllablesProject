using Sklady.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklady
{
    public class WordAnalyzer
    {
        CharactersTable table;
        private Settings settings;

        public WordAnalyzer(CharactersTable table, Settings settings)
        {
            this.table = table;
            this.settings = settings;
        }

        public string[] GetSyllables(string word)
        {
            var chars = word.Select(c => table.Get(c)).ToList();
            chars.RemoveAll(c => c == null);

            var vowelsCount = chars.Count(c => c != null && !c.IsConsonant);
            //var vow = chars.Count(c => !c.IsConsonant);

            var syllables = new List<string>();

            if (vowelsCount == 1)
            {
                return new string[] { word };
            }

            var firstWord = word.First();

            var done = false;
            var currentIndex = 0;
            while (!done)
            {
                if (syllables.Count == vowelsCount - 1) // if we already have syllables = vowels count - 1, then we can take all remaining word into the last syllable
                {
                    syllables.Add(word.Substring(currentIndex, word.Length - currentIndex));
                    done = true;
                    break;
                }

                var firstVowelIndex = chars.FindIndex(currentIndex, c => c != null && !c.IsConsonant);
                var nextVowelIndex = chars.FindIndex(firstVowelIndex + 1, c => c!= null && !c.IsConsonant);

                if (firstVowelIndex == -1 || nextVowelIndex == -1)
                {
                    syllables.Add(word);
                    break;
                }

                var indexOfDash = word.Substring(firstVowelIndex, nextVowelIndex - firstVowelIndex).IndexOf("-") ;
                if (indexOfDash != -1)
                {
                    syllables.Add(word.Substring(currentIndex, indexOfDash + firstVowelIndex - currentIndex));
                    currentIndex = indexOfDash + firstVowelIndex + 1;
                    continue;
                }                

                var distance = nextVowelIndex - firstVowelIndex;
                var specChars = chars.GetRange(firstVowelIndex, nextVowelIndex - firstVowelIndex).Count(c => c != null && c.Power == 0);
                distance -= specChars;

                if (distance == 1) // if they are together
                {
                    syllables.Add(word.Substring(currentIndex, nextVowelIndex - currentIndex));
                    currentIndex = nextVowelIndex;
                }
                else if (distance == 2) // if between these two vowels are only one consonant
                {
                    if (specChars > 0)
                    {
                        syllables.Add(word.Substring(currentIndex, nextVowelIndex - currentIndex));
                        currentIndex = nextVowelIndex;
                    }
                    else
                    {
                        syllables.Add(word.Substring(currentIndex, nextVowelIndex - 1 - currentIndex));
                        currentIndex = nextVowelIndex - 1;
                    }                    
                }
                else if (distance == 3) // two consonants between
                {
                    var firstConsIndex = firstVowelIndex + 1;
                    var firstConsonant = chars[firstConsIndex];
                    var secondConsIndex = chars[firstVowelIndex + 2].Power != 0 ? firstVowelIndex + 2 : firstVowelIndex + 3;
                    var secondConsonant = chars[secondConsIndex];

                    if (!isIncreasing(firstConsonant.Power, secondConsonant.Power)) // check if consonants sequence is increasing
                    {
                        syllables.Add(word.Substring(currentIndex, secondConsIndex - currentIndex));
                        currentIndex = secondConsIndex;
                    }
                    else
                    {
                        syllables.Add(word.Substring(currentIndex, firstConsIndex - currentIndex));
                        currentIndex = firstConsIndex;
                    }
                }
                else // three and more consonants
                {
                    var syllableIndex = -1;
                    for (var i = firstVowelIndex + 1; i < nextVowelIndex - 1; i++)
                    {
                        var char1 = chars[i].Power > 0 ? chars[i] : chars[i - 1];
                        var char2 = chars[i + 1].Power > 0 ? chars[i + 1] : chars[i + 2];
                        if (isIncreasing(char1.Power, char2.Power))
                        {
                            syllableIndex = i;
                            break;
                        }
                    }


                    if (syllableIndex == -1) // if there is no increasing sequence
                    {
                        if (settings.SeparateAfterFirst)
                        {
                            syllableIndex = firstVowelIndex + 2;
                        }
                        else
                        {
                            syllableIndex = nextVowelIndex - 1;
                        }
                    }

                    syllables.Add(word.Substring(currentIndex, syllableIndex - currentIndex));
                    currentIndex = syllableIndex;
                }

            }

            return syllables.ToArray();
        }

        private bool isIncreasing(int power1, int power2)
        {
            return power1 < power2;
        }
    }
}
