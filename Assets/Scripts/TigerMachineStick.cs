using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerMachineStick : MonoBehaviour
{
    public Transform RotatePoint;
    public float RotateAngle;

    public float timeChangeDir;

    float u;
    float timeStart;
    float rotateDeg;

    // Start is called before the first frame update
    void Start()
    {
        timeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        u = (Time.time - timeStart) / timeChangeDir;
        if (u <= 1)
        {
            transform.RotateAround(RotatePoint.transform.position, Vector3.forward, RotateAngle * Time.deltaTime);
        }
        else
        {
            ChangeRotateDir();
        }
        //transform.RotateAround(RotatePoint.transform.position, Vector3.forward, RotateAngle * Time.deltaTime);
        //rotateDeg += Mathf.Rad2Deg * (RotateAngle * Time.deltaTime);
        //Debug.Log(rotateDeg);
        //Debug.Log(Mathf.Rad2Deg * (RotateAngle * Time.deltaTime));
    }

    void ChangeRotateDir() 
    {
        RotateAngle = -RotateAngle;
        timeStart = Time.time;
    }
}
