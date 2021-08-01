using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject weapon;
    void Start()
    {
        weapon = GameObject.Find("Enemy_Gun");
        StartCoroutine(AutoAttack());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnHit()
    {
        //kill this
        Destroy(gameObject);
    }

    IEnumerator AutoAttack()
    {
        while (true)
        {
            Debug.Log("enemy firing");
            weapon.GetComponent<GunController>().OnFire();
            yield return new WaitForSeconds(2f);
        }
    }
}
