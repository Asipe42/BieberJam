using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public static HP instance;

    [SerializeField] Slider hpGauge;
    [SerializeField] float hpMax;
    [SerializeField] float decreaseValue;

    float hp;

    private void Awake()
    {
        instance = this;

        hp = hpMax;
    }

    public void RecoverHP()
    {
        hp = hpMax;
    }

    void DecreaseHP()
    {
        hp -= Time.deltaTime * decreaseValue;
    }

    private void Update()
    {
        DecreaseHP();
        SyncGauge();
    }

    void SyncGauge()
    {
        hpGauge.value = hp;
    }
}
