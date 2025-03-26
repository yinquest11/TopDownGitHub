using UnityEngine;


public class Movemet : MonoBehaviour
{
    public float Acceleration = 5f;

    protected float m_MovementSmoothing = 0.05f;

    protected Collider2D _collider;
    protected Rigidbody2D _rigidBody;
    protected WeaponHandle _weaponHandler;

    protected bool _isMoving = false;

    protected Vector2 _inputDirection;
    protected Vector2 m_Velocity = Vector2.zero;
    protected Vector2 _targetVelocity = Vector2.zero;

    public Vector2 Doge;
    public float DogeSpeed = 30f;

    public bool IsDoge = false;
    
    protected virtual void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _weaponHandler = GetComponent<WeaponHandle>();
        

        
    }

    
    protected Camera _mainCamera;
    
    protected virtual void Update()
    {
        //每帧更新的时候会顺序执行下面3个函数
        HandleInput();
        HandleMovement();
        HandleRotation();
        Doge = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * DogeSpeed;
        HandleDoge();
        
        
    }

    protected virtual void LateUpdate()
    {
        
        HandleCamara();
    }


    protected virtual void HandleInput()
    {

    }
    protected virtual void HandleMovement()
    {
        if(_rigidBody == null || _collider == null)
            return;

        if (IsDoge)
            return;



        Vector2 targetVelocity = Vector2.zero;


        targetVelocity = new Vector2(_inputDirection.x * Acceleration, _inputDirection.y * Acceleration);

        //use linearVelocity to let gameObject move
        //use SmoothDamp to smooth between 2 Vector2
        _rigidBody.linearVelocity = Vector2.SmoothDamp(_rigidBody.linearVelocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        


        //either (targetVelocity.x != 0) or (targetVelocity.y != 0) is true, _isMoving is true
        _isMoving = targetVelocity.x !=0 || targetVelocity.y !=0;

        _targetVelocity = targetVelocity;
    }
    protected virtual void HandleRotation()
    {
        if (_inputDirection == Vector2.zero) 
            return;

        //Look forward with _targetVelocity's posture
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _targetVelocity);
    }
    protected virtual void HandleDoge()
    {

    }

    protected virtual void HandleCamara()
    {

    }
}   
