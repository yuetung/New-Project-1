using UnityEngine;
using UnityEngine.UI;

public class unitInfoController : MonoBehaviour {

    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text currentHealthText;
    [SerializeField]
    private Text maxHealthText;
    [SerializeField]
    private Text currentStaminaText;
    [SerializeField]
    private Text maxStaminaText;

    private Unit _current;
    public Unit current
    {
        get { return this._current; }
        set
        {
            this._current = value;

            nameText.text = value.transform.name;
            currentHealthText.text = value.health.ToString();
            currentStaminaText.text = value.stamina.ToString();
            maxHealthText.text = value.maxHealth.ToString();
            maxStaminaText.text = value.maxStamina.ToString();
        }
    }
}
