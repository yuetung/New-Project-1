using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveListController : CombatUIController {

    [SerializeField]
    private GameObject moveButtonPrefab;

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
            
            foreach (CombatMove m in value.moves)
            {
                button = (GameObject)Instantiate(moveButtonPrefab);
                button.transform.SetParent(this.transform);
                button.GetComponent<MoveButtonController>().move = m;

                this.childButtons.Add(button);
            }
        }
    }

    public void selectMove(CombatMove m)
    {
        this.battle.playerInput.move = m;
    }
}
