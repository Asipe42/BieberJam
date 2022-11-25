using UnityEngine;
using UnityEngine.UI;

public class Fever : MonoBehaviour
{
    public static Fever instance;

    [SerializeField] Slider feverGauge;
    [SerializeField] float FeverMax;

    float fever;

    bool onFever;

    private void Awake()
    {
        instance = this;

        feverGauge.maxValue = FeverMax;
    }

    public void PlusFeverValue()
    {
        if (onFever)
            return;

        fever++;
    }

    private void Update()
    {
        CheckFever();
        SyncFever();
    }

    void CheckFever()
    {
        if (fever > FeverMax)
        {
            FeverLogic();
        }
    }

    void FeverLogic()
    {
        /*
         1.�ִϸ��̼� ���
         2. ȿ���� ���
         3. ī�޶� ��
         4. �Է� ���� -> ����
         */
    }

    void SyncFever()
    {
        feverGauge.value = fever;
    }
}
