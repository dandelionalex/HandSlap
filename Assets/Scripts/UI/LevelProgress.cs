using UnityEngine;

namespace UI
{
    public class LevelProgress : MonoBehaviour
    {
        [SerializeField] 
        private SlicedFilledImage fillImage;
        
        private BaseLevelBehaviour levelBehaviour;
        
        private GameSettings gameSettings;
        
        private void Awake()
        {
            gameSettings = FindObjectOfType<GameSettings>();
            levelBehaviour = FindObjectOfType<BaseLevelBehaviour>();
        }

        private void OnEnable()
        {
            fillImage.fillAmount = 0;
        }

        private void Update()
        {
            if(levelBehaviour.LevelState != LevelState.Started)
                return;
            
            fillImage.fillAmount = levelBehaviour.TimePassed / gameSettings.LevelLength;
        }
    }
}
