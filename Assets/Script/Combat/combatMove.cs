using System.Collections.Generic;

/*

    moves are created by composition of various effects that run in sequence.

*/

public class combatMove {

    private IList<combatEffect> moveEffects;
    public int delay;

    combatMove(IList<combatEffect> moveEffects, int delay)
    {
        this.moveEffects = moveEffects;
        this.delay = delay;
    }

    public void execute(Unit self, Unit other)
    {
        self.triggerEvent(combatEvent.BEFORE_MOVE);

        self.delay += this.delay;
        foreach (combatEffect e in this.moveEffects)
        {
            e.execute(self, other);
        }

        self.triggerEvent(combatEvent.AFTER_MOVE);
    }

}
