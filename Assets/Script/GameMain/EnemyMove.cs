using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Transform playerTr; // プレイヤーのTransform
    Transform KnockBackpos;
    [SerializeField] float _speed = 2.0f;
    [SerializeField] private int _hp;
    bool _dead = false;
    public GameObject dropItem;
    private SpriteRenderer _spriteRenderer;

    private Pose _pose;

    private AudioSource _audioSource;
    [SerializeField] AudioClip _deathSe;
    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _pose = FindAnyObjectByType<Pose>();
        _audioSource = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_pose != null && !_pose.isStop)
        {
            Move();
        }
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
        if (collision.gameObject.CompareTag("Bullet")||collision.gameObject.CompareTag("Weapon"))
        {
            _hp -= 1;
            if (_hp <= 0&&!_dead)
            {
                _dead = true;
                _speed = 0;
                Instantiate(dropItem, transform.position, Quaternion.identity);
                _audioSource.PlayOneShot(_deathSe);
                Invoke(nameof(Delete), 0.1f);
            }
        }
        
    }

    void Delete()
    {

        Destroy(gameObject);
    }

}
