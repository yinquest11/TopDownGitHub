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

        //没有拿到武器的时候照常
        if (_weaponHandler == null || _weaponHandler.CurrentWeapon == null)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                base.HandleRotation();
            }
            
            return;
        }

        //能来到这里代表拿到武器了

        //创建一个Vector3 mousePos用来储存第一个Tag为MainCamera对象上的Camera组件然后使用.ScreenToWorldPoint()函数
        //函数ScreenToWorldPoint会根据我鼠标的坐标返回一个该点在游戏内的坐标的Vector3，但通常我们需要额外去调整z值
        Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

        //这个是我们玩家要对齐的Vector，额外把z调整成和玩家一样的深度。
        //理论上你要一个和你一样深度的方向指向你要去的x y比较好
        //让这个Vector3的深度和我们玩家的一样
        mousePos = new Vector3(mousePos.x, mousePos.y, transform.position.z);


        //一个transform.position 指向 mousePos的 Vector2
        //当你把Vector3储存在Vector2，那么z会自然被忽略

        //Vector2 → Vector3：z 自动初始化为 0。
        //Vector3 → Vector2：z 自动被忽略。
        Vector2 direction = mousePos - transform.position;


        //Atan2计算从x正半轴转到mousePos需要的角度 in radian
        //然后通过Rad2Deg ( 180/3.122 )，从radian变成degree
        //-90f 进行角度偏移，在2D游戏里，角色的默认朝向是朝向上的y轴正半轴，但Mathf.Atan2会从朝向右的x轴正半轴开始
        //2d游戏角色朝向是以90度作为开始，而-90能减回去，变成假设从x轴开始，来匹配Mathf.Atan2也是以x轴开始的角度
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;

        //把这个角度应用给Player
        //2D游戏的话我只要z轴在转，所以把这个角度赋值给z轴，所以z轴会以这个角度去旋转
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
        //}
        
    }

}
