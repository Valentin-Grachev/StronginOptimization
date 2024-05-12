using System;
using UnityEngine;


public class Function : MonoBehaviour
{
    public event Action onChanged;


    [SerializeField] private Parameter _a;
    [SerializeField] private Parameter _b;
    [SerializeField] private Parameter _c;
    [SerializeField] private Parameter _d;

    private void Start()
    {
        _a.onChanged += () => onChanged?.Invoke();
        _b.onChanged += () => onChanged?.Invoke();
        _c.onChanged += () => onChanged?.Invoke();
        _d.onChanged += () => onChanged?.Invoke();
    }


    public double GetValue(double x) =>
        _a.value * Math.Sin(_b.value * x) + _c.value * Math.Cos(_d.value * x);



}
