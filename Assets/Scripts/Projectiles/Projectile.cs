﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TD.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        public ProjectileBehavior behavior;
        GameObject target;

        float startTime;
        public float lifeTime;
        public float Scale;
        public UnityEvent OnBlocked;
        public UnityEvent OnHit;
        
        

        public GameObject Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }

        public DamageType Type
        {
            get
            {
                return behavior.type;
            }
        }

        private void Start()
        {
            startTime = Time.time;
        }

        public void Init()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Time.time > startTime + lifeTime)
            {
                Destroy(gameObject);
            }

            if(behavior != null)
            {

                behavior.Move(this, target);
            }
            else
            {
                Debug.LogError("No behavior attached to " + name + ", destroying.");
                Destroy(gameObject);
            }
        }

        public void TriggerEffect(GameObject effect)
        {
            Instantiate(effect).transform.position = transform.position;
        }

        public void TriggerEffectOnTarget(GameObject effect)
        {
            if(target != null)
            {
                Instantiate(effect).transform.position = target.transform.position;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            behavior.Hit(this, collision.gameObject);
            Destroy(gameObject);
        }
    }
}

