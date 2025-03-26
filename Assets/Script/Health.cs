using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public float MaxHealth = 10f;

    private float _currentHealth = 10f;

    public GameObject EnemyDeathSoundObject;

    public GameObject DropRandomWeapon;

    private ScoreScript _scoremanager;

    public int ScoreW;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScoreW = 1;
        _scoremanager = GameObject.FindWithTag("ScoreManager").GetComponent<ScoreScript>();
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
        
        Destroy(this.gameObject);
        
        
    }


}
