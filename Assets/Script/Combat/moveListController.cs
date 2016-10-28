using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class moveListController : combatUiController {

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

            foreach (combatMove m in value.moves)
            {
                button = (GameObject)Instantiate(moveButtonPrefab);
                button.transform.SetParent(this.transform);
                button.GetComponent<moveButtonController>().move = m;

                this.childButtons.Add(button);
            }
        }
    }


    // API to bind to button callback
    public void activeUnitEndTurn()
    {
        this.current.endTurn();
    }
}
