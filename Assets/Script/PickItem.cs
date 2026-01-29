using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ここで所持数を増やすなど
            Debug.Log("アイテム取得！");

            Destroy(gameObject);
        }
    }
}
