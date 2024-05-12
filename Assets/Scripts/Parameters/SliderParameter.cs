using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderParameter : Parameter
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _prefix;


    private void Start()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }


    private void OnSliderValueChanged(float value)
    {
        this.value = value / 2f;
        _text.text = _prefix + this.value.ToString("F1");
        onChanged?.Invoke();
    }

}
