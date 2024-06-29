using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ThrowSystem : MonoBehaviour
{
    [SerializeField] GameObject throwOBJ;
    [SerializeField] public GameObject target;
    MoveTarget moveTarget;
    [SerializeField] float targetSpeed;
    public bool isMousePressed = false;
    Vector3 oriPosTarget, oriPosTargetWorld;

    public Action<bool> _switchIsTurn;
    public GameObject throwSlider;


    // Start is called before the first frame update
    void Start()
    {
        oriPosTarget = target.transform.localPosition;
        oriPosTargetWorld = target.transform.position;
        moveTarget = target.GetComponent<MoveTarget>();
        moveTarget.throwSlider = throwSlider.GetComponent<Slider>();
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

    public void AIThrowOBJ(Vector3 critTarget, bool isHit)
    {
        Vector3 tempPos = this.transform.position;
        tempPos += new Vector3(0, 0, -0.1f);
        GameObject temp = Instantiate(throwOBJ, tempPos, Quaternion.identity);
        Vector3 hitPos = critTarget;
        hitPos.x -= GameManager.Instance.windValue;
        hitPos.y -= target.transform.position.y;

        if (!isHit)
        {
            switch (UnityEngine.Random.Range(0, 2))
            {
                case 1:
                    hitPos.x += UnityEngine.Random.Range(2.5f, 3.5f);
                    break;
                case 0:
                    hitPos.x -= UnityEngine.Random.Range(2.5f, 7f);
                    break;
            }
        }

        temp.GetComponent<SimpleSlerp>().SetOBJValue(hitPos, EndThrowOBJ, oriPosTargetWorld);
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
        throwSlider.SetActive(false);
        throwSlider.GetComponent<Slider>().value = 0f;
    }
}
