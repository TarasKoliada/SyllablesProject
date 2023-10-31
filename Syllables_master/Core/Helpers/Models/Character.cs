using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklady
{
    public class Character
    {
        public char CharacterValue { get; set; }

        public int Power { get; set; }

        public bool IsConsonant { get { return Power != -1; } }

        public override string ToString()
        {
            return IsConsonant? String.Format("{0} - {1}", CharacterValue, Power) : CharacterValue.ToString();
        }
    }
}
