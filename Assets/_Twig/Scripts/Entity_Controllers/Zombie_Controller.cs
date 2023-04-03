using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public class Zombie_Controller : MonoBehaviour
{
    GameManager gameManager;
    Animator zAnimations;
    SphereCollider zombieAlert;
    public Zombie zombie;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        zAnimations = this.GetComponent<Animator>();
        //zombieAlert = this.GetComponent<SphereCollider>();

        zombie = new Zombie();
        zombie.SetGeneralValues();
       // zombieAlert.radius = zombie.noticeSphere;


    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isPaused == false)
        {
            if (zombie.canMove)
            {
                zAnimations.SetBool("walk", true);
                this.transform.Translate(Vector3.forward * zombie.moveSpeed * Time.deltaTime);
            }

            zombie.position = this.transform.position;
            Vector3 distance = gameManager.DetectPlayer(this.transform, zombie.noticeSphere);

            if(distance.magnitude != 0)
            {
                Vector3 direction = (distance - this.transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                this.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);

        if(collision.transform.tag == "Player")
        {
            zAnimations.SetBool("walk", false);
            zAnimations.SetBool("attack", true);
            zombie.canMove = false;

        }
    }

    private void OnCollisionExit(Collision collision)
    {

        zAnimations.SetBool("walk", true);
        zAnimations.SetBool("attack", false);
        zombie.canMove = true;

    }
    private void OnCollisionStay(Collision collision)
    {

      
        if (collision.transform.tag == "Player")
        {
            Debug.Log("FoundPlayer");
            if (zAnimations.GetBool("attackEnd"))
            { 
                Destroy(collision.gameObject);
                zAnimations.SetBool("walk", true);
                zAnimations.SetBool("attack", false);
                zombie.canMove = true;
            }
        }
    }

}
