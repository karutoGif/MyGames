using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scr_target : MonoBehaviour
{
    public float health = 100;


    void Start()
    {
        setRigidbodyState(true);
        setCollidersState(false);
    }
    public void TakeDamage(float amount) {

        health -= amount;
        if (health <= 0) {

            Die();

        }

    }

    void Die() {

        GetComponent<Animator>().enabled = false;
        GetComponent<scr_enemyController>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        setRigidbodyState(false);
        setCollidersState(true);

    }

    void setRigidbodyState(bool state) 
    {

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies) 
        {

            rigidbody.isKinematic = state;

        }

        GetComponent<Rigidbody>().isKinematic = !state;

    }void setCollidersState(bool state) 
    {

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders) 
        {

            collider.enabled = state;

        }

        GetComponent<Collider>().enabled = !state;

    }
 
}
