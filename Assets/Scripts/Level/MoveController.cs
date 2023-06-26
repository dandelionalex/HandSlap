using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] 
    private Transform movableTransform;
   
    [SerializeField]
    private GameObject windLines;

    private BaseLevelBehaviour levelBehaviour;
    
    private float lastX;
    private Vector3 initialPos;
    private GameSettings gameSettings;
    private ParticleSystem windLinesParticles;
    private int windRate = 0;
    private float speed;

    private void Awake()
    {
        gameSettings = FindObjectOfType<GameSettings>();
        levelBehaviour = FindObjectOfType<BaseLevelBehaviour>();
        windLinesParticles = windLines.GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        initialPos = movableTransform.position;
        lastX = 0;
        speed = gameSettings.Speed;
    }

    private void FixedUpdate()
    {
        if(levelBehaviour.LevelState != LevelState.Started)
            return;
        
        lastX += speed;
        movableTransform.position = new Vector3(lastX, initialPos.y, initialPos.z);
    }

    public void IncreaseSpeed()
    {
        if ( Mathf.Abs(speed) <= 0 )
            speed = gameSettings.Speed;
        
        speed *= 1.2f;
        windRate += 4;
        var em = windLinesParticles.emission;
        em.rateOverTime = windRate;
    }

    // public void StartTutor()
    // {
    //     speed = 0;
    //     //StartCoroutine(LowerSpeed());
    // }
    
    // private IEnumerator LowerSpeed()
    // {
    //     float tm = 0;
    //     while (Mathf.Abs(speed) > 0.001f)
    //     {
    //         yield return new FixedUpdate();
    //         tm += Time.fixedDeltaTime*0.3f;
    //         speed = gameSettings.Speed * gameSettings.TutorialCurve.Evaluate(tm);
    //     }
    //     
    //     speed = 0;
    // }
    
    public void ResetSpeed()
    {
        speed = gameSettings.Speed;
        windRate = 0;
        var em = windLinesParticles.emission;
        em.rateOverTime = windRate;
    }
}
