using UnityEngine;

public class EnemyMovementIInvinsible : Movemet
{
    
    private GameObject playerr;

    

    protected override void Start()
    {
        base.Start();
        playerr = GameObject.FindWithTag("Player");
        Acceleration = 2.5f;

        //First find on self have Spriterendewrer Component or not
        //if no have, find on children and return the first one
        

    }

    protected override void HandleInput()
    {
        if (playerr == null)
            return;

        //Let enemy face to player
        _inputDirection = 
        new Vector2(playerr.transform.position.x - transform.position.x, playerr.transform.position.y - transform.position.y).normalized;

        Debug.DrawRay(transform.position, _inputDirection, Color.yellow);
    }

    protected override void HandleRotation()
    {
        
        base.HandleRotation();
    }




}
