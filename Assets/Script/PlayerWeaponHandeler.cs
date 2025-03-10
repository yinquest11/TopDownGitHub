using NUnit.Framework;
using UnityEngine;

public class PlayerWeaponHandeler : WeaponHandle
{
    //相当于玩家的手指，决定要不要开枪 （充分体会面向对象编程）
    protected override void HandleInput()
    {

       

        //检查在Input Manager所指定的虚拟按钮的名称有没有被按下，有的话返回就true，否则false


            if (Input.GetButton("Fire1"))

            {


                _tryShoot = true;

            }




       
            

       
        

        if (Input.GetButtonUp("Fire1"))
            _tryShoot = false;
    }
}
