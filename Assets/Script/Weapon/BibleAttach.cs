using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class BibleAttach : MonoBehaviour
{

    [SerializeField] private GameObject _Bible;
    [SerializeField] private GameObject _Player;
    [SerializeField] private float _radius;
    [SerializeField] private float _speed;
    [SerializeField] private float _angle;
    private Vector3 _position;
    [SerializeField] private bool a;
    private void Start()
    {
        
    }
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))a=!a;

        if(a)
        {
            //_Bible.SetActive(true);
            _Bible.transform.position = _Player.transform.position;
            _position= _Bible.transform.position;
            _angle += _speed * Time.deltaTime * Mathf.PI * 2f;

            float x = Mathf.Cos(_angle) * _radius;
            float y = Mathf.Sin(_angle) * _radius;
            _Bible.transform.position = _position + new Vector3(x, y, 0f);
          
        }
        else
        {
            _Bible.SetActive(false);
        }
       
    }

 
}

