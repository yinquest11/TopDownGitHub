using UnityEngine;
using UnityEngine.UI;


//A script for manipulate an image to show the rest of bullet amount
public class UIReloadIndicator : MonoBehaviour
{
    private Image _reloadbar;

    private WeaponHandle playerWeaponHandler;

    

    void Start()
    {
        _reloadbar = GetComponent<Image>();

        GameObject playerGo = GameObject.FindWithTag("Player");

        if ( playerGo== null) { Debug.LogWarning(gameObject.name + ": ( playerGo ) is missing."); return; }

        //Get player's WeaponHandle script (component)
        playerWeaponHandler = playerGo.GetComponent<WeaponHandle>();
        
    }

   
    void Update()
    {
        if (playerWeaponHandler == null)
            return;
        if (playerWeaponHandler.CurrentWeapon == null)
            return;

        if (playerWeaponHandler.CurrentWeapon.CurrentBulletCount > 0)
        {
            //Current bullet divide total bullet and get the ratio,
            //apply the ratio on   image.fillAmount   properties
            float currentBulletCount = playerWeaponHandler.CurrentWeapon.CurrentBulletCount;
            float maxBulletCount = playerWeaponHandler.CurrentWeapon.MaxBulletCount;

            float bulletLeftFill = currentBulletCount / maxBulletCount;

            if(_reloadbar != null)
            {
                _reloadbar.fillAmount = bulletLeftFill; 
            }
        }

        if (playerWeaponHandler.CurrentWeapon.CurrentBulletCount <= 0)
        {
            //Left cooldown time divide total cooldown time required and get the ratio,
            //1 - the ratio make sure start from small to big,
            //apply the ratio after -1 on   image.fillAmount   properties
            float currentBulletCountt = playerWeaponHandler.CurrentWeapon.ReloadCooldown.CurrentDuration;
            float maxBulletCountt = playerWeaponHandler.CurrentWeapon.ReloadCooldown.Duration;

            float bulletLeftFill = currentBulletCountt / maxBulletCountt;

            if (_reloadbar != null)
            {
                _reloadbar.fillAmount = 1F - bulletLeftFill;
                
            }
        }
    }
}
