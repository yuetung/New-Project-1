using UnityEngine;

using System.Collections.Generic;

public class UnitTargetListController : CombatUIController {


    [SerializeField]
    private GameObject targetButtonPrefab;

    private List<GameObject> childButtons = new List<GameObject>();

    private Unit _current;
    override public Unit current
    {
        get { return this._current; }
        set
        {
            this._current = value;

            foreach (GameObject o in this.childButtons)
            {
                Destroy(o);
            }

            GameObject button;

            foreach (Unit u in battle.getAllUnits())
            {
                button = (GameObject)Instantiate(targetButtonPrefab);
                button.transform.SetParent(this.transform);
                button.GetComponent<UnitTargetButtonController>().target = u;

                this.childButtons.Add(button);
            }
        }
    }

    public void selectTarget(Unit u)
    {
        this.battle.playerInput.target = u;
    }
}
