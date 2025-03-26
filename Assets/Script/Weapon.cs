using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{

    
    //设置枚举器
    public enum FireModes
    {
        Auto, // = 0        
        BrustFire, // = 1
        ShotGun, // = 2
        Sniper // = 3

    }

    public FireModes FireMode;


    public float Spread = 0f;
    public float BrustFireInterval = 0.07f;
    public float FireRate = 0.07f;
    public float ShotgunSpread = 60f;
    private float timer = 0f;
    private float _brustFireInterval;

    public int BrustFireAmount = 3;
    public int MaxBulletCount = 10;
    public int ProjectileCount = 1;
    private int _currentBrustCount;
    
    public GameObject Projectile;
    public GameObject[] Feedbacks;
    public GameObject[] ReloadFeedbacks;
    public Transform SpawnPosition;

    //Use Cooldown script as a properties
    public Cooldown ShootInterval;
    public Cooldown ReloadCooldown;
    
    private bool canShoot = true;
    private bool fireRest = true;

    public AudioClip SingleClip;
    public AudioClip BrustClip;
    public AudioClip AutoClip;
    public AudioClip ShotgunClip;
    public AudioClip GunReloadClip;
    private AudioSource _source;

    public AudioClip SniperClip;
    



    //Share properties to other script
    public int CurrentBulletCount
    {   
        get
        {
            return currentBulletCount;
        }
    }
  
    protected int currentBulletCount =10;


    private void Start()
    {
        currentBulletCount = MaxBulletCount;
        _source = GetComponent<AudioSource>();
        _brustFireInterval = BrustFireInterval;
        _currentBrustCount = BrustFireAmount;
    }

    private void Update()
    {
        UpdateReloadCooldown();
        UpdateShootCooldown();

        //------------------------------
        if (_brustFireInterval > 0)
        {
            _brustFireInterval -= Time.deltaTime;

        }
    }

    private void UpdateReloadCooldown() 
    {

        if (ReloadCooldown.CurrentProgress != Cooldown.Progress.Finished)
            return;

        if (ReloadCooldown.CurrentProgress == Cooldown.Progress.Finished)
        {
            currentBulletCount = MaxBulletCount;
        }

        ReloadCooldown.CurrentProgress = Cooldown.Progress.Ready;

    }

    private void UpdateShootCooldown()
    {
        if (ShootInterval.CurrentProgress != Cooldown.Progress.Finished)
            return;

        ShootInterval.CurrentProgress = Cooldown.Progress.Ready;
    }

    public void Shoot()
    {
        if (Projectile == null || SpawnPosition == null)
            return;

        if (ReloadCooldown.IsOnCoolDown || ReloadCooldown.CurrentProgress != Cooldown.Progress.Ready) 
            return;

        if (Input.GetButtonDown("Dig") || Input.GetButton("Dig"))
            return;

        
        //Switch different fire mode base on the enum
        switch (FireMode)
        {
            case FireModes.Auto:
                {
                    AutoFireShoot();
                    
                    break;
                }
            case FireModes.ShotGun:
                {
                    ShotGunShoot();
                    break;
                }
            case FireModes.BrustFire:
                {
                    BurstFireShoot();
                    break;
                }
            case FireModes.Sniper:
                {
                    
                    
                    break;
                }
        }

    }

   public void SniperFireShoot()
   {

           if (SniperClip == null) { Debug.LogWarning(gameObject.name + ":  is missing something."); return; }

           if (!canShoot)
                return;
           if (ShootInterval.CurrentProgress != Cooldown.Progress.Ready)
                return;

        

        


            
            SniperShootProjectile();

            

        



        
            currentBulletCount--;
        
            ShootInterval.StartCooldown();

            if (currentBulletCount <= 0 && !ReloadCooldown.IsOnCoolDown)
            {
                ReloadCooldown.StartCooldown();
            
            }

 }
    void SniperShootProjectile()
    {
        //Shoot all projectile
        for (int i = 0; i < ProjectileCount; ++i)
        {


            float randomRot = Random.Range(-Spread, Spread);

            //Insantiate prpjectile with same position bur different rotation
            GameObject.Instantiate(Projectile, SpawnPosition.position, SpawnPosition.rotation * Quaternion.Euler(0, 0, randomRot));

            _source.PlayOneShot(SniperClip);

        }
    }
    void AutoFireShoot()
    {

        if (!canShoot)
            return;
        if (ShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;
        
        if ( AutoClip == null) { Debug.LogWarning(gameObject.name + ":  is missing something."); return; }

        if ( GunReloadClip == null) { Debug.LogWarning(gameObject.name + ":  is missing something."); return; }

        ShootProjectile();
        currentBulletCount--;

        ShootInterval.StartCooldown();

        if (currentBulletCount <= 0 && !ReloadCooldown.IsOnCoolDown)
        {
            ReloadCooldown.StartCooldown();
            _source.PlayOneShot(GunReloadClip,1.5f);
        }

    }
    void ShootProjectile()
    {
        //Shoot all projectile
        for (int i = 0; i < ProjectileCount; ++i)
        {
            
            
            float randomRot = Random.Range(-Spread, Spread);

            //Insantiate prpjectile with same position bur different rotation
            GameObject.Instantiate(Projectile, SpawnPosition.position, SpawnPosition.rotation * Quaternion.Euler(0, 0, randomRot));

            

            _source.PlayOneShot(AutoClip);

        }
    }


    void BurstFireShoot()
    {

        if (!canShoot)
            return;
        if (ShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;

        if (AutoClip == null) { Debug.LogWarning(gameObject.name + ":  is missing something."); return; }

        if (GunReloadClip == null) { Debug.LogWarning(gameObject.name + ":  is missing something."); return; }

        StartCoroutine("BrustShoot");
        currentBulletCount--;

        _source.PlayOneShot(BrustClip, 1.5f);

        

        ShootInterval.StartCooldown();

        if (currentBulletCount <= 0 && !ReloadCooldown.IsOnCoolDown)
        {
            ReloadCooldown.StartCooldown();
            _source.PlayOneShot(GunReloadClip, 1.5f);
        }

        
    }

    //Use coroutine for Brust fire mode
    IEnumerator BrustShoot()
    {
        for (int i = 0; i < BrustFireAmount; ++i)
        {

            float randomRot = Random.Range(-Spread, Spread);


            GameObject.Instantiate(Projectile, SpawnPosition.position, SpawnPosition.rotation * Quaternion.Euler(0, 0, randomRot));
            


            //wait 0.07 second (Fire rate) and continue to do the for loop
            yield return new WaitForSeconds(FireRate);

            
        }
    }


    void ShotGunShoot()
    {
        if (!canShoot)
            return;
        if (ShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;
        if (ShotgunClip == null) { Debug.LogWarning(gameObject.name + ":  is missing something."); return; }

        if (GunReloadClip == null) { Debug.LogWarning(gameObject.name + ":  is missing something."); return; }


        //Use another fuction to shoot
        ShootProjectileShotGun();
        

        _source.PlayOneShot(ShotgunClip);

        currentBulletCount--;
        ShootInterval.StartCooldown();

        if (currentBulletCount <= 0 && !ReloadCooldown.IsOnCoolDown)
        {
            ReloadCooldown.StartCooldown();

            _source.PlayOneShot(GunReloadClip, 1.5f);
        }
    }
    void ShootProjectileShotGun()
    {

        float halfAngle = ShotgunSpread / 2;

        //     if (ProjectileCount > 1)
        //     {
        //         angleStep = ShotgunSpread / (ProjectileCount - 1);
        //     }
        //     else
        //     {
        //         angleStep = 0;
        //     }
        float angleStep = ProjectileCount > 1 ? ShotgunSpread / (ProjectileCount - 1) : 0f; 

       
        //Shoot projectile with range 
        for (int i = 0; i < ProjectileCount; ++i)
        {

            //directy start form the most left side and add i angleStep
            float angle = -halfAngle + (i * angleStep);

            //Save the each angle
            Quaternion spreadRotation = Quaternion.Euler(0, 0, angle);

            GameObject.Instantiate(Projectile, SpawnPosition.position, SpawnPosition.rotation * spreadRotation);
            
        }
    }


    public void StopShoot()
    {
        if (FireMode == FireModes.Auto)
            return;

        fireRest = true;
    }

    
}
