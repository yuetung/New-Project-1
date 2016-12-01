using UnityEngine;
using UnityEngine.UI;

public class MoveButtonController : MonoBehaviour {

    [SerializeField]
    private Text moveName;

    private CombatMove _move;
    public CombatMove move
    {
        get { return this._move; }
        set
        {
            this._move = value;

            GetComponentInChildren<Text>().text = value.name;
        }
    }

    public void OnClick()
    {
        this.transform.GetComponentInParent<MoveListController>().selectMove(this.move);
    }
}
