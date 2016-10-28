using System.Collections.Generic;

public class combatBuff {

    private Dictionary<combatEvent, CombatEffect> effects;

    combatBuff(Dictionary<combatEvent, CombatEffect> effects)
    {
        this.effects = effects;
    }

    public void execute(Unit self, Unit other, combatEvent ev)
    {
        this.effects[ev].execute(self, other);
    }

}
