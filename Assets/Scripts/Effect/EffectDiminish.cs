using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDiminish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoroutineIntroduction());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CoroutineIntroduction()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
