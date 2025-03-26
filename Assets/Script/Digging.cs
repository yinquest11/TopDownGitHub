using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using Unity.Mathematics;

public class Digging : MonoBehaviour
{
    private int _currentLayer;

    public Camera MainCamera;

    private float _mainCameraSize;

    public float _currentCameraSize;

    public float MainCameraBigSize;

    public float _increasingRatio;

    public float ZoomDuration;

    private bool _isDigging;

    private bool _digBefore;

    private Weapon _weapon;

    private SpriteRenderer[] _gunSprite;

    private float _align;

    public void Start()
    {
        _digBefore = false;

        MainCameraBigSize = 10;

        _currentLayer = gameObject.layer;

        MainCamera = Camera.main;

        _mainCameraSize = Camera.main.orthographicSize;

        _isDigging = false;

        ZoomDuration = 0.5f;

        


    }

    public void Update()
    {
        if (GameObject.FindWithTag("Player").GetComponentInChildren<Weapon>())
        {
            _gunSprite = GetComponentInChildren<Weapon>().gameObject.GetComponentsInChildren<SpriteRenderer>();
        }

        // always get the current weapon
        
       
        

 

        if (Input.GetButtonDown("Dig"))
        {

           InitiaslizeZoom();

            
            
        }

        if (Input.GetButtonUp("Dig"))
        {

            InitializeSmallZoom();



        }


        if (_isDigging == true && Input.GetButton("Dig"))
        {
            Physics2D.IgnoreLayerCollision(_currentLayer, 6, true);



            Zoom();
            
            foreach (SpriteRenderer s in _gunSprite)
            {
                s.enabled = false;
                
            }


        }

         if (_isDigging == false && _digBefore)
         {
            Physics2D.IgnoreLayerCollision(_currentLayer, 6, false);

            SmallZoom();


            foreach (SpriteRenderer s in _gunSprite)
            {
                s.enabled = true;
                
            }




         }

         

    }

   
    public void InitiaslizeZoom()
    {
        

        _digBefore = true;

        _isDigging = true;
        _increasingRatio = 0f;

        _currentCameraSize = Camera.main.orthographicSize;
    }

    public void Zoom()
    {
        _increasingRatio += (Time.deltaTime / ZoomDuration);
        _increasingRatio = Mathf.Clamp01(_increasingRatio);
        MainCamera.orthographicSize = Mathf.Lerp(_currentCameraSize, MainCameraBigSize, _increasingRatio);
    }

    public void InitializeSmallZoom()
    {
        _isDigging = false;
        _increasingRatio = 0f;

        _currentCameraSize = Camera.main.orthographicSize;
    }
    public void SmallZoom()
    {
        _increasingRatio += (Time.deltaTime / ZoomDuration);
        _increasingRatio = Mathf.Clamp01(_increasingRatio);
        MainCamera.orthographicSize = Mathf.Lerp(_currentCameraSize, _mainCameraSize, _increasingRatio);
    }

    
}
