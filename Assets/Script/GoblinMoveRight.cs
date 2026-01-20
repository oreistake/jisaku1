using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMoveRight : MonoBehaviour
{

    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] float _hp = 1f;
    float _MaxposX = 62f;
    Transform playerTr; // ÉvÉåÉCÉÑÅ[ÇÃTransform

    private SpriteRenderer _spriteRenderer;

    private Pose _pose;

    private Rigidbody2D _rigid2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {
            _hp -= 1;
            if (_hp <= 0)
            {

                Destroy(gameObject);
            }
        }
    }

    private void Move()
    {
        _rigid2D.velocity = Vector2.right * _moveSpeed;

        if (transform.position.x > _MaxposX)
        {
            Destroy(gameObject);
        }
    }

}


