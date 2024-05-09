using UnityEngine;


public class Equation : MonoBehaviour
{
    [SerializeField] private Parameter _a;
    [SerializeField] private Parameter _b;
    [SerializeField] private Parameter _c;
    [SerializeField] private Parameter _d;


    public double GetValue(float x) =>
        _a.value * Mathf.Sin(_b.value * x) + _c.value * Mathf.Cos(_d.value * x);



}
