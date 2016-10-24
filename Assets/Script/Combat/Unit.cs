using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
    
    [SerializeField]
    private combatType primary_type;
    [SerializeField]
    private combatType secondary_type;

    private combatStance stance;

    // implementation undecided
    private float physical_attack;
    private float physical_defense;
    private float special_attack;
    private float special_defense;

    public float speed;
    
    private IList<combatMove> moves;
    private IList<combatAbility> abilities;
    private IList<combatBuff> buffs;

    [SerializeField]
    private float max_health;
    private float _health;
    public float health {
        get { return this._health; }
        set {
            this._health += value;
            this._health = Mathf.Max(Mathf.Min(this.max_health, this._health), 0); // restricts health between max_health and 0

            if (this._health == 0) { 
                this.die();
            }
        }
    }

    [SerializeField]
    private float max_stamina;
    private float _stamina;
    public float stamina
    {
        get { return this._stamina; }
        set
        {
            this._stamina += value;
            this._stamina = Mathf.Max(Mathf.Min(this._stamina, this.max_stamina), 0);
        }
    }

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

    /*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    */


}
