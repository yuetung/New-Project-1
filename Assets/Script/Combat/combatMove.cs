using UnityEngine;
using UnityEditor;

using System;

/*

    moves are created by composition of various effects that run in sequence.

*/

[Serializable]
public class CombatMove : ScriptableObject {
    
    public string[] moveEffects;

    public static CombatMove init(string moveName, string[] moveEffects)
    {
        CombatMove m = ScriptableObject.CreateInstance <CombatMove>() as CombatMove;

        if (AssetDatabase.LoadAssetAtPath<CombatMove>("Assets/Database/CombatMove.asset") == null)
        {
            AssetDatabase.CreateAsset(m, "Assets/Database/CombatMove.asset");
        }

        AssetDatabase.AddObjectToAsset(m, "Assets/Database/CombatMove.asset");
        AssetDatabase.SaveAssets();

        m.moveEffects = moveEffects;
        m.name = moveName;

        return m;
    }

    public void execute(Unit self, Unit other)
    {
        foreach (string effectName in this.moveEffects)
        {
            CombatEffect.lib[effectName].execute(self, other);
        }
    }

}