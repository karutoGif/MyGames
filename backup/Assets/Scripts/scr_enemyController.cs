using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scr_enemyController : MonoBehaviour
{

    public bool following = true;
    public float attackRadius = 2f;
    static Animator anim;
    public float firerate = 1f;
    public float nextTimeToFire = 0f;
    public float damage = 0.01f;
    scr_UI scr_UI;
    Transform target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        target = scr_findplayer.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        scr_UI = FindObjectOfType(typeof(scr_UI)) as scr_UI;

    }

    // Update is called once per frame
    void Update()
    {



        float distance = Vector3.Distance(target.position, transform.position);


            
        if (following && distance>attackRadius)
        {
            //Run animation
            anim.SetBool("Run", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Attack", false);

            //Follow
            agent.SetDestination(target.position);
            FaceTarget();

        }
        if(distance <= attackRadius && following)
        {

            //Attack animation
            anim.SetBool("Run", false);
            anim.SetBool("Idle", false);
            FaceTarget();
            if (Time.time >= nextTimeToFire)
            {

                nextTimeToFire = Time.time + 1f / firerate;
                Melee();

            }
            

        }
        if (!following)
        {

            //Not following animation
            anim.SetBool("Idle", true);
            anim.SetBool("Run", false);
            anim.SetBool("Attack", false);
        
        }
    }

    void FaceTarget() 
    {
        if (following)
        {

            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        
        }
    }

    void Melee() 
    {

        anim.SetBool("Attack", true);
        Debug.Log("-1hp");
        scr_UI.attHelthBar(damage);
 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

    }
}
