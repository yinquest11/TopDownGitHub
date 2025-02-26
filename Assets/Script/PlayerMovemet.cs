using UnityEngine;

public class PlayerMovemet : Movemet
{
    protected override void HandleInput()
    {
        _inputDirection = new Vector2 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
    }

    protected override void HandleRotation()
    {
        base.HandleRotation();
    }

}
