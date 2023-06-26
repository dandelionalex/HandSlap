using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float amplitude;
    public float amplitudeMax;
    Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = originalPos + Random.insideUnitSphere * amplitude;
    }

    void OnEnable()
    {
        originalPos = transform.localPosition;
    }

    private void OnDisable()
    {
        transform.localPosition = originalPos;
    }

}
