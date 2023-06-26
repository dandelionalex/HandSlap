using System;
using UnityEngine;

public abstract class BaseLevelBehaviour : MonoBehaviour
{
    public abstract void Init(Action<LevelState> onLevelStateChanged);
    public abstract float TimePassed { get; protected set; }

    public abstract LevelState LevelState { get; protected set; }

    public abstract void  SetTutorialPauseLevel( bool value);
}
