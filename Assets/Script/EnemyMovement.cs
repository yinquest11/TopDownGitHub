using UnityEngine;

public class EnemyMovement : Movemet
{
    //一个用来储存找到Player的GameObject
    private GameObject playerr;
    


    
    //override BaseClass的Start()
    protected override void Start()
    {
        //先通过base.使用BaseClass的Start()
        base.Start();

        //让一开始设定的GameObject = 当一个"Player" Tag 的GameObject 被找到
        //在场景中查找所有带有指定标签（这里是 "Player"）的游戏对象，并返回第一个匹配到的游戏对象。如果没有找到任何匹配的游戏对象，则返回 null。
        playerr = GameObject.FindWithTag("Player");

        //BaseClass的public variable
        
        

    }

    

    //覆盖BaseClass的HandleInput
    protected override void HandleInput()
    {

        //自我保护
        if (playerr == null)
            return;

        // A - B get B to A，谁被减就是谁作为出发点寻找谁。怪物的x是出发寻找玩家的x，怪物的y是出发寻找玩家的y
        //先创建一个new的Vector2，并且储存在_inputDirection
        //.normalized会把这个方向的长度变成1，但方向信息不变，归一化，后续进行操作的时候更方便
        //.normalized的意思代表不论你最后的长度，我都变成1，并且我只保留最简单的方向信息

        _inputDirection = new Vector2 (playerr.transform.position.x - transform.position.x, playerr.transform.position.y - transform.position.y).normalized;


        //Debug类的静态方法
        //线将会从第一个参数的位置开始绘制
        //第二个参数是线的方向和长度
        //第三个参数是线的颜色
        Debug.Log(Acceleration);
        Debug.DrawRay(transform.position,_inputDirection,Color.yellow);
    }
    
    //override BaseClass 的HandleRotation函数
    protected override void HandleRotation()
    {
        //使用原本BaseClass的HandleRotation()函数
        base.HandleRotation();
    }

   
    




}
