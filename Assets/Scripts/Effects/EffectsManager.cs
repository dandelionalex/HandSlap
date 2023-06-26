using System;
using System.Linq;
using UnityEngine;
using Utils;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] 
    private EffectConfig[] effects;

    [SerializeField] 
    private GameObject textHolder;
    
    public void ShowEffect(EffectData effectData)
    {
        var effectConfig = effects.FirstOrDefault(s => s.effectType == effectData.EffectType);
        if(effectConfig == null)
            return;
        
        var go = Instantiate(effectConfig.prefab, textHolder.transform);
        if( effectData.Duration < 0)
            return;
        
        var destroyByTime = go.GetComponent<DestroyByTime>();
        if(destroyByTime != null)
            destroyByTime.TimeToDestroy = effectData.Duration;

        var effect = go.GetComponent<IEffect>();
        if(effect != null)
            effect.SetValue(effectData.Value);
    }
    
    [Serializable]
    public class EffectConfig
    {
        public EffectType effectType;
        public GameObject prefab;
    }
}
