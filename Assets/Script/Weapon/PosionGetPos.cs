using UnityEngine;

public class PosionGetPos : MonoBehaviour
{
    [SerializeField] GameObject Circle;
    private Vector2 posionPos;
   
    public void OnDestroy()
    {
        posionPos = transform.position;

        Instantiate(Circle,posionPos,Quaternion.identity);
       
    }
}
