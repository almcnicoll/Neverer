using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;

namespace Neverer.UtilityClass
{
    [Flags]
    public enum ClueStatus
    {
        Unknown = 0,
        AnswerComplete = 1,
        QuestionComplete = 2,
        HasMatches = 4,
        HasFewMatches = 8,

        /* Legacy options (+16) */
        NoMatchingWord = 16,
        MatchingWordNoQuestion = 20,
        MatchingWordWithQuestion = 22,
        NoMatchingWordComplete = 19,
        FewMatchingWords = 28,
        Complete = 23

    }

}
