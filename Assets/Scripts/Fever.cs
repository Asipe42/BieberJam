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
         1.애니메이션 재생
         2. 효과음 재생
         3. 카메라 줌
         4. 입력 제한 -> 해제
         */
    }

    void SyncFever()
    {
        feverGauge.value = fever;
    }
}
