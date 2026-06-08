using UnityEngine;

public class PosionGetPos : MonoBehaviour
{
    public void OnDestroy()
    {
        Vector2 pos = transform.position;
        Debug.Log("ç≈å„ÇÃpos"+transform.position);
    }
}
