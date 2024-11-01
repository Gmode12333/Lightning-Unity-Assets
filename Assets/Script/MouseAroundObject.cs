using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed = 0.01f;
    public float _mousesens = 5.0f;
    public float _smoothTime = 0.3f;
    public float _distanceFromtarget = 10.0f;

    private Vector3 _currentRotation = Vector3.zero;
    private Vector3 _smoothVelocity = Vector3.zero;
    private Vector3 _targetPosition;

    private bool isZooming = false;

    private void Update()
    {
        // Mouse rotation
        if (Input.GetMouseButton(0))
        {
            RotateCamera();
        }

        MoveCamera();
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mousesens;
        float mouseY = Input.GetAxis("Mouse Y") * _mousesens;

        _currentRotation += new Vector3(-mouseY, mouseX, 0);
        transform.localEulerAngles = _currentRotation;
    }

    private void ZoomCamera()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            _distanceFromtarget -= 0.1f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            _distanceFromtarget += 0.1f;
        }

        _targetPosition = transform.position - transform.forward * _distanceFromtarget;
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _smoothVelocity, _smoothTime);
    }

    private void MoveCamera()
    {
        float xAxisValue = Input.GetAxis("Horizontal") * movementSpeed;
        float yAxisValue = Input.GetAxis("Vertical") * movementSpeed;
        float zAxisValue = Input.GetAxis("Mouse ScrollWheel") * movementSpeed; // use mouse wheel for forward/backward movement

        transform.position += transform.right * xAxisValue;
        transform.position += transform.up * yAxisValue;
        transform.position += transform.forward * yAxisValue;
    }

    private void OnDisable()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}