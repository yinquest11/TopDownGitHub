using UnityEngine;
using UnityEngine.UI;

public class UIReloadIndicator : MonoBehaviour
{
    private Image _reloadbar;
    private WeaponHandle playerWeaponHandler;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _reloadbar = GetComponent<Image>();

        GameObject playerGo = GameObject.FindGameObjectWithTag("Player");

        if (playerGo == null)
            return;

        playerWeaponHandler = playerGo.GetComponent<WeaponHandle>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerWeaponHandler == null)
            return;

        if (playerWeaponHandler.CurrentWeapon == null)
            return;

        if (playerWeaponHandler.CurrentWeapon.CurrentBulletCount > 0)
        {
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
            float currentBulletCountt = playerWeaponHandler.CurrentWeapon.ReloadCooldown.CurrentDuration;
            float maxBulletCountt = playerWeaponHandler.CurrentWeapon.ReloadCooldown.Duration;

            float bulletLeftFill = currentBulletCountt / maxBulletCountt;

            if (_reloadbar != null)
            {
                
                _reloadbar.fillAmount = 1F-bulletLeftFill;
                Debug.Log(bulletLeftFill);
            }
        }
    }
}
