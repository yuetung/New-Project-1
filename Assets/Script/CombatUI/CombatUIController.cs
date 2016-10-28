using UnityEngine;

public abstract class CombatUIController : MonoBehaviour {

    [SerializeField]
    protected CombatManager battle;
    
    public abstract Unit current { get; set; }

    void Awake()
    {
        battle.onActiveUnit.AddListener(() => { this.current = battle.activeUnit; });
    }
}
