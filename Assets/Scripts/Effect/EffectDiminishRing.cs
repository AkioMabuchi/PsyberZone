using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EffectDiminishRing : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private float maxSize;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        StartCoroutine(CoroutineIntroduction());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CoroutineIntroduction()
    {
        transform.DOScale(maxSize, 3.0f);
        yield return null;
        _spriteRenderer.DOColor(new Color(1.0f, 1.0f, 1.0f, 0.0f), 3.0f);
    }
}
