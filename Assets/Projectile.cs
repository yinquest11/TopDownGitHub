using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float Speed = 10f;
    public Cooldown LifeTime;

    private Rigidbody2D rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.AddRelativeForce(new Vector2(0, Speed));

        LifeTime.StartCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        if (LifeTime.CurrentProgress != Cooldown.Progress.Finished)
            return;

        Die();
    }

    void Die()
    {
        LifeTime.StopCooldown();
        Destroy(gameObject);
    }
}
