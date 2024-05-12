using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Function _function;
    [SerializeField] private LineRenderer _line;

    private float _minX, _maxX;


    private float _drawStep;
    private float _lineWidth;



    private void Start()
    {
        SetSettings(min: -10f, max: 10f, drawStep: 0.05f);
        _function.onChanged += OnFunctionChanged;
    }

    private void OnFunctionChanged() => DrawGraph();

    public void SetSettings(float min, float max, float drawStep)
    {
        _minX = min;
        _maxX = max;
        _drawStep = drawStep;
        _line.startWidth = drawStep;
        _line.endWidth = drawStep;
        DrawGraph();
    }


    private void DrawGraph()
    {
        var points = new List<Vector3>();

        float currentX = _minX;
        while (currentX < _maxX)
        {
            float y = (float)_function.GetValue(currentX);
            points.Add(new Vector3(currentX, y, 0f));
            currentX += _drawStep;
        }

        _line.positionCount = points.Count;
        _line.SetPositions(points.ToArray());
    }

}
