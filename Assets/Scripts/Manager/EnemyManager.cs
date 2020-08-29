using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabEnemyBullet;
    
    [SerializeField] private GameObject prefabEnemyCorona;

    [SerializeField] private GameObject prefabEnemyStar;

    [SerializeField] private GameObject prefabEnemyHexagon;

    [SerializeField] private GameObject prefabEnemyCount;

    [SerializeField] private GameObject prefabEnemyCanon;

    [SerializeField] private GameObject prefabEnemyGleam;

    [SerializeField] private GameObject prefabItemPoint;

    [SerializeField] private GameObject prefabItemLife;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateEnemyBullet(string bodyColor, float positionX, float positionY, float speedX, float speedY)
    {
        Instantiate(prefabEnemyBullet, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f))
            .GetComponent<EnemyBullet>().Initialize(bodyColor, speedX, speedY);
    }
    public void GenerateEnemyCorona(string bodyColor, float positionX, float positionY, float speedX, float speedY,
        int increaseCode)
    {
        Instantiate(prefabEnemyCorona, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f))
            .GetComponent<EnemyCorona>().Initialize(bodyColor, speedX, speedY, increaseCode);
    }

    public void GenerateEnemyStar(string bodyColor, float positionX, float positionY, float speedX, float speedY,
        int increaseCode)
    {
        Instantiate(prefabEnemyStar, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f))
            .GetComponent<EnemyStar>().Initialize(bodyColor, speedX, speedY, increaseCode);
    }

    public void GenerateEnemyHexagon(string bodyColor, float positionX, float positionY, float speedX, float speedY,
        int increaseCode)
    {
        Instantiate(prefabEnemyHexagon, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f))
            .GetComponent<EnemyHexagon>().Initialize(bodyColor, speedX, speedY, increaseCode);
    }

    public void GenerateEnemyCount(string bodyColor, float positionX, float positionY, float speedX, float speedY,
        int increaseCode, int bodyCount)
    {
        Instantiate(prefabEnemyCount, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f))
            .GetComponent<EnemyCount>().Initialize(bodyColor, speedX, speedY, increaseCode, bodyCount);
    }

    public void GenerateEnemyGleam(string bodyColor, float positionX, float positionY, float speedX, float speedY,
        int increaseCode, int intervalCount)
    {
        Instantiate(prefabEnemyGleam, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f))
            .GetComponent<EnemyGleam>().Initialize(bodyColor, speedX, speedY, increaseCode, intervalCount);
    }

    public void GenerateEnemyCanon(string bodyColor, float positionX, float positionY, int bulletAmount)
    {
        Instantiate(prefabEnemyCanon, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f))
            .GetComponent<EnemyCanon>().Initialize(bodyColor, bulletAmount);
    }

    public void GenerateItemPoint(float positionX, float positionY, float speedX, float speedY, int score)
    {
        Instantiate(prefabItemPoint, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f))
            .GetComponent<ItemPoint>().Initialize(speedX, speedY, score);
    }

    public void GenerateItemLife(float positionX, float positionY, float speedX, float speedY)
    {
        Instantiate(prefabItemLife, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f))
            .GetComponent<ItemLife>().Initialize(speedX, speedY);
    }
}
