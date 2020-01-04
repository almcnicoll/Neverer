using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neverer.UtilityClass
{
    public class LetterSubstitution
    {
        public int position { get; set; }
        public String oldValue { get; set; }
        public String newValue { get; set; }
        public bool accepted { get; set; }

        public LetterSubstitution(int position, String oldValue, String newValue)
        {
            this.position = position;
            this.oldValue = oldValue;
            this.newValue = newValue;
            accepted = false;
        }
        public LetterSubstitution(int position, String oldValue, String newValue, bool accepted)
        {
            this.position = position;
            this.oldValue = oldValue;
            this.newValue = newValue;
            this.accepted = accepted;
        }
    }
}
