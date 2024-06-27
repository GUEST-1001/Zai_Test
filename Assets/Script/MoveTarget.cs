using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Move(float targetSpeed)
    {
        this.transform.localPosition += Vector3.left * Time.deltaTime * targetSpeed;
        if (this.transform.localPosition.x <= -35f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
