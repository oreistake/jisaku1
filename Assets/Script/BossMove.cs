using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossMove : MonoBehaviour
{
    Transform playerTr;
    [SerializeField] float _speed = 2.0f;
    [SerializeField] private int _hp;

    bool _death = false;

    SpriteRenderer _spriteRenderer;
    Animator _animator;
    private Pose _pose;
    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _pose = FindAnyObjectByType<Pose>();
    }

    void Update()
    {
        if (_pose.isStop || _death) return;
        
        Move();
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, playerTr.position) < 0.1f) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            playerTr.position,
            _speed * Time.deltaTime);

        _spriteRenderer.flipX = playerTr.position.x < transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_death) return;

        if (collision.CompareTag("Bullet") || collision.CompareTag("Player"))
        {
            _hp--;

            if (_hp <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        _death = true;

        DestroyAllEnemies();

        _animator.SetBool("Death", true);
        Invoke(nameof(SceneAnime), 1.5f);
        Invoke(nameof(SceneTr), 2.5f);
    }

    void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }


    void SceneTr()
    {
        SceneManager.LoadScene("GameClearScene");
    }

    void SceneAnime()
    {
        _animator.SetBool("Transition", true);
    }
}


