using UnityEngine;

public class WeaponPickUp : PickUp
{
    public GameObject Weapon;

    //当等待被人捡起来的GameObject是一把枪时，枪专有的PickedUp()方法
    protected override void PickedUp(Collider2D col)
    {
        //如果要准备要掉落的东西没被赋到，弹出Missing Weapon因为不知道要掉落什么
        if (Weapon == null)
        {
            Debug.LogWarning("Missing Weapon");
            return;
        }
        
        //使用BaseClass里的PickedUp()方法
        base.PickedUp(col);

        //我们从在Trigger到这个gameObejct的游戏对象身上GetComponent<WeaponHandle>()，相等于col.gameObject.GetComponent<WeaponHandle>();
        WeaponHandle weaponHandler = col.GetComponent<WeaponHandle>();


        //如果找不到，退出函数，（自我保护）
        if (weaponHandler == null)
            return;


        //用我们准备给当前脚本掉落的Weapon填入EquipWeapon函数
        weaponHandler.EquipWeapon(Weapon);

        
    }
}
