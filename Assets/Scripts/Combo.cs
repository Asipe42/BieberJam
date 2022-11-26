using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Combo : MonoBehaviour
{
    public static Combo instance;

    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] Image ui;

    private void Awake()
    {
        instance = this;
    }

    public static int comboCount;

    public void SyncCombo(bool state = false) // true => miss, false = default
    {
        if (state)
        {
            ui.gameObject.SetActive(true);
            comboText.gameObject.SetActive(false);

            comboCount = 0;
            comboText.text = "MISS";
        }
        else
        {
            if (!comboText.gameObject.activeSelf)
            {
                ui.gameObject.SetActive(false);
                comboText.gameObject.SetActive(true);
            }

            comboCount++;
            comboText.text = "x" + comboCount;
        }
    }
}
