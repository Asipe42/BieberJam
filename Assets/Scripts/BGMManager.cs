using UnityEngine;
using DG.Tweening;

public enum EBGMType
{
    Default,
    Fever
}

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    EBGMType currentBGMType;

    [SerializeField] AudioSource defaultBGMChannel;
    [SerializeField] AudioSource chainSawChannel;
    [SerializeField] AudioSource feverBGMChannel;

    private void Awake()
    {
        instance = this;

        currentBGMType = EBGMType.Default;
    }

    public void DefaultToFever()
    {
        ChangeBGM(EBGMType.Fever);
    }

    [ContextMenu("Fever To Default")]
    public void FeverToDefault()
    {
        ChangeBGM(EBGMType.Default);
    }
    public void StartChainsaw()
    {
        chainSawChannel.Play();
        chainSawChannel.DOFade(1, 1f);
    }
    public void StopChainsaw()
    {
        chainSawChannel.DOFade(1, 1f).OnComplete(()=> chainSawChannel.Stop());
    }

    public void ChangeBGM(EBGMType type)
    {
        currentBGMType = type;

        ChangeChannel();
    }

    void ChangeChannel()
    {
        var sequence = DOTween.Sequence();

        switch (currentBGMType)
        {
            case EBGMType.Default:
                defaultBGMChannel.DOFade(1, 0.5f);
                feverBGMChannel.DOFade(0, 0.5f);
                chainSawChannel.Stop();
                break;
            case EBGMType.Fever:
                defaultBGMChannel.DOFade(0, 0.5f);
                feverBGMChannel.DOFade(1, 0.5f);
                chainSawChannel.Play();
                break;
        }
    }
}
