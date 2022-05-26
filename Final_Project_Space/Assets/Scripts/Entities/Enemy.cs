using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{

    public float speed = 3f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private Slider healthbar;
    private float canAttack;
    GameObject body;
    

    public Transform target;
    Rigidbody2D rb;
    AudioSource asrc;
    AudioClip hit;

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

    void Start()
    {
        canAttack = 0f;
        body = transform.Find("Body").gameObject;
        hit = Resources.Load<AudioClip>("Audio/Effects/EnemyHit");
        rb = GetComponent<Rigidbody2D>();
        asrc = GetComponent<AudioSource>();
    }
    
    void FixedUpdate()
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            Vector2 dir = ((Vector2)(target.position-transform.position)).normalized; 
            rb.MovePosition((Vector2)transform.position + dir * step);

            Vector2 deltaPos = target.position - transform.position;
            float angle = Mathf.Atan2(deltaPos.y, deltaPos.x) * Mathf.Rad2Deg - 90;
            body.transform.rotation = Quaternion.Euler(0, 0, angle);

            //rb.AddForce(dir * step * 10);
            //transform.position = Vector2.MoveTowards(transform.position, target.position, step);

            canAttack += Time.deltaTime;

            healthbar.maxValue = maxIntegrity;
            healthbar.value = integrity;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Crystal")
        {
            if (attackSpeed <= canAttack)
            {
                other.gameObject.GetComponent<Entity>().ChangeHealth(-attackDamage);
                canAttack = 0f;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Crystal")
        {
            if (attackSpeed <= canAttack)
            {
                other.gameObject.GetComponent<Entity>().ChangeHealth(-attackDamage);
                canAttack = 0f;
            }
        }
    }

    public override void OnHealthChanged(float delta)
    {
        asrc.PlayOneShot(hit);
    }
    public override void OnDeath()
    {
        Destroy(gameObject);
    }
}
