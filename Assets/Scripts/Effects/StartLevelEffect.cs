using TMPro;
using UnityEngine;

public class StartLevelEffect : MonoBehaviour, IEffect
{
    [SerializeField] 
    private TMP_Text textField;
    
    public void SetValue(int value)
    {
        textField.text = $"Day {value}";
    }
}