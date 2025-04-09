using UnityEngine;

public class WeaponPickUp : PickUp
{
    public GameObject[] Weapon;

    private int _currentChance;

    public Sprite[] _sprites;

    private SpriteRenderer _spriteRenderer;


    private void Start()
    {
        if (_sprites == null) { Debug.LogWarning(gameObject.name + ": _sprites is missing something."); return; }


        _currentChance = Random.Range(0, Weapon.Length);

        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.sprite = _sprites[_currentChance];

        if(_currentChance == 3)
        {
            transform.localScale = Vector3.one;
        }


    }

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

        
       
        weaponHandler.EquipWeapon(Weapon[_currentChance]);

        
    }
}
