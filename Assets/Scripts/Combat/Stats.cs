﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHitPoints;
    public float currentHitPoints;

    public delegate void statsDelegate();
    public event statsDelegate OnDead, OnDamageReceived, OnRevive, OnHpChange;

    private void Awake() {
        currentHitPoints = maxHitPoints;
    }

    public void Revive() {
        currentHitPoints = maxHitPoints;
        
        if(OnRevive != null) {
            OnRevive();
        }
        if(OnHpChange != null) {
            OnHpChange();
        }
    }

    public void GetDamage(float damage) {
        currentHitPoints -= damage;
        if(OnDamageReceived != null) {
            OnDamageReceived();
        }
        if(OnHpChange != null) {
            OnHpChange();
        }
        if(currentHitPoints <= 0f) {
            Dead();
        } 
    }

    public void Heal() {
        currentHitPoints += (maxHitPoints * 0.4f);
        currentHitPoints = Mathf.Clamp(currentHitPoints, -10f, maxHitPoints);

        if(OnHpChange != null) {
            OnHpChange();
        }
    }

    public void Dead() {

        if(OnDead != null) {
            OnDead();
        }

    }

    public bool isDead() {
        return (currentHitPoints <= 0f);
    }

}
