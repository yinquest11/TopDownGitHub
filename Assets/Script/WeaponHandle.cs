using UnityEngine;

public class WeaponHandle : MonoBehaviour
{

    public Weapon CurrentWeapon;
    //性能开销较小，并且比较直观，专注在Trasform组件
    public Transform GunPosition;
    protected bool _tryShoot;
    
    
    //这个Handle每一帧会执行这两个东西
    protected virtual void Update()
    {
        HandleInput();
        HandleWeapon();
    }

    //HandleInput函数()
    protected virtual void HandleInput()
    {

    }

    //HandleWeapon函数()
    protected virtual void HandleWeapon()
    {
        //如果当前CurrentWeapon没有或者GunPosition也没有，那么退出函数
        if(CurrentWeapon == null || GunPosition == null)
            return;

        //把CurrentWepon的位置和旋转全部向GunPosition的对齐
        //就算CurrentWeapon的位置乱移动，到最后也可以吸附回来GunPosition的位置
        CurrentWeapon.transform.position = GunPosition.position;
        CurrentWeapon.transform.rotation = GunPosition.rotation;

        //如果检测到_tryShoot是true
        //那么调用当前武器的Shoot方法
        if (_tryShoot)
        {
            CurrentWeapon.Shoot();
        }
        //没有要尝试射击，那么调用StopShoot()函数
        else
        {
            CurrentWeapon.StopShoot();
        }

        

    }

    public void Start()
    {
        //忘记拖拽赋值CurrentWeapon可以自动帮忙找
        if(CurrentWeapon == null)
        {
            CurrentWeapon = GetComponentInChildren<Weapon>();
        }
    }

    //EquipWeapon()函数
    //可以传入一个GameObject类型
    public void EquipWeapon(GameObject equipWeapon)
    {
        //如果没有传到进来就退出函数
        if (equipWeapon == null)
            return;
        //如果当前有Weapon也退出
        if (CurrentWeapon != null)
            return;

        //生成传进来的这个游戏对象，并且使用GunPosition的位置
        GameObject _weaponGO = GameObject.Instantiate(equipWeapon, GunPosition);

        //拿到 _weaponGO 上的Weapon组件
        //我的武器上都应该要有Weapon脚本的组件
        Weapon weapon = _weaponGO.GetComponent<Weapon>();

        //如果没拿到，就返回
        if (weapon == null)
            return;

        //拿到的话，CurrentWeapon就变成这个weapon了
        CurrentWeapon = weapon;
    }


}
