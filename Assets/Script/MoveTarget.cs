using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTarget : MonoBehaviour
{
    public Slider throwSlider;

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
        throwSlider.value = -5f - this.transform.localPosition.x;
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
