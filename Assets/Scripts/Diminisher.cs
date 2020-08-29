using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diminisher : MonoBehaviour
{
    [SerializeField] private float time;
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
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
