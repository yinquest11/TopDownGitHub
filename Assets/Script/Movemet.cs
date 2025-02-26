using UnityEngine;


public class Movemet : MonoBehaviour
{
    
    //定义变量
    public float Acceleration = 5f;
    protected float m_MovementSmoothing = 0.05f;
    protected Collider2D _collider;
    protected Rigidbody2D _rigidBody;
    protected bool _isMoving = false;
    protected Vector2 _inputDirection;
    protected Vector2 m_Velocity = Vector2.zero;
    protected Vector2 _targetVelocity = Vector2.zero;
    
    //protected，只有继承Movement的类才能访问并且使用Movement的class
    protected virtual void Start()
    {
        //每个继承Movement的脚本都会在一开始GetComponent<>(); Colider2D 和 Rigidbody2D
        _collider = GetComponent<Collider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    
    protected virtual void Update()
    {
        //每帧更新的时候会顺序执行下面3个函数
        HandleInput();
        HandleMovement();
        HandleRotation();
    }



    protected virtual void HandleInput()
    {

    }


    protected virtual void HandleMovement()
    {
        if(_rigidBody == null || _collider == null)
            return;

        Vector2 targetVelocity = Vector2.zero;

        targetVelocity = new Vector2(_inputDirection.x * Acceleration, _inputDirection.y * Acceleration);

        _rigidBody.linearVelocity = Vector2.SmoothDamp(_rigidBody.linearVelocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        //_isMoving = (targetVelocity.x !=0), 如果targetVelocity.x !=0 为true，那么 _isMoving = (true)
        _isMoving = targetVelocity.x !=0 || targetVelocity.y !=0;

        _targetVelocity = targetVelocity;
    }


    protected virtual void HandleRotation()
    {
        if(_inputDirection == Vector2.zero)
            return;
        transform.rotation = Quaternion.LookRotation(Vector3.forward,_targetVelocity);
        
    }
}
