using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

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
            switch (status)
            {
                case ClueStatus.NoMatchingWord:
                    return Color.Red;
                case ClueStatus.MatchingWordNoQuestion:
                    return Color.Gold;
                case ClueStatus.MatchingWordWithQuestion:
                    return Color.CornflowerBlue;
                case ClueStatus.NoMatchingWordComplete:
                    return Color.LightSlateGray;
                default:
                    return Color.FromKnownColor(KnownColor.Control);
            }
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
        private bool __uncheckedChanges = true;
        private bool __pauseEvents = false;

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

        public void recalculateStatus()
        {
            if (clue.answer.Contains("?"))
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
                        __status = ClueStatus.MatchingWordWithQuestion;
                    }
                }
            }
            Changed();
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
        private void Changed(Clue sender = null, ClueChangedEventArgs e = null)
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

        public void clearMatches()
        {
            __matches = null;
            __matches = new SerializableDictionary<String, List<String>>();
            //status = ClueStatus.NoMatchingWord;
            //if (clue.answer.Contains("?"))
            //{
            //    status = ClueStatus.NoMatchingWord;
            //} else if ((clue.question == null)||(clue.question == Clue.BlankQuestion))
            //{
            //    status = ClueStatus.MatchingWordNoQuestion;
            //} else
            //{
            //    status = ClueStatus.MatchingWordWithQuestion;
            //}
        }
        public void addMatch(String answer, String question = "")
        {
            // Ensure there's an entry
            if (!__matches.ContainsKey(answer))
            {
                __matches.Add(answer, new List<String>());
                if (__status != ClueStatus.MatchingWordWithQuestion) { status = ClueStatus.MatchingWordNoQuestion; }
            }
            // No duplicate questions (including blank question, which means "answer only")
            if (!__matches[answer].Contains(question))
            {
                __matches[answer].Add(question);
                if (__status != ClueStatus.MatchingWordWithQuestion) { status = ClueStatus.MatchingWordNoQuestion; }
                if (question != "") { status = ClueStatus.MatchingWordWithQuestion; }
            }
        }
    }
}
