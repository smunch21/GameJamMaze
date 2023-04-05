using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera: MonoBehaviour
{
    void Update()
    {
        this.transform.position = this.transform.parent.transform.position;
    }
}

