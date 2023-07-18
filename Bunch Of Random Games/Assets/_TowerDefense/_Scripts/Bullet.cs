using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Action<Bullet> OnDeath;
    private float _elapsedTime;
    private float damageAmount;
    private void Awake() {
    }
    private void Update() {
        if(CalculateTime(3f))
        {
            OnDeath(this);
        }
    }
    public void KillBullet(Action<Bullet> action)
    {
        OnDeath = action;
    }
    public void KillBullet(float _damageAmount)
    {
        this.damageAmount = _damageAmount;
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Enemy")
        {
            //Decrease enemy's health
            var health = other.gameObject.GetComponent<Health>();          
            health.healthAmount -= damageAmount;
            OnDeath(this);//Release bullet to the pool
        }

    }
    private bool CalculateTime(float maxTime)
    {
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime > maxTime)
        {
            _elapsedTime = 0f;
            return true;
        }             
        else return false;   
    }
}
