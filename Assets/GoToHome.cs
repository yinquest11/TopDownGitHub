using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToHome : MonoBehaviour
{
    private string _menuName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _menuName = "Menu";
    }

    // Update is called once per frame
    public void BackToMenu()
    {
        SceneManager.LoadScene(_menuName);
    }
}
