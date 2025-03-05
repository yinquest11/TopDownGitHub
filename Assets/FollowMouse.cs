using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private RectTransform reticle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reticle = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reticle == null)
        {
            return;
        }
        reticle.position = Input.mousePosition;

        if (Cursor.visible)
        {
            Cursor.visible = false;
        }




    }
}
