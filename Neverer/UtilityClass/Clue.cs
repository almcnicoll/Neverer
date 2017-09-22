using System;
using System.Collections.Generic;
using System.Linq;
using regex = System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Neverer.UtilityClass
{

    [Serializable]
    public class Clue
    {
        public const String BlankQuestion = "[blank clue]";

        private String __question = "";
        private String __answer = "";

        public String question {
            get
            {
                return __question;
            }
            set
            {
                __question = value;
            }
        }
        public String answer
        {
            get
            {
                return __answer;
            }
            set
            {
                regex.Regex reStrip = new regex.Regex("[^A-Za-z? ]+");
                __answer = reStrip.Replace(value, "").ToUpper();
            }
        }
        public String letters
        {
            get
            {
                regex.Regex reStrip = new regex.Regex("[^A-Za-z?]+");
                return reStrip.Replace(__answer, "");
            }
        }

        public int length
        {
            get
            {
                return letters.Length;
            }
        }
        public List<int> pattern {
            get
            {
                List<String> sep = new List<String>();
                sep.Add(" ");
                List<String> parts = answer.Split(sep.ToArray(),StringSplitOptions.RemoveEmptyEntries).ToList();
                //List<int> lengths = new List<int>();
                return (from String part in parts
                           select part.Length).ToList();
            }
        }

        public override String ToString()
        {
            return String.Format("{0} ({1})", __question, String.Join(",", pattern));
        }

        [XmlIgnore()]
        public Guid id { get; set; }

        public Clue blankClone()
        {
            Clue c = new Clue();
            regex.Regex reAllQuestionMarks = new regex.Regex("[^? ]");
            c.answer = reAllQuestionMarks.Replace(answer, "?");
            c.question = BlankQuestion;
            return c;
        }
        public void CopyTo(Clue cDest)
        {
            if (cDest==null) { cDest = new Clue(); }
            cDest.__question = __question;
            cDest.__answer = __answer;
        }

        public Clue()
        {
            id = Guid.NewGuid();
            question = "";
            answer = "";
        }

        public Clue(String q,String a)
        {
            id = Guid.NewGuid();
            question = q;
            answer = a;
        }
    }
}
