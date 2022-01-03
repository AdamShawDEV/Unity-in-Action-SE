using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum ViewMode
    {
        Vertical = 0,
        Horizontal = 1,
        Both = 2
    }

    public ViewMode mode = ViewMode.Vertical;

    public float verticalSpeed = 9.0f;
    public float horizontalSpeed = 9.0f;
    public float minimumVertical = -45.0f;
    public float maximumVertical = 45.0f;
    private float _rotationX = 0;
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
    }

    void Update()
    {
        if (mode == ViewMode.Horizontal)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") *horizontalSpeed, 0);
        }
        else if (mode == ViewMode.Vertical)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * verticalSpeed;
            _rotationX = Mathf.Clamp(_rotationX, minimumVertical, maximumVertical);

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * verticalSpeed;
            _rotationX = Mathf.Clamp(_rotationX, minimumVertical, maximumVertical);

            float delta = Input.GetAxis("Mouse X") * horizontalSpeed;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }

}
