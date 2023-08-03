using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using regex = System.Text.RegularExpressions;
using System.Linq;

namespace Neverer.UtilityClass
{
    [Serializable]
    public enum AD
    {
        Unset = 0,
        Across = 1,
        Down = 2
    }

    [Serializable]
    public class PlacedClue
    {
        // Declarations
        public Color statusColor
        {
            get
            {
                return PlacedClue.StatusColor(status);
            }
        }
        public static Color StatusColor(ClueStatus status)
        {
            /*
            switch (status)
            {
                case ClueStatus.NoMatchingWord:
                    return Color.Red;
                case ClueStatus.MatchingWordNoQuestion:
                    return Color.Gold;
                case ClueStatus.MatchingWordWithQuestion:
                    return Color.MediumSeaGreen;
                case ClueStatus.NoMatchingWordComplete:
                    return Color.LightSlateGray;
                case ClueStatus.FewMatchingWords:
                    return Color.DarkOrange;
                case ClueStatus.Complete:
                    return Color.CornflowerBlue;
                default:
                    return Color.FromKnownColor(KnownColor.Control);
            }
            */

            // Do we have matches?
            if (status.HasFlag(ClueStatus.HasMatches))
            {
                // Yes - matches
                // Do we have a chosen answer?
                if (status.HasFlag(ClueStatus.AnswerComplete))
                {
                    // Yes - chosen answer
                    // Do we have a question filled out?
                    if (status.HasFlag(ClueStatus.QuestionComplete))
                    {
                        // Yes - we're done here - blue
                        return Color.CornflowerBlue;
                    }
                    else
                    {
                        // No - need a question still
                        return Color.MediumSeaGreen;
                    }
                }
                else
                {
                    // No - some blanks remain
                    // Are we down to only a few matching words?
                    if (status.HasFlag(ClueStatus.HasFewMatches))
                    {
                        // Yes - dark orange
                        return Color.DarkOrange;
                    }
                    else
                    {
                        // No - gold
                        return Color.Gold;
                    }
                }
            }
            else
            {
                // No matches
                // Do we have a chosen answer in place?
                if (status.HasFlag(ClueStatus.AnswerComplete))
                {
                    // Answer chosen but we don't recognise it - might not be a valid word
                    return Color.LightSlateGray;
                }
                else
                {
                    // No answer, no matches - we could be in trouble
                    return Color.Red;
                }
            }
            //return Color.FromKnownColor(KnownColor.Control);
        }

        // Member variables
        public Clue clue = new Clue();
        public int x = -1;
        public int y = -1;
        public AD orientation = AD.Unset;
        private int __placeNumber = 0; // This property is not updated by this class, but is for storage by calling routines
        private Guid __UniqueID;
        private ClueStatus __status = ClueStatus.Unknown;

        private SerializableDictionary<String, List<String>> __matches = new SerializableDictionary<String, List<String>>();
        private bool __matchesChanged = true;
        private bool __uncheckedChanges = true;
        private bool __pauseEvents = false;
        private int lowMatchesTrigger = 5;

        // Refined properties take into account possible solutions on other clues, and their effect on this clue
        [XmlIgnore]
        private Dictionary<int, HashSet<char>> __refinedLetters = null;
        [XmlIgnore]
        private Dictionary<int, HashSet<char>> __externalConstraints = null;
        [XmlIgnore]
        private Dictionary<int, HashSet<char>> __internalConstraints = null;
        private SerializableDictionary<String, List<String>> __refinedMatches = new SerializableDictionary<String, List<String>>();
        private bool __refinedChanges = true;
        private regex.Regex __refinedRegex = null;

        private HashSet<char> allLetters = new HashSet<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        [XmlIgnore()]
        public int height
        {
            get
            {
                if (this.orientation == AD.Across) { return 1; }
                return this.clue.length;
            }
        }
        [XmlIgnore()]
        public int width
        {
            get
            {
                if (this.orientation == AD.Down) { return 1; }
                return this.clue.length;
            }
        }
        public static int GetOrder(int row, int col)
        {
            return (row * 1000) + col;
        }

        // Properties
        public int order
        {
            get
            {
                return PlacedClue.GetOrder(y, x);
            }
        }

