using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{

    public enum FireModes
    {
        Auto, // = 0
        SingleFire, // = 1
        BrustFire // = 2
    }

    public FireModes FireMode;
    public  float Spread = 0f;
    public int BrustFireAmount = 3;

    public float BrustFireInterval = 0.1f;

    public int ProjectileCount = 1;

    public GameObject Projectile;

    public GameObject[] Feedbacks;
    public GameObject[] ReloadFeedbacks;

    public Transform SpawnPosition;
    public Cooldown ShootInterval;




    private float timer =0f;
    private bool canShoot = true;
    private bool fireRest = true;

    public Cooldown ReloadCooldown;
    public int MaxBulletCount = 10;
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

    }

    private void Update()
    {
        UpdateReloadCooldown();
        UpdateShootCooldown();
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
        if (ProjectileCount == null)
            return;


        switch (FireMode)
        {
            case FireModes.Auto:
                {
                    AutoFireShoot();
                    break;
                }
            case FireModes.SingleFire:
                {
                    SingleFireShoot();
                    break;
                }
            case FireModes.BrustFire:
                {
                    BurstFireShoot();
                    break;
                }
        }
    }

    
    void AutoFireShoot()
    {
        if (!canShoot)
            return;
        if (ShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;

        ShootProjectile();

        currentBulletCount--;

        ShootInterval.StartCooldown();

        if(currentBulletCount <= 0 && !ReloadCooldown.IsOnCoolDown)
        {
            ReloadCooldown.StartCooldown();
        }

    }
    void SingleFireShoot()
    {


    }
    void BurstFireShoot()
    {

    }

    void ShootProjectile()
    {
        for(int i = 0; i < ProjectileCount; ++i)
        {
            float randomRot = Random.Range(-Spread, Spread);

            GameObject.Instantiate(Projectile, SpawnPosition.position, SpawnPosition.rotation * Quaternion.Euler(0, 0, randomRot));
        }
    }

    public void StopShoot()
    {
        if (FireMode == FireModes.Auto)
            return;

        fireRest = true;
    }
}
