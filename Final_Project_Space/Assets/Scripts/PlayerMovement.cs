using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 20f;
    public Camera cam;

    Vector2 mousePos;

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

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