        /// <summary>
        /// Returns the __refinedLetters variable, blank-populated if null
        /// </summary>
        [XmlIgnore]
        public Dictionary<int, HashSet<char>> refinedLetters
        {
            get
            {
                // Make sure our private variable is populated
                if (__refinedLetters == null)
                {
                    // Populate a per-character-position lookup of which letters are valid
                    __refinedLetters = new Dictionary<int, HashSet<char>>();
                    __internalConstraints = new Dictionary<int, HashSet<char>>();
                    __externalConstraints = new Dictionary<int, HashSet<char>>();
                    for (int i = 0; i < clue.length; i++)
                    {
                        // TODO - fine to do this now, but what if clue changes length?
                        if (clue.letters[i] == '?')
                        {
                            __refinedLetters.Add(i, new HashSet<char>(allLetters)); // Create this pre-populated
                        }
                        else
                        {
                            __refinedLetters.Add(i, new HashSet<char>()); __refinedLetters[i].Add(Char.ToLower(clue.letters[i]));
                        }
                        __internalConstraints.Add(i, new HashSet<char>(allLetters)); // Create this pre-populated
                        __externalConstraints.Add(i, new HashSet<char>(allLetters)); // Create this pre-populated
                    }
                }
                // Return the variable
                return __refinedLetters;
            }
        }

        public String placeDescriptor
        {
            get
            {
                //return __placeDescriptor;
                return placeNumber.ToString() + " " + orientation.ToString();
            }
            /*set
            {
                if (__placeDescriptor != value) { EventHandler<EventArgs> evt = ClueDefinitionChanged; if (evt != null) { evt(this, new EventArgs()); } }
                __placeDescriptor = value;
            }*/
        }
        public int placeNumber
        {
            get
            {
                return __placeNumber;
            }
            set
            {
                if (__placeNumber != value) { EventHandler<EventArgs> evt = ClueDefinitionChanged; if (evt != null) { evt(this, new EventArgs()); } }
                __placeNumber = value;
                Changed();
            }
        }
        public String clueText
        {
            get
            {
                return clue.ToString();
            }
        }

        public regex.Regex refinedRegex
        {
            get
            {
                if (__refinedChanges)
                {
                    // TODO - rewrite recalculateRefinedRegex();
                    recalculateRefinedRegex();
                }
                return __refinedRegex;
            }
        }

        /// <summary>
        /// Recalculates the status of the clue based on available word-matches data
        /// </summary>
        public void recalculateStatus()
        {
            ClueStatus newStatus = ClueStatus.Unknown;
            if (!clue.answer.Contains("?")) { newStatus |= ClueStatus.AnswerComplete; }
            if ((clue.question != "") && (clue.question != Clue.BlankQuestion)) { newStatus |= ClueStatus.QuestionComplete; }
            if ((Matches != null) && (Matches.Count > 0))
            {
                newStatus |= ClueStatus.HasMatches;
                if (Matches.Count <= lowMatchesTrigger) { newStatus |= ClueStatus.HasFewMatches; }
            }

            /*if (clue.answer.Contains("?"))
            {
                if ((Matches == null) || (Matches.Count == 0))
                {
                    __status = ClueStatus.NoMatchingWord;
                }
                else if ((clue.question == "") || (clue.question == Clue.BlankQuestion))
                {
                    __status = ClueStatus.MatchingWordNoQuestion;
                }
                else
                {
                    __status = ClueStatus.MatchingWordWithQuestion;
                }
            }
            else
            {
                if ((clue.question == "") || (clue.question == Clue.BlankQuestion))
                {
                    __status = ClueStatus.MatchingWordNoQuestion;
                }
                else
                {
                    if ((Matches == null) || (Matches.Count == 0))
                    {
                        __status = ClueStatus.NoMatchingWordComplete;
                    }
                    else
                    {
                        __status = ClueStatus.Complete;
                    }
                }
            }
            // Handle low-matches
            if (((__status == ClueStatus.MatchingWordWithQuestion)
                || (__status == ClueStatus.MatchingWordNoQuestion))
                && (Matches.Count <= lowMatchesTrigger)
                && (clue.answer.Contains("?")))
            {
                __status = ClueStatus.FewMatchingWords;
            }
            */
            // Put the Changed() call in an if(), otherwise we end up in a recursive loop
            if (__status != newStatus)
            {
                __status = newStatus;
                Changed();
            }
        }

