using UnityEngine;

using UnityEngine.UI;
public class ScoreScript : MonoBehaviour
{
    public int CurrentScore;

    public Text Scoretext;

    private void Start()
    {
        CurrentScore = 50;
    }
    public void AddScore(int _score)
    {
        CurrentScore -= _score;

        if (Scoretext)
        {
            Scoretext.text = "Enemy: " + CurrentScore.ToString();
        }

        if (CurrentScore <= 0)
        {
            Scoretext.text = "Boss Fight !!";
        }
    }

   

}
