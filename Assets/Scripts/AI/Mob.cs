﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace TD.AI
{
    [RequireComponent(typeof(Health), typeof(NavMeshAgent))]
    public class Mob : MonoBehaviour
    {
        NavMeshAgent agent;
        Health health;
        public float TimeValue;
        public FloatEvent OnDie;
        public FloatEvent OnDelete;
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
            if(agent != null)
            {
                agent.SetDestination(GameObject.FindGameObjectWithTag("Exit").transform.position);
            }
            else
            {
                Debug.LogWarning("Mob has no NavMeshAgent attached");
            }
        }

        private void Start()
        {
            health.OnDie.AddListener(Die);
        }

        /// <summary>
        /// Sent to the health object, which handles dying
        /// </summary>
        public void Die()
        {
            OnDie.Invoke(TimeValue);
        }
        
        /// <summary>
        /// Called by this object when it has been identified for deletion (i.e. when it exits without dying)
        /// </summary>
        public void Delete()
        {
            OnDelete.Invoke(TimeValue);
            health.OnDelete.Invoke();
            Destroy(gameObject);
        }
        
    }
}

[System.Serializable]
public class FloatEvent : UnityEvent<float>
{

}

