using UnityEngine;


//继承自Movement类
public class PlayerMovemet : Movemet
{



    //override Movement的HandleInput
    protected override void HandleInput()
    {
        //赋值_inputDirection 然后传给 Movement的HandleMovemnet();里的 targetVelocity = new Vector2(_inputDirection.x * Acceleration, _inputDirection.y * Acceleration);
        //GetAxis会返回Input Manager所register的虚拟按键的-1 0 1 （negative 没碰 positive）
        //Input.GetAxis会慢慢过度0到1
        _inputDirection = new Vector2 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
    }

    //覆盖Movement的HandleRotation()
    protected override void HandleRotation()
    {
        //GetAxisRaw是检测当你按下，就会返回一个值，正面输入positive（轴的正向）是1，负向输入negative（轴的负向）是-1. ！=0代表有在按，就会有1或者-1的值，来判断是否是在按按钮（输入轴类按钮，比如上下左右）
        //Input.GetAxisRaw没有平滑处理，直接-1 0 1

        //if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        //{   
        //有碰的时候才执行旋转逻辑
        //用base.关键字使用BaseClass的方法

        if (_weaponHandler == null || _weaponHandler.CurrentWeapon == null)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                base.HandleRotation();
            }
            
            return;
        }


        Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, transform.position.z);

        Vector2 direction = mousePos - transform.position;

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
        //}
        
    }

}
