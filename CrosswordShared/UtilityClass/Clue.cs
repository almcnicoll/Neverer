using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using regex = System.Text.RegularExpressions;

namespace Neverer.UtilityClass
{

    [Serializable]
    public class Clue
    {
        public const String BlankQuestion = "[blank clue]";
        public static readonly String[] NonCountingChars = new String[] { " ", "-", "'" };
        public static String NonCountingChars_Regex
        {
            get
            {
                //return regex.Regex.Escape(String.Join("", NonCountingChars));
                return @"\" + String.Join(@"\", NonCountingChars);
            }
        }

        private String __question = "";
        private String __answer = "";

        public String question
        {
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
                if (value==null) { value = ""; }
                regex.Regex reStrip = new regex.Regex("[^A-Za-z?" + Clue.NonCountingChars_Regex + "]+");
                __answer = reStrip.Replace(value, "").ToUpper();
                changed?.Invoke(this, new ClueChangedEventArgs());
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

        /// <summary>
        /// Takes a clue pattern in the form it's displayed to end-users, and returns it as a regular expression
        /// </summary>
        /// <param name="source">The clue pattern</param>
        /// <param name="allowFlexibleSpaces">Whether to return matches which split the clue differently (spaces/hyphens)</param>
        /// <returns></returns>
        public static regex.Regex toRegExp(String source, Boolean allowFlexibleSpaces = true)
        {
            if (allowFlexibleSpaces)
            {
                // build with blocks of (.([...])?)
                char[] allChars = source.Replace("?", ".").ToCharArray();
                String prefix = "(";
                String suffix = "([" + Clue.NonCountingChars_Regex + "]?))";
                String pattern = "^" + prefix + String.Join(suffix + prefix, allChars) + suffix + "$";
                return new regex.Regex(pattern, regex.RegexOptions.IgnoreCase);
            }
            else
            {
                throw new NotImplementedException("No implementation of this function yet");
                /*return new regex.Regex(
                            "^"
                            + regex.Regex.Replace(
                                source.Replace("?", ".")
                                , "."
                                , "$0[" + Clue.NonCountingChars_Regex + "]*"
                            )
                            + "$"
                        , regex.RegexOptions.IgnoreCase);*/
            }
        }

        public regex.Regex regExp
        {
            get
            {
                return Clue.toRegExp(this.answer);
            }
        }

        public int length
        {
            get
            {
                return letters.Length;
            }
        }
        public List<int> pattern
        {
            get
            {
                /*List<String> sep = new List<String>();
                sep.Add(" ");
                sep.Add("-");*/
                //List<String> parts = answer.Split(sep.ToArray(),StringSplitOptions.RemoveEmptyEntries).ToList();
                List<String> parts = answer.Split(Clue.NonCountingChars, StringSplitOptions.RemoveEmptyEntries).ToList();
                //List<int> lengths = new List<int>();
                return (from String part in parts
                        select part.Length).ToList();
            }
        }

        public override String ToString()
        {
            String output = "";  // String.Format("{0} ({1})", __question, String.Join(",", pattern));
            int i = 0;
            foreach (int l in pattern)
            {
                if ((l + i) >= __answer.Length)
                {
                    output += l.ToString();
                    continue;
                }
                switch (__answer.Substring(l + i, 1))
                {
                    case "-":
                        output += l.ToString() + "-";
                        break;
                    default:
                        output += l.ToString() + ",";
                        break;
                }
                i += l + 1;
            }
            return String.Format("{0} ({1})", __question, output); ;
        }

        [XmlIgnore()]
        public Guid id { get; set; }

        public Clue blankClone(bool makeContiguous = true)
        {
            Clue c = new Clue();
            // Replace everything that's a letter with a question-mark
            regex.Regex reAllQuestionMarks = new regex.Regex(String.Format("[^?{0}]", String.Join("", NonCountingChars)));
            c.answer = reAllQuestionMarks.Replace(this.answer, "?");
            if (makeContiguous)
            {
                // Lose any dividers (space, hyphen, etc.)
                regex.Regex reNoDividers = new regex.Regex("[^?]");
                c.answer = reNoDividers.Replace(c.answer, "");
            }
            // Make question blank
            c.question = BlankQuestion;
            return c;
        }
        public void CopyTo(Clue cDest)
        {
            if (cDest == null) { cDest = new Clue(); }
            cDest.__question = __question;
            cDest.__answer = __answer;
        }

        public Clue()
        {
            id = Guid.NewGuid();
            question = "";
            answer = "";
        }

        public Clue(String q, String a)
        {
            id = Guid.NewGuid();
            question = q;
            answer = a;
        }

        public delegate void ChangedEvent(Clue sender, ClueChangedEventArgs e);

        public event ChangedEvent changed;
    }
}
