using Sklady.Models;
using System.Collections.Generic;
using System.Linq;
using System;

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

        public CharactersTable Table
        {
            get
            {
                return table;
            }
        }

        public string[] GetSyllables(string word)
        {
            var unionOpt = table.UnionOpt;

            var chars = new List<Character>();
            foreach (var c in word)
            {
                Character ch = null;
                foreach (var u in unionOpt)
                {
                    if (u.CharacterValue == c)
                    {
                        ch = u;
                        break;
                    }
                }
                if (ch != null)
                {
                    chars.Add(ch);
                }
            }

            var vowelsCount = chars.Count(c => !c.IsConsonant);

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

        public List<string> GetSyllablesOld(string word)
        {
            var chars = GetCharacterList(word);
            var vowelsCount = CountVowels(chars);
            var syllables = new List<string>();

            if (vowelsCount == 1)
            {
                return new List<string>() { word };
            }

            var currentIndex = 0;

            while (true)
            {
                if (syllables.Count == vowelsCount - 1)
                {
                    syllables.Add(word.Substring(currentIndex, word.Length - currentIndex));
                    break;
                }

                // Extracting the logic into separate functions
                currentIndex = ProcessSyllable(chars, currentIndex, vowelsCount, syllables, word);
            }

            if (word == "vаран")
            {
                var a = 1;
            }

            return syllables.ToList();
        }

        private List<Character> GetCharacterList(string word)
        {
            return word.Select(c => table.Get(c)).Where(c => c != null).ToList();
        }

        private int CountVowels(List<Character> chars)
        {
            return chars.Count(c => c != null && !c.IsConsonant);
        }

        private int ProcessSyllable(List<Character> chars, int currentIndex, int vowelsCount, List<string> syllables, string word)
        {
            var firstVowelIndex = chars.FindIndex(currentIndex, c => c != null && !c.IsConsonant);
            var nextVowelIndex = chars.FindIndex(firstVowelIndex + 1, c => c != null && !c.IsConsonant);
            
            if (firstVowelIndex == -1 || nextVowelIndex == -1)
            {
                syllables.Add(word);
                return currentIndex;
            }

            var indexOfDash = word.Substring(firstVowelIndex, nextVowelIndex - firstVowelIndex).IndexOf("-");
            if (indexOfDash != -1)
            {
                syllables.Add(word.Substring(currentIndex, indexOfDash + firstVowelIndex - currentIndex));
                return indexOfDash + firstVowelIndex + 1;
            }

            var distance = nextVowelIndex - firstVowelIndex;
            var specChars = chars.GetRange(firstVowelIndex, nextVowelIndex - firstVowelIndex).Count(c => c != null && c.Power == 0);
            distance -= specChars;
            
            switch (distance)
            {
                case 1:
                    syllables.Add(word.Substring(currentIndex, nextVowelIndex - currentIndex));
                    return nextVowelIndex;
                case 2:
                    if (specChars > 0)
                    {
                        syllables.Add(word.Substring(currentIndex, nextVowelIndex - currentIndex));
                        return nextVowelIndex;
                    }
                    else
                    {
                        syllables.Add(word.Substring(currentIndex, nextVowelIndex - 1 - currentIndex));
                        return nextVowelIndex - 1;
                    }
                case 3:
                    return ProcessThreeConsonants(chars, currentIndex, firstVowelIndex, nextVowelIndex, syllables, word);
                default:
                    return ProcessMoreThanThreeConsonants(chars, currentIndex, firstVowelIndex, nextVowelIndex, syllables, word);
            }
        }

        private int ProcessThreeConsonants(List<Character> chars, int currentIndex, int firstVowelIndex, int nextVowelIndex, List<string> syllables, string word)
        {
            var firstConsIndex = firstVowelIndex + 1;
            var firstConsonant = chars[firstConsIndex];
            var secondConsIndex = chars[firstVowelIndex + 2].Power != 0 ? firstVowelIndex + 2 : firstVowelIndex + 3;
            var secondConsonant = chars[secondConsIndex];

            if (!isIncreasing(firstConsonant.Power, secondConsonant.Power)) // check if consonants sequence is increasing
            {
                syllables.Add(word.Substring(currentIndex, secondConsIndex - currentIndex));
                return secondConsIndex;
            }
            else
            {
                syllables.Add(word.Substring(currentIndex, firstConsIndex - currentIndex));
                return firstConsIndex;
            }
        }

        private int ProcessMoreThanThreeConsonants(List<Character> chars, int currentIndex, int firstVowelIndex, int nextVowelIndex, List<string> syllables, string word)
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
            return syllableIndex;
        }


        private bool isIncreasing(int power1, int power2)
        {
            return power1 < power2;
        }
    }
}