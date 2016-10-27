using UnityEngine;
using System.Collections;

public class moveListController : combatUiController {

    [SerializeField]
    private GameObject moveButtonPrefab;

    private Unit _current;
    override public Unit current
    {
        get { return this._current; }
        set
        {
            this._current = value;

            GameObject button;

            foreach (combatMove m in value.moves)
            {
                button = (GameObject)Instantiate(moveButtonPrefab);
                button.transform.SetParent(this.transform);
                button.GetComponent<moveButtonController>().move = m;
            }
        }
    }
}
