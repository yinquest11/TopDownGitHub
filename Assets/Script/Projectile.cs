using UnityEngine;

public class Projectile : MonoBehaviour
{

    //就是这个东西被Weapon Insantiate 的 public GameObject Projectile;
    public float Speed = 10f;
    public Cooldown LifeTime;
    private Rigidbody2D _rigidbody;
    
    

    void Start()
    {
        
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        //我们要从脚本操控Rigidbody2D就要在脚本把这个组件GetComponent，Collider2D没有在脚本里使用到就不用GetComponent
        _rigidbody = GetComponent<Rigidbody2D>();

        //增加相对力，以什么方向和模式
        //ForceMode.Force = 一开始为这个刚体施加一个力，接着刚体会按照物理定律不断往前，如果有阻力到最后可能会停下来
        _rigidbody.AddRelativeForce(new Vector2(0, Speed),ForceMode2D.Force);

        //倒计时结倒数
        LifeTime.StartCooldown();

        
    }

    // Update is called once per frame
    void Update()
    {
        //倒计时没有完都退出Update()函数
        if (LifeTime.CurrentProgress != Cooldown.Progress.Finished)
            return;

        //倒计时一结束，代表倒计时完了，启用Die()函数
        Die();
    }

    //倒计时结束之后被启用的函数
    void Die()
    {
        //暂停协程，并且结束协程，进行重置
        LifeTime.StopCooldown();

        //销毁当前gameObject
        Destroy(gameObject);
    }
}
