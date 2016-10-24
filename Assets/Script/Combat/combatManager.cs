using UnityEngine;
using System;


/*

    Combat Start
        Turn Start
        Turn End
    Combat End

*/

public class combatManager : MonoBehaviour {

    private Unit _active_unit;
    public Unit active_unit
    {
        get { return _active_unit; }
        private set
        {
            this._active_unit = value;
        }
    }

    // Helper Methods

    private Unit[] getAllUnits()
    {
        return transform.GetComponentsInChildren<Unit>();
    }

    // Combat Methods

    private void combatStart()
    {
        foreach (Unit child in this.getAllUnits())
        {
            child.triggerEvent(combatEvent.COMBAT_START);
            child.delay = 0;
        }

        this.assignActiveUnit();
    }

    private bool victoryCondition()
    {
        return false;
    }

    private void combatEnd()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Unit>().triggerEvent(combatEvent.COMBAT_END);
        }

    }

    // Turn Methods

    public void assignActiveUnit()
    {
        if (this.victoryCondition()) {
            this.combatEnd();
        }

        Unit nextunit = this.getTurnOrder()[0];

        // try to get a unit with <= 0 delay
        // if cannot, then do recursion, decreasing all unit's delay by speed

        while (nextunit.delay > 0)
        {
            foreach (Unit u in this.getAllUnits())
            {
                u.triggerEvent(combatEvent.SPEED_TICK);
                u.delay -= u.speed;
            }

            nextunit = this.getTurnOrder()[0];
        }

        this.active_unit = nextunit;
        this.active_unit.startTurn();
    }
        

    private Unit[] getTurnOrder()
    {
        Unit[] rt = this.getAllUnits();

        // in-place sort
        Array.Sort(rt, delegate (Unit u1, Unit u2) { return (int)(u1.delay - u2.delay); });

        return rt;
    }
}
