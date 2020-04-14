using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scr_gun : MonoBehaviour
{
    //Other variables
    public Camera fpscam;
    public ParticleSystem muzzle;
    public GameObject impact;
    private Animator anim;

    //Shooting variables
    public float damage = 10f;
    public float range = 100f;
    public float impactforce = 30f;
    public float firerate = 15f;
    private float nextTimeToFire = 0f;
    //public AudioSource shot;
    public AudioSource shot;

    //Reloading variables
    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Text ammocount;
    public AudioSource reloadsnd;







    void Start() 
    {

        anim = GetComponent<Animator>();
        currentAmmo = maxAmmo;
        

    }

    // Update is called once per frame
    void Update()
    {
        
        ammocount.text = currentAmmo.ToString()+" | ∞";
        if (isReloading) 
        {

            return;

        }
        
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0 && isReloading == false)
        {

            nextTimeToFire = Time.time + 1f / firerate;
            shot.Play();
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo) 
        {
            StartCoroutine(Reload());
            StartCoroutine(ReloadSnd());
            return;
            
        }

    }

    void Shoot()
    {
        muzzle.Play();
        
        currentAmmo--;
        anim.CrossFadeInFixedTime("Fire", 0.01f);
        anim.SetBool("Fire", true);
        
        RaycastHit hit;



        int layerMask = 1 << 8;

        //supposed the camera of the player is the main camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //this is the max distance to you can shoot 
        float maxDistance = 2475.0f;
        // the ray will just hit gameObjects from the EnemyLayer
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            scr_target target = hit.transform.GetComponent<scr_target>();
            scr_target_gun targetGun = hit.transform.GetComponent<scr_target_gun>();
            if (target != null)
            {

                target.TakeDamage(damage);

            }
            if (targetGun != null) 
            {

                targetGun.TakeDamage(damage);

            }

                if (hit.rigidbody != null)
            {

                hit.rigidbody.AddForce(-hit.normal * impactforce);

            }
            Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
                
        }


    }

    IEnumerator Reload() 
    {

        isReloading = true;
        anim.SetBool("Reload", true);
        yield return new WaitForSeconds(reloadTime);
        anim.SetBool("Reload", false);
        currentAmmo = maxAmmo;
        isReloading = false;

    }
    IEnumerator ReloadSnd()
    {

        yield return new WaitForSeconds(0.6f);
        reloadsnd.Play();

    }
       void FixedUpdate() 
    {

        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if (info.IsName("Fire")) {

            anim.SetBool("Fire", false);

        }

    }
}
