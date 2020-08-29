using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class SliderHelper : MonoBehaviour, IPointerUpHandler
{
    private TitleSceneManager _titleSceneManager;
    // Start is called before the first frame update
    void Start()
    {
        _titleSceneManager = GameObject.Find("TitleSceneManager").GetComponent<TitleSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _titleSceneManager.SoundTest();
    }
}
