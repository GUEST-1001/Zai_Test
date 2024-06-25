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
    bool isMousePressed = false;
    Vector3 oriPosTarget;

    public bool isTurn = false;


    // Start is called before the first frame update
    void Start()
    {
        oriPosTarget = target.transform.localPosition;
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

    private void OnMouseDown()
    {
        if (isTurn)
        {
            // target.transform.position += Vector3.left * Time.deltaTime;
            isMousePressed = true;
        }
    }

    private void OnMouseUp()
    {
        if (isTurn)
        {
            isMousePressed = false;
            isTurn = !isTurn;
            ThrowOBJ();
        }
    }

    private void mousecontroller()
    {
        if (isMousePressed)
        {
            moveTarget.Move(targetSpeed);
        }
    }

    private void ThrowOBJ()
    {
        Vector3 tempPos = this.transform.position;
        tempPos += new Vector3(0, 0, -0.1f);
        GameObject temp = Instantiate(throwOBJ, tempPos, Quaternion.identity);
        temp.GetComponent<SimpleSlerp>().SetOBJValue(target.transform.position, EndThrowOBJ);
        // target.transform.localPosition = Vector3.zero;
    }

    void EndThrowOBJ()
    {
        target.transform.localPosition = oriPosTarget;
        isTurn = !isTurn;
    }
}
