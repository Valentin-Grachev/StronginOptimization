using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinFinder : MonoBehaviour
{
    [SerializeField] private Function _function;
    [SerializeField] private Parameter _from;
    [SerializeField] private Parameter _to;
    [Space(10)]
    [SerializeField] private Parameter _r;
    [SerializeField] private Parameter _accuracy;
    [SerializeField] private Parameter _maxIterations;
    [Header("Result")]
    [SerializeField] private TextMeshProUGUI _iterationsText;
    [SerializeField] private TextMeshProUGUI _minArgumentText;
    [SerializeField] private TextMeshProUGUI _minValueText;
    [Header("Points")]
    [SerializeField] private GameObject _testPointPrefab;
    [SerializeField] private GameObject _resultPointPrefab;

    private List<GameObject> _testMarks = new List<GameObject>();

    private List<double> _testPoints = new List<double>();
    private float _iterations = 0;

    public void Clear()
    {
        _iterationsText.text = "-";
        _minArgumentText.text = "-";
        _minValueText.text = "-";

        foreach (var mark in _testMarks) Destroy(mark);
        _testMarks.Clear();

        _testPoints.Clear();
    }

    public void Run()
    {
        Clear();

        _iterations = 0;
        double argument = _to.value;
        double previousArgument = _from.value;

        _testPoints.Add(_from.value);
        _testPoints.Add(_to.value);
        MarkTestPoint(_from.value);
        MarkTestPoint(_to.value);


        while (_iterations < _maxIterations.value && Math.Abs(previousArgument - argument) > _accuracy.value)
        {
            double M = CalculateM(_testPoints);
            double m = Calculate_m(M);

            var R_list = new List<double>();
            for (int i = 1; i < _testPoints.Count; i++)
                R_list.Add(R(i, m));

            int intervalIndex = GetInterval(R_list);
            previousArgument = argument;
            argument = GetNextTestPoint(intervalIndex, m);

            _testPoints.Add(argument);
            MarkTestPoint(argument);

            _iterations++;
        }

        MarkTestPoint(argument, result: true);
        
        _iterationsText.text = _iterations.ToString();
        _minArgumentText.text = ((float)argument).ToString();
        double result = _function.GetValue(argument);
        _minValueText.text = ((float)result).ToString();

        _testMarks.Add(Instantiate(_resultPointPrefab, 
            new Vector2((float)argument, (float)result), Quaternion.identity));
    }


    private void MarkTestPoint(double x, bool result = false)
    {
        var pointPrefab = result ? _resultPointPrefab : _testPointPrefab;

        var mark = Instantiate(pointPrefab, new Vector2((float)x, 0), Quaternion.identity);
        _testMarks.Add(mark);
    }


    private double GetNextTestPoint(int intervalIndex, double m)
    {
        double previousX = _testPoints[intervalIndex];
        double currentX = _testPoints[intervalIndex + 1];

        double previousZ = _function.GetValue(previousX);
        double currentZ = _function.GetValue(currentX);

        return 0.5 * (currentX + previousX) - (currentZ - previousZ) / (2 * m);
    }


    private double Calculate_m(double M) => M > 0 ? _r.value * M : 1;

    private double CalculateM(List<double> testPoints)
    {
        testPoints.Sort();
        foreach (var item in testPoints)
            print(item);

        List<double> intervalValues = new List<double>();

        for (int i = 1; i < testPoints.Count; i++)
        {
            double previousX = testPoints[i - 1];
            double currentX = testPoints[i];

            double previousZ = _function.GetValue(previousX);
            double currentZ = _function.GetValue(currentX);

            intervalValues.Add(Math.Abs(currentZ - previousZ) / (currentX - previousX));
        }

        return GetMax(intervalValues);
    }


    private int GetInterval(List<double> R_list)
    {
        double maxValue = R_list[0];
        int interval = 0;

        for (int i = 1; i < R_list.Count; i++)
            if (maxValue < R_list[i])
            {
                maxValue = R_list[i];
                interval = i;
            }

        return interval;
    }


    private double R(int index, double m)
    {
        double previousX = _testPoints[index - 1];
        double currentX = _testPoints[index];

        double previousZ = _function.GetValue(previousX);
        double currentZ = _function.GetValue(currentX);

        double differentX = currentX - previousX;
        double differentZ = currentZ - previousZ;

        return m * differentX + 
            Math.Pow(differentZ, 2) / (m * differentX)
            - 2 * (currentZ + previousZ);
    }


    private double GetMax(List<double> array)
    {
        double max = array[0];
        foreach (var value in array) max = Math.Max(max, value);
        return max;
    }



}
