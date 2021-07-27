using UnityEngine;
using System.Collections;
public class DemoNavigation : MonoBehaviour
{
    public Transform target;
    void Start()
    {
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
            yield return new WaitForSeconds(0.3f);
        }
    }
}