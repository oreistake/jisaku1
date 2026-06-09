using UnityEngine;

public class CircleSize : MonoBehaviour
{
    void Update()
    {

        transform.localScale -= Vector3.one * Time.deltaTime;

        if(transform.localScale.x <= 0 )
        {
            Destroy(gameObject);
        }

       
    }
}
