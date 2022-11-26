using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private void Update()
    {
        UpdateScoreText();
    }

    public void ResetScore()
    {
        TreeManager.instance.killCount = 0;
    }

    public void UpdateScoreText()
    {
        scoreText.text = "x " + TreeManager.instance.killCount;
    }
}
