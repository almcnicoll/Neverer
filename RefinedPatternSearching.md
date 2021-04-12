# Refined Pattern Searching
## Overview
The Neverer uses dictionaries to determine possible solutions for as-yet unpopulated clues. Where other clues intersect a given clue, this limits the possible solutions for that clue.

However, there is a further step that's really helpful to consider. Say, for example, that you have a 7-letter clue with intersections that give the pattern ??Z?N?A. The Neverer will calculate the possible words that can fit - there's three in the SOWPODS dictionary: _fazenda_, _gazania_ and _zizania_.

Now imagine that the first letter of that clue is also the first letter of another clue, which is 10 letters and has the pattern ???????D?R. That pattern has 94 matches in SOWPODS, but from the previous clue we know that its first letter, while not yet entered, can only be _F_, _G_ or _Z_. Limiting our ten-letter word to start with one of those three letters gives only 7 SOWPODS matches: _fairleader_, 
_flatlander_, 	_freeholder_, 	_freeloader_, 	_fundholder_, 	_gasconader_, 	and _goaltender_.

The aim of this documentation is to describe the iterative process by which the Neverer calculates these limitations.

In this file and the program code, the term _Refined Pattern_ and the prefix _Refined_ before other words indicate functionality relating to this interdependency analysis.

## The Stack
Each clue that is created or changed is added to the stack of clues to evaluate. When it has been evaluated, the program determines whether this latest evaluation has created any new constraints that affect:
- The possible solutions for this clue
- The letter choices at intersections with other clues

If the letter choices at a given intersection are altered, the intersecting clue is added to the stack. Clues only appear once in the stack at any given time.

The original clue is then removed from the stack (but might be re-added later by changes to an intersecting clue)

## Methodology
Each `PlacedClue` object contains a `Dictionary<int,HashSet<char>>` called `ExternalConstraints` and another `Dictionary<int,HashSet<char>>` called `RefinedLetters`, which is calculated from `ExternalConstraints` and the clue's letter pattern.
The `int` corresponds to the position in the string, while the `HashSet<char>`
corresponds to the possible letters at that position.

The steps to evaluate a clue are as follows:
1. Populate the clue's `RefinedLetters` if not already populated.
2. Take a copy of `RefinedLetters` for comparison later.
3. Test all dictionaries for possible solutions.
4. Refresh `ExternalConstraints` and `RefinedLetters` based on the possible responses.
5. Compare `RefinedLetters` to the copy from **Step 2**. In any position where there are changes, search for intersecting clues.
6. In the event of a letter-choice change coinciding with a clue intersection, call `LimitToLetters` on that clue. This will make a change to `ExternalConstraints` in that clue and add it to the stack if not already present.

**TODO**: this system has a flaw - `LimitToLetters` would most naturally output the intersection of 
the existing `RefinedLetters` and the new constraints from the calling clue.
However, this does not adequately cover the scenario in which the 
constraints of the calling clue are _loosened_ (e.g. `[rt]` => `[drst]`) or
changed in a non-overlapping manner (e.g. `[rt]` => `[aio]`). **This points to  a need for 
a more complex constraint system, in which we store the external constraints on a clue separately 
from its own internal constraints (letters entered manually, dictionary possibilities)**
**Think that the addition of ExternalConstraints dictionary will fix this.**

## No-win situations
Sometimes it is inevitable that within the 
constraints of the supplied dictionaries, 
the limitations that clues place on each 
other will result in an impossible match. 
There is as yet no determined method of 
signalling this to the user. It could be 
that this system overrides the current 
colouring system on the PlacedClue display.

IT is also unclear whether stack processing
should continue in the event of hitting a _no-win_
situation. The most logical action would seem to be to
suspend stack processing until a further clue change
is made.