using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstScreenUI : MonoBehaviour
{
    [SerializeField] 
    private Button playButton;

    [SerializeField] 
    private InputField speedInput;

    [SerializeField] 
    private InputField difficultyInput;

    [SerializeField] 
    private InputField levelLengthInput;

    [SerializeField] 
    private GameSettings gameSettings;

    private void Start()
    {
        DOTween.Init();
        LoadLastValues();
        playButton.onClick.AddListener(OnPlayClick);
    }

    private void OnPlayClick()
    {
        gameSettings.Speed = float.Parse(speedInput.text)/-10;
        PlayerPrefs.SetFloat("SPEED", gameSettings.Speed);
        
        gameSettings.LevelLength = float.Parse(levelLengthInput.text);
        PlayerPrefs.SetFloat("LEVEL_LENGTH", gameSettings.LevelLength);
        
        gameSettings.Difficulty = float.Parse(difficultyInput.text)/10;
        PlayerPrefs.SetFloat("DIFFICULTY", gameSettings.Difficulty);

        SceneManager.LoadScene("SampleScene");
    }

    private void LoadLastValues()
    {
        speedInput.text = (PlayerPrefs.GetFloat("SPEED", -0.2f)*-10).ToString();
        levelLengthInput.text = PlayerPrefs.GetFloat("LEVEL_LENGTH", 10).ToString();
        difficultyInput.text = (PlayerPrefs.GetFloat("DIFFICULTY", 0.5f)*10).ToString();
    }
}
