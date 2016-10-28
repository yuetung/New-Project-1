﻿using UnityEngine;
using UnityEngine.Events;
using System;

public class combatManager : MonoBehaviour {

    [HideInInspector]
    public UnityEvent onActiveUnit;

    private Unit _activeUnit;
    public Unit activeUnit
    {
        get { return _activeUnit; }
        set
        {
            this._activeUnit = value;
            
            this.onActiveUnit.Invoke();
        }
    }


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
            child.health = child.maxHealth;
            child.stamina = child.maxStamina;
        }

        this.startNextUnitTurn();
    }

    public void combatEnd()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Unit>().triggerEvent(combatEvent.COMBAT_END);
        }

    }

    public bool combatVictoryCondition()
    {
        return false;
    }

    public void startNextUnitTurn()
    {
        if (this.combatVictoryCondition()) {

            this.combatEnd();

        } else {

            Unit first = this.getTurnOrder()[0];
            Unit u;

            while (first.delay > 0)
            // check if first unit can take its turn
            // if it is able to, exit
            // else reduce all unit's delay by speed and try again
            {
                foreach (Transform child in transform)
                {
                    u = child.GetComponent<Unit>();
                    u.tick();
                }

                first = this.getTurnOrder()[0];
            }

            this.activeUnit = first;
            activeUnit.startTurn();

        }
    }

    // Turn Sequencer

    public Unit[] getTurnOrder()
    {
        Unit[] rt = this.getAllUnits();

        // in-place sort
        Array.Sort(rt, delegate (Unit u1, Unit u2) { return (int)(u1.delay - u2.delay); });

        return rt;
    }

    // Monobehavior API

    void Start()
    {
        this.combatStart();
    }
}
