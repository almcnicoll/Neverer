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
        public bool NoRecheck { get; set; }

        public ClueChangedEventArgs(bool NoRecheck = false)
        {
            this.NoRecheck = NoRecheck;
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
