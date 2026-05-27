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
    // �v���C���[�̃A�j���[�V��������p
    private Animator _animator;
    [SerializeField] GameObject _AxePrefab;
    GameObject _Axeobj;
    public bool axePick;
    [SerializeField]private int axeMaxTime;
    private int axeTime;
    private Vector2 axePosition;
    // �v���C���[�̈ړ����x
    [SerializeField] private float _moveSpeed = 5f;

    // �v���C���[�̈ړ�����
    private Vector2 moveDirection;
    [SerializeField]private Vector2 currentPos;

    // �J�����̈ʒu
    private Vector2 _camPos;

    // �v���C���[��HP
    [SerializeField] private float _maxHp;

    // ���݂�HP
    public float _currentHp;

    // ���S��Ԃ𔻒肷��t���O
    private bool isDeath;

    // �e�̑��x
    [SerializeField] private float _bulletspeed = 3.0f;

    // �e��Prefab
    [SerializeField] private GameObject _bullet1;
    [SerializeField] private GameObject _bullet2;
    [SerializeField] private GameObject _bullet3;

    // ���ۂɐ������ꂽ�e�̃C���X�^���X���i�[����ϐ�
    private GameObject _bulletIns;

    // �}�E�X�̍��W��ێ�
    private Vector2 _mousePos;

    // �v���C���[����}�E�X�ւ̊p�x�x�N�g��
    private Vector2 _angle;

    // �_���[�W��H��������̓_�Ŏ���
    [SerializeField] private float _damageTime;

    // �_���[�W��H��������̓_�Ŏ���
    [SerializeField] private float _damageCycle;

    // �v���C���[�̃X�v���C�g�𐧌䂷�邽�߂̃R���|�[�l���g
    private SpriteRenderer _spriteRenderer;

    // ���݂̓_�Ōo�ߎ���
    private float _damageTimeCount;

    // ��_���[�W�����ǂ����������t���O
    private bool _bDamage;

    private Pose _pose;
    private bool _isLevelUp   = false;

    //private bool _shootCheck;
    [SerializeField] private int _shootMaxCount;
    [SerializeField] private int _shootCount;


    public SpriteRenderer _hpBarFill;
    public GameObject _hpBarRoot;

    [SerializeField] GameObject _levelUpPanel;

    //[SerializeField] GameObject _HealBotton;
    [SerializeField] private Animator _levelUpAnimator;

    [SerializeField] float _hideDelay = 2f;

    private Coroutine _hideCoroutine;
    private Vector3 _hpBarOriginalScale;

    private Camera _mainCamera;
    private float _playerHalfWidth;
    private float _playerHalfHeight;

    private AudioSource _audioSource;
    [SerializeField] AudioClip _shotSe;
    [SerializeField] AudioClip _DamageSe;
    [SerializeField] AudioClip _DeathSe;
    [SerializeField] AudioClip _LevelUpSe;

    [SerializeField] private UnityEngine.UI.Slider _gauge; // �Q�[�W�o�[
    public float _maxGaugeValue = 5; // �ő�l
    [SerializeField] private float _currentGaugeValue; // ���ݒl
    private float _velocity = 0; // �X���[�Y�ȕω��p
    private bool _isMaxGauge = false;

    void Start()
    {
        
        // FPS��60�ɐݒ�
        Application.targetFrameRate = 60;

        axePick = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _damageTimeCount = 0;
        _bDamage = false;
        _currentHp = _maxHp;
        _animator = GetComponent<Animator>();
        _levelUpAnimator = GetComponent<Animator>();
        isDeath = false;
        _pose = FindAnyObjectByType<Pose>();// �V�[�����Pose��T���ĎQ��
        
        
        //
        //_shootCheck = false;
        //_shootMaxCount = 0;
        _shootCount = 0;
        //

        _hpBarRoot.SetActive(false); // �ŏ���SpriteRenderer���\���ɂ���

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
        //Shoot();
        UpdateHPBarPosition();
        
        _AxePrefab.transform.position = gameObject.transform.position;
        axePosition = _AxePrefab.transform.position;
    }

    private void FixedUpdate()
    {
        _shootCount++;
        if(_shootCount >= _shootMaxCount)
        {
            Shoot();
            _shootCount = 0;
            //_shootCheck = true;
        }
        if (axePick)
        {
            axeTime++;
                _AxePrefab.transform.Rotate(0,0,-10);
            if (axeTime >= axeMaxTime)
            {
                axeTime = 0;
                //Axe.transform.Rotate(0, 0, 10);
                AttackSword();

            }
        }
    }


    void LateUpdate()// Update�֐��̎��ɌĂ΂��֐�
    {
        Vector3 scale = _hpBarRoot.transform.localScale;
        scale.x = Mathf.Abs(scale.x);// ��ɐ��̒l�ɂ���
        _hpBarRoot.transform.localScale = scale;
    }

    void ProcessInputs() // ���͏���
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

    void Move() // �ړ�
    {

        if (Time.timeScale == 0f) return;
        if (_pose != null && _pose.isStop) return;


        transform.Translate(moveDirection * _moveSpeed * Time.deltaTime, Space.World);

        // �J�����͈͂��擾�i���[���h���W�j
        Vector3 min = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 max = _mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // �v���C���[�ʒu�𐧌�
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, min.x + _playerHalfWidth, max.x - _playerHalfWidth);
        pos.y = Mathf.Clamp(pos.y, min.y + _playerHalfHeight, max.y - _playerHalfHeight);
        transform.position = pos;

        // �A�j���[�V����
        _animator.SetBool("Walk", moveDirection.x != 0.0f || moveDirection.y != 0.0f);

    }

    void Shoot() // �ˌ�
    {
        if (Time.timeScale == 0f) return;
        if (_pose != null && _pose.isStop) return;

        _mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //if (Input.GetMouseButtonDown(0))
        //if (_shootCheck)
        //{
        Vector2 direction = (_mousePos - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _bulletIns = Instantiate(_bullet1, transform.position, Quaternion.Euler(0, 0, angle));
        _bulletIns.GetComponent<Rigidbody2D>().linearVelocity = direction * _bulletspeed;
        _audioSource.PlayOneShot(_shotSe);
        //}

    }
    public void TakeDamage(int damage)
    {
        if (_pose != null && _pose.isStop) return;
        if(_isLevelUp) return;
        if (isDeath) return;

        _currentHp -= damage;
        _currentHp = Mathf.Max(_currentHp, 0);
        UpdateHPBar();

        // HP�o�[��\��
        _hpBarRoot.SetActive(true);

        // ��莞�Ԍ�ɔ�\���ɂ���
        if (_hideCoroutine != null)
            StopCoroutine(_hideCoroutine);
        _hideCoroutine = StartCoroutine(HideAfterDelay());

        // HP��0�ɂȂ����玀�Ƀ��[�V�����Ɠ����Ȃ�����
        if (_currentHp <= 0)
        {
            //Destroy(gameObject);
            _animator.SetBool("Death", true);
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

        // ���[���Œ肷��
        Vector3 pos = _hpBarFill.transform.localPosition;
        pos.x = -(_hpBarOriginalScale.x - _hpBarFill.transform.localScale.x) / 2f;
        _hpBarFill.transform.localPosition = pos;
    }

    void UpdateHPBarPosition()
    {
        if (_hpBarRoot.activeSelf)
        {
            Vector3 hpPos = transform.position + new Vector3(0, 1.0f, 0); // �������1�P��
            _hpBarRoot.transform.position = hpPos;

            // ��]�͌Œ�i���E���]�̉e�����󂯂Ȃ��j
            _hpBarRoot.transform.rotation = Quaternion.identity;

            // �X�P�[�������]��ł�����
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
    private void OnTriggerEnter2D(Collider2D collision) // �����������̏���
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
            if (_currentGaugeValue < _maxGaugeValue)
            {
                _currentGaugeValue++;
                Destroy(collision.gameObject);
            }
            if(_currentGaugeValue > _maxGaugeValue)
            {
                _maxGaugeValue = 0;
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

    /// <summary>
    /// �Q�[�W��MAX�ɂȂ�����
    /// </summary>
    void CheckGaugeMax()
    {
        if (_currentGaugeValue >= _maxGaugeValue && !_isMaxGauge)
        {
            _isMaxGauge = true;
            _isLevelUp = true;

            OnGaugeMax();
            ResetGauge();
        }
    }
    /// <summary>
    /// �Q�[�W��MAX�ɂȂ����Ƃ��̏���
    /// </summary>
    void OnGaugeMax()
    {

        _audioSource.PlayOneShot(_LevelUpSe);

        _levelUpPanel.SetActive(true);

        _levelUpAnimator.SetBool("Move", true);

        Time.timeScale = 0.0f;
    }

    /// <summary>
    /// �Q�[�W�����Z�b�g
    /// </summary>
    void ResetGauge()
    {
        _currentGaugeValue = 0;
        _isMaxGauge = false;
    }

    public void HealHp()
    {
        // HP��
        _currentHp += _maxHp;   // �S�񕜂������ꍇ
        if (_currentHp > _maxHp)
        {
            _currentHp = _maxHp;
        }

        UpdateHPBar();

        LevelUp();


    }

    public void AttackSword()
    {
        axePick = true;

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

    void LevelUp()
    {
        _levelUpPanel.SetActive(false);

        _isLevelUp = false;

        Time.timeScale = 1.0f;
    }
    //public void AttackPlus()
    //{

    //    //Vector2 direction = (_mousePos - (Vector2)transform.position).normalized;
    //    //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    //    //_bulletIns = Instantiate(_bullet2, transform.position, Quaternion.Euler(100, 0, angle));
    //    //_bulletIns.GetComponent<Rigidbody2D>().velocity = direction * _bulletspeed;

    //    //Vector2 direction1 = (_mousePos - (Vector2)transform.position).normalized;
    //    //float angle1 = Mathf.Atan2(direction1.y, direction1.x) * Mathf.Rad2Deg;

    //    //_bulletIns = Instantiate(_bullet2, transform.position, Quaternion.Euler(-100, 0, angle));
    //    //_bulletIns.GetComponent<Rigidbody2D>().velocity = direction1 * _bulletspeed;


    //    // �p�l�������
    //    _levelUpPanel.SetActive(false);

    //    _isLevelUp = false;

    //    // �Q�[���ĊJ
    //    Time.timeScale = 1.0f;
    //}
}
