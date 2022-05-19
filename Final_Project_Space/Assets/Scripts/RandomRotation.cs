using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    Quaternion begin;
    Quaternion end;
    float timer = 0;
    float interval = 1;
    // Start is called before the first frame update
    void Start()
    {
        begin = Random.rotation;
        end = Random.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            begin = end;
            end = Random.rotation;
            timer = 0;
        }
        float t = Mathf.SmoothStep(0, 1, timer / interval);
        transform.rotation = Quaternion.Slerp(begin, end, t);
    }
}
