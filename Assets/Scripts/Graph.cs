using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Function _function;
    [SerializeField] private LineRenderer _line;
    [SerializeField] private Parameter _from;
    [SerializeField] private Parameter _to;

    private const float drawStep = 0.01f;



    private void Start()
    {
        SetWidth(0.05f);
        DrawGraph();
        
        _function.onChanged += DrawGraph;
        _from.onChanged += DrawGraph;
        _to.onChanged += DrawGraph;
    }

    public void SetWidth(float width)
    {
        _line.startWidth = width;
        _line.endWidth = width;
    }


    public void DrawGraph()
    {
        var points = new List<Vector3>();

        float currentX = (float)_from.value;
        while (currentX < _to.value)
        {
            float y = (float)_function.GetValue(currentX);
            points.Add(new Vector3(currentX, y, 0f));
            currentX += drawStep;
        }

        _line.positionCount = points.Count;
        _line.SetPositions(points.ToArray());
    }

}
