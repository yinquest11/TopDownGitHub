using UnityEngine;

public class EnemyMovementIInvinsible : Movemet
{
    
    private GameObject playerr;

    //多加了一个SpriteRenderer因为要获取到子GameObejct的SpriteRenderer
    private SpriteRenderer childSpriteRenderer;


    

    protected override void Start()
    {
        base.Start();
        playerr = GameObject.FindWithTag("Player");
        Acceleration = 2.5f;

        //先从自身找，没有的话就从Children上GetComponent<>();
        childSpriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    protected override void HandleInput()
    {
        if (playerr == null)
            return;

        _inputDirection = new Vector2(playerr.transform.position.x - transform.position.x, playerr.transform.position.y - transform.position.y).normalized;

        Debug.DrawRay(transform.position, _inputDirection, Color.yellow);
    }

    protected override void HandleRotation()
    {
        
        base.HandleRotation();
    }



    //当别的Collider2D进入了当前gameObject的isTrigger的Collider2D
    protected void OnTriggerEnter2D(Collider2D playerCollier)
    {
        //直接调用CompareTag，原理和直接调用transform一样
        //继承了Component类，那么你就有gameObject这个属性。当然你也有Component类的方法，比如CompareTag()。
        //这就是为什么可以直接.CompareTag来找当前组件所挂在的脚本的Tag，而不是通过gameObject.因为Component的CompareTag()里包括了直接向上找gameObject的功能。


        if (playerCollier.CompareTag("Player"))
        {
            //属于Behaiour属性，MonoBehaviour又继承了Behaviour，那么在继承MonoBehaviour的脚本里，组件.enable就可以控制启用和禁用状态
            //接触就开启
            childSpriteRenderer.enabled = true;

        }
    }

    protected void OnTriggerExit2D(Collider2D playerCollier)
    {
        if (playerCollier.CompareTag("Player"))
        {
            //ExitTrigger了就再次禁用该组件
            childSpriteRenderer.enabled = false;

        }
    }





}
