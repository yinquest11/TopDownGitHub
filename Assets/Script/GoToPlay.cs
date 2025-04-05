using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToPlay : MonoBehaviour
{
    private string _sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sceneName = "SampleScene";
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
