using UnityEngine;
using UnityEngine.UIElements;

public class BibleAttack : MonoBehaviour
{

    [SerializeField] private GameObject _Bible;
    [SerializeField] private GameObject _Player;
    [SerializeField] private float _radius;
    [SerializeField] private float _speed;
    [SerializeField] private float _angle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _Bible.transform.position = _Player.transform.position;
        _angle += _speed * Time.deltaTime * Mathf.PI * 2f;

        float x = Mathf.Cos(_angle) * _radius;
        float z = Mathf.Sin(_angle) * _radius;
        transform.position = _Bible.transform.position + new Vector3(x, 0f, z);
    }
}

