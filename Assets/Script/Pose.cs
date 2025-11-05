using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pose : MonoBehaviour
{
    [SerializeField] bool _isStop; 

    public bool isStop => _isStop;
    // Start is called before the first frame update
    void Start()
    {
        _isStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        Stop();
    }

    private void Stop()
    {
        if(Input.GetKeyDown(KeyCode.P)&&!_isStop)
        {
            _isStop=true;
        }
        else if(Input.GetKeyDown(KeyCode.P)&& _isStop)
        {
            _isStop=false;
        }
        if(_isStop)
        {
            Time.timeScale=0f;
        }
        else if(!_isStop)
        {
            Time.timeScale = 1f;
        }

    }
}
