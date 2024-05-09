using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int _size;

    [SerializeField] private List<TextMeshProUGUI> _horizontalNumbers;
    [SerializeField] private List<TextMeshProUGUI> _verticalNumbers;


    private void Start()
    {
        transform.localScale = Vector3.one * _size;

        float currentNumber = -_size * 1.5f;
        foreach (var numberText in _horizontalNumbers)
        {
            numberText.text = currentNumber.ToString("F1");
            currentNumber += _size * 0.1f;
        }

        currentNumber = -_size * 1.5f;
        foreach (var numberText in _verticalNumbers)
        {
            numberText.text = currentNumber.ToString("F1");
            currentNumber += _size * 0.1f;
        }


    }





}
