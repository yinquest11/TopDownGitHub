using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    //OnTrigger2D时你要Instantiate的GameObject
    public GameObject[] PickupFeedbacks;

    //表示游戏对象层级的变量，可以指定要操作的是哪些层级的游戏对象，从而对特定层级的对象进行筛选和处理。
    public LayerMask TargetLayerMask;
    
    void Start()
    {
        
        
    }

    
    void Update()
    {
        
    }

    //当挂载此脚本的游戏对象被Trigger的时候
    //col是触发这个OnTrigger2D的GameObject的Collider2D组件
    private void OnTriggerEnter2D(Collider2D col)
    {
        //检查一个游戏对象的层级是否在指定的 LayerMask 范围内，如果不在则直接返回，不继续执行后续代码
        //gameObject.layer 会返回一个32位的int，代表该游戏对象所属的层级编号，0到31 （包括）
        //<< 是左移运算符，将数字 1 向左移动 col.gameObject.layer 位，3位 (Player Layer是3)
        //1 的二进制表示是 0000 0000 0000 0000 0000 0000 0000 0001
        //1 << 3         0000 0000 0000 0000 0000 0000 0000 1000
        //计算机自动识别是3号层

        //TargetLayerMask.value 本质上是一个用于表示层级集合的位掩码，会返回一个int，这个int背后的32位2进制就是我们的层级信息
        //.layer可以生成.value需要的数据

        //按位与运算符 & 会对两个int对应二进制位进行逻辑与运算。逻辑与运算的规则是：只有当两个对应位都为 1 时，结果的该位才为 1；否则为 0
        //>0代表.layer包含在.value中，然后返回结果true
        //但是我们！，所以变成false，不执行if里面的return。
        //false!变true的话执行return

        //查看捡的人是不是在 TargerLayerMask里面
        if (                 !(      (TargetLayerMask.value & (1 << col.gameObject.layer)   ) > 0      )                  )
           
            return;
        //只有结果为！ture的话，最后才会来到这边
        //调用这个会被ovverride的PickedUP()函数并调用触发这个gameObject的游戏对象上的Collider2D作为参数
        PickedUp(col);
        
        //利用PickupFeedbacks GameObejct数组去Instantiate它们想要的feedback
        foreach(var feedback in PickupFeedbacks)
        {
            //以当前的位置和角度生成
            GameObject.Instantiate(feedback, transform.position, transform.rotation);
        }

        //生成之后摧毁自己这个等着被Player捡起来的gameObject
        Destroy(gameObject);

    }

    protected virtual void PickedUp(Collider2D col)
    {

    }


}
