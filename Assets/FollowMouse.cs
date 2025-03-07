using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    //挂在Corsshair image上面的
    //先获取这个image上的RectTransform组件 （如果是under Canvas就没有Transform组件，只有RectTransform组件）
    private RectTransform reticle;
    
    void Start()
    {
        //当开始的时候，赋值reticle引用变量，GetComponent<RectTransform>();
        reticle = GetComponent<RectTransform>();
    }

    
    void Update()
    {
        //如果没获取到，就退出函数，（自我保护）
        if (reticle == null)
        {
            return;
        }

        //这个image的位置设定成我鼠标的位置
        reticle.position = Input.mousePosition;

        //并且如果目前我原生自带的鼠标.visible = ture
        if (Cursor.visible)
        {
            //Cursor.visible改成false
            Cursor.visible = false;
        }


        


    }
}
