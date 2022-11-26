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

    // TEST CODE
    [ContextMenu("Default To Fever")]
    void DefaultToFever()
    {
        ChangeBGM(EBGMType.Fever);
    }

    [ContextMenu("Fever To Default")]
    void FeverToDefault()
    {
        ChangeBGM(EBGMType.Default);
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
                sequence.Append(feverBGMChannel.DOFade(0, 0.5f).OnComplete(() => feverBGMChannel.Stop()))
                        .Append(defaultBGMChannel.DOFade(1, 0.5f).OnStart(() => defaultBGMChannel.Play()));
                break;
            case EBGMType.Fever:
                sequence.Append(defaultBGMChannel.DOFade(0, 0.5f).OnComplete(() => defaultBGMChannel.Stop()))
                        .InsertCallback(0.2f, () => chainSawChannel.Play())
                        .Append(feverBGMChannel.DOFade(1, 0.5f).OnStart(() => feverBGMChannel.Play()));
                break;
        }
    }
}
