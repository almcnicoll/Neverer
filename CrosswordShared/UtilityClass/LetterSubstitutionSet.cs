using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neverer.UtilityClass
{
    public class LetterSubstitutionSet
    {
        public List<LetterSubstitution> proposedChanges = new List<LetterSubstitution>();
        public List<LetterSubstitution> definiteChanges = new List<LetterSubstitution>();

        public static LetterSubstitutionSet getIntersectionChanges(PlacedClue pc, List<PlacedClue> otherClues, int length = 0)
        {
            if (length == 0) { length = pc.clue.answer.Length; }
            String[] letters = new String[length];

            LetterSubstitutionSet ls = new LetterSubstitutionSet();

            // TODO - "Collection was modified" firing here
            foreach (PlacedClue pc2 in otherClues)
            {
                System.Drawing.Point? overlap = pc.GetOverlap(pc2);
                if ((pc.order == pc2.order) && (pc.orientation == pc2.orientation)) { continue; }
                if (overlap.HasValue)
                {
                    int pos = Math.Max(overlap.Value.X - pc.x, overlap.Value.Y - pc.y);
                    int pos2 = Math.Max(overlap.Value.X - pc2.x, overlap.Value.Y - pc2.y);
                    letters[pos] = (pc2.clue.letters[pos2]).ToString();
                    if (pc.clue.letters[pos].ToString() != letters[pos])
                    {
                        if (letters[pos] == "?")
                        {
                            // No need to do anything - it intersects with a clue that hasn't been filled out
                        }
                        else if (pc.clue.letters[pos] == '?')
                        {
                            ls.definiteChanges.Add(new LetterSubstitution(pos, pc.clue.letters[pos].ToString(), letters[pos], true));
                        }
                        else
                        {
                            ls.proposedChanges.Add(new LetterSubstitution(pos, pc.clue.letters[pos].ToString(), letters[pos]));
                        }
                    }
                }
            }
            return ls;
        }
    }
}
