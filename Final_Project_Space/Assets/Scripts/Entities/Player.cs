using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    [SerializeField] private Slider healthSlider;
    float regen = 2f;
    public float attackPower = 15f;

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

    public Transform Barrel;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    Vector2 movement;
    Vector2 mousePos;
    public float moveSpeed = 5f;

    Rigidbody2D rb;
    AudioSource asrc;

    public Camera cam;
    public AudioClip gunShoot;

    GameManager game;

    private void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
        asrc = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        integrity = maxIntegrity;
        healthSlider.maxValue = maxIntegrity;
    }

    private void Update()
    {
        integrity += Time.deltaTime * regen;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

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
        game.PlayerDeath();
        gameObject.SetActive(false);
    }

    private void OnGUI()
    {
        //float t = Time.deltaTime / 1f;
        //healthSlider.value = Mathf.Lerp(healthSlider.value, integrity, t);
        healthSlider.maxValue = maxIntegrity;
        healthSlider.value = integrity;
    }



    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, Barrel.position, Barrel.rotation);
        bullet.GetComponent<Bullet>().plr = this;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Barrel.up * bulletForce, ForceMode2D.Impulse);
        asrc.PlayOneShot(gunShoot);
    }
}
