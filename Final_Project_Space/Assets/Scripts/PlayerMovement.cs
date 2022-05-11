using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 20f;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 p = transform.position;
        float xin = Input.GetAxisRaw("Horizontal");
        float yin = Input.GetAxisRaw("Vertical");
        rb.MovePosition(new Vector2(p.x, p.y) + new Vector2(xin, yin) * moveSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {

    }
}
