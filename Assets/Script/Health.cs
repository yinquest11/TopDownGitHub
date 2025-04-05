using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    private DisplayHealth _displayHealth;

    public float MaxHealth = 10f;

    public float _currentHealth = 10f;

    public GameObject EnemyDeathSoundObject;

    public GameObject DropRandomWeapon;

    private ScoreScript _scoremanager;

    public int ScoreW;

    private SpawnerC _spawner;

    private SpriteRenderer _spriteRenderer;

    private Coroutine _turnRedCoroutine;

    public bool isBoss;

    public bool isPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _displayHealth = GameObject.Find("Displayhealth").GetComponent<DisplayHealth>();
        ScoreW = 1;
        _scoremanager = GameObject.FindWithTag("ScoreManager").GetComponent<ScoreScript>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (GameObject.FindWithTag("Spawner").GetComponent<SpawnerC>() != null)
        {
            _spawner = GameObject.FindWithTag("Spawner").GetComponent<SpawnerC>();
        }
        
        Initialization();
    }

    private void Initialization()
    {
        _currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damage, GameObject source)
    {

        GameObject.Instantiate(EnemyDeathSoundObject, transform.position, transform.rotation);
        

        _currentHealth -= damage;

        if(isPlayer == false)
        {
            _turnRedCoroutine = StartCoroutine(TurnRed());
        }

        if (isPlayer == true)
        {
            _displayHealth.DisplayBlood();
        }








        if (_currentHealth <= 0)
        {
            _currentHealth = 0;

            _scoremanager.AddScore(ScoreW);

            int _dropChance = Random.Range(0, 10);
            

            if (_dropChance == 9)
            {
                if (DropRandomWeapon == null) { Debug.LogWarning(gameObject.name + ":  is missing something."); return; }
                

                Instantiate(DropRandomWeapon,transform.position,transform.rotation);

            }
            


            Die();
        }

        
    }
    public void Die()
    {
        if (isBoss == true)
        {
            
            if (_spawner == null) { Debug.LogWarning(gameObject.name + ": _spawner is missing something."); return; }
            _spawner._bossFight = false;
            
        }
        
        if(isPlayer == false)
        {
            StopCoroutine(_turnRedCoroutine);
        }
        
        Destroy(this.gameObject);
        
        
    }


    IEnumerator TurnRed()
    {
        _spriteRenderer.color = new Color(0.9433962f, 0.5844309f, 0.5844309f);

        yield return new WaitForSeconds(0.1f);

        _spriteRenderer.color = Color.white;


    }



}
