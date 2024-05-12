using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private Vector2 _startTouchPosition;

    [SerializeField] private float _scaleSensitivity = 0.1f;


    private void Start()
    {
        _camera = GetComponent<Camera>();
    }


    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0) ChangeScale(Input.mouseScrollDelta.y);

        if (Input.GetMouseButtonDown(0))
            _startTouchPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector2 offset = _startTouchPosition -(Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
            _camera.transform.position += (Vector3)offset;
        }

    }


    private void ChangeScale(float change)
    {
        _camera.orthographicSize -= _camera.orthographicSize * change * _scaleSensitivity;
    }



}
