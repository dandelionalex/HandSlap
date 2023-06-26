using UnityEngine;

public class FlexType : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if(!collider.gameObject.CompareTag("Sticky")) // TODO use composite type for all surfaces
            return;

        collider.GetComponent<ISticky>()?.StickTape(transform);
        Destroy(this);
    }
}