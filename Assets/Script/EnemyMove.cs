using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Transform playerTr; // ÉvÉåÉCÉÑÅ[ÇÃTransform
    [SerializeField] float _speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, playerTr.position) < 0.1f) return;

        transform.position = Vector2.MoveTowards(transform.position,
            new Vector2(playerTr.position.x,playerTr.position.y),
            _speed * Time.deltaTime);
    }
}
