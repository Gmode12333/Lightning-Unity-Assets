using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    public Vector3 _defaultPosition;
    public Quaternion _defaultRotation;
    public GameObject refObject;
    private Vector3 startPos;
    private Vector3 currentPos;
    private Vector3 diffPos;

    private Vector3 currentRot;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            currentRot = new Vector3(refObject.transform.rotation.eulerAngles.x, 0, refObject.transform.rotation.eulerAngles.z);
        }
        else if (Input.GetMouseButton(0))
        {
            currentPos = Input.mousePosition;
            diffPos = currentPos - startPos;
            float xRotation = currentRot.x + diffPos.y / 2;
            float zRotation = currentRot.z - diffPos.x / 2;
            refObject.transform.rotation = Quaternion.Euler(xRotation, 0, zRotation);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            refObject.transform.rotation = _defaultRotation;
        }
    }
}
