using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeaponHandeler : WeaponHandle
{
    protected override void HandleInput()
    {

        if (Input.GetButton("Fire1"))
        {
            _tryShoot = true;
        }
        

        if (Input.GetButtonUp("Fire1"))
            _tryShoot = false;
    }

    
}
