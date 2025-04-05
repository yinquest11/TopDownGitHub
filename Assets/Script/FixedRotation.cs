using UnityEngine;

public class FixedRotation : MonoBehaviour
{

    private Transform _transform;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _transform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_transform != null)
        {
            gameObject.transform.position = new Vector3(_transform.position.x, _transform.position.y, _transform.position.z + 10f);
        }
        if(_transform == null)
        {
            Destroy(gameObject);
        }
        

        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            _spriteRenderer.flipX = true;
        }
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            _spriteRenderer.flipX = false;
        }

    }
}
