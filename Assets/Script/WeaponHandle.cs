using System.Collections;
using UnityEngine;

public class WeaponHandle : MonoBehaviour
{

    public Weapon CurrentWeapon;
    public Transform GunPosition;
    private Digging _dig;
    protected bool _tryShoot;

    

    


    protected virtual void Update()
    {
        HandleInput();
        HandleWeapon();
        if (GameObject.FindWithTag("Player").GetComponentInChildren<Weapon>())
        {
            _dig = GameObject.FindWithTag("Player").GetComponentInChildren<Digging>();
        }
    }

    protected virtual void HandleInput()
    {

    }

    protected virtual void HandleWeapon()
    {
        if (CurrentWeapon == null || GunPosition == null)
            return;

        //Initialize CurrentWeapom position and rotation
        CurrentWeapon.transform.position = GunPosition.position;
        CurrentWeapon.transform.rotation = GunPosition.rotation;

        if (_tryShoot)
        {
            CurrentWeapon.Shoot();

            if (CurrentWeapon.FireMode == Weapon.FireModes.Sniper && Input.GetButton("Fire1") && CurrentWeapon.CurrentBulletCount > 0 )
            {
                if (Input.GetButtonDown("Dig") || Input.GetButton("Dig"))
                    return;

                if (_dig == null) { Debug.LogWarning(gameObject.name + ": _dig is missing something."); return; }
                _dig.MainCameraBigSize = 30f;
                _dig.InitiaslizeZoom();
                
                _dig.Zoom();
                

               
                


            }

        }
        else
        {

            if (CurrentWeapon.FireMode == Weapon.FireModes.Sniper && Input.GetButtonUp("Fire1") && CurrentWeapon.CurrentBulletCount >0)
            {
                if (Input.GetButtonDown("Dig") || Input.GetButton("Dig"))
                    return;
                
                CurrentWeapon.SniperFireShoot();
                _dig.InitializeSmallZoom();
                _dig.SmallZoom();
                
                
                _dig.MainCameraBigSize = 15;
                

            }
                
                CurrentWeapon.StopShoot();
                          
        }
    }

    

    public void Start()
    {
        //Auto find Weapon component (script) if Player Gameobejct got
        if (CurrentWeapon == null)
        {
            CurrentWeapon = GetComponentInChildren<Weapon>();
        }

       

    }

    public void EquipWeapon(GameObject equipWeapon)
    {
        if (equipWeapon == null)
            return;

        if (CurrentWeapon != null)
        {
            Destroy(CurrentWeapon.gameObject);
        }

           

        GameObject _weaponGO = GameObject.Instantiate(equipWeapon, GunPosition);

        Weapon weapon = _weaponGO.GetComponent<Weapon>();

        if (weapon == null)
            return;

        CurrentWeapon = weapon;
    }



    
}