        public ClueStatus status
        {
            get
            {
                if (__status == ClueStatus.Unknown)
                {
                    recalculateStatus();
                }
                return __status;
            }
            set
            {
                if (__status != value)
                {
                    if (value == ClueStatus.Unknown) { return; }
                    __status = value;
                    //EventHandler<ClueStatusChangedEventArgs> evt = ClueStatusChanged;
                    //if (evt != null) { evt(this, new ClueStatusChangedEventArgs(value)); }
                    Changed();
                }
            }
        }

        public SerializableDictionary<String, List<String>> Matches
        {
            get
            {
                return __matches;
            }
        }

        public bool uncheckedChanges
        {
            get
            {
                return __uncheckedChanges;
            }
        }
        public void changesChecked(bool isChecked = true)
        {
            __uncheckedChanges = !isChecked;
        }

        // Events
        public event EventHandler<ClueStatusChangedEventArgs> ClueStatusChanged;
        public event EventHandler<EventArgs> ClueDefinitionChanged;

        // Constructor
        public PlacedClue()
        {
            __UniqueID = Guid.NewGuid();
            clue.changed += Changed;
        }

        // Functions
        public void Changed(Clue sender = null, ClueChangedEventArgs e = null)
        {
            // One universal place to register that the clue has changed
            //  and needs to bubble up an event.
            //  this includes when the base text of the clue changes

            // __pauseEvents allows us to make multiple changes without triggering a repaint every time (use with caution!)
            if (this.__pauseEvents) { return; }

            // Trigger event, leading to repaint etc.
            if (e == null) { e = new ClueChangedEventArgs(false); }
            if (!e.NoRecheck) { this.__uncheckedChanges = true; }
            EventHandler<EventArgs> evt = ClueDefinitionChanged;
            if (evt != null) { evt(this, new EventArgs()); }
        }
        public Guid UniqueID
        {
            get
            {
                return __UniqueID;
            }
        }

        public override string ToString()
        {
            String clueText = "";
            if (clue == null)
            {
                clueText = "[no clue]";
            }
            else
            {
                clueText = clue.ToString();
            }
            return String.Format("{0} [{1},{2}] {3}", clueText, x, y, orientation);
        }

        /// <summary>
        /// Copies the <see cref="PlacedClue"/> object to another, essentially creating a clone
        /// </summary>
        /// <param name="target">The target <see cref="PlacedClue"/> object of the copy operation</param>
        public void CopyTo(PlacedClue target)
        {
            target.__pauseEvents = true; // Don't repaint on every property-copy
            target.orientation = orientation;
            target.placeNumber = __placeNumber;
            target.__UniqueID = __UniqueID;
            //target.placeDescriptor = __placeDescriptor;
            target.x = x;
            target.y = y;
            target.status = status;
            target.__pauseEvents = false;
            clue.CopyTo(target.clue);
            target.Changed();
        }

        /// <summary>
        /// Creates a clone of the current clue
        /// </summary>
        /// <returns>Cloned <see cref="PlacedClue"/> object</returns>
        public PlacedClue clone()
        {
            PlacedClue tmp = new PlacedClue();
            this.CopyTo(tmp);
            return tmp;
        }

        public bool Overlaps(int col, int row) // row & col are zero-based
        {
            switch (orientation)
            {
                case AD.Across:
                    if (row != y) { return false; }
                    if (col < x) { return false; }
                    if (col >= (x + clue.length)) { return false; }
                    break;
                case AD.Down:
                    if (col != x) { return false; }
                    if (row < y) { return false; }
                    if (row >= (y + clue.length)) { return false; }
                    break;
                default:
                    return false;
            }
            return true;
        }
        public bool Overlaps(PlacedClue pc)
        {
            // Determine which direction we iterate
            int xMult1 = ((pc.orientation == AD.Down) ? 0 : 1);
            int yMult1 = ((pc.orientation == AD.Down) ? 1 : 0);
            // Iterate through all co-ords of argument clue (pc), seeing if it overlaps with current clue (this)
            for (int i = 0; i < pc.clue.length; i++)
            {
                int x1 = pc.x + (i * xMult1);
                int y1 = pc.y + (i * yMult1);
                if (this.Overlaps(x1, y1)) { return true; }
            }
            return false;
        }
        public System.Drawing.Point? GetOverlap(PlacedClue pc)
        {
            // Determine which direction we iterate
            int xMult1 = ((pc.orientation == AD.Down) ? 0 : 1);
            int yMult1 = ((pc.orientation == AD.Down) ? 1 : 0);
            // Iterate through all co-ords of argument clue (pc), seeing if it overlaps with current clue (this)
            for (int i = 0; i < pc.clue.length; i++)
            {
                int x1 = pc.x + (i * xMult1);
                int y1 = pc.y + (i * yMult1);
                if (this.Overlaps(x1, y1)) { return new System.Drawing.Point(x1, y1); }
            }
            return null;
        }

