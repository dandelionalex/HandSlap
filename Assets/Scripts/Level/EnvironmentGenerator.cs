using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    [SerializeField] 
    private GameObject glassCellPrefab;

    [SerializeField] 
    private float glassPrefabWidth;
    
    [SerializeField] 
    private float initialWidth;

    [SerializeField] 
    private Transform glassParent;

    [SerializeField] 
    private Transform sandParent;
    
    [Space]
    [SerializeField] 
    private Transform bottomParent;

    [SerializeField] 
    private GameObject bottomPrefab;

    [SerializeField] 
    private GameObject sandPrefab;

    [SerializeField] 
    private float sandWidth;
    [Space] 
    [SerializeField]
    private GameObject waterLeakagePrefab;

    [Space] 
    [SerializeField] 
    private Transform[] flocks;
    [SerializeField] 
    private float flockPrefabWidth;
    
    private float glassLength = 0;
    private float sandLength = 0;
    private GameSettings gameSettings;

    private void Awake()
    {
        gameSettings = FindObjectOfType<GameSettings>();
    }

    private void OnEnable()
    {
        glassLength = 0;
        sandLength = 0;

        foreach (Transform obj in glassParent)
        {
            Destroy(obj.gameObject);
        }
        
        foreach ( Transform obj in sandParent)
        {
            Destroy(obj.gameObject);
        }
        
        while (glassLength < initialWidth)
        {
            InsertGlass();
        }

        InsertSand();
    }

    private void InsertGlass()
    {
        var go= Instantiate(glassCellPrefab, glassParent);
        go.transform.localPosition = new Vector3(glassLength, 0, 0);
            
        if (CheckForWaterLeakages(glassLength))
            go.transform.GetComponent<IHasLeackage>()?.PlaceWaterLeakage(waterLeakagePrefab);
            
        glassLength += glassPrefabWidth;
    }
    
    private void InsertSand()
    {
        var go = Instantiate(sandPrefab, sandParent);
        go.transform.localPosition = new Vector3(sandLength, go.transform.position.y, go.transform.position.z);
        sandLength += sandWidth;
    }

    private bool CheckForWaterLeakages(float xPos)
    {
        if (gameSettings.InAquariumTutorialMode)
        {
            if (glassLength >= gameSettings.FirstTutorialLeakage && glassLength <gameSettings.FirstTutorialLeakage + glassLength - 0.1f)
            {
                return true;
            }

            return false;
        }
        return UnityEngine.Random.value < gameSettings.Difficulty;
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) + initialWidth > glassLength)
        {
            InsertGlass();
        }
        
        if (Mathf.Abs(transform.position.x) + initialWidth > sandLength)
        {
            InsertSand();
        }

        foreach (var flock in flocks)
        {
            if ( flock.transform.position.x < -flockPrefabWidth)
            {
                var pos = flock.transform.position;
                flock.transform.position = new Vector3(flockPrefabWidth, pos.y, pos.z);
                return;
            } 
        }
    }
}
