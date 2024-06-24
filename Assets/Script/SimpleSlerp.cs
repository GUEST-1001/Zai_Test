using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSlerp : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float lerpController;
    [SerializeField] GameObject target;
    [SerializeField] Vector3 startPos, endPos, startNewPos, endNewPos;

    [SerializeField] float centerOffset;
    [SerializeField] Vector3 centerPivot;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        endPos = target.transform.position;

        centerPivot = (startPos + endPos) * 0.5f;
        centerPivot -= new Vector3(0, -centerOffset);

        startNewPos = startPos - centerPivot;
        endNewPos = endPos - centerPivot;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Slerp(startNewPos, endNewPos, lerpController) + centerPivot;
    }
}
