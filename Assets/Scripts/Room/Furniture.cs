using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    private List<GameObject> furnitureItems = new List<GameObject>();
    private List<Sequence> itemTweens = new List<Sequence>();

    public float height = 20;
    
    protected void OnEnable()
    {
        if(isStatic)
            return;
        
        int itemsCount = transform.childCount;
        for (int i = 0; i < itemsCount; i++)
        {
            furnitureItems.Add(transform.GetChild(i).gameObject);
        }
        
        PrepareItems();
    }

    protected void PrepareItems()
    {
        for (int i = 0; i < furnitureItems.Count; i++)
        {
            // setting DOTWeen animation sequence for every item
            var s = DOTween.Sequence().Pause();
            s.Append(furnitureItems[i].transform.DOMoveY(Random.Range(height * 0.8f, height * 1.2f), Random.Range(0.8f, 1f))
                .From()
                .SetEase(Ease.OutBounce)
                );
            itemTweens.Add(s);
        }
    }

    private bool isStatic = false;
    public void RevealItem(bool isStatic = false)
    {
        this.isStatic = isStatic;
        var customBehaviour = GetComponent<ICustomFurnitureReveal>();
        if (customBehaviour != null)
        {
            customBehaviour.RevealItem();
            return;
        }
        
        gameObject.SetActive(true);
        for (int i = 0; i < itemTweens.Count; i++)
        {
            itemTweens[i].Play();
        }
    }

}
