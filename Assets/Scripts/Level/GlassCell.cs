using System;
using UnityEngine;

public class GlassCell : MonoBehaviour, ISticky, IHasLeackage
{
    [SerializeField]
    private GameObject waterEffect;

    [SerializeField] 
    private Transform leakageParent;
    
    private GameObject leakage;
    private GameSettings settings;
    private SoundManager soundManager;
    private ScoreManager scoreManager;
    private EffectsManager effectsManager;
    private bool alreadyFixed;
    private MoveController moveController;
    private LevelBehaviour levelBehaviour;
    private FirebaseController firebaseController;
    
    private void Start()
    {
        settings = FindObjectOfType<GameSettings>();
        soundManager = FindObjectOfType<SoundManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        effectsManager = FindObjectOfType<EffectsManager>();
        moveController = FindObjectOfType<MoveController>();
        levelBehaviour = FindObjectOfType<LevelBehaviour>();
        firebaseController = FindObjectOfType<FirebaseController>();
    }

    public void StickTape(Transform tapeTransform)
    {
        tapeTransform.SetParent(transform, true);
        var tapeLocalPosition = tapeTransform.localPosition;
        tapeLocalPosition.z = 0;
        tapeTransform.localPosition = tapeLocalPosition;

        var diff = 0.0f;

        if (leakage != null)
        {
            diff = tapeLocalPosition.x - leakage.transform.localPosition.x;
        } else
        {
            diff = 100f; //assigning a big number to not produce a hit;
        }
        
        if (Math.Abs(diff) < settings.PerfectHitDistance && !alreadyFixed)
        {
            soundManager.PlaySound(SoundType.LeakageFixed);
            leakage.GetComponent<Leakage>().FixLeakage(true);
            scoreManager.FixLeakage(true);
            effectsManager.ShowEffect(new EffectData(EffectType.PerfectFixed));
            Instantiate(waterEffect, leakage.transform);
            alreadyFixed = true;
            moveController.IncreaseSpeed();
        }
        else if (Math.Abs(diff) < settings.HitDistance && !alreadyFixed)
        {
            soundManager.PlaySound(SoundType.LeakageFixed);
            leakage.GetComponent<Leakage>().FixLeakage(false);
            scoreManager.FixLeakage(false);
            effectsManager.ShowEffect(new EffectData(EffectType.Fixed));
            Instantiate(waterEffect, leakage.transform);
            alreadyFixed = true;
            moveController.IncreaseSpeed();
        }
        else
        {
            soundManager.PlaySound(SoundType.Slap);
            scoreManager.MissHit();
            effectsManager.ShowEffect(new EffectData(EffectType.Missed));
            moveController.ResetSpeed();
        }

        if (levelBehaviour.LevelState == LevelState.TutorialPaused)
        {
            firebaseController.TutorialFinishedEvent();
            levelBehaviour.SetTutorialPauseLevel(false);
        }
            
        
    }

    public void PlaceWaterLeakage(GameObject leakageTransform)
    {
        leakage = Instantiate(leakageTransform, leakageParent, false);
    }

}
