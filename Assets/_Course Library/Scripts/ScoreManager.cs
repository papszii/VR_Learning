using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private string playerName = "Player";
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private int score = 0;

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
        Debug.Log($"{playerName} új pontja: {score}");
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = $"{playerName}\nScore: {score}";
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }
}
