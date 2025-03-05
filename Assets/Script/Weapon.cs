using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{

    
    //设置枚举器
    public enum FireModes
    {
        Auto, // = 0
        SingleFire, // = 1
        BrustFire // = 2
    }

    //在Inspector访问枚举器，更方便随时更换想要的举
    public FireModes FireMode;


    public  float Spread = 0f;
    public int BrustFireAmount = 3;

    public float BrustFireInterval = 0.1f;

    public int ProjectileCount = 1;

    public GameObject Projectile;

    //GameObject数组，可以随时扩充数量
    public GameObject[] Feedbacks;
    public GameObject[] ReloadFeedbacks;

    public Transform SpawnPosition;

    //利用Cooldown当成一个类型，在Inspector通过ShootInterval和ReloadCooldown 访问Cooldown类的public变量
    //也可以在当前脚本使用通过ReloadCooldown访问Cooldown的变量
    public Cooldown ShootInterval;
    public Cooldown ReloadCooldown;



    private float timer =0f;
    private bool canShoot = true;
    private bool fireRest = true;

    

    public int MaxBulletCount = 10;

    //属性没办法在Inspector看到
    //可以把 CurrentBulletCount 借给外面而保护cuurentBullet不会被修改
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
        //一开始的时候刷新currentBullet去MaxBulletCount
        currentBulletCount = MaxBulletCount;

    }

    private void Update()
    {
        //每帧调用这两个函数
        //Weapon每帧都会检查需不需要冷却
        UpdateReloadCooldown();
        UpdateShootCooldown();
        
    }

    //定义私有函数
    private void UpdateReloadCooldown() 
    {
        
        //如果Cooldown类型的当前脚本的ReloadCooldown变量引用Cooldown的CurrentProgress 不是 Cooldown类里的枚举器的Progress的FInished成员，就结束UpdateReloadCoolDown方法
        if (ReloadCooldown.CurrentProgress != Cooldown.Progress.Finished)
            return;

        //如果当前类的ReloadCooldown引用的CurrrentProgress 是 Cooldown类里的枚举器的Progress的Finished成员
        if (ReloadCooldown.CurrentProgress == Cooldown.Progress.Finished)
        {
            //Finished了代表协程结束了并且CurrentProgress = Progress.Finished;执行
            currentBulletCount = MaxBulletCount;
        }

        //装弹完成ReloadCooldown.CurrentProgress设置为Ready
        ReloadCooldown.CurrentProgress = Cooldown.Progress.Ready;


    }
    private void UpdateShootCooldown()
    {
        //如果Cooldown类型的当前脚本的ShootInterval变量引用Cooldown的CurrentProgress 不是 Cooldown类里的枚举器的Progress的FInished成员，就结束UpdateShootCoolDown方法
        if (ShootInterval.CurrentProgress != Cooldown.Progress.Finished)
            return;

        //Finished才能进到来，然后调整为Ready，冷却完毕，下一颗子弹的射击
        ShootInterval.CurrentProgress = Cooldown.Progress.Ready;
    }


    //定义Shoot()函数
    public void Shoot()
    {
        //自我保护
        //如果GameObject Projectile 和 Transform 组件为空 就结束函数
        if (Projectile == null || SpawnPosition == null)
            return;

        //只要ReloadCooldown的IsOnCoolDown为true 或者 ReloadCooldown的CurrentProgress 不是 Cooldown类的枚举器里的Ready成员

        //只要你还在Cooldown 或者 你的CurrentProgress 不是Ready的话 我都不执行Shoot()函数
        if (ReloadCooldown.IsOnCoolDown || ReloadCooldown.CurrentProgress != Cooldown.Progress.Ready) 
            return;
        


        //看FireMode变量是哪个enum的成员，就使用哪个模式的函数
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

    //Auto模式的开火函数
    void AutoFireShoot()
    {

        //如果canShoot的值是false，就进入if, return, 结束AutoFireShoot()函数
        if (!canShoot)
            return;
        //如果ShootInterval引用的CurrentProgress 不是 Cooldown类里的Progress枚举的Ready成员， 就return，结束AutoFireShoot()函数
        //只有Ready才可以射击，不是Ready的话代表不能射击
        if (ShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;

        //使用ShootProjectile()函数
        ShootProjectile();
        //发射一次弹丸之后，子弹总数减少1       
        currentBulletCount--;

        //利用ShootInterval使用Cooldown类的StartCooldown()函数
        ShootInterval.StartCooldown();

        //ShootInterval之后检查是否还有剩余的currentBullet数量可以发射弹丸
        //如果 currentBullet <= 0 同时 枪械也不是正在CoolDown; 如果已经是IsOnCoolDown就不用开新的StartCooldown
        if (currentBulletCount <= 0 && !ReloadCooldown.IsOnCoolDown)
        {
            //那么就使用RealoadCooldown开始引用Cooldown类的StartCooldown
            ReloadCooldown.StartCooldown();
        }

    }

    //发射弹（一次过射几发）丸函数
    void ShootProjectile()
    {
        //看有几发弹丸，调用ShootProjectile()的时候一次过就发射几发子弹
        //ProjectileCount数量代表一次过发射几发子弹 （循环一次代表一粒子弹）
        //弹丸数量多少，ShootProjectileBurust被调用的时候就一次过就会Instantiate多少次GameObejct
        for (int i = 0; i < ProjectileCount; ++i)
        {
            //创建一个Random float
            //可以在Inspector中调整Spread值
            //由于 -Spread 和 Spread 是float， 那么都是可取值
            float randomRot = Random.Range(-Spread, Spread);

            //使用GameObject类的静态方法Instantiate
            //不同的旋转被生成之后，被直线加速，就变成了扩散成一个范围伤害的现象
            GameObject.Instantiate(Projectile, SpawnPosition.position, SpawnPosition.rotation * Quaternion.Euler(0, 0, randomRot));

            //所以如果这个for循环 循环两次的话一次过会Instantiate两个Projectile，也就是弹丸
        }
    }

    void BurstFireShoot()
    {

        if (!canShoot)
            return;
        if (ShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;

        ShootProjectileBurst();

        currentBulletCount--;

        ShootInterval.StartCooldown();

        if (currentBulletCount <= 0 && !ReloadCooldown.IsOnCoolDown)
        {

            ReloadCooldown.StartCooldown();
        }
    }

    void ShootProjectileBurst()
    {

        for (int i = 0; i < ProjectileCount; ++i)
        {

            float randomRot = Random.Range(-Spread, Spread);



            GameObject.Instantiate(Projectile, SpawnPosition.position, SpawnPosition.rotation * Quaternion.Euler(0, 0, randomRot));
            

        }
    }

    void SingleFireShoot()
    {


    }
    

    //StopShoot()函数
    public void StopShoot()
    {
        //如果当前FireMode是FireModes枚举器的Auto成员，则退出函数

        if (FireMode == FireModes.Auto)
            return;

        //不是Auto的话，fireRest = ture
        fireRest = true;
    }
}
