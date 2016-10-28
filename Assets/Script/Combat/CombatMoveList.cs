using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatMoveList : ScriptableObject
{

    [SerializeField]
    public List<CombatMove> moveList;

    public static CombatMoveList init()
    {
        CombatMoveList mList = ScriptableObject.CreateInstance<CombatMoveList>() as CombatMoveList;
        mList.moveList = new List<CombatMove>();
        return mList;
    }
}