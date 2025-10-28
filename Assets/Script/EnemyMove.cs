using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Transform playerTr; // プレイヤーのTransform
    [SerializeField] float _speed = 2.0f;
    [SerializeField] private int _hp;

    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, playerTr.position) < 0.1f) return;

        transform.position = Vector2.MoveTowards(transform.position,
            new Vector2(playerTr.position.x,playerTr.position.y),
            _speed * Time.deltaTime);

        if (playerTr.position.x > transform.position.x)
        {
            // 右を向く
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")||collision.gameObject.CompareTag("Bullet"))
        {
            _hp -= 1;
            if (_hp < 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
