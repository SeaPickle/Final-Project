using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float maxIntegrity = 100;
    public float integrity = 100;
    public void ChangeHealth(float delta)
    {
        if (integrity == 0)
            return;
        integrity = Mathf.Clamp(integrity+delta, 0, maxIntegrity);
        if (delta!=0)
        {
            OnHealthChanged(delta);
        }
        if (integrity == 0)
            OnDeath();
    }

    public virtual void OnHealthChanged(float delta)
    {
    }
    public virtual void OnDeath()
    {
    }



}