        /// <summary>
        /// Returns the coordinates of the cell at position <paramref name="position"/> in the clue
        /// </summary>
        /// <param name="position">The position in the clue to search at</param>
        /// <returns></returns>
        public Point GetCoordsAtPosition(int position)
        {
            int posX = this.x;
            int posY = this.y;
            if (this.orientation == AD.Across) { posX += position; }
            if (this.orientation == AD.Down) { posY += position; }
            return new Point(posX, posY);
        }

        /* RefinePattern functions for rewriting
        /// <summary>
        /// Filters the list of "refined matches" by a further string-pattern
        /// </summary>
        /// <param name="overlay">String of letters or question marks indicating the character mask</param>
        /// <returns>True if the operation changed the number of possible matches</returns>
        public bool RefinePattern(String overlay)
        {
            regex.Regex reStrip = new regex.Regex("[^A-Za-z?]+");
            overlay = reStrip.Replace(overlay, "");

            char[] overlayLetters = overlay.ToCharArray();
            char[] rl = refinedLetters.ToCharArray();
            for (int i = 0; i < overlayLetters.Length; i++)
            {
                char c = overlayLetters[i];
                if (c != '?')
                {
                    rl[i] = c;
                }
            }
            refinedLetters = rl.ToString();

            // Now refine the matches further
            regex.Regex re = Clue.toRegExp(refinedLetters);
            return RefinePattern(re);
        }

        /// <summary>
        /// Filters the list of "refined matches" by limiting the specified cell to a set list of characters
        /// </summary>
        /// <param name="x">The x coordinate of the cell</param>
        /// <param name="y">The y coordinate of the cell</param>
        /// <param name="possibleLetters">An array of the possible letters for this slot</param>
        /// <returns>True if the operation changed the number of possible matches</returns>
        public bool RefinePattern(int x, int y, char[] possibleLetters)
        {
            int pos; // Zero-based position within the clue of the specified cell
            switch (this.orientation)
            {
                case AD.Across:
                    if (
                        (y == this.y) &&
                        (x >= this.x) &&
                        (x < this.x + this.clue.length)
                        )
                    {
                        pos = x - this.x;
                    }
                    else
                    {
                        // Doesn't intersect
                        return false;
                    }
                    break;
                case AD.Down:
                    if (
                        (x == this.x) &&
                        (y >= this.y) &&
                        (y < this.y + this.clue.length)
                        )
                    {
                        pos = y - this.y;
                    }
                    else
                    {
                        // Doesn't intersect
                        return false;
                    }
                    break;
                default:
                    return false;
            }
            String pattern = "";
            for (int i = 0; i < pos; i++)
            {
                pattern += ".[" + Clue.NonCountingChars_Regex + "]*";
            }
            regex.Regex overlay = new regex.Regex(pattern, regex.RegexOptions.IgnoreCase);
            return RefinePattern(overlay);
        }

        /// <summary>
        /// Filters the list of "refined matches" by a regular expression
        /// </summary>
        /// <param name="overlay">Regex indicating the character mask</param>
        /// <returns>True if the operation changed the number of possible matches</returns>
        public bool RefinePattern(regex.Regex overlay)
        {
            bool refinementsMade = false;
            // Now refine the matches further
            List<String> keys = new List<String>(__refinedMatches.Keys);
            foreach (String key in keys)
            {
                if (!overlay.IsMatch(key))
                {
                    __refinedMatches.Remove(key);
                    refinementsMade = true;
                }
            }
            __refinedChanges = true;
            return refinementsMade;
        }
        */


