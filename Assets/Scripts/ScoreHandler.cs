using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private GameObject gameFinish;
    [SerializeField] private TextMeshProUGUI scoreText;
 
    [SerializeField] private int scoreP1 = 0;
    [SerializeField] private int scoreP2 = 0;
    
    [SerializeField] private int pointsToWin = 11;
    
    #endregion

    #region Methods
    private void OnWin(Player winner)
    {
        gameFinish.GetComponentInChildren<TextMeshProUGUI>().text = 
            $"Game Over,\n {(winner == Player.Player2 ? "Left" : "Right")} Paddle Wins";
        
        Debug.Log(gameFinish.GetComponentInChildren<TextMeshProUGUI>().text);
        
        gameFinish.SetActive(true);
    }
    
    public void UpdateScore()
    {
        (scoreP1, scoreP2) = (0,0);
        scoreText.text = scoreP2 + " - " + scoreP1;
    }

    public void UpdateScore(Player player)
    {
        if (player == Player.Player1)
        {
            scoreText.text = ++scoreP2 + " - " + scoreP1;
            if (scoreP2 == pointsToWin)
            {
                BallBehavior.Play = false;
                OnWin(Player.Player2);
            }
        }

        else
        {
            scoreText.text = scoreP2 + " - " + ++scoreP1;
            if (scoreP1 == pointsToWin)
            {
                BallBehavior.Play = false;
                OnWin(Player.Player1);
            }
        }
        
        Debug.Log($"Score : {scoreText.text}");
    }
    #endregion
}
