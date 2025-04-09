using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class DisplayHealth : MonoBehaviour
{
    private float _health;
    private Text _healthText;
    public Image _healthBar;
    public GameObject BloodPanel;
    private Coroutine _coroutine;
    private Health _healthClass;
    private float _remaningRatio;


    private void Start()
    {
        _healthText = GetComponent<Text>();
        _healthClass = GameObject.FindWithTag("Player").GetComponent<Health>();
    }
    private void Update()
    {

        if ( _healthClass == null) { Debug.LogWarning(gameObject.name + ":  is missing something."); return; }
        _remaningRatio = _healthClass._currentHealth / _healthClass.MaxHealth;

        

        if (GameObject.FindWithTag("Player") != null) 
        {
            _health = GameObject.FindWithTag("Player").GetComponent<Health>()._currentHealth;
        }
        else
        {
            _health = 0;
        }
        

        if (_healthBar != null)
        {
            _healthBar.fillAmount = _remaningRatio;
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
