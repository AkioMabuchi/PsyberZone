using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DG.Tweening;
using UnityEngine;

public class EffectDiminishParticle : MonoBehaviour
{
    [SerializeField] private float ea;
    [SerializeField] private float ex;
    [SerializeField] private float ey;
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.DOFade(0.0f, 0.5f);

        transform.DOLocalMove(new Vector3(ea * ex, ea * ey, 0.0f), 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
