using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
//using UnityEngine.UIElements;
//using Slider = UnityEngine.UI.Slider;


public class PlayerMove : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////////////////////
    ///                                 プレイヤー関連
    ////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// プレイヤーのアニメーション
    /// </summary>
    private Animator _PlayerAnimator;

    /// <summary>
    /// プレイヤーの移動速度
    /// </summary>
    [SerializeField] private float _moveSpeed = 5f;

    /// <summary>
    /// プレイヤーの移動方向
    /// </summary>
    private Vector2 moveDirection;

    /// <summary>
    /// プレイヤーの最大HP
    /// </summary>
    [SerializeField] private float _maxHp;

    /// <summary>
    /// プレイヤーの現在のHP
    /// </summary>
    public float _currentHp;

    /// <summary>
    /// プレイヤーが死亡したかどうか
    /// </summary>
    private bool isDeath;

    /// <summary>
    /// プレイヤーがダメージを食らった時の無敵時間
    /// </summary>
    [SerializeField] private float _damageTime;

    /// <summary>
    /// プレイヤーがダメージを食らった時の無敵時間
    /// </summary>
    [SerializeField] private float _damageCycle;

    /// <summary>
    /// プレイヤーがダメージを食らった時の無敵時間
    /// </summary>
    [SerializeField] private float _damageTimeCount;

    /// <summary>
    /// プレイヤーのスプライトレンダラー
    /// </summary>
    private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// プレイヤーがダメージを食らったかどうか
    /// </summary>
    private bool _bDamage;


    ////////////////////////////////////////////////////////////////////////////////
    ///                                 HP関連
    ////////////////////////////////////////////////////////////////////////////////


    /// <summary>
    /// HpBarのスプライトレンダラー
    /// </summary>
    public SpriteRenderer _hpBarFill;

    /// <summary>
    /// HPバー
    /// </summary>
    public GameObject _hpBarRoot;

    /// <summary>
    /// HPバーが隠れるまでの時間
    /// </summary>
    [SerializeField] float _hideDelay = 2f;

    /// <summary>
    /// HPバーのスケール
    /// </summary>
    private Vector3 _hpBarOriginalScale;

    ////////////////////////////////////////////////////////////////////////////////
    ///                                 マウスカーソル関連
    ////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// マウスカーソルのポジション
    /// </summary>
    private Vector2 _mousePos;

    ////////////////////////////////////////////////////////////////////////////////
    ///                                 弾関連
    ////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// 弾のオブジェクト
    /// </summary>
    [SerializeField] private GameObject _bullet;

    /// <summary>
    /// 生成されたときの弾の保存場所
    /// </summary>
    private GameObject _bulletIns;

    /// <summary>
    /// 弾の速度
    /// </summary>
    [SerializeField] private float _bulletspeed = 3.0f;

    /// <summary>
    /// 弾の発射時間
    /// </summary>
    [SerializeField] private int _shootMaxCount;

    /// <summary>
    /// 弾の発射カウント
    /// </summary>
    [SerializeField] private int _shootCount;

    ////////////////////////////////////////////////////////////////////////////////
    ///                                 斧関連
    ////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// 斧オブジェクト
    /// </summary>
    [SerializeField] GameObject _AxePrefab;

    /// <summary>
    /// 生成されたときの斧の保存場所
    /// </summary>
    GameObject _Axeobj;

    /// <summary>
    /// 斧をとったかどうか
    /// </summary>
    public bool _axePick;

    /// <summary>
    /// 斧が生成される時間
    /// </summary>
    [SerializeField]private int _GenerateAxeTime = 100;

    /// <summary>
    /// 斧が生成されるまでの時間
    /// </summary>
    private int _GeneratedAxeTime;

    /// <summary>
    /// 斧のポジション
    /// </summary>
    private Vector2 axePosition;

    /// <summary>
    /// 斧のレベル
    /// </summary>
    public int axeLevel = 0;
  

    ///////////////////////////////////////////////////////////////////////////////////////////
    /// ポーション関連
    ///////////////////////////////////////////////////////////////////////////////////////////

    // ポーションのプレハブ
    [SerializeField] GameObject PosionPreFab = null;

    // ポーションのポジション
    [SerializeField] private Vector2 PosionPos;

    // ポーションの移動スピード
    [SerializeField] private float PosionSpeed;

    public bool _PosionPick;

    /// <summary>
    /// ポーションが生成される時間
    /// </summary>
    [SerializeField] private int _GeneratePosionTime = 100;

    /// <summary>
    /// ポーションが生成されるまでの時間
    /// </summary>
    private int _GeneratedPosionTime;

    public int posionLevel = 0;

    /// <summary>
    /// ポーズ参照
    /// </summary>
    private Pose _pose;

    /// <summary>
    /// レベルアップしているかどうか
    /// </summary>
    private bool _isLevelUp   = false;

   

   
    /// <summary>
    /// レベルアップした時のパネル
    /// </summary>
    [SerializeField] GameObject _levelUpPanel;

    /// <summary>
    /// レベルアップパネルのアニメーション
    /// </summary>
    [SerializeField] private Animator _levelUpAnimator;

  
    /// <summary>
    /// HPバーが隠れている時のコルーチン
    /// </summary>
    private Coroutine _hideCoroutine;

   
    /// <summary>
    /// シーン上のカメラ
    /// </summary>
    private Camera _mainCamera;


    private float _playerHalfWidth;
    private float _playerHalfHeight;

    private AudioSource _audioSource;
    [SerializeField] AudioClip _shotSe;
    [SerializeField] AudioClip _DamageSe;
    [SerializeField] AudioClip _DeathSe;
    [SerializeField] AudioClip _LevelUpSe;

    [SerializeField] private UnityEngine.UI.Slider _gauge; 
    public float _maxGaugeValue; 
    [SerializeField] private float _currentGaugeValue; 
    private float _velocity = 0; 
    public bool _isMaxGauge = false;

    [SerializeField] LevelUpSelect[] _levelUpSelect;

    void Start()
    {
        
        // 60FPS想定
        Application.targetFrameRate = 60;

        _axePick = false;

        _PosionPick = false;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        _damageTimeCount = 0;

        _bDamage = false;

        _currentHp = _maxHp;

        _PlayerAnimator = GetComponent<Animator>();

        _levelUpAnimator = GetComponent<Animator>();

        isDeath = false;

        _pose =GetComponent<Pose>();

        _shootCount = 0;

        _hpBarRoot.SetActive(false);

        _hpBarOriginalScale = _hpBarFill.transform.localScale;

        _mainCamera = Camera.main;

        Bounds bounds = GetComponent<SpriteRenderer>().bounds;
        _playerHalfWidth = bounds.extents.x;
        _playerHalfHeight = bounds.extents.y;

        _audioSource = GetComponent<AudioSource>();

        _currentGaugeValue = 0;
        _gauge.maxValue = _maxGaugeValue;
        _gauge.value = 0;

        _levelUpPanel.SetActive(false);

    }
    void Update()
    {
        if (isDeath) return;
        if (_pose != null && _pose.isStop) return;
        if(_isLevelUp) return;

        _gauge.value = Mathf.SmoothDamp(_gauge.value, _currentGaugeValue, ref _velocity, 0.1f);

        CheckGaugeMax();
        ProcessInputs();
        Move();
        Damage();
        UpdateHPBarPosition();
        
        _AxePrefab.transform.position = gameObject.transform.position;
        axePosition = _AxePrefab.transform.position;

        PosionPreFab.transform.position = gameObject.transform.position;
        PosionPos = PosionPreFab.transform.position;
    }

    private void FixedUpdate()
    {
        _shootCount++;
        if (_shootCount >= _shootMaxCount)
        {
            Shoot();
            _shootCount = 0;
        }

        if (_axePick)
        {
            AxeLevel();
        }
        if(_PosionPick)
        {
            PosionLevel();
        }
    }

    void LateUpdate()
    {
        Vector3 scale = _hpBarRoot.transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        _hpBarRoot.transform.localScale = scale;
    }

    /// <summary>
    /// プレイヤーの移動取得
    /// </summary>
    void ProcessInputs() 
    {

        if (Time.timeScale == 0f) return;
        if (_pose != null && _pose.isStop) return;

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

    /// <summary>
    /// プレイヤーの移動
    /// </summary>
    void Move()
    {

        if (Time.timeScale == 0f) return;
        if (_pose != null && _pose.isStop) return;

        transform.Translate(moveDirection * _moveSpeed * Time.deltaTime, Space.World);

        Vector3 min = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 max = _mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, min.x + _playerHalfWidth, max.x - _playerHalfWidth);
        pos.y = Mathf.Clamp(pos.y, min.y + _playerHalfHeight, max.y - _playerHalfHeight);
        transform.position = pos;

        _PlayerAnimator.SetBool("Walk", moveDirection.x != 0.0f || moveDirection.y != 0.0f);

    }

    void Shoot()
    {
        if (Time.timeScale == 0f) return;
        if (_pose != null && _pose.isStop) return;

        _mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (_mousePos - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _bulletIns = Instantiate(_bullet, transform.position, Quaternion.Euler(0, 0, angle));
        _bulletIns.GetComponent<Rigidbody2D>().linearVelocity = direction * _bulletspeed;
        _audioSource.PlayOneShot(_shotSe);

    }
    public void TakeDamage(int damage)
    {
        if (_pose != null && _pose.isStop) return;
        if(_isLevelUp) return;
        if (isDeath) return;

        _currentHp -= damage;
        _currentHp = Mathf.Max(_currentHp, 0);
        UpdateHPBar();

        _hpBarRoot.SetActive(true);

        if (_hideCoroutine != null)
            StopCoroutine(_hideCoroutine);
        _hideCoroutine = StartCoroutine(HideAfterDelay());

        if (_currentHp <= 0)
        {
            //Destroy(gameObject);
            _PlayerAnimator.SetBool("Death", true);
            moveDirection = new Vector2(0, 0);
            isDeath = true;
            _audioSource.PlayOneShot(_DeathSe);
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

        Vector3 pos = _hpBarFill.transform.localPosition;
        pos.x = -(_hpBarOriginalScale.x - _hpBarFill.transform.localScale.x) / 2f;
        _hpBarFill.transform.localPosition = pos;
    }

    void UpdateHPBarPosition()
    {
        if (_hpBarRoot.activeSelf)
        {
            Vector3 hpPos = transform.position + new Vector3(0, 1.0f, 0); 
            _hpBarRoot.transform.position = hpPos;

            _hpBarRoot.transform.rotation = Quaternion.identity;

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (_pose != null && _pose.isStop) return;
        if(_isLevelUp) return;
        if (isDeath) return;

        if (collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.CompareTag("BOSS"))
        {
            if (!_bDamage)
            {
                TakeDamage(1);
                _audioSource.PlayOneShot(_DamageSe);
                _bDamage = true;
                _damageTimeCount = 0;
            }
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            if (_currentGaugeValue < _gauge.maxValue)
            {
                _currentGaugeValue++;
                Destroy(collision.gameObject);
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

 
    void CheckGaugeMax()
    {
        if (_currentGaugeValue >= _gauge.maxValue && !_isMaxGauge)
        {
            _isMaxGauge = true;
            _isLevelUp = true;
            for (int i = 0; i < _levelUpSelect.Length; i++) 
            {
                _levelUpSelect[i].RandomSelect();
            }
            OnGaugeMax();
            ResetGauge();
        }
    }
    /// <summary>
    /// ゲージが最大になった時
    /// </summary>
    void OnGaugeMax()
    {

        _audioSource.PlayOneShot(_LevelUpSe);

        _levelUpPanel.SetActive(true);

        _levelUpAnimator.SetBool("Move", true);

        Time.timeScale = 0.0f;
    }

    /// <summary>
    /// ゲージをリセットする
    /// </summary>
    void ResetGauge()
    {
        _gauge.maxValue += 10;
        _currentGaugeValue = 0;
        _isMaxGauge = false;
    }

    public void HealHp()
    {
        _currentHp += _maxHp;
        if (_currentHp > _maxHp)
        {
            _currentHp = _maxHp;
        }

        UpdateHPBar();

        LevelUp();

    }

    public void AttackSword()
    {
        _axePick = true;

        _Axeobj = Instantiate(_AxePrefab, axePosition, Quaternion.identity);
        Rigidbody2D rb2d = _Axeobj.GetComponent<Rigidbody2D>();
        rb2d.linearVelocity = new Vector2(Random.Range(-3,3),8);
        if(rb2d.linearVelocity.x <= 0)
        {
            rb2d.angularVelocity = 360;
        }
        if (rb2d.linearVelocity.x > 0)
        {
            rb2d.angularVelocity = -360;
        }
        Destroy(_Axeobj ,5.0f);
        LevelUp();

    }

    void AxeLevel()
    {
        _GeneratedAxeTime++;
        _AxePrefab.transform.Rotate(0, 0, -10);
        
        if (axeLevel == 0) _GenerateAxeTime = 100;
        if (axeLevel == 1) _GenerateAxeTime = 90;
        if (axeLevel == 2) _GenerateAxeTime = 80;
        if (axeLevel == 3) _GenerateAxeTime = 70;
        if (axeLevel == 4) _GenerateAxeTime = 60;

        if (_GeneratedAxeTime >= _GenerateAxeTime)
        {
            _GeneratedAxeTime = 0;
            AttackSword();
        }
    }

    public void AttackPosion()
    {
        _PosionPick = true;
        PosionPos.y += 10;

        GameObject posion = Instantiate(PosionPreFab,PosionPos,Quaternion.identity);
        Rigidbody2D rd2d = posion.GetComponent<Rigidbody2D>();
        rd2d.linearVelocity = new Vector2(Random.Range(-6, 6), Random.Range(-10, -5));
        float deathTIme = Random.Range(0.8f, 1.2f);
        Destroy(posion,deathTIme);
        LevelUp();
    }
   
    void PosionLevel()
    {
        _GeneratedPosionTime++;

        if (posionLevel == 0) _GeneratePosionTime = 100;
        if (posionLevel == 1) _GeneratePosionTime = 90;
        if (posionLevel == 2) _GeneratePosionTime = 80;
        if (posionLevel == 3) _GeneratePosionTime = 70;
        if (posionLevel == 4) _GeneratePosionTime = 60;

        if (_GeneratedPosionTime >= _GeneratePosionTime)
        {
            _GeneratedPosionTime = 0;
            AttackPosion();
        }
    }
   

    void LevelUp()
    {
        _levelUpPanel.SetActive(false);

        _isLevelUp = false;

        Time.timeScale = 1.0f;
    }
  
}
