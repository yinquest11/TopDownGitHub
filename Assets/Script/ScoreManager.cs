using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

using UnityEngine.UI;
public class ScoreScript : MonoBehaviour
{
    public int CurrentScore;

    public Text Scoretext;

    public void AddScore(int _score)
    {
        CurrentScore += _score;

        if (Scoretext)
        {
            Scoretext.text = "Score: " + CurrentScore.ToString();
        }
    }

   

}
