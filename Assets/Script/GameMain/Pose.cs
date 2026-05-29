using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pose : MonoBehaviour
{
    [SerializeField] bool _isStop; 
    [SerializeField] GameObject _panel;

    public bool isStop => _isStop;

    void Start()
    {
        _panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Onclick()
    {
        _isStop = !_isStop;

        if (_isStop)
        {
            Time.timeScale = 0f;
            _panel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            _panel.SetActive(false);
        }
    }


}
