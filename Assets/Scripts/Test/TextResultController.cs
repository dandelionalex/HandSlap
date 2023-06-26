using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TextResultController : MonoBehaviour
{

    Sequence anim;

    private void Start()
    {
        /*        transform.localScale = Vector3.zero;
                DOTween.Init();
                anim = DOTween.Sequence()
                    .Append(transform.DOScale(1, 0.1f))
                    .PrependInterval(1)
                    .Append(transform.DOMoveZ(0.2f, 1f))
          */
        Destroy(gameObject, 2f);
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
