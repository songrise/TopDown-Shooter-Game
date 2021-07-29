using UnityEngine;
using System.Collections;
public class Navigation : MonoBehaviour
{
    // public Transform target;
    private Transform target;
    void Start()
    {
        //search for player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        StartCoroutine(MoveToTarget());

    }

    IEnumerator MoveToTarget()
    {
        while (true)
        {
            if (target != null)
            {
                this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = target.position;
            }
            yield return new WaitForSeconds(0.33f);
        }
    }
}