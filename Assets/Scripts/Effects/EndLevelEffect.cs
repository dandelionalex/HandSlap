using TMPro;
using UnityEngine;

public class EndLevelEffect : MonoBehaviour, IEffect
{
    [SerializeField] 
    private TMP_Text textField;
    
    public void SetValue(int value)
    {
        textField.text = $"DAY {value}\n complete!";
    }
}