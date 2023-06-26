using DG.Tweening;
using UnityEngine;

public class FinalAnimManager : MonoBehaviour
{
    public GameObject createLine;
    public GameObject head;
    public GameObject tail;
    public GameObject heroTape;
    
    private CreateLine cl;
    private Tween headTween;
    private Tween tailTween;
    private Tween camTween;
    private Camera cam;
    private CameraShake camShake;

    private bool isPlaying;
    void Start()
    {
        cl = createLine.GetComponent<CreateLine>();
        headTween = head.GetComponent<DOTweenAnimation>().GetTweens()[0];
        tailTween = tail.GetComponent<DOTweenAnimation>().GetTweens()[0];

        cam = Camera.main;
        camShake = cam.GetComponent<CameraShake>();
        camTween = cam.GetComponent<DOTweenAnimation>().GetTweens()[0];
    }
    
    void Update()
    {
        //TODO consider touch phase  ( TouchPhase.Began, TouchPhase.Ended )
        if ( !isPlaying && (Input.GetKeyDown("space") || Input.touchCount > 0) )
        {
            isPlaying = true;
            cl.ShouldPlay();
            headTween.Play();
            tailTween.Play();
            camTween.Play();

            heroTape.SetActive(false);
            camShake.enabled = true;
            return;
        }
        
        if ( isPlaying && (Input.GetKeyDown("space") || Input.touchCount > 0) )
        {
            camShake.amplitude = camShake.amplitudeMax * cl.getAnimProgress() + 0.001f;
            return;
        }

        if ( isPlaying && ( Input.GetKeyUp("space") || Input.touchCount == 0) )
        {
            isPlaying = false;
            
            cl.ShouldPause();
            if (headTween.active)
                headTween.Pause();
            if (tailTween.active)
                tailTween.Pause();

            camTween.Pause();
            cam.GetComponent<CameraShake>().enabled = false;
        }
    }
    
}
