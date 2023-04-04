using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;
public class Seal_Player : MonoBehaviour
{
   
    private GameManager gameManager;
    private Player player;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = gameManager.player;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            player.canMove = false;
            player.isSealed = true;
            collision.gameObject.transform.position = this.transform.position + new Vector3(0,1,0);
            
        }
    }
}
