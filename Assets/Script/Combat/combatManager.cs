using UnityEngine;
using System;
using System.Collections;

public class combatManager : MonoBehaviour {

    // Helper Methods

    public Unit[] getAllUnits()
    {
        return transform.GetComponentsInChildren<Unit>();
    }

    // Combat Start and End

    public void combatStart()
    {
        foreach (Unit child in this.getAllUnits())
        {
            child.triggerEvent(combatEvent.COMBAT_START);
        }
    }

    public void combatEnd()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Unit>().triggerEvent(combatEvent.COMBAT_END);
        }

    }

    // Turn Sequencer

    public Unit[] getTurnOrder()
    {
        Unit[] rt = this.getAllUnits();

        // in-place sort
        Array.Sort(rt, delegate (Unit u1, Unit u2) { return (int)(u1.speed - u2.speed); });

        return rt;
    }
}
