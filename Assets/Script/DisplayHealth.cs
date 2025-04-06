using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class DisplayHealth : MonoBehaviour
{
    private float _health;
    private Text _healthText;
    public GameObject BloodPanel;
    private Coroutine _coroutine;

    private void Start()
    {
        _healthText = GetComponent<Text>();
    }
    private void Update()
    {
        if (GameObject.FindWithTag("Player") != null) 
        {
            _health = GameObject.FindWithTag("Player").GetComponent<Health>()._currentHealth;
        }
        else
        {
            _health = 0;
        }
        

        if (_healthText)
        {
            _healthText.text = "Health: " + _health.ToString("F2");
        }
    }

    public void DisplayBlood()
    {
        if (BloodPanel == null) { Debug.LogWarning(gameObject.name + ": BloodPanel is missing something."); return; }
        _coroutine = StartCoroutine(CloseBlood());
    }

    IEnumerator CloseBlood()
    {
        

        BloodPanel.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        BloodPanel.SetActive(false);
    }
}
