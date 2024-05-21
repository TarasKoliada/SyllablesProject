using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklady
{
    public class CharactersTable
    {
        private List<Character> _vowel;
        private static CharactersTable _instance;

        private List<Character> _table1;
        private List<Character> _table2;

        private List<Character> _currentTable;

        private IEnumerable<Character> _unionOpt;

        private Table selectedTable;

        public Table SelectedTable
        {
            get
            {
                return selectedTable;
            }
            set
            {
                ChangeTable(value);
                selectedTable = value;
            }
        }

        public IEnumerable<Character> UnionOpt
        {
            get
            {
                return _unionOpt;
            }
        }
        
        public CharactersTable(Table selectedTable)
        {
            _vowel = GetVowelCharacters();
            _table1 = GetFirstTable();
            _table2 = GetSecondTable();
            SelectedTable = selectedTable;
            _currentTable = SelectedTable == Table.Table1 ? _table1 : _table2;
            _unionOpt = _currentTable.Union(_vowel);
        }

        public bool isConsonant(char character)
        {
            return _currentTable.Any(c => c.CharacterValue == character);
        }

        public Character Get(char character)
        {
            return _unionOpt.SingleOrDefault(c => c.CharacterValue == character);
        }

        public int GetPower(char character)
        {
            var foundCharacter = _unionOpt.SingleOrDefault(c => c.CharacterValue == character);
            return foundCharacter != null ? foundCharacter.Power : 0;
        }

        private void ChangeTable(Table table)
        {
            _currentTable = table == Table.Table1 ? _table1 : _table2;
            _unionOpt = _currentTable.Union(_vowel);
        }

        public List<Character> GetConsonants()
        {
            return _currentTable;
        }

        public List<Character> GetVowels()
        {
            return _vowel;
        }

        public void Add(Character character)
        {
            _currentTable.Add(character);
            _unionOpt = _currentTable.Union(_vowel);
        }

        public void Remove(char character)
        {
            _currentTable.RemoveAll(c => c.CharacterValue == character);
            _unionOpt = _currentTable.Union(_vowel);
        }        

        private List<Character> GetFirstTable()
        {
            return new List<Character>()
            {
                new Character() {CharacterValue = 'ь', Power = 0},
                new Character() {CharacterValue = '-', Power = 0},
                new Character() {CharacterValue = '\'', Power = 0},
                new Character() {CharacterValue = 'ъ', Power = 0},
                new Character() {CharacterValue = 'ф', Power = 1},
                new Character() {CharacterValue = 'с', Power = 1},
                new Character() {CharacterValue = 'х', Power = 1},
                new Character() {CharacterValue = 'ц', Power = 1},
                new Character() {CharacterValue = 'ч', Power = 1},
                new Character() {CharacterValue = 'ш', Power = 1},
                new Character() {CharacterValue = 'щ', Power = 1},
                new Character() {CharacterValue = 'd', Power = 2},
                new Character() {CharacterValue = 'z', Power = 2},
                new Character() {CharacterValue = 'j', Power = 8},
                new Character() {CharacterValue = 'г', Power = 2},
                new Character() {CharacterValue = 'з', Power = 2},
                new Character() {CharacterValue = 'ж', Power = 2},
                new Character() {CharacterValue = 'v', Power = 2},
                new Character() {CharacterValue = 'в', Power = 2},
                new Character() {CharacterValue = 'к', Power = 3},
                new Character() {CharacterValue = 'п', Power = 3},
                new Character() {CharacterValue = 'т', Power = 3},
                new Character() {CharacterValue = 'б', Power = 4},
                new Character() {CharacterValue = 'д', Power = 4},
                new Character() {CharacterValue = 'ґ', Power = 4},
                new Character() {CharacterValue = 'м', Power = 5},
                new Character() {CharacterValue = 'н', Power = 5},
                new Character() {CharacterValue = 'р', Power = 6},
                new Character() {CharacterValue = 'л', Power = 6},
                new Character() {CharacterValue = 'Y', Power = 7},
                new Character() {CharacterValue = 'b', Power = 7},
                new Character() {CharacterValue = 'c', Power = 7},
                new Character() {CharacterValue = 'f', Power = 7},
                new Character() {CharacterValue = 'g', Power = 7},
                new Character() {CharacterValue = 'h', Power = 7},
                new Character() {CharacterValue = 'k', Power = 7},
                new Character() {CharacterValue = 'l', Power = 7},
                new Character() {CharacterValue = 'm', Power = 7},
                new Character() {CharacterValue = 'n', Power = 7},
                new Character() {CharacterValue = 'p', Power = 7},
                new Character() {CharacterValue = 'q', Power = 7},
                new Character() {CharacterValue = 'r', Power = 7},
                new Character() {CharacterValue = 's', Power = 7},
                new Character() {CharacterValue = 't', Power = 7},
                new Character() {CharacterValue = 'w', Power = 7},
                new Character() {CharacterValue = 'x', Power = 7},
                new Character() {CharacterValue = 'u', Power = 7},
                new Character() {CharacterValue = 'y', Power = 7},
                //new Character() {CharacterValue = 'z', Power = 7},
                new Character() { CharacterValue = 'ź', Power = 7 },
                new Character() { CharacterValue = 'ś', Power = 7 },
                new Character() { CharacterValue = 'ł', Power = 7 },
                new Character() { CharacterValue = 'ń', Power = 7 },
                new Character() { CharacterValue = 'ć', Power = 7 },
            };
        }
        
        private List<Character> GetSecondTable()
        {
            return new List<Character>()
            {
                new Character() {CharacterValue = 'ь', Power = 0},
                new Character() {CharacterValue = '-', Power = 0},
                new Character() {CharacterValue = '\'', Power = 0},
                new Character() {CharacterValue = 'ъ', Power = 0},
                new Character() {CharacterValue = 'п', Power = 1},
                new Character() {CharacterValue = 'т', Power = 1},
                new Character() {CharacterValue = 'к', Power = 1},
                new Character() {CharacterValue = 'ф', Power = 2},
                new Character() {CharacterValue = 'с', Power = 2},
                new Character() {CharacterValue = 'х', Power = 2},
                new Character() {CharacterValue = 'ц', Power = 2},
                new Character() {CharacterValue = 'ч', Power = 2},
                new Character() {CharacterValue = 'ш', Power = 2},
                new Character() {CharacterValue = 'щ', Power = 2},
                new Character() {CharacterValue = 'б', Power = 3},
                new Character() {CharacterValue = 'д', Power = 3},
                new Character() {CharacterValue = 'ґ', Power = 3},
                new Character() {CharacterValue = 'v', Power = 4},
                new Character() {CharacterValue = 'j', Power = 4},
                new Character() {CharacterValue = 'г', Power = 4},
                new Character() {CharacterValue = 'з', Power = 4},
                new Character() {CharacterValue = 'ж', Power = 4},
                new Character() {CharacterValue = 'd', Power = 4},
                new Character() {CharacterValue = 'z', Power = 4},
                new Character() {CharacterValue = 'м', Power = 5},
                new Character() {CharacterValue = 'н', Power = 5},
                new Character() {CharacterValue = 'л', Power = 6},
                new Character() {CharacterValue = 'w', Power = 6},
                new Character() {CharacterValue = 'р', Power = 7},
                new Character() {CharacterValue = 'u', Power = 8},
                new Character() {CharacterValue = 'Y', Power = 8},
            };
        }

        private List<Character> GetVowelCharacters()
        {
            return new List<Character>()
            {
                new Character() { CharacterValue = 'а', Power = -1 },
                new Character() { CharacterValue = 'о', Power = -1 },
                new Character() { CharacterValue = 'у', Power = -1 },
                new Character() { CharacterValue = 'и', Power = -1 },
                new Character() { CharacterValue = 'е', Power = -1 },
                new Character() { CharacterValue = 'і', Power = -1 },
                new Character() { CharacterValue = 'ї', Power = -1 },
                new Character() { CharacterValue = 'є', Power = -1 },
                new Character() { CharacterValue = 'я', Power = -1 },
                new Character() { CharacterValue = 'ю', Power = -1 },
                new Character() { CharacterValue = 'ы', Power = -1 },
                new Character() { CharacterValue = 'э', Power = -1 }, 
                new Character() { CharacterValue = 'ѣ', Power = -1 },
                new Character() { CharacterValue = 'ђ', Power = -1 },
                new Character() { CharacterValue = 'Ђ', Power = -1 },
                new Character() { CharacterValue = 'ё', Power = -1 },
                new Character() { CharacterValue = 'ӕ', Power = -1 },//ӕ
                new Character() { CharacterValue = 'Ü', Power = -1 },
                new Character() { CharacterValue = 'u', Power = -1 },
                new Character() { CharacterValue = 'i', Power = -1 },
                new Character() { CharacterValue = 'a', Power = -1 },
                new Character() { CharacterValue = 'o', Power = -1 },
                new Character() { CharacterValue = 'e', Power = -1 },
                new Character() {CharacterValue = 'ó', Power = -1},
                new Character() {CharacterValue = 'ę', Power = -1},
                new Character() {CharacterValue = 'ë', Power = -1},
                new Character() {CharacterValue = 'ą', Power = -1},
                new Character() {CharacterValue = 'ö', Power = -1}
                
            };
        }

    }
}
