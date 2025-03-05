using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    public GameObject[] PickupFeedbacks;

    public LayerMask TargetLayerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!((TargetLayerMask.value & (1 << col.gameObject.layer)) > 0))
            return;

        PickedUp(col);

        foreach(var feedback in PickupFeedbacks)
        {
            GameObject.Instantiate(feedback, transform.position, transform.rotation);
        }


        Destroy(gameObject);

    }

    protected virtual void PickedUp(Collider2D col)
    {

    }


}
