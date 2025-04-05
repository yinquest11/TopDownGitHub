using UnityEngine;

public class ControlInvinsible : MonoBehaviour
{

    private SpriteRenderer SpriteRendererr;

    public void Start()
    {
        SpriteRendererr = GetComponent<SpriteRenderer>();
    }

    //The Collider2D that trigger this fuction is playerCollider
    protected void OnTriggerEnter2D(Collider2D playerCollier)
    {

        //Compare the Tag from the playerCollider.gameObject
        if (playerCollier.CompareTag("Player"))
        {
            //enable SpriteRendere component
            SpriteRendererr.enabled = true;
        }
    }

    //The collider exit from gameObject Collider2D is playerCollider
    protected void OnTriggerExit2D(Collider2D playerCollier)
    {
        if (playerCollier.CompareTag("Player"))
        {
            //disable SpriteRendere component
            SpriteRendererr.enabled = false;

        }
    }
}
