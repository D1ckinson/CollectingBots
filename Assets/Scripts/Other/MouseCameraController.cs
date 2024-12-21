using UnityEngine;

public class MouseCameraController : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _edgeDistance = 50f;

    private Camera _camera;

    private void Awake() =>
        _camera = Camera.main;

    void Update()
    {
        if (Input.mousePosition.x < _edgeDistance)
        {
            Move(Vector3.left);
        }
        else if (Input.mousePosition.x > Screen.width - _edgeDistance)
        {
            Move(Vector3.right);
        }

        if (Input.mousePosition.y < _edgeDistance)
        {
            Move(Vector3.back);
        }
        else if (Input.mousePosition.y > Screen.height - _edgeDistance)
        {
            Move(Vector3.forward);
        }
    }

    private void Move(Vector3 direction) =>
            _camera.transform.position += _speed * Time.deltaTime * direction;

}
