using UnityEngine;
using UnityEngine.UI;


//挂载在Crosshair之下的Reload游戏对象，用来表示目前剩余弹药
public class UIReloadIndicator : MonoBehaviour
{
    //引用变量用来引用Image对象
    private Image _reloadbar;

    //引用WeaponHandle组件
    private WeaponHandle playerWeaponHandler;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //游戏开始的时候让引用变量指向当前gameObject上挂载的Image
        _reloadbar = GetComponent<Image>();

        //寻找场景所有带Player标签的游戏对象并返回第一个，储存在playerGo
        GameObject playerGo = GameObject.FindWithTag("Player");
        //自我保护
        if ( playerGo== null) { Debug.LogWarning(gameObject.name + ": ( playerGo ) is missing."); return; }
        //Get playerGo身上的Component并储存在playerWeaponHandler
        playerWeaponHandler = playerGo.GetComponent<WeaponHandle>();

        //3个步骤获取别人的脚本组件，
        //1. 首先拿到这个人
        //2. 然后找他的组件
        //3. 引用在一个引用变量上
        
    }

   
    void Update()
    {
        //自我保护
        if (playerWeaponHandler == null)
            return;
        //自我保护
        if (playerWeaponHandler.CurrentWeapon == null)
            return;

        //当playerWeaponHandler所引用的脚本组件里的CurrentWeapon的CurrentBulletCount打过0就执行一下代码
        if (playerWeaponHandler.CurrentWeapon.CurrentBulletCount > 0)
        {
            //两个变量，目前剩余弹药和弹药上限，小除大来获得一个百分比。获得还剩多少八仙的弹药，进而用这个八仙控制Image组件的fillAmount参数

            float currentBulletCount = playerWeaponHandler.CurrentWeapon.CurrentBulletCount;
            float maxBulletCount = playerWeaponHandler.CurrentWeapon.MaxBulletCount;

            float bulletLeftFill = currentBulletCount / maxBulletCount;

            //还有的话才 = bulletLeftFill, null了的话就没有了当然没必要赋值
            if(_reloadbar != null)
            {
                //1到0.0
                _reloadbar.fillAmount = bulletLeftFill; 
            }
        }

        //当没有子弹了来到这里，利用剩余冷却时间 / 总动冷却时间 来得到总共还剩多少八仙没冷却完
        if (playerWeaponHandler.CurrentWeapon.CurrentBulletCount <= 0)
        {
            float currentBulletCountt = playerWeaponHandler.CurrentWeapon.ReloadCooldown.CurrentDuration;
            float maxBulletCountt = playerWeaponHandler.CurrentWeapon.ReloadCooldown.Duration;

            float bulletLeftFill = currentBulletCountt / maxBulletCountt;

            if (_reloadbar != null)
            {
                //用1-确保数字是从小到大，fillAmount可以反方向操作
                //0.0到1
                _reloadbar.fillAmount = 1F-bulletLeftFill;
                
            }
        }
    }
}
