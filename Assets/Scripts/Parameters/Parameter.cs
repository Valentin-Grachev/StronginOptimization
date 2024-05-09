using System;
using UnityEngine;


public abstract class Parameter : MonoBehaviour
{
    public event Action onChanged;

    private float _value;

    public float value
    {
        protected set
        {
            _value = value;
            onChanged?.Invoke();
        }
        get => _value;
    }


}
