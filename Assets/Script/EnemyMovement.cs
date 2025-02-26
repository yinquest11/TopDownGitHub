using UnityEngine;

public class EnemyMovement : Movemet
{

    private GameObject playerr;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected override void Start()
    {
        base.Start();
        playerr = GameObject.FindWithTag("Player");
        Acceleration = 2.5f;
    }
    
    protected override void HandleInput()
    {
        if (playerr == null)
            return;

        _inputDirection = new Vector2 (playerr.transform.position.x - transform.position.x, playerr.transform.position.y - transform.position.y).normalized;
        
        Debug.DrawRay(transform.position,_inputDirection,Color.yellow);

    }
    
    protected override void HandleRotation()
    {
        base.HandleRotation();
    }

    protected void OnTriggerEnter2D(Collider2D playerCollier)
    {
        if(playerCollier.CompareTag("Player"));
        {
            SpriteRenderer childSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            childSpriteRenderer.enabled = true;

        }
    }

    protected void OnTriggerExit2D(Collider2D playerCollier)
    {
        if(playerCollier.CompareTag("Player"));
        {
            SpriteRenderer childSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            childSpriteRenderer.enabled = false;

        }
    }


    


}
