using UnityEngine;

public class WeaponPickUp : PickUp
{
    public GameObject[] Weapon;

    private int _currentChance;

    protected override void PickedUp(Collider2D col)
    {
        if (Weapon == null)
        {
            Debug.LogWarning("Missing Weapon");
            return;
        }
        
        base.PickedUp(col);

        WeaponHandle weaponHandler = col.GetComponent<WeaponHandle>();


        if (weaponHandler == null)
            return;

        _currentChance = Random.Range(0, Weapon.Length);
       
        weaponHandler.EquipWeapon(Weapon[_currentChance]);

        
    }
}
