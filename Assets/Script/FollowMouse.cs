using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    //A script (component) on Corsshair image
    private RectTransform reticle;
    
    void Start()
    {
        reticle = GetComponent<RectTransform>();
        if (Cursor.visible)
        {
            //Cursor.visible改成false
            Cursor.visible = false;
        }
    }

    
    void Update()
    {
        if (reticle == null)
        {
            return;
        }
        //Let image position equal to my mousePointer position
        reticle.position = Input.mousePosition;


        //Hide my original cursor's image
        

    }
}
