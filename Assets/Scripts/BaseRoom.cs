using System;
using UnityEngine;

public abstract class BaseRoom : MonoBehaviour
{
    public abstract void Init(Action onRoomClosed);
    public abstract void Hide();

    public abstract void Show();
}
