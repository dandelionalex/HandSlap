using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] 
    private GameObject flexTypePrefab;

    [SerializeField] 
    private Transform flexTypeParent;

    private void Start()
    {
        Instantiate(flexTypePrefab, flexTypeParent);
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
