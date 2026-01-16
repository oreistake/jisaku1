using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    // プレイヤーのアニメーション制御用
    private Animator _animator;

    // プレイヤーの移動速度
    [SerializeField] private float _moveSpeed = 5f;

    // プレイヤーの移動方向
    private Vector2 moveDirection;

    // カメラの位置
    private Vector2 _camPos;

    // プレイヤーのHP
    [SerializeField] private float _maxHp;

    // 現在のHP
    public float _currentHp;

    // 死亡状態を判定するフラグ
    private bool isDeath;

    // 弾の速度
    [SerializeField] private float _bulletspeed = 3.0f;

    // 弾のPrefab
    [SerializeField] private GameObject _bullet;

    // 実際に生成された弾のインスタンスを格納する変数
    private GameObject _bulletIns;

    // マウスの座標を保持
    private Vector2 _mousePos;

    // プレイヤーからマウスへの角度ベクトル
    private Vector2 _angle;

    // ダメージを食らった時の点滅時間
    [SerializeField] private float _damageTime;

    // ダメージを食らった時の点滅周期
    [SerializeField] private float _damageCycle;

    // プレイヤーのスプライトを制御するためのコンポーネント
    private SpriteRenderer _spriteRenderer;

    // 現在の点滅経過時間
    private float _damageTimeCount;

    // 被ダメージ中かどうかを示すフラグ
    private bool _bDamage;

    private Pose _pose;

    public SpriteRenderer _hpBarFill;
    public GameObject _hpBarRoot;

    [SerializeField] float _hideDelay = 2f;

    private Coroutine _hideCoroutine;
    private Vector3 _hpBarOriginalScale;

    private Camera _mainCamera;
    private float _playerHalfWidth;
    private float _playerHalfHeight;

    void Start()
    {
        
        // FPSを60に設定
        Application.targetFrameRate = 60;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _damageTimeCount = 0;
        _bDamage = false;
        _currentHp = _maxHp;
        _animator = GetComponent<Animator>();

        isDeath = false;
        _pose = FindAnyObjectByType<Pose>();// シーン上のPoseを探して参照

        _hpBarRoot.SetActive(false); // 最初はSpriteRendererを非表示にする

        _hpBarOriginalScale = _hpBarFill.transform.localScale;

        _mainCamera = Camera.main;

        Bounds bounds = GetComponent<SpriteRenderer>().bounds;
        _playerHalfWidth = bounds.extents.x;
        _playerHalfHeight = bounds.extents.y;
    }
    void Update()
    {
        if (isDeath) return;
        if (_pose != null && _pose.isStop) return;

        ProcessInputs();
        Move();
        Damage();
        Shoot();
        UpdateHPBarPosition();

    }

    void LateUpdate()// Update関数の次に呼ばれる関数
    {
        Vector3 scale = _hpBarRoot.transform.localScale;
        scale.x = Mathf.Abs(scale.x);// 常に正の値にする
        _hpBarRoot.transform.localScale = scale;
    }

    void ProcessInputs() // 入力処理
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

    void Move() // 移動
    {
        transform.Translate(moveDirection * _moveSpeed * Time.deltaTime, Space.World);

        // カメラ範囲を取得（ワールド座標）
        Vector3 min = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 max = _mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // プレイヤー位置を制限
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, min.x + _playerHalfWidth, max.x - _playerHalfWidth);
        pos.y = Mathf.Clamp(pos.y, min.y + _playerHalfHeight, max.y - _playerHalfHeight);
        transform.position = pos;

        // アニメーション
        _animator.SetBool("Walk", moveDirection.x != 0.0f || moveDirection.y != 0.0f);

    }

    void Shoot() // 射撃
    {
        _mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 direction = (_mousePos - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            _bulletIns = Instantiate(_bullet, transform.position, Quaternion.Euler(0, 0, angle));
            _bulletIns.GetComponent<Rigidbody2D>().velocity = direction * _bulletspeed;
        }

    }
    public void TakeDamage(int damage)
    {
        if (_pose != null && _pose.isStop) return;
        if (isDeath) return;

        _currentHp -= damage;
        _currentHp = Mathf.Max(_currentHp, 0);
        UpdateHPBar();

        // HPバーを表示
        _hpBarRoot.SetActive(true);

        // 一定時間後に非表示にする
        if (_hideCoroutine != null)
            StopCoroutine(_hideCoroutine);
        _hideCoroutine = StartCoroutine(HideAfterDelay());

        // HPが0になったら死にモーションと動けなくする
        if (_currentHp <= 0)
        {
            //Destroy(gameObject);
            _animator.SetBool("Death", true);
            moveDirection = new Vector2(0, 0);
            isDeath = true;

            Invoke(nameof(Death), 3.5f);
        }


    }

    private void Death()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    void UpdateHPBar()
    {
        float fillAmount = _currentHp / _maxHp;
        _hpBarFill.transform.localScale = new Vector3(_hpBarOriginalScale.x * fillAmount,
                                                   _hpBarOriginalScale.y,
                                                   _hpBarOriginalScale.z);

        // 左端を固定する
        Vector3 pos = _hpBarFill.transform.localPosition;
        pos.x = -(_hpBarOriginalScale.x - _hpBarFill.transform.localScale.x) / 2f;
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
    private void OnTriggerEnter2D(Collider2D collision) // 当たった時の処理
    {
        /*if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BOSS"))
        {
            if (!_bDamage)
            {
                TakeDamage(1);
                _bDamage = true;
                _damageTimeCount = 0;
            }
        }*/
        if (_pose != null && _pose.isStop) return;
        if (isDeath) return;

        if (collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.CompareTag("BOSS"))
        {
            if (!_bDamage)
            {
                TakeDamage(1);
                _bDamage = true;
                _damageTimeCount = 0;
            }
        }
    }


    private void Damage()
    {
        if (!_bDamage) return;

        _damageTimeCount += Time.deltaTime;

        float value = Mathf.Repeat(_damageTimeCount, _damageCycle);
        _spriteRenderer.enabled = value >= _damageCycle * 0.5f;

        if(_damageTimeCount>=_damageTime)
        {
            _damageTimeCount = 0;
            _spriteRenderer.enabled = true;
            _bDamage = false;

        }
    }


}
