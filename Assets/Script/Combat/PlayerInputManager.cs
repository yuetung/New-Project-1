using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInputManager : MonoBehaviour {

    private CombatMove _move;
    public CombatMove move
    {
        get { return this._move; }
        set {
            this._move = value;

            this.executeMove();
        }
    }

    private Unit _target;
    public Unit target
    {
        get { return this._target; }
        set
        {
            this._target = value;

            this.executeMove();
        }
    }

    public CombatManager battle
    {
        get { return this.transform.GetComponent<CombatManager>();  }
    }

    public Unit self
    {
        get { return this.battle.activeUnit; }
    }

    private void executeMove()
    {
        if (this.move != null && this.target != null)
        {
            this.battle.execute(this.move, this.self, this.target);

            this.move = null;
            this.target = null;
        }
    }
}
