using UnityEngine;
using Random = UnityEngine.Random;

public class HandController : MonoBehaviour
{
    public GameObject hand;
    public float rotationRangeFrom = 0;
    public float rotationRangeTo = 40;
    
    private BaseLevelBehaviour levelBehaviour;
    private GameSettings gameSettings;
    
    private void Awake()
    {
        levelBehaviour = FindObjectOfType<BaseLevelBehaviour>();
        gameSettings = FindObjectOfType<GameSettings>();
    }

    private bool endRegistred = false;
    
    private void Update()
    {
        if( (levelBehaviour.LevelState != LevelState.Started || gameSettings.InAquariumTutorialMode) 
            && levelBehaviour.LevelState != LevelState.TutorialPaused )
            return;
        
        if (Input.GetKeyDown("space"))
        {
            Slap();
        }

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
                Slap();
        }
    }

    private void Slap()
    {
        var i = Random.Range(rotationRangeFrom, rotationRangeTo);
        Instantiate(hand, transform.position, Quaternion.Euler(new Vector3(i, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z)), transform);
    }
}
