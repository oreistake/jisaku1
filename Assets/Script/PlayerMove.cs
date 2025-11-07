using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

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
    [SerializeField] private int _hp;

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
    void Start()
    {

        // FPSを60に設定
        Application.targetFrameRate = 60;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _damageTimeCount = 0;
        _bDamage = false;

        _animator = GetComponent<Animator>();

        isDeath = false;
        _pose = FindAnyObjectByType<Pose>();// シーン上のPoseを探して参照
    }
    void Update()
    {
        if (!_pose.isStop) { 
            ProcessInputs();
            Move();
            _Damage();
            if (!isDeath)
            {
                Shoot();
            }
        }
    }

    void ProcessInputs() // 入力処理
    {
        if (!isDeath)
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

    }

    void Move() // 移動
    {
        transform.Translate(moveDirection * _moveSpeed * Time.deltaTime,Space.World);
        _animator.SetBool("Walk", moveDirection.x != 0.0f || moveDirection.y != 0.0f);
    }

    void Shoot()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            if(!_bDamage)
            {
                _hp -= 1;
                _bDamage = true;
                _damageTimeCount = 0;

                if (_hp <= 0)
                {
                    //Destroy(gameObject);
                    _animator.SetBool("Death", true);
                    moveDirection = new Vector2(0, 0);
                    isDeath = true;
                }
            }
            
        }
    }

    private void _Damage()
    {
        if(!_bDamage)return;

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
