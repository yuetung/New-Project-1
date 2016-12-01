using System;
using System.Collections.Generic;
using UnityEngine;

/*

    An effect represents a single action that happens in the game.

    Various things may use effects:

    Buffs
    Abilities
    Moves

    They use a chain of effects.

*/

[Serializable]
public class CombatInstructionList: List<CombatInstruction> { }

[Serializable]
public class CombatInstruction
{
    // Encodes a single CombatEffect to be executed

    public CombatEffect.effect effect;
    public float potency = 0;
    public CombatBuff buff;
    public string stat = "";
    public CombatType type;
    public CombatStance stance;

}

public class CombatEffectHandler
{
    // Each Move, Buff and Ability will contain a List of instructions to execute Combat Effects
    // Each instruction encodes a mutation on a CombatEffectHandler
    // After all instructions are executed, the CombatEffectHandler is fired to run all effects in sequence.

    private List<CombatInstruction> instructions;

    public CombatEffectHandler()
    {
        this.instructions = new List<CombatInstruction>();
    }

    public CombatEffectHandler(CombatInstruction[] inst)
    {
        this.instructions = new List<CombatInstruction>(inst);
    }

    public void add(IEnumerable<CombatInstruction> instructions)
    {
        this.instructions.AddRange(instructions);
    }

    public void execute(CombatManager battle, Unit self, Unit other, float potency)
    {

        float movePotency;

        foreach (CombatInstruction inst in this.instructions)
        {

            movePotency = potency + inst.potency;

            switch (inst.effect)
            {
                case CombatEffect.effect.ADD_BUFF:
                    CombatEffect.addBuff(battle, self, other, movePotency, inst.buff);
                    break;
                case CombatEffect.effect.REMOVE_BUFF:
                    CombatEffect.removeBuff(battle, self, other, movePotency, inst.buff);
                    break;
                case CombatEffect.effect.DAMAGE:
                    CombatEffect.damage(battle, self, other, movePotency, inst.type, inst.stance);
                    break;
            }

        }

    }
}

public static class CombatEffect {
    // Library for all effects

    public enum effect {
        DAMAGE,
        HEAL,

        REGENERATE,
        DRAIN,

        DELAY,
        HASTEN,

        ADD_BUFF,
        REMOVE_BUFF,

        ALTER_STAT
    }

    public static void addBuff(CombatManager battle, Unit self, Unit other, float potency, CombatBuff buff)
    {
        battle.triggerEvent(CombatEvent.BEFORE_BUFF, self);
        battle.triggerEvent(CombatEvent.BEFORE_BUFFED, other);

        CombatBuff b = UnityEngine.Object.Instantiate(buff) as CombatBuff;
        b.potency = potency;

        other.buffs.Add(b);

        battle.triggerEvent(CombatEvent.AFTER_BUFF, self);
        battle.triggerEvent(CombatEvent.AFTER_BUFFED, other);

    }

    public static void removeBuff(CombatManager battle, Unit self, Unit other, float potency, CombatBuff buff)
    {

    }

    public static void damage(CombatManager battle, Unit self, Unit other, float potency, CombatType type, CombatStance stance)
    {
        battle.triggerEvent(CombatEvent.BEFORE_DAMAGE, self);
        battle.triggerEvent(CombatEvent.BEFORE_DAMAGED, other);

        float damage_dealt = potency;

        // modify by physical attack and defense
        damage_dealt += self.physicalAttack;
        damage_dealt -= other.physicalDefense;

        // check for type

        // check for stance

        other.health -= damage_dealt;

        battle.triggerEvent(CombatEvent.AFTER_DAMAGE, self);
        battle.triggerEvent(CombatEvent.AFTER_DAMAGED, other);
    }

}