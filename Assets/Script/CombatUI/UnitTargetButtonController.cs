using UnityEngine;
using UnityEngine.UI;

public class UnitTargetButtonController : MonoBehaviour {

    [SerializeField]
    private Text unitName;

    private Unit _target;
    public Unit target
    {
        get { return this._target; }
        set
        {
            this._target = value;

            unitName.text = value.name;
        }
    }

    public void OnClick()
    {
        this.transform.GetComponentInParent<UnitTargetListController>().selectTarget(this.target);
    }
}
