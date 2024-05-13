using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [System.Serializable]
    private struct ModeSetting
    {
        public float enableUntilOrthoSize;
        public float cellSize;
        public float graphWidth;
    }

    private int _currentModeIndex = -1;
    private Camera _camera;
    private Vector2 _startTouchPosition;

    [SerializeField] private float _scaleSensitivity = 0.1f;
    [SerializeField] private float _minOrthographicSize = 1f;
    [SerializeField] private float _maxOrthographicSize = 50f;
    [Space(10)]
    [SerializeField] private GridBuilder _gridBuilder;
    [SerializeField] private Graph _graph;
    [Space(10)]
    [SerializeField] private List<ModeSetting> _settings;


    private bool pointerIsOverUI
    {
        get
        {
            var pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            if (raycastResults.Count == 0) return false;

            foreach (var raycastResult in raycastResults)
            {
                if (raycastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                    return true;
            }


            return false;
        }
    }


    private void Start()
    {
        _camera = GetComponent<Camera>();
        ChangeScale(0);
    }

    private bool _scrolling = false;



    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0) ChangeScale(Input.mouseScrollDelta.y);

        if (Input.GetMouseButtonDown(0) && !pointerIsOverUI)
        {
            _scrolling = true;
            _startTouchPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
            

        if (Input.GetMouseButton(0) && _scrolling)
        {
            Vector2 offset = _startTouchPosition -(Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
            _camera.transform.position += (Vector3)offset;
        }

        if (Input.GetMouseButtonUp(0)) _scrolling = false;

    }


    private void ChangeScale(float change)
    {
        _camera.orthographicSize -= _camera.orthographicSize * change * _scaleSensitivity;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, 
            _minOrthographicSize, _maxOrthographicSize);

        int modeIndex = 0;
        while (_camera.orthographicSize > _settings[modeIndex].enableUntilOrthoSize)
            modeIndex++;

        if (_currentModeIndex != modeIndex)
        {
            _currentModeIndex = modeIndex;

            _gridBuilder.Build(_settings[modeIndex].cellSize);
            _graph.SetWidth(_settings[modeIndex].graphWidth);
        }


    }



}
