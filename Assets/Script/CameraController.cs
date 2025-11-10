using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public GameObject player; // 追従するプレイヤー
    public Tilemap backgroundTilemap; // Tilemap をインスペクターで指定

    private Camera cam;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        cam = Camera.main;

        // Tilemap の範囲を取得
        Bounds bounds = backgroundTilemap.localBounds;
        minX = bounds.min.x;
        maxX = bounds.max.x;
        minY = bounds.min.y;
        maxY = bounds.max.y;
    }

    void LateUpdate()
    {
        if (player == null || cam == null) return;

        // カメラの表示範囲
        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;

        // プレイヤー位置をClamp
        Vector3 playerPos = player.transform.position;
        float clampX = Mathf.Clamp(playerPos.x, minX + camWidth / 2f, maxX - camWidth / 2f);
        float clampY = Mathf.Clamp(playerPos.y, minY + camHeight / 2f, maxY - camHeight / 2f);

        // カメラ移動
        transform.position = new Vector3(clampX, clampY, transform.position.z);
    }
}
