using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float Speed = 10f;
    public Cooldown LifeTime;
    private Rigidbody2D _rigidbody;
    private Weapon Weaponn;
    private DamageOnTouch _damageOnTouch;

    public bool DieOnTouch = true;

    void Start()
    {
        
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddRelativeForce(new Vector2(0, Speed),ForceMode2D.Force);

        _damageOnTouch = GetComponent<DamageOnTouch>();

       //  subscribe to OnHit
        if (_damageOnTouch != null && DieOnTouch)
            _damageOnTouch.OnHit += Die;





        LifeTime.StartCooldown();

        
    }

    void Update()
    {
        if (GameObject.FindWithTag("Player").GetComponentInChildren<Weapon>())
        {
            Weaponn = GameObject.FindWithTag("Player").GetComponentInChildren<Weapon>();
        }

        //Only use Die function when LifeTime is calculate finish
        if (LifeTime.CurrentProgress != Cooldown.Progress.Finished)
            return;

        Die();
    }

    void Die()
    {
        //Debug.Log(gameObject.name);

        //if (gameObject.name != "BulletSniper")
        //    return;
            

        //unsubscribe
        if (_damageOnTouch != null && DieOnTouch) 
            _damageOnTouch.OnHit -= Die;

        LifeTime.StopCooldown();
        Destroy(gameObject);
    }
}
