using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaScriptNew : MonoBehaviour
{
    int speed;
    int directionMove;
    Rigidbody2D rb;
    SpriteRenderer renderer;

    void Start()
    {
        speed = 3;
        directionMove = 1;
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (IsGrounded())
            directionMove *= -1;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * directionMove, rb.velocity.y);
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position + new Vector3(renderer.bounds.size.x / 1.8f * directionMove, 0, 0);
        Vector2 direction = Vector2.right * directionMove;
        float radius = 0.1f;
        float distance = 0.1f;

        RaycastHit2D hit = Physics2D.CircleCast(position, radius, direction, distance);
        if (hit.collider != null)
            if (hit.transform.CompareTag("ground"))
                return true;
        return false;
    }
}
