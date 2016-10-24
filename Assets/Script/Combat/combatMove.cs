using System.Collections.Generic;

/*

    moves are created by composition of various effects that run in sequence.

*/

public class combatMove {

    private IList<combatEffect> moveEffects;

    combatMove(IList<combatEffect> moveEffects)
    {
        this.moveEffects = moveEffects;
    }

    public void execute(Unit self, Unit other)
    {
        foreach (combatEffect e in this.moveEffects)
        {
            e.execute(self, other);
        }
    }

}
