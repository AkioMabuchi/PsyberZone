using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject prefabNormalBullet;

    private string _bodyColor = "Yellow";
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        switch (_bodyColor)
        {
            case "Yellow":
                _spriteRenderer.color = new Color(1.0f,1.0f,0.5f);
                break;
            case "Cyan":
                _spriteRenderer.color = new Color(0.5f,1.0f,1.0f);
                break;
            case "Magenta":
                _spriteRenderer.color = new Color(1.0f,0.5f,1.0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Vector3 currentPosition = transform.position;
            float px = currentPosition.x + 50.0f;
            float py = currentPosition.y;
            float pz = currentPosition.z;
            Instantiate(prefabNormalBullet, new Vector3(px,py,pz), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<NormalBullet>().Initialize(_bodyColor);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            switch (_bodyColor)
            {
                case "Yellow":
                    _bodyColor = "Cyan";
                    _spriteRenderer.color = new Color(0.5f,1.0f,1.0f);
                    break;
                case "Cyan":
                    _bodyColor = "Magenta";
                    _spriteRenderer.color = new Color(1.0f,0.5f,1.0f);
                    break;
                case "Magenta":
                    _bodyColor = "Yellow";
                    _spriteRenderer.color = new Color(1.0f,1.0f,0.5f);
                    break;
            }
        }
    }
}
