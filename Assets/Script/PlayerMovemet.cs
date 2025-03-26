using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerMovemet : Movemet
{

    public GameObject DogeEffect;
    private GameObject _cloneDogeEffect;

    //override Movement的HandleInput
    protected override void HandleInput()
    {
        _inputDirection = new Vector2 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
    }

    protected override void HandleRotation()

    
    {
        
        if (_weaponHandler == null || _weaponHandler.CurrentWeapon == null)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                base.HandleRotation();
            }
            
            return;
        }
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Use player z position for mousePos
        mousePos = new Vector3(mousePos.x, mousePos.y, transform.position.z);



        //Get the direction that player face to the mouse pointer
        Vector2 direction = mousePos - transform.position;



        //Caluculate the angle that player need to rotate to face the pointer
        //And turn into degree from radian 
        //-90 offset because object in Unity default facing Y-axis, but Atan2 default calculate object facing X-axis
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;

        //2D game only Z-axis suppose to be rotate
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

       




        
    }
    protected override void HandleDoge()
    {
        
        if ( Input.GetButtonDown("Fire2"))
        {
            if (Input.GetButtonDown("Dig") || Input.GetButton("Dig"))
                return;

            //Start Doge by AddRelativeForce
            IsDoge = true;
            if (Doge != Vector2.zero)
            {
                _rigidBody.AddForce(Doge);
            }
            else
            {
                _rigidBody.AddRelativeForce(Vector2.down * DogeSpeed);  
            }
            
            

            //IsDoge = flase after 0.1s
            Invoke("EndDoge", 0.1f);

        }

        if ( DogeEffect == null) { Debug.LogWarning(gameObject.name + ":  is missing something."); return; }

        //Instantiate particle effect
        if (IsDoge)
        {
            _cloneDogeEffect =  Instantiate(DogeEffect, transform.position, transform.rotation);

            
        }


    }
    public void EndDoge()
    {
        IsDoge = false;
        
    }

    //Let the Camera follow player
    protected override void HandleCamara()
    {

        Vector3 s_position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 44);
        _mainCamera = Camera.main;

        _mainCamera.transform.position = s_position;

    }
    

    
}
