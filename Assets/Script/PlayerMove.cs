using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator _animator;
    public float moveSpeed = 5f;
    private Vector2 moveDirection;

    // Start is called before the first frame update

    void Start()
    {

        // FPSÇ60Ç…ê›íË
        Application.targetFrameRate = 60;

        
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        ProcessInputs();
        Move();
    }

    void ProcessInputs()
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

    void Move()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime,Space.World);
        _animator.SetBool("Walk", moveDirection.x != 0.0f || moveDirection.y != 0.0f);
    }
}
