using UnityEngine;
using UnityEngine.UI;

public class moveButtonController : MonoBehaviour {

    [SerializeField]
    private Text moveName;

    private combatMove _move;
    public combatMove move
    {
        get { return this._move; }
        set
        {
            this._move = value;

            moveName.text = value.moveName;
        }
    }
}
