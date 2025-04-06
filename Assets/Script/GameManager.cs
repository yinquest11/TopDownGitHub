using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] OnWhenDie;
    public GameObject[] OffWhenDie;
    public GameObject[] OnWhenWin;
    public GameObject[] OffWhenWin;
    private Health[] _allHealth; 
    private Health _health;
    private BgmManager _bgmManager;
    
    private bool Winn;
    float _myHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Winn = false;
        _health = GameObject.FindWithTag("Player").GetComponent<Health>();
        _bgmManager = GameObject.FindWithTag("BgmManager").GetComponent<BgmManager>();
        

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

    public void Win()
    {
        Debug.Log("Win the game");
        

        foreach (var _g in OnWhenWin)
        {
            _g.SetActive(true);
        }

        _bgmManager.WinSound();


        foreach (var _g in OffWhenWin)
        {
            _g.SetActive(false);
        }

        _allHealth = FindObjectsByType<Health>(FindObjectsSortMode.None);

        foreach(Health _g in _allHealth)
        {
            if(_g.CompareTag("Player") == false && _g.isBoss == false)
            {
                _g.gameObject.SetActive(false);
            }
            
        }
        
                                                                                        
    }
}
