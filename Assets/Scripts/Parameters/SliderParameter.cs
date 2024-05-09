using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderParameter : Parameter
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _prefix;


    private void SetSliderValue(float value)
    {
        this.value = value / 2f;
        _text.text = _prefix + this.value.ToString();
    }

}
