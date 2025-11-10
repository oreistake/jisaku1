using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    
    private GameObject player;

    private float minX = -22, maxX = 22, minY = -11, maxY = 11;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("luchador-idle-right_0");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null) return;

        Vector3 playerPos = player.transform.position;

        // ÉJÉÅÉâà íuÇêßå¿ïtÇ´Ç≈í«è]
        float clampX = Mathf.Clamp(playerPos.x, minX, maxX);
        float clampY = Mathf.Clamp(playerPos.y, minY, maxY);

        transform.position = new Vector3(clampX, clampY, transform.position.z);
    }


}
