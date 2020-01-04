using System;

namespace Neverer.UtilityClass
{
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
