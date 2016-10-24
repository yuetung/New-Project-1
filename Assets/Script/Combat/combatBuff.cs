using System.Collections.Generic;

public class combatBuff {

    private Dictionary<combatEvent, combatEffect> effects;

    combatBuff(Dictionary<combatEvent, combatEffect> effects)
    {
        this.effects = effects;
    }

    public void execute(Unit self, Unit other, combatEvent ev)
    {
        this.effects[ev].execute(self, other);
    }

}
