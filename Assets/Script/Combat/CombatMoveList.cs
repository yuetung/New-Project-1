using System.Collections.Generic;
using UnityEngine;

public class CombatMoveList : ScriptableObject
{
    public List<combatMove> moveList;

    public static CombatMoveList init()
    {
        CombatMoveList mList = ScriptableObject.CreateInstance<CombatMoveList>() as CombatMoveList;
        mList.moveList = new List<combatMove>();
        return mList;
    }
}