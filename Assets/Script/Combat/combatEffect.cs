using System;
using System.Collections.Generic;

/*

    An effect represents a single action that happens in the game.

    Various things may use effects:

    Buffs
    Abilities
    Moves

    They use a chain of effects.

*/

public abstract class CombatEffect {
    
    // applies the relevant effect
    public abstract void execute(Unit self, Unit other);

    public static readonly Dictionary<string, CombatEffect> lib = new Dictionary<string, CombatEffect>
    {
        { "10dmgfirestanding", new combatEffectDamage(1000, combatType.FIRE, combatStance.STANDING) }
    };

}

public class combatEffectDamage : CombatEffect
{

    private combatType dmgtype;
    private combatStance stancetype;

    private float? amount;
    private Func<Unit, Unit, float> var_amount;

    public combatEffectDamage(float amount, combatType type, combatStance stancetype) 
        : this(type, stancetype)
    {
        this.amount = amount;
    }

    public combatEffectDamage(Func<Unit, Unit, float> expr, combatType type, combatStance stancetype) 
        : this(type, stancetype)
    {
        this.var_amount = expr;
    }

    public combatEffectDamage(combatType type, combatStance stancetype)
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

        // modify by physical attack and defense
        damage_dealt += self.physicalAttack;
        damage_dealt -= other.physicalDefense;

        // check for type

        // check for stance

        other.health -= damage_dealt;

        self.triggerEvent(combatEvent.AFTER_DAMAGE);
        other.triggerEvent(combatEvent.AFTER_DAMAGED);
    }

}