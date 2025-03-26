using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    public GameObject[] PickupFeedbacks;

    //LayerMask variable
    //If choose multiple layeMask on inspector, they will all be | and combine 
    public LayerMask TargetLayerMask;
    
    void Start()
    {
        
        
    }

    
    void Update()
    {
        
    }

    //When a GameObject Trigger gameObject 
    private void OnTriggerEnter2D(Collider2D col)
    {

        //if the GameObject layer does not include in TargetLayerMask, then return
        if (                 !(       (TargetLayerMask.value & 1 << (col.gameObject.layer))   > 0       )                  )
           
            return;
        
        PickedUp(col);

        //use foreach to instantiate all the GameObejct in PickupFeedbacks array
        foreach (var feedback in PickupFeedbacks)
        {
            
            GameObject.Instantiate(feedback, transform.position, transform.rotation);
        }

        
        Destroy(gameObject);

    }

    protected virtual void PickedUp(Collider2D col)
    {

    }


}