        /// <summary>
        /// Recalculates the regex that defines what letters can be where
        /// </summary>
        private void recalculateRefinedRegex()
        {
            bool changesMade = false;
        }
        /* recalculateRefinedRegex function for rewriting
        private void recalculateRefinedRegex()
        {
            // Check whether we make any changes - so we know whether to recalculate intersects
            bool changesMade = false;

            // If we haven't populated this dict then it must be the first time around
            if (__refinedPatternLetters == null)
            {
                // Populate a per-character-position lookup of which letters are valid
                __refinedPatternLetters = new Dictionary<int, HashSet<char>>();
                for (int i = 0; i < clue.length; i++)
                {
                    __refinedPatternLetters.Add(i, new HashSet<char>());
                }
                changesMade = true;
            }

            // Log old refinedPatternLetters so we can tell if it's changed
            Dictionary<int, HashSet<char>> oldLetters;
            if (changesMade)
            {
                // If we know we already have changes, there's no point checking for more
                oldLetters = new Dictionary<int, HashSet<char>>();
            }
            else
            {
                // Otherwise work from a deep copy
                oldLetters = __refinedPatternLetters.DeepCopy();
            }

            // Go through each possible word, seeing which letters could fit in each position
            foreach (String word in __refinedMatches.Keys)
            {
                char[] wordChars = word.ToCharArray();
                for (int i = 0; i < word.Length; i++)
                {
                    __refinedPatternLetters[i].Add(wordChars[i]);
                }
            }

            // Check if refinedPatternLetters has changed as a result - but only bother if we haven't already got changes
            if (!changesMade)
            {
                // TODO - would be good to see _which_ positions changed so we don't recalculate ALL intersecting clues
                // That would involve switching from a boolean to a Dict<int,boolean> or something like that
                changesMade = !__refinedPatternLetters.DeepEquals(oldLetters);
            }

            // Now generate a character class per position, separating them
            String[] refinedPatternClasses = new string[clue.length];
            for (int i = 0; i < clue.length; i++)
            {
                refinedPatternClasses[i] = "[" + String.Join("", __refinedPatternLetters[i]) + "]";
            }
            __refinedRegex = new regex.Regex("^" + String.Join("[" + Clue.NonCountingChars_Regex + "]*", refinedPatternClasses) + "$", regex.RegexOptions.IgnoreCase);
            __refinedChanges = false;

            // TODO - if changes made, recalculate refinedPatterns of all intersecting clues?
        }
        */

        /// <summary>
        /// Clear the list of matches against the clue
        /// </summary>
        public void clearMatches()
        {
            __matches = null;
            __matches = new SerializableDictionary<String, List<String>>();
            __refinedMatches = null;
            __refinedMatches = new SerializableDictionary<String, List<String>>();
            /*
                        status = ClueStatus.NoMatchingWord;
                        if (clue.answer.Contains("?"))
                        {
                            status = ClueStatus.NoMatchingWord;
                        }
                        else if ((clue.question == null) || (clue.question == Clue.BlankQuestion))
                        {
                            status = ClueStatus.MatchingWordNoQuestion;
                        }
                        else
                        {
                            status = ClueStatus.MatchingWordWithQuestion;
                        }
            */
        }

        /// <summary>
        /// Adds a match to the list
        /// </summary>
        /// <param name="answer">The matching answer</param>
        /// <param name="question">The suggested question text</param>
        public void addMatch(String answer, String question = "")
        {
            // Ensure there's an entry
            if (!__matches.ContainsKey(answer))
            {
                __matches.Add(answer, new List<String>());
                //if (__status != ClueStatus.MatchingWordWithQuestion) { status = ClueStatus.MatchingWordNoQuestion; }
                __status |= ClueStatus.HasMatches;
            }
            if (!__refinedMatches.ContainsKey(answer))
            {
                __refinedMatches.Add(answer, new List<String>());
            }
            // No duplicate questions (including blank question, which means "answer only")
            if (!__matches[answer].Contains(question))
            {
                __matches[answer].Add(question);
                //if (__status != ClueStatus.MatchingWordWithQuestion) { status = ClueStatus.MatchingWordNoQuestion; }
                //if (question != "") { status = ClueStatus.MatchingWordWithQuestion; }
                __status |= ClueStatus.HasMatches;
            }
            if (!__refinedMatches[answer].Contains(question))
            {
                __refinedMatches[answer].Add(question);
            }
        }

