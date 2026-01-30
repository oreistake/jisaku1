using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pose : MonoBehaviour
{
    [SerializeField] bool _isStop; 
    [SerializeField] GameObject _panel;

    public bool isStop => _isStop;
    // Start is called before the first frame update
    //void Start()
    //{
    //    _isStop = false;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    Stop();
    //}

    //private void Stop()
    //{
    //    if(Input.GetKeyDown(KeyCode.P)&&!_isStop)
    //    {
    //        _isStop=true;
    //    }
    //    else if(Input.GetKeyDown(KeyCode.P)&& _isStop)
    //    {
    //        _isStop=false;
    //    }
    //    if(_isStop)
    //    {
    //        Time.timeScale=0.0f;
    //    }
    //    else if(!_isStop)
    //    {
    //        Time.timeScale = 1.0f;
    //    }

    //}

    //void Start()
    //{
    //    _panel.SetActive(false);
    //    Time.timeScale = 1f;
    //}

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        _isStop = !_isStop; // êÿÇËë÷Ç¶

    //        if (_isStop)
    //        {
    //            _panel.SetActive(true);
    //            Time.timeScale = 0.0f;
    //        }
    //        else
    //        {
    //            _panel.SetActive(false);
    //            Time.timeScale = 1.0f;
    //        }
    //    }
    //}

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
