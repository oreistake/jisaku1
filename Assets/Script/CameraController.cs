using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("luchador-idle-right_0" +
            "");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos=this.player.transform.position;
        transform.position = new Vector3(
            playerPos.x, playerPos.y, transform.position.z);
    }
}
