using System;
using UnityEngine;
using TMPro;

public class InputParameter : Parameter
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private float _startValue;
    [SerializeField] private Vector2 _range;

    private void Awake()
    {
        value = _startValue;
    }


    private void Start()
    {
        _inputField.onEndEdit.AddListener ((str) =>
        {
            if (double.TryParse(str, out double newValue))
            {
                value = Math.Clamp(newValue, _range.x, _range.y);
                _inputField.text = value.ToString();
            }
                
           
        });
    }


}
