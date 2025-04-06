using UnityEngine;

public class BossIndicator : MonoBehaviour
{
    private GameObject _boss;
    private Vector2 _bossScreenPos;
    private SpriteRenderer _indicatorImage;

    private Camera _camera;
    private Vector3 _bossPos;
    private Vector2 _direction;
    private float angle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main;
        _indicatorImage = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(GameObject.FindWithTag("Boss") != null)
        {
            _boss = GameObject.FindWithTag("Boss").gameObject;
        }
        else
        {
            return;
        }





        _bossScreenPos = _camera.WorldToViewportPoint(_boss.transform.position);
        Debug.Log(_bossScreenPos);

        if (_bossScreenPos.x < 0 || _bossScreenPos.x > 1 || _bossScreenPos.y < 0 || _bossScreenPos.y > 1)
        {
            _indicatorImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            _indicatorImage.color = new Color(0, 0, 0, 0);
        }



        _bossPos = _boss.transform.position;

        _direction = _bossPos - transform.position;

        angle = (Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg) -90f;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Debug.DrawRay(transform.position, _direction, Color.yellow);






        //Debug.Log(_camera.WorldToViewportPoint(_boss.transform.position));




    }
}
