using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowSystem : MonoBehaviour
{
    [SerializeField] GameObject throwOBJ;
    [SerializeField] GameObject target;
    [SerializeField] float targetSpeed;
    bool isMousePressed = false;


    // Start is called before the first frame update
    void Start()
    {

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
        // target.transform.position += Vector3.left * Time.deltaTime;
        isMousePressed = true;
    }

    private void OnMouseUp()
    {
        isMousePressed = false;
        ThrowOBJ();
    }

    private void mousecontroller()
    {
        if (isMousePressed)
        {
            target.transform.position += Vector3.left * Time.deltaTime * targetSpeed;
        }
    }

    private void ThrowOBJ()
    {
        Vector3 tempPos = this.transform.position;
        tempPos += new Vector3(0, 0, -0.1f);
        GameObject temp = Instantiate(throwOBJ, tempPos, Quaternion.identity);
        temp.GetComponent<SimpleSlerp>().SetOBJValue(target.transform.position);
        // target.transform.localPosition = Vector3.zero;
        target.transform.localPosition = new Vector3(0, 3f, 0);
    }
}
