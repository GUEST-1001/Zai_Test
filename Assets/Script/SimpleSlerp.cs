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
            yield return null;
        }
        Destroy(this.gameObject);
    }

    public void SetOBJValue(Vector3 targetPos)
    {
        startPos = this.transform.position;
        endPos = targetPos;

        centerPivot = (startPos + endPos) * 0.5f;
        centerPivot -= new Vector3(0, -centerOffset);

        startNewPos = startPos - centerPivot;
        endNewPos = endPos - centerPivot;

        StartCoroutine(StartThrowOBJ());
    }
}