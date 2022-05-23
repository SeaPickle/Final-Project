using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    [SerializeField] private Slider healthSlider;


    [SerializeField]
    float Integrity
    {
        get { return integrity; }
        set { integrity = value; }
    }
    [SerializeField]
    float MaxIntegrity
    {
        get { return maxIntegrity; }
        set { maxIntegrity = value; }
    }


    public float moveSpeed = 5f;

    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        integrity = maxIntegrity;
        healthSlider.maxValue = maxIntegrity;
    }

    private void LateUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    public override void OnHealthChanged(float delta)
    {
    }

    public override void OnDeath()
    {
        LevelManager.instance.GameOver();
        gameObject.SetActive(false);
    }

    private void OnGUI()
    {
        //float t = Time.deltaTime / 1f;
        //healthSlider.value = Mathf.Lerp(healthSlider.value, integrity, t);
        healthSlider.maxValue = maxIntegrity;
        healthSlider.value = integrity;
    }
}
