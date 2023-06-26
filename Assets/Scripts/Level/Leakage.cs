using UnityEngine;

public class Leakage : MonoBehaviour
{
    public ParticleSystem waterFlow;
    public ParticleSystem waterDrips;
    private GameSettings gameSettings;
    private BaseLevelBehaviour baseLevelBehaviour;
    private void Start()
    {
        gameSettings = FindObjectOfType<GameSettings>();
        baseLevelBehaviour = FindObjectOfType<BaseLevelBehaviour>();
    }
    
    public void FixLeakage(bool isPerfect)
    {
        waterDrips.Stop();
        waterFlow.Stop();
    }

    private void Update()
    {
        if (gameSettings.InAquariumTutorialMode)
        {
            if (transform.position.x < gameSettings.FirstTutorialLeakagePausePos)
            {
                baseLevelBehaviour.SetTutorialPauseLevel(true);
            }
        }
    }
}
