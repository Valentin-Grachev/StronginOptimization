using System;
using UnityEngine;


public abstract class Parameter : MonoBehaviour
{
    public Action onChanged;

    private double _value = 1f;

    public double value
    {
        protected set
        {
            _value = value;
            onChanged?.Invoke();
        }
        get => _value;
    }


}
