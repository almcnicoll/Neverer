using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Neverer.UtilityClass
{
    [Serializable]
    public class Crossword
    {
        private int __width = 15;
        private int __height = 15;
        private int __rotationalSymmetryOrder = 4;
        public int rotationalSymmetryOrder
        {
            get
            {
                return __rotationalSymmetryOrder;
            }
            set
            {
                switch (value)
                {
                    case 1:
                    case 2:
                    case 4:
                        __rotationalSymmetryOrder = value;
                        break;
                    default:
                        throw new Exception(String.Format("Cannot set order of rotational symmetry to {0}. Valid values are 1, 2 & 4.", value));
                }
            }
        }
        public int size
        {
            set
            {
                __width = value;
                __height = value;
            }
        }
        public int width
        {
            get
            {
                return __width;
            }
            set
            {
                __width = value;
                if (rotationalSymmetryOrder == 4)
                {
                    __height = value;
                }
            }
        }
        public int height
        {
            get
            {
                return __height;
            }
            set
            {
                __height = value;
                if (rotationalSymmetryOrder == 4)
                {
                    __width = value;
                }
            }
        }

        public String title { get; set; }

        public int cols
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }
        public int rows
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        public Crossword()
        {
            title = null;
        }

        public List<PlacedClue> placedClues = new List<PlacedClue>();

        [XmlIgnore]
        public Dictionary<KeyValuePair<AD, int>, PlacedClue> sortedClues
        {
            get
            {
                Dictionary<KeyValuePair<AD, int>, PlacedClue> sc = new Dictionary<KeyValuePair<AD, int>, PlacedClue>();

                List<PlacedClue> pcTmp = (from PlacedClue pc in placedClues
                                          orderby pc.order, pc.orientation
                                          select pc).ToList();
                int lastOrder = -1;
                int clueIncrement = 0;
                for (int i = 0; i < pcTmp.Count; i++)
                {
                    PlacedClue pc = pcTmp[i];

                    if (pc.order != lastOrder)
                    {
                        clueIncrement++;
                    }

                    if (pc.orientation > AD.Unset)
                    {
                        KeyValuePair<AD, int> k = new KeyValuePair<AD, int>(pc.orientation, clueIncrement);
                        while (sc.ContainsKey(k))
                        {
                            // This happens when we mirror clues that are on an axis of symmetry - just ignore and increment as often as needed
                            clueIncrement++;
                            k = new KeyValuePair<AD, int>(pc.orientation, clueIncrement);
                        }
                        pc.placeNumber = clueIncrement;
                        sc.Add(k, pc);
                    }

                    lastOrder = pc.order;
                }
                return sc;
            }
        }

        [XmlIgnore]
        public List<PlacedClue> sortedClueList
        {
            get
            {
                //Dictionary<KeyValuePair<AD, int>, PlacedClue> sc = sortedClues;
                return
                    (
                    from KeyValuePair<KeyValuePair<AD, int>, PlacedClue> entry in sortedClues
                    orderby Convert.ToInt32(((KeyValuePair<AD, int>)entry.Key).Key), ((KeyValuePair<AD, int>)entry.Key).Value
                    select (PlacedClue)entry.Value
                    ).ToList();
            }
        }

        private IEnumerable<KeyValuePair<int, int>> clueHeads()
        {
            int r = 0;
            return
                from PlacedClue pc in this.placedClues
                group pc by new { pc.y, pc.x } into g
                orderby g.Key.y, g.Key.x
                let number = r++
                select new KeyValuePair<int, int>(PlacedClue.GetOrder(g.Key.y, g.Key.x), r)
                ;

        }

        public void refreshNumbers()
        {
            Dictionary<int, int> lookup = new Dictionary<int, int>();
            var clues = this.clueHeads();
            foreach (KeyValuePair<int, int> kvp in clues)
            {
                lookup.Add(kvp.Key, kvp.Value);
            }

            foreach (PlacedClue pc in this.placedClues)
            {
                int o = PlacedClue.GetOrder(pc.y, pc.x);
                if (lookup.ContainsKey(o))
                {
                    pc.placeNumber = lookup[o];
                }
            }
        }
    }
}
