using UnityEngine;

public class CoroutineHost : MonoBehaviour
{


   


    public static int a = 8;
    
    //CoroutineHost的唯一实例，通过将其声明为静态的，无论在程序的哪个地方，只要能访问到 CoroutineHost 类，就可以通过 CoroutineHost.Instance 来get这个唯一实例。
    public static CoroutineHost m_Instance;

    //静态属性，它提供了一个全局访问点，其他类可以通过 CoroutineHost.Instance 来获取 CoroutineHost 类的唯一实例，也就是 m_Instance
    //get 访问器中包含了确保单例实例唯一性的逻辑。
    public static CoroutineHost Instance
    {
        //只读属性
        get//开始判断这个唯一的实例是否已经存在
        {
            //回到来CoroutineHost的类里面检查当前 m_Instance 是否为 null。
            //如果 m_Instance != null，说明 CoroutineHost 类的实例已经被创建并且赋值给m_Instance，直接返回 m_Instance 即可。
            //如果 == null，代表m_Instance目前还没被赋值到，继续往下执行。
            
            //所以第一次null检查实际上是检查 “m_Instance是否不管经历什么，被赋值过了吗”
            if (m_Instance == null)
            {
                //使用 FindFirstObjectByType<CoroutineHost>() 方法在当前场景中查找是否已经存在 CoroutineHost 的实例（组件）
                //这个方法的作用是寻找并且尝试返回第一个再目前Scene中的所有游戏对象包括子游戏对象<CoroutineHost>组件

                m_Instance = (CoroutineHost)FindFirstObjectByType<CoroutineHost>();
                //所以是尝试给m_Instance去寻找并复制CoroutineHost组件，如果还是==null，代表目前场景并没有一个游戏物体上有着CoroutineHost组件
                //还是null，就进行下一步
                //接着看是否有被赋值到，!=null的话就是有被赋值到了那么把 m_Instance 赋值给 Instance

                if (m_Instance == null)
                {
                    //还是没有找到，创建一个
                    GameObject go = new GameObject();
                    //名字设定叫CoroutineHost
                    go.name = "CoroutineHost";
                    
                    //m_Instance = 加进来的这个Component，而不是go这个GameObject
                    m_Instance = go.AddComponent<CoroutineHost>();
                }


                
                //调用 DontDestroyOnLoad 方法，确保 m_Instance 所引用的实例在场景切换时不会被销毁， （默认切换场景会被销毁）
                //从而保证在整个应用程序的生命周期内只有一个 CoroutineHost 实例存在。
                DontDestroyOnLoad(m_Instance);
            }

            //把 m_Instance 赋值给 Instance
            return m_Instance;


            //所以整段代码其实是让m_Instance去储存一个组件的地址，也就是当前组件，也就是CoroutineHost script，这也是为什么要让m_Instance 的变量是CoroutineHost。
            //在我的情况中CoroutineHost组件的实例其实是go游戏对象上的CoroutineHost组件

            //所以整段代码就是要确保当你调用CoroutineHost.Instance的时候永远指向唯一一个GameObject上的CoroutineHost组件。
            //至于静态m_Instance和Instance储存的是一个地址，所以是合理的

            //整段代码的核心目标是实现 CoroutineHost 类的单例模式，也就是保证在整个应用程序的生命周期内，CoroutineHost 类只有一个实例存在，
            //并且无论何时调用 CoroutineHost.Instance，都能获取到这个唯一的实例
        }
    }




}
