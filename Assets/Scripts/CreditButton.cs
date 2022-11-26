using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CreditButton : MonoBehaviour
{
    [SerializeField] GameObject credit;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject logo;
    [SerializeField] GameObject creditButton;

    public static bool endFade;

    public void ShowCredit()
    {
        credit.SetActive(true);
        startButton.SetActive(false);
        logo.SetActive(false);
        creditButton.SetActive(false);

        credit.GetComponent<Image>().DOFade(1, 1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            endFade = true;
        });
    }
}
