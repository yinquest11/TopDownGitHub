using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] OnWhenDie;
    public GameObject[] OffWhenDie;
    private Health _health;
    float _myHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _health = GameObject.FindWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_health != null)
        {
            _myHealth = _health._currentHealth;
        }
        else
        {
            Cursor.visible = true;

            foreach (var _g in OffWhenDie)
            {
                _g.SetActive(false);
            }


            foreach (var _g in OnWhenDie)
            {
                _g.SetActive(true);
            }
        }
        


        
    }
}
