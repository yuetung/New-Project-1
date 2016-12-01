using UnityEngine;
using UnityEditor;

using System;

/*

    moves are created by composition of various effects that run in sequence.

*/

public enum CombatMoveType
{
    PHYSICAL,
    MAGICAL
}


public class CombatMove : ScriptableObject {

    const string COMBAT_ASSETS_PATH = "Assets/Resources/CombatMove/";
    
    [SerializeField] public int delayCost;
    [SerializeField] public int staminaCost;
    [SerializeField] public int potency;
    [SerializeField] public CombatMoveType type;
    [SerializeField]
    public CombatInstruction[] instructions = new CombatInstruction[0];
    // Constructor

    void OnEnable()
    {
        hideFlags = HideFlags.DontSave;
    }

    public static CombatMove make(string moveName, int delayCost, int staminaCost, int potency, CombatMoveType type)
    {
        string path = COMBAT_ASSETS_PATH + moveName + ".asset";
        CombatMove m = ScriptableObject.CreateInstance <CombatMove>() as CombatMove;

        if (AssetDatabase.LoadAssetAtPath<CombatMove>(path) == null)
        {
            AssetDatabase.CreateAsset(m, path);
        } else
        {
            // if name already exists, then try again with a different name
            return make(moveName + "1", delayCost, staminaCost, potency, type);
        }
        
        AssetDatabase.SaveAssets();
        
        m.delayCost = delayCost;
        m.staminaCost = staminaCost;
        m.potency = potency;
        m.type = type;

        return m;
    }

    [MenuItem("Assets/Create/CombatMove")]
    public static CombatMove default_make()
    {
        return CombatMove.make("New Combat Move", 0, 0, 0, CombatMoveType.PHYSICAL);
    }

}