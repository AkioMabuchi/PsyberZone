using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeFixer : MonoBehaviour
{
    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.width * 9 / Screen.height < 16)
        {
            _camera.orthographicSize = Screen.height * 960.0f / Screen.width;
        }
        else
        {
            _camera.orthographicSize = 540.0f;
        }
    }
}
