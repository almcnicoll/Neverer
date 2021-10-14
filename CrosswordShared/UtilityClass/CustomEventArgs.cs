using System;
using System.Collections.Generic;

namespace Neverer.UtilityClass
{
    public class PlacedClueChangedEventArgs : EventArgs
    {
        public List<PlacedClue> clues { get; set; }
        public bool newlyCreated { get; set; }

        public PlacedClueChangedEventArgs(List<PlacedClue> clues = null, bool newlyCreated = false)
        {
            this.newlyCreated = newlyCreated;
            if (clues == null)
            {
                this.clues = null;
            }
            else
            {
                this.clues = clues;
            }
        }
        public PlacedClueChangedEventArgs(PlacedClue clue = null, bool newlyCreated = false)
        {
            this.newlyCreated = newlyCreated;
            if (clue == null)
            {
                this.clues = null;
            }
            else
            {
                this.clues = new List<PlacedClue>();
                this.clues.Add(clue);
            }
        }
    }

    public class ClueChangedEventArgs : EventArgs
    {
        [Flags, Serializable]
        public enum ClueChangeType
        {
            NothingSignificant = 0,
            LengthChanged = 1,
            PositionChanged = 2,
            LettersChanged = 4
        }
        public bool NoRecheck { get; set; }
        public ClueChangeType type { get; set; }

        public ClueChangedEventArgs(bool NoRecheck, ClueChangeType type)
        {
            this.NoRecheck = NoRecheck;
            this.type = type;
        }
    }
    public class ClueStatusChangedEventArgs : EventArgs
    {
        public ClueStatus NewStatus { get; set; }

        public ClueStatusChangedEventArgs(ClueStatus NewStatus)
        {
            this.NewStatus = NewStatus;
        }
    }

}
