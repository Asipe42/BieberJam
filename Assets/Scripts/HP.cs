using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public static HP instance;

    [SerializeField] Slider hpGauge;
    [SerializeField] float hpMax;
    [SerializeField] float decreaseValue;
    [SerializeField] int branchDamageValue = 20;
    [SerializeField] int treeRecoverValue = 5;

    float hp;

    private void Awake()
    {
        instance = this;

        hp = hpMax;
    }

    public void RecoverHP(int value)
    {
        hp = Mathf.Clamp(hp + value, 0, hpMax);
    }
    public void RecoverAllHP()
    {
        hp = hpMax;
    }
    public void DamageHP(int value)
    {
        hp = Mathf.Clamp(hp - value, 0, hpMax);
    }
    public void RecoverByTree()
    {
        RecoverHP(treeRecoverValue);
    }
    public void DamageByBranch()
    {
        DamageHP(branchDamageValue);
    }

    void DecreaseHP()
    {
        hp -= Time.deltaTime * decreaseValue;
    }

    private void Update()
    {
        if (!GameManager.onStart)
            return;

        DecreaseHP();
        SyncGauge();
    }

    void SyncGauge()
    {
        hpGauge.value = hp;
    }

    public void SpeedUp()
    {
        decreaseValue += 5;
    }
}
