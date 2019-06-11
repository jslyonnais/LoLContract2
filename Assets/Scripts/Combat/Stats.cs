﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHitPoints;
    public float currentHitPoints;

    public delegate void statsDelegate();
    public event statsDelegate OnDead, OnDamageReceived, OnRevive;

    private void Start() {
        currentHitPoints = maxHitPoints;
    }

    public void Revive() {
        currentHitPoints = maxHitPoints;
        if(OnRevive != null) {
            OnRevive();
        }
    }

    public void GetDamage(float damage) {
        currentHitPoints -= damage;
        if(OnDamageReceived != null) {
            OnDamageReceived();
        }
        if(currentHitPoints <= 0f) {
            Dead();
        } 
    }

    public void Dead() {

        if(OnDead != null) {
            OnDead();
        }

    }

    public bool isDead() {
        Debug.Log(currentHitPoints);
        return (currentHitPoints <= 0f);
    }

}