        /// <summary>
        /// Refreshes the RefinedLetters structure based on the clue itself and the ExternalConstraints structure
        /// </summary>
        /// <returns>A set of integer positions in the clue where the constraints have changed</returns>
        public HashSet<int> RefreshRefinedLetters()
        {
            // Prepare return variable
            HashSet<int> modifiedList = new HashSet<int>();

            // Make a deep copy of the original
            Dictionary<int, HashSet<char>> old = __refinedLetters.DeepCopy();

            bool furtherRefiningNeeded = true;

            while (furtherRefiningNeeded)
            {
                // If matches have changed, update InternalConstraints
                if (__matchesChanged)
                {
                    // Remove any constraint keys beyond clue-length
                    foreach (int k in (from int k in __internalConstraints.Keys where k>=this.clue.length select k))
                    {
                        __internalConstraints.Remove(k);
                    }
                    // Loop through each character of the word, updating InternalConstraints from matches
                    for(int pos=0;pos< this.clue.length; pos++)
                    {
                        // Populate with a-z if key not present
                        if (!__internalConstraints.ContainsKey(pos)) { __internalConstraints.Add(pos,new HashSet<char>(allLetters)); }
                        // Populate with all possible letters found at this position in the string
                        //HashSet<char> posLetters = new HashSet<char>(from String word in __matches.Keys select Char.ToUpper(word[pos]));
                        __internalConstraints[pos] = new HashSet<char>(from String word in __matches.Keys select Char.ToUpper(word[pos]));
                    }
                }
                __matchesChanged = false;
                
                var answerChars = clue.letters.ToCharArray();

                // Loop through clue length, adding in constraints from clue and ExternalConstraints
                for (int pos = 0; pos < clue.length; pos++)
                {
                    HashSet<char> thisPos;

                    // Start with clue constraint
                    if (answerChars[pos] == '?')
                    {
                        thisPos = new HashSet<char>(allLetters);
                    }
                    else
                    {
                        thisPos = new HashSet<char>();
                        thisPos.Add(Char.ToLower(answerChars[pos]));
                    }

                    // Then look to ExternalConstraints
                    if (__externalConstraints.ContainsKey(pos))
                    {
                        // If there's external constraints, apply them (intersect)
                        __refinedLetters[pos] = new HashSet<char>(__externalConstraints[pos]);
                        __refinedLetters[pos].IntersectWith(thisPos);
                        //__refinedLetters[pos] = (HashSet<char>)__externalConstraints[pos].Intersect(thisPos);
                    }
                    else
                    {
                        // Otherwise just use the clue constraints
                        __refinedLetters[pos] = thisPos;
                    }
                }

                furtherRefiningNeeded = false;

                // Now compare to the original structure - remembering to loop past end of clueLength if old struct is longer (if, for instance, the clue has been shortened)
                var oldMax = (from int k in __externalConstraints.Keys select k).Max();
                for (int pos = 0; pos < Math.Max(clue.length, oldMax); pos++) // TODO - clue.length is 1-based, oldMax is 0-based. Is this a problem?
                {
                    if ((!old.ContainsKey(pos)) || (!__refinedLetters.ContainsKey(pos)))
                    {
                        // Mismatch of lengths - must be a difference
                        modifiedList.Add(pos);
                    }
                    else
                    {
                        // Both have a value - are they the same?
                        if (!old[pos].SetEquals(__refinedLetters[pos]))
                        {
                            // Lists are different - changes at this position
                            modifiedList.Add(pos);
                        }
                        else
                        {
                            // No changes here
                        }
                    }
                }

                // If we've made changes, refine our list of possible words
                if (modifiedList.Count > 0)
                {
                    // Count current words
                    int oldMatchCount = this.Matches.Count;
                    // Refine list
                    // TODO refineMatches();
                    // If we've narrowed it down, re-run refining process because refinedLetters will have been updated
                    if (this.Matches.Count != oldMatchCount)
                    {
                        furtherRefiningNeeded = true;
                    }
                }
            }

            return modifiedList;
        }
    }
}
