using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string connecedLevel;
    private GameObject LoadingScreen;
    // Start is called before the first frame update
    void Awake()
    {
        LoadingScreen = GameObject.Find("Load_Screen");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            DontDestroyOnLoad(collision.transform);
            LoadingScreen.transform.GetChild(0).gameObject.SetActive(true);
            DontDestroyOnLoad(LoadingScreen);
            SceneManager.LoadScene(connecedLevel);
            collision.gameObject.transform.position = new Vector3(0, 2, 0);
        }
    }
}
