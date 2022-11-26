using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    Image image;
    [SerializeField] GameObject logo;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject creditButton;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void EndCredit()
    {
        if (!CreditButton.endFade)
            return;

        image.DOFade(0, 1f).OnComplete(() =>
        {
            logo.SetActive(true);
            startButton.SetActive(true);
            creditButton.SetActive(true);

            CreditButton.endFade = false;
            gameObject.SetActive(false);
        });
    }
}
