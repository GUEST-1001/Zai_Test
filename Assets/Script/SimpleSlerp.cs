using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSlerp : MonoBehaviour
{
    // [SerializeField] GameObject target;
    // public GameObject Target
    // {
    //     get { return target; }
    //     set { target = value; }
    // }

    Vector3 startPos, endPos, startNewPos, endNewPos;

    [SerializeField] float centerOffset;
    Vector3 centerPivot;

    // [SerializeField] float duration;
    [SerializeField] float speed;

    Action _endThrow;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartThrowOBJ()
    {
        // float elapsedTime = 0f;
        float precentComplete = 0f;
        // Debug.Log("+++is now run fade");
        // Debug.Log(Mathf.Approximately(myMaterial.GetFloat("_FresnelPower"), TagetFre));
        while (precentComplete < 1)
        {
            // Debug.Log("+++is now run loop");
            // Debug.Log(myMaterial.GetFloat("_FresnelPower"));
            // elapsedTime += Time.deltaTime;
            precentComplete += (Time.deltaTime * speed) / 10f;
            this.transform.position = Vector3.Slerp(startNewPos, endNewPos, precentComplete) + centerPivot;
            if (precentComplete > 0.5f)
            {
                this.GetComponent<ThrowOBJHit>().SetHitBoxOn();
            }
            yield return null;
        }
        if (GameManager.Instance.isDoubleAtk)
            GameManager.Instance.UseItemDoulde(() => StartCoroutine(StartThrowOBJ()));
        else
            _endThrow();
        StartCoroutine(this.GetComponent<ThrowOBJHit>().CheckColliderList());
    }

    public void SetOBJValue(Vector3 targetPos, Action endThrow, Vector3 oriTargetPos)
    {
        startPos = this.transform.position;
        endPos = targetPos;

        endPos.x += GameManager.Instance.windValue;
        switch (GameManager.Instance.isPlayer1Turn)
        {
            case true:
                if (endPos.x > oriTargetPos.x)
                    endPos = oriTargetPos;
                break;
            case false:
                if (endPos.x < oriTargetPos.x)
                    endPos = oriTargetPos;
                break;
        }

        _endThrow = endThrow;

        centerPivot = (startPos + endPos) * 0.5f;
        centerPivot -= new Vector3(0, -centerOffset);

        startNewPos = startPos - centerPivot;
        endNewPos = endPos - centerPivot;

        StartCoroutine(StartThrowOBJ());
    }

}
