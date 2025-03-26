using UnityEngine;

public class EnemyMovement : Movemet
{
    private GameObject playerr;

    
    protected override void Start()
    {
        base.Start();
        playerr = GameObject.FindWithTag("Player");
        

        

        



    }

    

    protected override void HandleInput()
    {
        

        if (playerr == null)
            return;

        //Let enemy face to player
        _inputDirection =
        new Vector2 (playerr.transform.position.x - transform.position.x, playerr.transform.position.y - transform.position.y).normalized;


        Debug.DrawRay(transform.position,_inputDirection,Color.yellow);
    }
    
    protected override void HandleRotation()
    {
        base.HandleRotation();
    }

   
    




}
