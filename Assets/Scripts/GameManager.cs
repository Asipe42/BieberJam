using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static bool onStart;

    [SerializeField] Canvas HUDCanvas;
    [SerializeField] Canvas TitleCanvas;
    [SerializeField] Canvas GameOverCanvas;
    [SerializeField] Image gameOverPanel;

    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        onStart = true;

        FeverManager.instance.OnStartGame();
    }

    public void GameOver()
    {
        HUDCanvas.gameObject.SetActive(false);
        GameOverCanvas.gameObject.SetActive(true);

        gameOverPanel.color = new Color(0, 0, 0, 0);
        gameOverPanel.DOFade(0.4f, 0.5f);
    }
}
