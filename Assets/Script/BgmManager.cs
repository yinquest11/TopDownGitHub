using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public AudioClip clip;
    public AudioClip _bossClip;
    public AudioClip _endClip;
    public AudioClip WinClip;
    private AudioSource _source;
    private SpawnerC _spawner;
    private bool Winn;
    private Health _health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Winn = false;
        if (GameObject.FindWithTag("Spawner")!= null)
        {
            _spawner = GameObject.FindWithTag("Spawner").GetComponent<SpawnerC>();
        }
        
        _source = GetComponent<AudioSource>();

        if (clip == null) { Debug.LogWarning(gameObject.name + ": clip is missing something."); return; }
        _source.clip = clip;
        _source.Play();
        if ( _bossClip == null) { Debug.LogWarning(gameObject.name + ": _bossCLip is missing something."); return; }
        _health = GameObject.FindWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Winn == true)
            return;

        if(_health == null)
        {
            if ( _endClip== null) { Debug.LogWarning(gameObject.name + ": _endClip is missing something."); return; }

            if (_source.clip != _endClip)
            {
                _source.clip = _endClip;
                _source.Play();
                
            }
            return;
        }

        if (_spawner == null) { Debug.LogWarning(gameObject.name + ": _spawner is missing something."); return; }

        if(_spawner._bossFight == true)
        {
            if (_source.clip != _bossClip )
            {
                
                _source.clip = _bossClip;
                _source.volume = 1f;
                _source.Play(); 
            }
        }

        if (_spawner == null)
            return;
        if(_spawner._bossFight == false )
        {
            if(_source.clip != clip)
            {
                _source.clip = clip;
                _source.Play();
            }
        }

        
    }

    public void WinSound()
    {

        Winn = true;
        if (WinClip == null) { Debug.LogWarning(gameObject.name + ": WinClip is missing something."); return; }

        if (_source.clip != WinClip)
        {
            
            _source.clip = WinClip;
            Debug.Log("Hi");
            _source.volume = 1f;
            _source.Play();
        }



    }


}
