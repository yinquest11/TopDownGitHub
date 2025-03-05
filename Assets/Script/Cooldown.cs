using System;
using System.Collections;
using UnityEngine;


//专门给类，结构体或枚举可序列化的关键词
//当别的类public引用这个类时，在Inspector面板可以展开进一步编辑Cooldown类的参数
[Serializable]
public class Cooldown // 不继承MonoBehaviour，成为一个单独的类可以方便移植到其他项目，成为某个能够脚本能够自定义的属性
{
    //创造枚举器enum
    //枚举类型重写了 ToString() 方法，会返回枚举成员的名称，Debug.Log 会输出这个名称字符串。
    public enum Progress
    {
        Ready, // == 0
        Started, // == 1
        InProgress, // == 2
        Finished // == 3
    }

    //自定义一个枚举器类型的变量，开始先初始化为枚举器Progress 的 Ready 成员
    public Progress CurrentProgress = Progress.Ready;

    //在Inspector中也可以修改Duration值
    public float Duration = 0.01f;

    //通过CurrentDuration，向外借出currentDuration这个私有变量
    //防止外部代码意外修改currentDuration
    //后续方便添加逻辑
    public float CurrentDuration
    {
        get
        {
            return currentDuration;
        }
    }
    //在当前脚本中都使用private变量进行内部修改
    private float currentDuration = 0f;


    //通过IsOnCoolDown，向外借出isOnCoolDown这个私有变量
    //防止外部代码意外修改isOnCoolDown
    //后续方便添加逻辑
    public bool IsOnCoolDown
    {
        get
        {
            return isOnCoolDown;
        }
    }
    //在当前脚本中都使用private变量进行内部修改
    private bool isOnCoolDown = false;




    //有using unityEngine; 引入 UnityEngine 命名空间
    //没有继承自MonoBehaviour不能直接调用相关的变量
    //可以借助其他继承自 MonoBehaviour 的类来完成协程的启动和停止操作
    //该命名空间包含了大量 Unity 相关的类型定义

    //coroutine声明为Coroutine类型变量
    //Coroutine 类型变量能够清晰地表明其用途是存储一个协程对象
    //Coroutine 类型代表一个协程实例
    //调用 StartCoroutine 方法启动一个协程时，该方法会返回一个 Coroutine 对象
    //如果要储存一个Coroutine对象，最好使用Coroutine类型变量
    //Coroutine变量只是一个记录哪个协程被开启了，最后要关闭的时候使用回这个协程对象

    //协程的身份证
    //Coroutine变量并不会影响协程内部的计算，只是为某一个协程提供了某一个实例
    //如果要使用一个协程两次，最好创建两个实例分别用
    private Coroutine coroutine;

    //定义StartCoolDown()函数
    public void StartCooldown()
    {
        //如果自定义的枚举器类型变量是枚举器的Started或者InProgress成员就return，不执行接下来的代码
        if (CurrentProgress is Progress.Started or Progress.InProgress)
            return;

        //协程的启动依赖MonoBehaviour
        //利用CoroutineHost这个类 （组件），有继承自MonoBehaviour来开启和关闭协程
        //基本上和我们在CoroutineHost脚本里输入StartCoroutine一样的概念
        //获取了CoroutineHost的类的实例（组件），并调用了该实例的StartCoroutine（）方法，并且将我们的DoCoolDown协程传递进去
        //实际上是让CoroutineHost的实例来执行协程的启动操作

        //CoroutineHost的唯一实例开启了这个定义在当前类的协程
        //CoroutineHost的实例主要的功能是提供StartCoroutine和StopCoroutine这个方法并且 执行传入的协程

        //Cooldown定义了协程的逻辑和行为，而CoroutineHost负责启动和结束这个协程
        coroutine = CoroutineHost.Instance.StartCoroutine(DoCoolDown());
    }

    //定义StopCoroutine()函数
    public void StopCooldown()
    {
        //如果coroutine这个实例存存在，刚才有被Start到
        if (coroutine != null)
        {
            //那么就暂停这个coroutine实例，不要在后台
            CoroutineHost.Instance.StopCoroutine(coroutine);
        }
            
        //并且初始化以下变量
        currentDuration = 0f;
        isOnCoolDown = false;
        CurrentProgress = Progress.Ready;
    }

    //协程函数的返回类型是IEnumerator
    IEnumerator DoCoolDown()
    {
        
        CurrentProgress = Progress.Started;

        //把 public Duration 赋值给 currentDuration
        currentDuration = Duration;
        isOnCoolDown = true;

        //当currentDuration，也就是Duration的值>0，那么
        while (currentDuration > 0)
        {
            //currentDuration就会开始减Time.deltaTime
            //如果是1，那么就会耗时一秒
            currentDuration -= Time.deltaTime;
            CurrentProgress = Progress.InProgress;

            //Debug.Log(currentDuration);
            //接下来先等下一帧才开始
            //下一帧的协程从这里开始，避免在一帧之内完成整个冷却过程
            yield return null;
            
        }

        //当currentDuration扣完之后就
        currentDuration = 0f;
        isOnCoolDown = false;
        CurrentProgress = Progress.Finished;
    }

    //是在每一帧中减少一次冷却时间，通过 yield return null; 暂停协程执行，等待下一帧继续执行，从而实现了一个基于真实时间流逝的冷却过程
    //因为理论上我们就是要一帧计算一次




}
