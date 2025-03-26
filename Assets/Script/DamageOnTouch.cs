using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    // radio signal
    public delegate void OnHitSomething();

    public OnHitSomething OnHit;
    private Weapon Weaponn;




    public float MinDamage = 1f;
    public float MaxDamage = 2f;
    public float PushForce = 10f;

    public LayerMask TargetLayerMask;
    public LayerMask IgnoreLayerMask;

    float damageAmount;



    public void Update()
    {
        if (GameObject.FindWithTag("Player").GetComponentInChildren<Weapon>())
        {
            Weaponn = GameObject.FindWithTag("Player").GetComponentInChildren<Weapon>();
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {

        if (((IgnoreLayerMask.value & (1 << col.gameObject.layer)) > 0))
            return;

        


        if (((TargetLayerMask.value & (1 << col.gameObject.layer)) > 0))
            HitDamageable(col);

        else
            HitAnyThing(col);


    }

    private void HitAnyThing(Collider2D col)
    {
        
        OnHit?.Invoke();
    }

    private void HitDamageable(Collider2D col)
    {
        Health targetHealth = col.gameObject.GetComponent<Health>();

        if ( targetHealth == null) { Debug.LogWarning(gameObject.name + ":  is missing something."); return; }

        Rigidbody2D targetRigidbody = col.gameObject.GetComponent<Rigidbody2D>();

        if(targetRigidbody != null)
        {
            targetRigidbody.AddForce((col.transform.position - transform.position).normalized * PushForce);
        }

        TryDamage(targetHealth);



    }

    private void TryDamage(Health targetHealth)
    {
        if(Weaponn.FireMode == Weapon.FireModes.Auto)
        {
            damageAmount = 0.5f;
        }
        else if (Weaponn.FireMode == Weapon.FireModes.BrustFire)
        {
            damageAmount = 1f;
        }
        else if(Weaponn.FireMode == Weapon.FireModes.ShotGun)
        {
            damageAmount = 0.5f;
        }
        else if (Weaponn.FireMode == Weapon.FireModes.Sniper)
        {
            damageAmount = 10f;
        }

        targetHealth.Damage(damageAmount, transform.gameObject);

        
        OnHit?.Invoke();
        
    }
}
