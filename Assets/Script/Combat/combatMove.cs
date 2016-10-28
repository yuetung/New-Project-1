using System;
using UnityEngine;

/*

    moves are created by composition of various effects that run in sequence.

*/

[Serializable]
public class combatMove : ScriptableObject {

    private combatEffect[] moveEffects;
    public string moveName;

    public static combatMove init(string moveName, combatEffect[] moveEffects)
    {
        combatMove m = ScriptableObject.CreateInstance <combatMove>() as combatMove;

        m.moveEffects = moveEffects;
        m.moveName = moveName;

        return m;
    }

    public void execute(Unit self, Unit other)
    {
        for (int i = 0; i < this.moveEffects.Length; i++)
        {
            moveEffects[i].execute(self, other);
        }
    }

}