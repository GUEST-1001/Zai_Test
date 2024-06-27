using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowSystem : MonoBehaviour
{
    [SerializeField] GameObject throwOBJ;
    [SerializeField] GameObject target;
    MoveTarget moveTarget;
    [SerializeField] float targetSpeed;
    public bool isMousePressed = false;
    Vector3 oriPosTarget, oriPosTargetWorld;

    public Action<bool> _switchIsTurn;


    // Start is called before the first frame update
    void Start()
    {
        oriPosTarget = target.transform.localPosition;
        oriPosTargetWorld = target.transform.position;
        moveTarget = target.GetComponent<MoveTarget>();
        targetSpeed = GameManager.Instance.mainTargetSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        mousecontroller();
    }

    public void mousecontroller()
    {
        if (isMousePressed)
        {
            if (moveTarget.Move(targetSpeed))
            {
                _switchIsTurn(false);
                ThrowOBJ();
                isMousePressed = false;
            }
        }
    }

    public void ThrowOBJ()
    {
        Vector3 tempPos = this.transform.position;
        tempPos += new Vector3(0, 0, -0.1f);
        GameObject temp = Instantiate(throwOBJ, tempPos, Quaternion.identity);
        temp.GetComponent<SimpleSlerp>().SetOBJValue(target.transform.position, EndThrowOBJ, oriPosTargetWorld);
        // target.transform.localPosition = Vector3.zero;
    }

    //just in case
    void EndThrowOBJ()
    {
        target.transform.localPosition = oriPosTarget;
        _switchIsTurn(false);
    }
}
