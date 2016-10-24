using System;

/*

    An effect represents a single action that happens in the game.

    Various things may use effects:

    Buffs
    Abilities
    Moves

    They use a chain of effects.

*/

public abstract class combatEffect {
    
    // applies the relevant effect
    public abstract void execute(Unit self, Unit other);

}

public class combatEffectDamage : combatEffect
{

    private combatType dmgtype;
    private combatStance stancetype;

    private float? amount;
    private Func<Unit, Unit, float> var_amount;

    combatEffectDamage(float amount, combatType type, combatStance stancetype) 
        : this(type, stancetype)
    {
        this.amount = amount;
    }

    combatEffectDamage(Func<Unit, Unit, float> expr, combatType type, combatStance stancetype) 
        : this(type, stancetype)
    {
        this.var_amount = expr;
    }

    combatEffectDamage(combatType type, combatStance stancetype)
    {
        this.dmgtype = type;
        this.stancetype = stancetype;
    }

    override
    public void execute(Unit self, Unit other)
    {
        self.triggerEvent(combatEvent.BEFORE_DAMAGE);
        other.triggerEvent(combatEvent.BEFORE_DAMAGED);

        float damage_dealt = 0;

        if (this.amount == null) {
            damage_dealt = this.var_amount(self, other);
        } else {
            damage_dealt = (float) this.amount;
        }

        // check for type

        // check for stance

        // modify by atk stat

        // modify by defense stat

        other.health -= damage_dealt;

        self.triggerEvent(combatEvent.AFTER_DAMAGE);
        other.triggerEvent(combatEvent.AFTER_DAMAGED);
    }

}