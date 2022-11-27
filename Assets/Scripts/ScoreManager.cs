using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _highScoreText;

    // Property
    public int score
    {
        get { return TreeManager.instance.killCount; }
    }

    // Value
    private int _highScore;

    // Event
    private void Awake()
    {
        this._highScore = 0;
    }

    // Function - Public
    public void ChangeHighScore(int highScore)
    {
        _highScore = Mathf.Max(_highScore, highScore);
        _highScoreText.text = "HIGHSCORE:" + _highScore;
    }
}
