using UnityEngine;


public class Movemet : MonoBehaviour
{
    //protected，只有继承的class能访问
    //定义变量，公开变量首字母大写
    public float Acceleration = 5f;

    //关键时刻的小帮手变量
    protected float m_MovementSmoothing = 0.05f;

    //会被其他类继承的变量，_开头
    //每个有Movement的GameObject都会有这两个Component
    protected Collider2D _collider;
    protected Rigidbody2D _rigidBody;
    protected WeaponHandle _weaponHandler;

    //布尔变量
    protected bool _isMoving = false;

    //3个Vector2变量
    protected Vector2 _inputDirection;
    protected Vector2 m_Velocity = Vector2.zero;
    protected Vector2 _targetVelocity = Vector2.zero;

    public Vector2 Doge;
    public float DogeSpeed = 30f;

    public bool IsDoge = false;
    
    //protected，只有继承Movement的类才能访问并且使用Movement的class
    protected virtual void Start()
    {
        //每个继承Movement的脚本都会在一开始GetComponent<>(); Colider2D 和 Rigidbody2D 和 WeaponHandle 组件（脚本）
        _collider = GetComponent<Collider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _weaponHandler = GetComponent<WeaponHandle>();
        Doge = transform.up * DogeSpeed;

        
    }

    
    protected Camera _mainCamera;
    
    protected virtual void Update()
    {
        //每帧更新的时候会顺序执行下面3个函数
        HandleInput();
        HandleMovement();
        HandleRotation();
        HandleDoge();
        
        
    }

    protected virtual void LateUpdate()
    {
        
        HandleCamara();
    }


    //放给其他子类override
    protected virtual void HandleInput()
    {

    }
    protected virtual void HandleMovement()
    {
        //自我保护
        if(_rigidBody == null || _collider == null)
            return;

        if (IsDoge)
            return;

        

        //本地私人变量，小写开头
        //初始化一个私人变量targetVelocity，并且初始化为0
        Vector2 targetVelocity = Vector2.zero;


        //赋值targetVelocity x = _inputDirection.x * Acceleration, y = _inputDirection.y * Acceleration
        //其他子类的HandleInput()会赋值_inputDirection, 从而改变targetVelocity这个私人变量
        //new 后面跟构造函数用来创建实例
        targetVelocity = new Vector2(_inputDirection.x * Acceleration, _inputDirection.y * Acceleration);


        //最终透过linearVelocity让玩家和怪物移动
        //m_Velocity 是一个动态更新的 Vector2 变量，代表速度变化趋势，作为关键参数参与 Vector2.SmoothDamp 计算，影响其返回的平滑过渡后的 Vector2 值
        //m_Velocity有种moumentum动量的感觉
        //Vector2.SmoothDamp的值会返回：如果差距较大，会让速度增加得快一些；如果接近目标值，就会逐渐减小速度，以实现平滑的过渡效果
        //所以如果发现当前速度也就是m_Velocity大，但是距离目标很靠近了，那么这时返回给Vector2.SmoothDamp函数的值就会降低，确保平滑过度。
        //换而言之，m_Velocity就是要获取当前速度去考虑下一步函数返回的速度应不应该变大或变小
        _rigidBody.linearVelocity = Vector2.SmoothDamp(_rigidBody.linearVelocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        


        //_isMoving = (targetVelocity.x !=0), 如果targetVelocity.x !=0 为true，那么 _isMoving = (true)
        _isMoving = targetVelocity.x !=0 || targetVelocity.y !=0;

        //把当前速度用于HandleRotation();,保护targetVelocity不会被直接修改
        _targetVelocity = targetVelocity;
    }
    protected virtual void HandleRotation()
    {
        if (_inputDirection == Vector2.zero) 
            return;

        //第一个参数定义了物体应该要看的地方
        //所以第二个参数定义了我在看的时候，我是以怎样的姿势看
        //那么我有了这两个参数之后，Quaternion.LookRotation函数可以给我一个可以最终达成这个形态的四元数

        //这个四元数代表了能让物体从初始姿态旋转到满足你指定条件的目标姿态所需的旋转
        //刚好transform.rotation接受的正是四元数类型的值

        //速度是向量，所以在这个情况速度可以被当成一个方向
        //我要的方向，是这个速度的方向
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _targetVelocity);
    }
    protected virtual void HandleDoge()
    {

    }

    protected virtual void HandleCamara()
    {

    }
}   
