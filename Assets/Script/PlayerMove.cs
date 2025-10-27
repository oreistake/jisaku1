using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
     //private GameObject _bulletPrefab;
    private Animator _animator;
    [SerializeField] private float _moveSpeed = 5f;
    private Vector2 moveDirection;
    private Vector2 _camPos;
    [SerializeField] private int _hp;

    //[SerializeField] private float _shootTime;

    //[SerializeField] private float _shootCount;

    // Start is called before the first frame update

    void Start()
    {

        // FPSÇ60Ç…ê›íË
        Application.targetFrameRate = 60;

        //_shootCount = 0;
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        ProcessInputs();
        Move();
       
    }

    void ProcessInputs() // ì¸óÕèàóù
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if (moveX < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

    }

    void Move() // à⁄ìÆ
    {
        transform.Translate(moveDirection * _moveSpeed * Time.deltaTime,Space.World);
        _animator.SetBool("Walk", moveDirection.x != 0.0f || moveDirection.y != 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _hp -= 1;
            if (_hp < 0)
            {
                Destroy(gameObject);
            }
        }
    }




}
