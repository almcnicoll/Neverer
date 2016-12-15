using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Clue clue = new Clue();
        public int x = -1;
        public int y = -1;
        public AD orientation = AD.Unset;
        private String __placeDescriptor = ""; // This property is not updated by this class, but is for storage by calling routines
        private int __placeNumber = 0; // This property is not updated by this class, but is for storage by calling routines
        private Guid __UniqueID;

        public int order
        {
            get
            {
                return (y * 1000) + x;
            }
        }

        public PlacedClue()
        {
            __UniqueID = Guid.NewGuid();
        }
        public Guid UniqueID
        {
            get
            {
                return __UniqueID;
            }
        }
        public String placeDescriptor
        {
            get
            {
                return __placeDescriptor;
            }
            set
            {
                __placeDescriptor = value;
            }
        }
        public int placeNumber
        {
            get
            {
                return __placeNumber;
            }
            set
            {
                __placeNumber = value;
            }
        }
        public String clueText
        {
            get
            {
                return clue.ToString();
            }
        }

        public void CopyTo(PlacedClue pcDest)
        {
            pcDest.orientation = orientation;
            pcDest.__UniqueID = __UniqueID;
            pcDest.__placeDescriptor = __placeDescriptor;
            pcDest.x = x;
            pcDest.y = y;
            clue.CopyTo(pcDest.clue);
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
    }
}
