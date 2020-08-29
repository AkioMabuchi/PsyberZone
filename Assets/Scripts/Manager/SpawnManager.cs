using System;
using System.Collections;
using System.Collections.Generic;
using DG.DemiLib;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private GameManager _gameManager;
    private MainSceneManager _mainSceneManager;
    private MainGameStateManager _mainGameStateManager;
    private EnemyManager _enemyManager;
    private CanvasMain _canvasMain;


    private Coroutine _activeCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _mainSceneManager = GameObject.Find("MainSceneManager").GetComponent<MainSceneManager>();
        _mainGameStateManager = GameObject.Find("MainGameStateManager").GetComponent<MainGameStateManager>();
        _enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        _canvasMain = GameObject.Find("CanvasMain").GetComponent<CanvasMain>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry()
    {
        StartCoroutine(CoroutineRetry());

    }

    public void ActivateCoroutineNormalMode()
    {
        _activeCoroutine = StartCoroutine(CoroutineNormalMode());
    }

    public void ActivateCoroutineTutorialMode()
    {
        _activeCoroutine = StartCoroutine(CoroutineTutorialMode());
    }

    IEnumerator CoroutineRetry()
    {
        GameObject[] gameObjectsEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject gameObjectEnemy in gameObjectsEnemies)
        {
            Destroy(gameObjectEnemy);
        }

        GameObject[] gameObjectsItems = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject gameObjectItem in gameObjectsItems)
        {
            Destroy(gameObjectItem);
        }

        GameObject[] gameObjectsEffects = GameObject.FindGameObjectsWithTag("Effect");
        foreach (GameObject gameObjectEffect in gameObjectsEffects)
        {
            Destroy(gameObjectEffect);
        }
        
        StopCoroutine(_activeCoroutine);

        yield return new WaitForSeconds(2.0f);
        
        ActivateCoroutineNormalMode();
    }

    IEnumerator CoroutineNormalMode()
    {
        int count = 0;
        while (true)
        {
            string[] bodyColors = {"Yellow", "Cyan", "Magenta"};
            string[] bodyColors2 = {"White", "Yellow", "Yellow", "Cyan", "Cyan", "Magenta", "Magenta"};
            int nextSpawnCount = 50;
            int max = 1;
            count++;
            switch (_mainGameStateManager.GetLevel())
            {
                case 1:
                    nextSpawnCount = Random.Range(60, 100);
                    max = Random.Range(1, 3);
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-220.0f, -150.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        _enemyManager.GenerateEnemyCorona(bodyColor, positionX, positionY, speedX, speedY,0);

                    }
                    break;
                case 2:
                    nextSpawnCount = Random.Range(50, 80);
                    max = Random.Range(2, 4);
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-260.0f, -180.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        if (Random.Range(0, 20) == 0) 
                        {
                            _enemyManager.GenerateEnemyHexagon(bodyColor, positionX, positionY, speedX, speedY, 0);
                        }
                        else
                        {
                            _enemyManager.GenerateEnemyCorona(bodyColor, positionX, positionY, speedX, speedY, 0);
                        }
                    }
                    
                    if (count % 30 == 0)
                    {
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -250.0f;
                        float speedY = Random.Range(-50.0f, 50.0f);
                        _enemyManager.GenerateItemPoint(positionX, positionY, speedX, speedY, 10000);
                    }
                    break;
                case 3:
                    nextSpawnCount = Random.Range(40, 60);
                    max = Random.Range(2, 5);
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-300.0f, -200.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        if (Random.Range(0, 20) == 0)
                        {
                            _enemyManager.GenerateEnemyHexagon(bodyColor, positionX, positionY, speedX, speedY,0);
                        }
                        else
                        {
                            _enemyManager.GenerateEnemyCorona(bodyColor, positionX, positionY, speedX, speedY, 0);
                        }

                    }

                    if (count % 16 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -1000.0f;
                        float speedY = 0.0f;
                        _enemyManager.GenerateEnemyStar(bodyColor, positionX, positionY, speedX, speedY, 0);
                    }

                    if (count % 22 == 0)
                    {
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -250.0f;
                        float speedY = Random.Range(-50.0f, 50.0f);
                        _enemyManager.GenerateItemPoint(positionX, positionY, speedX, speedY, 20000);
                    }
                    
                    break;
                
                case 4:
                    nextSpawnCount = Random.Range(40, 60);
                    max = Random.Range(2, 5);
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor;
                        if (Random.Range(0, 10) == 0)
                        {
                            bodyColor = "White";
                        }
                        else
                        {
                            bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        }

                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-300.0f, -200.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        if (Random.Range(0, 10) == 0 && bodyColor != "White")
                        {
                            _enemyManager.GenerateEnemyHexagon(bodyColor, positionX, positionY, speedX, speedY,0);
                        }
                        else
                        {
                            _enemyManager.GenerateEnemyCorona(bodyColor, positionX, positionY, speedX, speedY, 0);
                        }
                        

                    }
                    
                    if (count % 5 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-300.0f, -200.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        int bodyCount = Random.Range(2, 5);
                        _enemyManager.GenerateEnemyCount(bodyColor, positionX, positionY, speedX, speedY, 0, bodyCount);
                    }
                    
                    if (count % 14 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -1000.0f;
                        float speedY = 0.0f;
                        _enemyManager.GenerateEnemyStar(bodyColor, positionX, positionY, speedX, speedY, 0);
                    }
                    
                    if (count % 22 == 0)
                    {
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -250.0f;
                        float speedY = Random.Range(-50.0f, 50.0f);
                        _enemyManager.GenerateItemPoint(positionX, positionY, speedX, speedY, 25000);
                    }
                    break;
                
                case 5:
                    nextSpawnCount = Random.Range(40, 60);
                    max = Random.Range(2, 6);
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor;
                        if (Random.Range(0, 10) == 0)
                        {
                            bodyColor = "White";
                        }
                        else
                        {
                            bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        }

                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-350.0f, -250.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        if (Random.Range(0, 8) == 0 && bodyColor != "White")
                        {
                            _enemyManager.GenerateEnemyHexagon(bodyColor, positionX, positionY, speedX, speedY,0);
                        }
                        else
                        {
                            _enemyManager.GenerateEnemyCorona(bodyColor, positionX, positionY, speedX, speedY, 0);
                        }

                    }

                    if (count % 4 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-350.0f, -250.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        int bodyCount = Random.Range(3, 5);
                        _enemyManager.GenerateEnemyCount(bodyColor, positionX, positionY, speedX, speedY, 0, bodyCount);
                    }
                    if (count % 13 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -1200.0f;
                        float speedY = 0.0f;
                        _enemyManager.GenerateEnemyStar(bodyColor, positionX, positionY, speedX, speedY, 0);
                    }

                    if (count % 10 == 0) 
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-480.0f, 480.0f);
                        int bulletAmount = 3;
                        _enemyManager.GenerateEnemyCanon(bodyColor, positionX, positionY, bulletAmount);
                    }
                    
                    if (count % 25 == 0)
                    {
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -250.0f;
                        float speedY = Random.Range(-50.0f, 50.0f);
                        _enemyManager.GenerateItemPoint(positionX, positionY, speedX, speedY, 30000);
                    }

                    break;
                case 6:
                    nextSpawnCount = Random.Range(40, 60);
                    max = Random.Range(3, 6);
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor;
                        if (Random.Range(0, 10) == 0)
                        {
                            bodyColor = "White";
                        }
                        else
                        {
                            bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        }

                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-400.0f, -300.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        if (Random.Range(0, 5) == 0 && bodyColor != "White")
                        {
                            _enemyManager.GenerateEnemyHexagon(bodyColor, positionX, positionY, speedX, speedY,0);
                        }
                        else
                        {
                            _enemyManager.GenerateEnemyCorona(bodyColor, positionX, positionY, speedX, speedY, 0);
                        }

                    }

                    if (count % 3 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-400.0f, -300.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        int bodyCount = Random.Range(3, 6);
                        _enemyManager.GenerateEnemyCount(bodyColor, positionX, positionY, speedX, speedY, 0, bodyCount);
                    }
                    if (count % 12 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -1200.0f;
                        float speedY = 0.0f;
                        _enemyManager.GenerateEnemyStar(bodyColor, positionX, positionY, speedX, speedY, 0);
                    }

                    if (count % 10 == 0) 
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-480.0f, 480.0f);
                        int bulletAmount = 3;
                        _enemyManager.GenerateEnemyCanon(bodyColor, positionX, positionY, bulletAmount);
                    }
                    
                    if (count % 25 == 0)
                    {
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -250.0f;
                        float speedY = Random.Range(-50.0f, 50.0f);
                        _enemyManager.GenerateItemPoint(positionX, positionY, speedX, speedY, 30000);
                    }

                    break;
                case 7:
                    nextSpawnCount = Random.Range(40, 60);
                    max = Random.Range(4, 7);
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor;
                        if (Random.Range(0, 8) == 0)
                        {
                            bodyColor = "White";
                        }
                        else
                        {
                            bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        }

                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-400.0f, -300.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        if (Random.Range(0, 5) == 0)
                        {
                            _enemyManager.GenerateEnemyHexagon(bodyColor, positionX, positionY, speedX, speedY,0);
                        }
                        else
                        {
                            _enemyManager.GenerateEnemyCorona(bodyColor, positionX, positionY, speedX, speedY, 0);
                        }

                    }

                    if (count % 2 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-400.0f, -300.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        int bodyCount = Random.Range(3, 7);
                        _enemyManager.GenerateEnemyCount(bodyColor, positionX, positionY, speedX, speedY, 0, bodyCount);
                    }
                    if (count % 10 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -1500.0f;
                        float speedY = 0.0f;
                        _enemyManager.GenerateEnemyStar(bodyColor, positionX, positionY, speedX, speedY, 0);
                    }

                    if (count % 10 == 0) 
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-480.0f, 480.0f);
                        int bulletAmount = 3;
                        _enemyManager.GenerateEnemyCanon(bodyColor, positionX, positionY, bulletAmount);
                    }
                    
                    if (count % 25 == 0)
                    {
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -250.0f;
                        float speedY = Random.Range(-50.0f, 50.0f);
                        _enemyManager.GenerateItemPoint(positionX, positionY, speedX, speedY, 40000);
                    }

                    break;
                case 8:
                    nextSpawnCount = Random.Range(35, 50);
                    max = Random.Range(4, 7);
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor;
                        if (Random.Range(0, 8) == 0)
                        {
                            bodyColor = "White";
                        }
                        else
                        {
                            bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        }

                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-500.0f, -400.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        if (Random.Range(0, 5) == 0)
                        {
                            _enemyManager.GenerateEnemyHexagon(bodyColor, positionX, positionY, speedX, speedY,0);
                        }
                        else
                        {
                            _enemyManager.GenerateEnemyCorona(bodyColor, positionX, positionY, speedX, speedY, 0);
                        }

                    }

                    if (count % 2 == 0)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                            float positionX = 1000.0f;
                            float positionY = Random.Range(-550.0f, 550.0f);
                            float speedX = Random.Range(-500.0f, -400.0f);
                            float speedY = Random.Range(-50.0f, 50.0f);
                            int bodyCount = Random.Range(3, 7);
                            _enemyManager.GenerateEnemyCount(bodyColor, positionX, positionY, speedX, speedY, 0,
                                bodyCount);
                        }
                    }
                    if (count % 12 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -2000.0f;
                        float speedY = 0.0f;
                        _enemyManager.GenerateEnemyStar(bodyColor, positionX, positionY, speedX, speedY, 0);
                    }

                    if (count % 12 == 0) 
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-480.0f, 480.0f);
                        int bulletAmount = 5;
                        _enemyManager.GenerateEnemyCanon(bodyColor, positionX, positionY, bulletAmount);
                    }

                    if (count % 30 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = Random.Range(-750.0f, -500.0f);
                        float speedY = 0.0f;
                        int intervalCount = 50;
                        _enemyManager.GenerateEnemyGleam(bodyColor, positionX, positionY, speedX, speedY, 0,
                            intervalCount);
                    }
                    
                    if (count % 35 == 0)
                    {
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -250.0f;
                        float speedY = Random.Range(-50.0f, 50.0f);
                        _enemyManager.GenerateItemPoint(positionX, positionY, speedX, speedY, 50000);
                    }

                    break;
                case 9:
                    nextSpawnCount = Random.Range(35, 50);
                    max = Random.Range(4, 7);
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor;
                        if (Random.Range(0, 8) == 0)
                        {
                            bodyColor = "White";
                        }
                        else
                        {
                            bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        }

                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-500.0f, -400.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        if (Random.Range(0, 3) == 0)
                        {
                            _enemyManager.GenerateEnemyHexagon(bodyColor, positionX, positionY, speedX, speedY,0);
                        }
                        else
                        {
                            _enemyManager.GenerateEnemyCorona(bodyColor, positionX, positionY, speedX, speedY, 0);
                        }

                    }

                    if (count % 2 == 0)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                            float positionX = 1000.0f;
                            float positionY = Random.Range(-550.0f, 550.0f);
                            float speedX = Random.Range(-500.0f, -400.0f);
                            float speedY = Random.Range(-50.0f, 50.0f);
                            int bodyCount = Random.Range(3, 7);
                            _enemyManager.GenerateEnemyCount(bodyColor, positionX, positionY, speedX, speedY, 0,
                                bodyCount);
                        }
                    }
                    if (count % 6 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -2000.0f;
                        float speedY = 0.0f;
                        _enemyManager.GenerateEnemyStar(bodyColor, positionX, positionY, speedX, speedY, 0);
                    }

                    if (count % 12 == 0) 
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-480.0f, 480.0f);
                        int bulletAmount = 5;
                        _enemyManager.GenerateEnemyCanon(bodyColor, positionX, positionY, bulletAmount);
                    }

                    if (count % 30 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = Random.Range(-750.0f, -500.0f);
                        float speedY = 0.0f;
                        int intervalCount = 50;
                        _enemyManager.GenerateEnemyGleam(bodyColor, positionX, positionY, speedX, speedY, 0,
                            intervalCount);
                    }
                    
                    if (count % 50 == 0)
                    {
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -250.0f;
                        float speedY = Random.Range(-50.0f, 50.0f);
                        _enemyManager.GenerateItemPoint(positionX, positionY, speedX, speedY, 60000);
                    }
                    break;
                case 10:
                    nextSpawnCount = Random.Range(35, 50);
                    max = Random.Range(4, 7);
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor = bodyColors2[Random.Range(0, bodyColors2.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-500.0f, -400.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        if (Random.Range(0, 3) == 0)
                        {
                            _enemyManager.GenerateEnemyHexagon(bodyColor, positionX, positionY, speedX, speedY,0);
                        }
                        else
                        {
                            _enemyManager.GenerateEnemyCorona(bodyColor, positionX, positionY, speedX, speedY, 0);
                        }

                    }

                    max = Random.Range(1, 4);
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-500.0f, -400.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        int bodyCount = Random.Range(5, 8);
                        _enemyManager.GenerateEnemyCount(bodyColor, positionX, positionY, speedX, speedY, 0,
                            bodyCount);
                    }

                    if (count % 6 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -2000.0f;
                        float speedY = 0.0f;
                        _enemyManager.GenerateEnemyStar(bodyColor, positionX, positionY, speedX, speedY, 0);
                    }

                    if (count % 10 == 0) 
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-480.0f, 480.0f);
                        int bulletAmount = 10;
                        _enemyManager.GenerateEnemyCanon(bodyColor, positionX, positionY, bulletAmount);
                    }

                    if (count % 30 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = Random.Range(-300.0f, -200.0f);
                        float speedY = 0.0f;
                        int intervalCount = 50;
                        _enemyManager.GenerateEnemyGleam(bodyColor, positionX, positionY, speedX, speedY, 0,
                            intervalCount);
                    }
                    
                    if (count % 50 == 0)
                    {
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -250.0f;
                        float speedY = Random.Range(-50.0f, 50.0f);
                        _enemyManager.GenerateItemPoint(positionX, positionY, speedX, speedY, 70000);
                    }
                    break;
                case 11:
                    nextSpawnCount = Random.Range(25, 40);
                    max = Random.Range(4, 7);
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor = bodyColors2[Random.Range(0, bodyColors2.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-550.0f, -500.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        if (Random.Range(0, 3) == 0)
                        {
                            _enemyManager.GenerateEnemyCorona(bodyColor, positionX, positionY, speedX, speedY, 0);
                        }
                        else
                        {
                            _enemyManager.GenerateEnemyHexagon(bodyColor, positionX, positionY, speedX, speedY,0);
                        }

                    }

                    max = Random.Range(2, 4);
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-550.0f, -500.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        int bodyCount = Random.Range(5, 8);
                        _enemyManager.GenerateEnemyCount(bodyColor, positionX, positionY, speedX, speedY, 0,
                            bodyCount);
                    }

                    if (count % 6 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -2000.0f;
                        float speedY = 0.0f;
                        _enemyManager.GenerateEnemyStar(bodyColor, positionX, positionY, speedX, speedY, 0);
                    }

                    if (count % 10 == 0) 
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-480.0f, 480.0f);
                        int bulletAmount = 10;
                        _enemyManager.GenerateEnemyCanon(bodyColor, positionX, positionY, bulletAmount);
                    }

                    if (count % 20 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = Random.Range(-300.0f, -200.0f);
                        float speedY = 0.0f;
                        int intervalCount = 50;
                        _enemyManager.GenerateEnemyGleam(bodyColor, positionX, positionY, speedX, speedY, 0,
                            intervalCount);
                    }
                    
                    if (count % 80 == 0)
                    {
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -250.0f;
                        float speedY = Random.Range(-50.0f, 50.0f);
                        _enemyManager.GenerateItemPoint(positionX, positionY, speedX, speedY, 80000);
                    }
                    break;
                case 12:
                    nextSpawnCount = Random.Range(10, 25);
                    max = Random.Range(2, 5);
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor = bodyColors2[Random.Range(0, bodyColors2.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-550.0f, -500.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        if (Random.Range(0, 3) == 0)
                        {
                            _enemyManager.GenerateEnemyCorona(bodyColor, positionX, positionY, speedX, speedY, 0);
                        }
                        else
                        {
                            _enemyManager.GenerateEnemyHexagon(bodyColor, positionX, positionY, speedX, speedY,0);
                        }

                    }

                    max = Random.Range(0, 3);
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-550.0f, -500.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        int bodyCount = Random.Range(5, 8);
                        _enemyManager.GenerateEnemyCount(bodyColor, positionX, positionY, speedX, speedY, 0,
                            bodyCount);
                    }

                    if (count % 6 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -2000.0f;
                        float speedY = 0.0f;
                        _enemyManager.GenerateEnemyStar(bodyColor, positionX, positionY, speedX, speedY, 0);
                    }

                    if (count % 10 == 0) 
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-480.0f, 480.0f);
                        int bulletAmount = 10;
                        _enemyManager.GenerateEnemyCanon(bodyColor, positionX, positionY, bulletAmount);
                    }

                    if (count % 20 == 0)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = Random.Range(-300.0f, -200.0f);
                        float speedY = 0.0f;
                        int intervalCount = 50;
                        _enemyManager.GenerateEnemyGleam(bodyColor, positionX, positionY, speedX, speedY, 0,
                            intervalCount);
                    }
                    
                    if (count % 200 == 0)
                    {
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-500.0f, 500.0f);
                        float speedX = -250.0f;
                        float speedY = Random.Range(-50.0f, 50.0f);
                        _enemyManager.GenerateItemPoint(positionX, positionY, speedX, speedY, 100000);
                    }
                    break;
                
                default:
                    nextSpawnCount = Random.Range(10, 20);
                    max = 10;
                    for (int i = 0; i < max; i++)
                    {
                        string bodyColor = bodyColors[Random.Range(0, bodyColors.Length)];
                        float positionX = 1000.0f;
                        float positionY = Random.Range(-550.0f, 550.0f);
                        float speedX = Random.Range(-800.0f, -500.0f);
                        float speedY = Random.Range(-50.0f, 50.0f);
                        _enemyManager.GenerateEnemyCorona(bodyColor, positionX, positionY, speedX, speedY,0);

                    }
                    break;
                    

            }
            if (count % 130 == 50)
            {
                float positionX = 1000.0f;
                float positionY = Random.Range(-480.0f, 480.0f);
                float speedX = -200.0f;
                float speedY = Random.Range(-50.0f, 50.0f);
                _enemyManager.GenerateItemLife(positionX, positionY, speedX, speedY);
            }

            for (int i = 0; i < nextSpawnCount; i++)
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }

    IEnumerator CoroutineTutorialMode()
    {
        _canvasMain.ShowMessage("この度はゲームをプレイしていただき、ありがとう");
        yield return new WaitForSeconds(8.0f);
        _canvasMain.ShowMessage("ここは人々の精神状況などが実体化された\n「PsyberZone」と呼ばれる空間\nある日突如、この空間に危険な精神物体が大量発生");
        yield return new WaitForSeconds(8.0f);
        _canvasMain.ShowMessage("まぁとにかく早速、基本的な操作を説明するよ");
        yield return new WaitForSeconds(8.0f);
        _canvasMain.ShowMessage("まず「WASD」で上下左右に移動");
        yield return new WaitForSeconds(8.0f);
        _canvasMain.ShowMessage("次に「J」で弾を前方に発射");
        yield return new WaitForSeconds(8.0f);
        _canvasMain.ShowMessage("「L」で本体と弾の色を変更\n（順番は「<color=#FFFF7F>黄</color>→<color=#7FFFFF>水</color>→<color=#FF7FFF>桃</color>→<color=#FFFF7F>黄</color>」）");
        yield return new WaitForSeconds(8.0f);
        _enemyManager.GenerateEnemyCorona("Yellow",1000.0f,300.0f,-200.0f,0.0f,0);
        _enemyManager.GenerateEnemyCorona("Cyan",1000.0f,0.0f,-200.0f,0.0f,0);
        _enemyManager.GenerateEnemyCorona("Magenta",1000.0f,-300.0f,-200.0f,0.0f,0);
        _canvasMain.ShowMessage("敵にも<color=#FFFF7F>「黄」</color><color=#7FFFFF>「水」</color><color=#FF7FFF>「桃」</color>の三色が存在\n敵と同じ色の弾を当てよう\n敵と違う色の弾を当てると増殖など負の効果があるよ");
        yield return new WaitForSeconds(8.0f);
        _canvasMain.ShowMessage("本体を画面の右側に寄せれば\n獲得ポイントの倍率が高くなるよ");
        yield return new WaitForSeconds(4.0f);
        _enemyManager.GenerateEnemyCorona("Yellow",1000.0f,300.0f,-250.0f,0.0f,0);
        _enemyManager.GenerateEnemyCorona("Cyan",1000.0f,0.0f,-250.0f,0.0f,0);
        _enemyManager.GenerateEnemyCorona("Magenta",1000.0f,-300.0f,-250.0f,0.0f,0);
        yield return new WaitForSeconds(4.0f);
        _enemyManager.GenerateEnemyHexagon("White",1000.0f,0.0f,-300.0f,0.0f,0);
        _canvasMain.ShowMessage("白い敵も存在、この敵は倒すことができないよ");
        yield return new WaitForSeconds(8.0f);
        _enemyManager.GenerateItemPoint(1000.0f, 300.0f, -200.0f, 0.0f, 100000);
        _enemyManager.GenerateItemPoint(1000.0f, -300.0f, -200.0f, 0.0f, 100000);
        _canvasMain.ShowMessage("<color=#CFFF00>黄緑色</color>に光る物体はアイテム、\n本体と接触するとゲットだよ");
        yield return new WaitForSeconds(4.0f);
        _enemyManager.GenerateItemLife(1000.0f, 0.0f, -200.0f, 0.0f);
        yield return new WaitForSeconds(4.0f);
        _canvasMain.ShowMessage("左上のライフゲージが0になると\nゲームオーバー");
        yield return new WaitForSeconds(8.0f);
        _canvasMain.ShowMessage("チュートリアルはこれでおしまい");
        yield return new WaitForSeconds(8.0f);
        _gameManager.ReturnToTitle2();
    }
}
