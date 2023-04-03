using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public class Third_Person_Controller : MonoBehaviour
{
    
    List<KeyCode> keyInteraction = new List<KeyCode>(); //probably unecessary see about optimizing if statments into one foreach loop
    float mainThreadTime = 0; //must always know the mainthread time if out of fixed update
    float[] sprintTime = new float[4]{ 0,0,0,0}; //build to sprint NOTE ---> Consider changing this to a .01 to 1 method
    public float MouseSpeed = 4; //generalized mouse Speed NOTE ---> add x and y sensitivities as public variables  
    private Vector3 originalOrientation;
    
    private GameManager gameManager;
    private Player player;
    private bool canJump;
    public bool canMove;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = gameManager.player;
    }

    // Update is called once per frame
    void Update()
    {
        mainThreadTime = Time.deltaTime;

        SetOrientation();

        if (!player.isSealed)
        {
            LinearMotion();
            RotationalMotion();
        }
    }

    //Generalized Script for 3d motion of a player
    void LinearMotion()
    {
        //Controls initial Behaviors
        void KeyDownSection()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                this.transform.localPosition += this.transform.rotation * (Vector3.forward * mainThreadTime);
                //  this.transform.
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                this.transform.position += this.transform.rotation * (Vector3.right * mainThreadTime);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                this.transform.position += this.transform.rotation * (Vector3.down * mainThreadTime);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                this.transform.position += this.transform.rotation * (Vector3.left * mainThreadTime);
            }
        }

        //Controls ending or release behaviors
        void KeyUpSection()
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                sprintTime[0] = 0;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                sprintTime[1] = 0;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                sprintTime[2] = 0;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                sprintTime[3] = 0;
            }
            if (Input.GetKeyUp(KeyCode.Space) && player.canJump)
            {
                this.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 5334 * 4, 0));
                player.canJump = false;
            }
        }
        
        //Controls persistant or continous behaviors
        void KeyHeldDownSection()
        {

            //moving to srpinting
            void Sprinting(int index)
            {
                if (!Input.GetKey(KeyCode.LeftShift) && sprintTime[index] < 2)
                {
                    sprintTime[index] += Time.deltaTime;
                }
            }

            //KeyHeldDown Section
            if (Input.GetKey(KeyCode.W))
            {
                Sprinting(0);
                this.transform.position += this.transform.rotation * Vector3.forward * (mainThreadTime + sprintTime[0] / 50);
            }
            if (Input.GetKey(KeyCode.D))
            {
                Sprinting(1);
                this.transform.position += this.transform.rotation * (Vector3.right * (mainThreadTime + sprintTime[1] / 50));
            }
            if (Input.GetKey(KeyCode.S))
            {
                Sprinting(2);
                this.transform.position += this.transform.rotation * (Vector3.back * (mainThreadTime + sprintTime[2] / 50));
            }
            if (Input.GetKey(KeyCode.A))
            {
                Sprinting(3);
                this.transform.position += this.transform.rotation * (Vector3.left * (mainThreadTime + sprintTime[3] / 50));
            }

        }


        //logic calls
        if (player.canMove == true)
        {
            KeyDownSection();
            KeyUpSection();
            KeyHeldDownSection();
        }

    }

    //Script for Basic third person camera rotational motion 
    void RotationalMotion()
    {

        Vector3 tempCameraPos = this.transform.GetComponentInChildren<Camera>().transform.localPosition;
        this.transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * MouseSpeed, 0); //Input.GetAxis("Mouse Y") * MouseSpeed
        this.transform.GetComponentInChildren<Camera>().transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y") * MouseSpeed/2, 0, 0);
        this.transform.GetComponentInChildren<Camera>().transform.localPosition = tempCameraPos;//new Vector3(tempCameraPos.y * Mathf.Cos;
        originalOrientation = this.transform.eulerAngles;
    }

    void SetOrientation()
    {
        if (this.transform.eulerAngles.x != 0)
        {
            this.transform.transform.eulerAngles = originalOrientation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.gameObject.name);
        if (player.canJump == false)
        {
            player.canJump = true;
        }
    }

}
