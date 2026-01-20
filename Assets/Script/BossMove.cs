using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossMove : MonoBehaviour
{
    Transform playerTr;
    [SerializeField] float _speed = 2.0f;
    [SerializeField] private int _currentHp;
    [SerializeField] private int _maxHp;

    bool _death = false;

    SpriteRenderer _spriteRenderer;
    Animator _animator;
    private Pose _pose;

    [SerializeField] float _hideDelay = 2f;
    public SpriteRenderer _hpBarFill;
    public GameObject _hpBarRoot;
    private Coroutine _hideCoroutine;
    private Vector3 _hpBarOriginalScale;
    private Vector3 _hpBarOriginalPos;

    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _pose = FindAnyObjectByType<Pose>();
        
        _currentHp = _maxHp;

        _hpBarRoot.SetActive(false); // 最初はSpriteRendererを非表示にする

        _hpBarOriginalScale = _hpBarFill.transform.localScale;
        _hpBarOriginalPos = _hpBarFill.transform.localPosition;


    }

    void Update()
    {
        if (_pose.isStop || _death) return;
        
        Move();
        UpdateHPBarPosition();
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
            _currentHp--;

            _hpBarRoot.SetActive(true);
            UpdateHPBar();

            if (_currentHp <= 0)
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

    void UpdateHPBar()
    {
        float fillAmount = (float)_currentHp / _maxHp;

        Vector3 scale = _hpBarFill.transform.localScale;
        scale.x = _hpBarOriginalScale.x * fillAmount;
        _hpBarFill.transform.localScale = scale;

        Vector3 pos = _hpBarOriginalPos;
        pos.x = _hpBarOriginalPos.x
              - (_hpBarOriginalScale.x - scale.x) / 2f;
        _hpBarFill.transform.localPosition = pos;

    }

    void UpdateHPBarPosition()
    {
        if (_hpBarRoot.activeSelf)
        {
            Vector3 hpPos = transform.position + new Vector3(0, 1.0f, 0); // 上方向に1単位
            _hpBarRoot.transform.position = hpPos;

            // 回転は固定（左右反転の影響を受けない）
            _hpBarRoot.transform.rotation = Quaternion.identity;

            // スケールも反転を打ち消す
            Vector3 scale = _hpBarRoot.transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            _hpBarRoot.transform.localScale = scale;
        }
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(_hideDelay);
        _hpBarRoot.SetActive(false);
    }

}


