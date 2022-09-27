using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    protected float _maxHP;
    protected float maxHP = 100;
    [SerializeField]
    protected float currentHP;

    public void Awake()
    {
        currentHP = _maxHP;
        maxHP = _maxHP;
        //Debug.Log("Health Up");
    }
    public void Start()
    {
        while (GameEventSystem.current == null)
            maxHP = maxHP;
        GameEventSystem.current.HealthChange(currentHP, maxHP);
    }

    public void DealDamage(float amount)
    {
        currentHP -= amount;
        if (GameEventSystem.current != null)
            GameEventSystem.current.HealthChange(currentHP, maxHP);
    }

    public void HealDamage(float amount)
    {
        currentHP += amount;
        if (GameEventSystem.current != null)
            GameEventSystem.current.HealthChange(currentHP, maxHP);
    }

    public void SetMaxHP(float newMaxHP)
    {
        float currentPercentHP = currentHP / maxHP;
        maxHP = Math.Max(1f, newMaxHP);  // protect from setting it to zero causing NaN values for currentPercentHP
        currentHP = currentPercentHP * maxHP;
        //Debug.Log(GameEventSystem.current);
        if(GameEventSystem.current != null)
            GameEventSystem.current.HealthChange(currentHP, maxHP);
    }

    public void DisplayHP()
    {
        Debug.Log("Current HP: " + currentHP.ToString() + " Max HP: " + maxHP.ToString());
    }

    public float getCurrentHP() => currentHP;
    public float getMaxHP() => maxHP;

    void OnValidate()
    {
        SetMaxHP(_maxHP);
    }

}