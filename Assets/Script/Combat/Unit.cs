using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
    
    [SerializeField]
    private combatType primary_type;
    [SerializeField]
    private combatType secondary_type;

    private combatStance stance;

    // implementation undecided
    public int physicalAttack;
    public int physicalDefense;
    public int specialAttack;
    public int specialDefense;

    public int speed;
    public int delay;
    
    public IList<combatMove> moves;
    public IList<combatAbility> abilities;
    public IList<combatBuff> buffs;

    [SerializeField]
    public float maxHealth;
    private float _health;
    public float health {
        get { return this._health; }
        set {
            this._health += value;
            this._health = Mathf.Max(Mathf.Min(this.maxHealth, this._health), 0); // restricts health between max_health and 0

            if (this._health == 0) { 
                this.die();
            }
        }
    }

    [SerializeField]
    public float maxStamina;
    private float _stamina;
    public float stamina
    {
        get { return this._stamina; }
        set
        {
            this._stamina += value;
            this._stamina = Mathf.Max(Mathf.Min(this._stamina, this.maxStamina), 0);
        }
    }

    // Helper Method

    public combatManager battle
    {
        get { return this.GetComponentInParent<combatManager>(); }
    }

    // Functionality

    public void die()
    {
        this.triggerEvent(combatEvent.ON_DEATH);
        // kills this unit, to be implemented
    }

    public void triggerEvent(combatEvent e)
    {
        // triggers combat events
        // API for combatEffects to call
    }

    public void tick()
    {
        this.triggerEvent(combatEvent.SPEED_TICK);
        this.delay -= this.delay;
    }

    public void startTurn()
    {
        this.triggerEvent(combatEvent.TURN_START);
    }

    public void endTurn()
    {
        this.triggerEvent(combatEvent.TURN_END);
        battle.startNextUnitTurn();
    }
}
