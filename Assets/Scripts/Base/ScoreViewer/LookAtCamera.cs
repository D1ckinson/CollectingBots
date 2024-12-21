using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _camera;
    private float _yRotate = 180f;

    private void Awake() =>
        _camera = Camera.main;

    private void Update()
    {
        transform.LookAt(_camera.transform);
        transform.Rotate(new(0, _yRotate, 0));
    }
}
