using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // íeÇÃë¨ìx
    [SerializeField] private float _speed = 3.0f;
    // íeÇÃPreFabÇì¸ÇÍÇÈïœêî    
    [SerializeField] private GameObject _bullet;
    private GameObject _bulletIns;
    private Vector2 _mousePos;
    private Vector2 _angle;
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        _mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 direction = (_mousePos - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            _bulletIns = Instantiate(_bullet, transform.position, Quaternion.Euler(0, 0, angle));
            _bulletIns.GetComponent<Rigidbody2D>().velocity = direction * _speed;
        }

    }
}
