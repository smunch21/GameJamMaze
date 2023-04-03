using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private float mainThreadTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.GetChild(0).gameObject.activeSelf)
        {
            Loading(mainThreadTime);
            mainThreadTime += Time.deltaTime;
        }
    }

    void Loading(float time)
    {
        if (time > 1)
        {
            this.gameObject.SetActive(false);
            mainThreadTime = 0;
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
