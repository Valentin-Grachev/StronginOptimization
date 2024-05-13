using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    private enum Axis { Horizontal, Vertical }

    [SerializeField] private Canvas _worldCanvas;
    [SerializeField] private Transform _lineContainer;
    [Space(10)]
    [SerializeField] private float _gridSize; 
    private float _cellSize;



    [Space(10)]
    [SerializeField] private SpriteRenderer _linePrefab;
    [SerializeField] private TextMeshProUGUI _horizontalLabelPrefab;
    [SerializeField] private TextMeshProUGUI _verticalLabelPrefab;
    [Space(10)]
    [SerializeField] private Color _axisColor;
    [SerializeField] private Color _gridColor;


    private const float lineWidth = 0.03f;
    private const float fontSizeCoef = 10f;
    private const float eps = 0.001f;

    
    public void Build(float cellSize)
    {
        _cellSize = cellSize;

        foreach (Transform child in _lineContainer) Destroy(child.gameObject);
        foreach (Transform child in _worldCanvas.transform) Destroy(child.gameObject);

        BuildAxis(Axis.Horizontal);
        BuildAxis(Axis.Vertical);
    }


    private void BuildAxis(Axis axis)
    {
        float currentValue = -_gridSize;

        while (currentValue < _gridSize || Mathf.Approximately(currentValue, _gridSize))
        {
            bool isAxis = Mathf.Abs(currentValue) < eps;

            var position = new Vector3(
                x: axis == Axis.Horizontal ? currentValue : 0f,
                y: axis == Axis.Vertical ? currentValue : 0f, z: 0f);

            var lineScale = new Vector3(
                x: axis == Axis.Horizontal ? _cellSize * lineWidth : _gridSize * 2,
                y: axis == Axis.Vertical ? _cellSize * lineWidth : _gridSize * 2, z: 1f);

            var lineInstance = Instantiate(_linePrefab, _lineContainer);
            lineInstance.transform.position = position;
            lineInstance.transform.localScale = lineScale;
            lineInstance.color = isAxis ? _axisColor : _gridColor;


            if (!isAxis)
            {
                var labelPrefab = axis == Axis.Horizontal ? _horizontalLabelPrefab : _verticalLabelPrefab;

                var labelInstance = Instantiate(labelPrefab, _worldCanvas.transform);
                labelInstance.text = currentValue.ToString("F1");
                labelInstance.transform.position = position;
                labelInstance.transform.localScale = Vector3.one * _cellSize * fontSizeCoef;
            }

            currentValue += _cellSize;
        }
    }




}
