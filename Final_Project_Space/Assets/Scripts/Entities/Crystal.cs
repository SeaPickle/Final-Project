using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal : Entity
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] GameObject crystal;
    Quaternion begin;
    Quaternion end;
    float timer = 0;
    float interval = 1;
    float scaleTimer = 100;

    [SerializeField] float Integrity
    {
        get { return integrity; }
        set { integrity = value; }
    }
    [SerializeField] float MaxIntegrity
    {
        get { return maxIntegrity; }
        set { maxIntegrity = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        begin = Random.rotation;
        end = Random.rotation;

        healthSlider.maxValue = maxIntegrity;
        healthSlider.value = integrity;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        scaleTimer += Time.deltaTime;
        if (timer >= interval)
        {
            begin = end;
            end = Random.rotation;
            timer = 0;
        }
        float t = Mathf.SmoothStep(0, 1, timer / interval);
        crystal.transform.rotation = Quaternion.Slerp(begin, end, t);

        float scale = Mathf.Exp(-scaleTimer*4);
        crystal.transform.localScale = new Vector3(1,1,1) * (0.5f+scale/4);

        healthSlider.maxValue = maxIntegrity;
        healthSlider.value = integrity;
    }

    public override void OnHealthChanged(float delta)
    {
        scaleTimer = 0;
    }

    public override void OnDeath()
    {
        LevelManager.instance.GameOver();
        gameObject.SetActive(false);
    }
}
