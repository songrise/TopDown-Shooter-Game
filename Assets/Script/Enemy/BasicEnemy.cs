using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BasicEnemyState
{
    idle, patrol, track, attack, die
}

public class BasicEnemy : MonoBehaviour, IEnemy
{
    public float health = 1.0f;
    private BasicEnemyState crtState;
    private GameObject target;

    public GameObject weapon;
    void Start()
    {
        OnSpawn();
    }

    void Update()
    {
        switch (crtState)
        {
            case BasicEnemyState.idle:
                OnIdle();
                break;
            case BasicEnemyState.patrol:
                OnPatrol();
                break;
            case BasicEnemyState.track:

                if (Vector3.Distance(transform.position, target.transform.position) < 20.0f)
                {
                    OnAttack();
                }
                break;
            case BasicEnemyState.attack:

                if (Vector3.Distance(transform.position, target.transform.position) >= 20.0f)
                {
                    OnTrack();
                }
                break;
            case BasicEnemyState.die:
                Destroy(gameObject);
                break;

        }
    }

    public void OnSpawn()
    {
        crtState = BasicEnemyState.idle;
        target = GameObject.FindGameObjectWithTag("Player");
    }


    public void OnIdle()
    {
        crtState = BasicEnemyState.idle;
        StopAllCoroutines();
        OnPatrol(); // for testing
    }
    public void OnPatrol()
    {
        crtState = BasicEnemyState.patrol;
        OnTrack();
    }

    public void OnHit()
    {
        health -= 1.0f;
        if (health <= 0.0f)
        {
            OnDeath();
        }
    }


    public void OnAttack()
    {
        crtState = BasicEnemyState.attack;
        StopCoroutine(MoveToTarget());
        StartCoroutine(AutoAttack());
    }

    public void OnTrack()
    {
        crtState = BasicEnemyState.track;
        StopCoroutine(AutoAttack());
        StartCoroutine(MoveToTarget());
    }

    public void OnDeath()
    {
        crtState = BasicEnemyState.die;
    }


    IEnumerator MoveToTarget()
    {
        while (true)
        {
            if (target != null)
            {
                this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = target.transform.position;
            }
            yield return new WaitForSeconds(0.33f);
        }
    }

    IEnumerator AutoAttack()
    {
        while (crtState == BasicEnemyState.attack)
        {
            Debug.Log("enemy firing");
            weapon.GetComponent<GunController>().OnFire();
            yield return new WaitForSeconds(2f);
        }
    }
}
