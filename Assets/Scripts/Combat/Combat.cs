﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatorController))]
public class Combat : MonoBehaviour
{
    public static bool canAttack;

    public bool isPlayer;
    AnimatorController animator;
    QuestionHandler questionHandler;
    bool isAttacking;

    public delegate void combatDelegate();
    public event combatDelegate OnStartAttack, OnArriveOnTarget, OnStartSlash, OnFinishSlash, OnReturnToPosition;

    Combat target;
    Stats stats;

    public int damage;
    [Header("Boss Combat Values")]
    public float timeBetweenAttacks;
    float nextAttack = Time.time;

    private void Start(){
        stats = GetComponent<Stats>();
        questionHandler = GetComponent<QuestionHandler>();
        nextAttack = Time.time;
        Combat[] combats = FindObjectsOfType<Combat>();
        foreach(Combat c in combats) {
            if(c.isPlayer != isPlayer) {
                target = c;
            }
        }
        animator = GetComponent<AnimatorController>();
        if (isPlayer){
            questionHandler.OnCorrect += Attack;
        }
        else{
            StartCoroutine(BossCombat());
            //questionHandler.OnWrong += InitAttack;
        }
    }

    public bool IsPlayer()
    {
        return isPlayer;
    }
    
    public void InitAttack(){
        if(!isAttacking) {
            isAttacking = true;
            canAttack = false;
            StartCoroutine(animator.Attack());
        }
    }

    // Delegates triggers
    #region 

    public void StartAttack()
    {
       
        if (OnStartAttack != null)
        {
            OnStartAttack.Invoke();
        }
    }

    public void ArriveOnTarget()
    {
        if (OnArriveOnTarget != null)
        {
            OnArriveOnTarget.Invoke();
        }
    }

    public void StartSlash()
    {
        if (OnStartSlash != null)
        {
            OnStartSlash.Invoke();
        }
    }

    public void FinishSlash()
    {
        if (OnFinishSlash != null)
        {
            OnFinishSlash.Invoke();
        }
    }

    public void ReturnToPosition()
    {
        if (OnReturnToPosition != null)
        {
            OnReturnToPosition.Invoke();
        }
        isAttacking = false;
        canAttack = true;
    }

    #endregion

    IEnumerator BossCombat() {
        
        while(!stats.isDead()) {

            if(nextAttack < Time.time) {
                yield return StartCoroutine(WaitAttackTurn());
            }
        }
        yield return null;
    }

    public void Attack() {
        StartCoroutine(WaitAttackTurn());
    }

    IEnumerator WaitAttackTurn() {
        while(!canAttack) {
            yield return null;
        }
        InitAttack();
        nextAttack = Time.time + timeBetweenAttacks;

    }
}