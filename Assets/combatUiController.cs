using UnityEngine;
using System;

public abstract class combatUiController : MonoBehaviour {

    [SerializeField]
    private combatManager battle;
    
    public abstract Unit current { get; set; }

    void Awake()
    {
        battle.onActiveUnit.AddListener(() => { this.current = battle.activeUnit; });
    }
}
