public class EffectData 
{
    public EffectType EffectType { get; }

    public int Value { get; }

    public float Duration { get; }

    public EffectData(EffectType effectType, float duration =- 1, int value = 0)
    {
        EffectType = effectType;
        Duration = duration;
        Value = value;
    }
}
