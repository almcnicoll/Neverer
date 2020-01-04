using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neverer.UtilityClass
{
    public enum ClueStatus
    {
        Unknown = 0,
        NoMatchingWord = 1,
        MatchingWordNoQuestion = 2,
        MatchingWordWithQuestion = 3,
        NoMatchingWordComplete = 4
    }

}
