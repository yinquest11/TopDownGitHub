using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;


public class BackGroundMovement : MonoBehaviour
{
    public float ScrollSpeedMultiplier = 0.05f;
    private Transform _playerTransform;
    private PlayerMovemet _playerMovement;
    private SpriteRenderer _spriteRenderer;
    

    private void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovemet>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        

    }

    private void Update()
    {
        if(_playerTransform!= null)
        {
            _spriteRenderer.material.mainTextureOffset = new Vector2(_playerTransform.position.x, _playerTransform.position.y) * ScrollSpeedMultiplier;
        }
        
        

        //Debug.Log(_playerMovement._inputDirection);
        




    }










}
