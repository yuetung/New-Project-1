using System.Collections.Generic;

/*

    moves are created by composition of various effects that run in sequence.

*/

public class combatMove {

    private combatEffect[] moveEffects;
    public string name
    {
        get
        {
            foreach (string key in combatMove.lib.Keys)
            {
                if (combatMove.lib[key] == this)
                {
                    return key;
                }
            }

            return "Unknown Move";
        }
    }

    public static readonly Dictionary<string, combatMove> lib = new Dictionary<string, combatMove> {
        { "Ember", new combatMove(new combatEffect[] { combatEffect.lib["10dmgfirestanding"] } ) }
    };

    private combatMove(combatEffect[] moveEffects)
    {
        this.moveEffects = moveEffects;
    }

    public void execute(Unit self, Unit other)
    {
        for (int i = 0; i < this.moveEffects.Length; i++)
        {
            moveEffects[i].execute(self, other);
        }
    }

}
