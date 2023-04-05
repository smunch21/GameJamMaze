using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Entity;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isPaused = false;

    public List<GameObject> EnemyList = null;
    public Player player;
    public GameObject playerObject = null;
    private Camera mainCamera = null;
    private GameObject currentBall = null;
    private GameObject nextBall = null;
    private int ballIndex = 1;
    private int bammIndexMax = 9;

    private GameObject currentBallObject;


    void Awake()
    {
        DontDestroyOnLoad(this);
       // PlayerCreation();
        FindAllEnemies();

        if (playerObject != null)
        {
            if (playerObject.transform.childCount > 0)
            {
                if (playerObject.transform.GetChild(0).GetComponent<Camera>())
                {
                    mainCamera = playerObject.transform.GetChild(0).GetComponent<Camera>();
                }
                else
                {
                    Debug.LogWarning("Camera not attached to player Object as the First Child");
                }
            }
            else
            {
                Debug.LogWarning("Camera not attached to player Object as the First Child");
            }
        }

    }

    private void FindAllEnemies()
    {
        EnemyList = new List<GameObject>();
        EnemyList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

   /* private void PlayerCreation()
    {
        player = new Entity.Player();
        player.SetGeneralValues();
        player.position = playerObject.transform.position;
    }*/

    public Vector3 DetectPlayer(Transform enemyTransform, float enemyRange)
    {
        float distance = Vector3.Distance(player.position, enemyTransform.position);
        if (distance < enemyRange)
        {
            return player.position;
        }
        else
            return Vector3.zero;
    }


    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
       
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Application");
    }
}
