using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzBomber : MonoBehaviour
{

    public GameObject Gun;
    public GameObject Bullet;
    public float fireRate = 0.6f;
    public Transform firePoint;
    public float Range;
    public Transform Target;
    //public GameObject DetectLight;

    bool Detected = false;

    float timeUntilFire;

    Vector2 Direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        {
        Vector2 targetPos = Target.position;

        Direction = targetPos - (Vector2)transform.position;
        
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Direction, Range);

        if(raycastHit)
        {
            if(raycastHit.collider.gameObject.tag == "Player")
            {
                if(Detected == false)
                {
                    Debug.Log("hello");
                    Detected = true;
                    //DetectLight.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }

            else
            {
                if(Detected == true)
                {
                    Detected = false;
                    //DetectLight.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
        }

        if(Detected)
        {
            Gun.transform.up = Direction;
            if (timeUntilFire < Time.time)
            {
                Shoot();
                timeUntilFire = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        Instantiate(Bullet, firePoint.position, firePoint.rotation);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,Range);
    }
    }
}
