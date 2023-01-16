//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//#endregion


public class SpawnManager : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private float delaySpawn;
    [SerializeField] private int spawnObstacle;
    [SerializeField] private float modificatorSpeedObstacle;

    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float randomeRangePosition;

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject pushEnemy;
    [SerializeField] private Transform world;

    [Header("Tweens")]
    [SerializeField] private TweenConfig tweenMove;

    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties
    private Tween tweenSpawn;

    private int currentSpawnEnemy = 0;

    private List<GameObject> obstacles = new List<GameObject>();
    //#endregion


    //#region life-cycle callbacks

    void OnEnable()
    {
        GameEventManager.PrepereLevel.AddListener(OnPrepereLevel);
        GameEventManager.ChangeLevelSpeed.AddListener(OnChangeLevelSpeed);
        GameEventManager.PlayerDeath.AddListener(OnPlayerDeath);
        GameEventManager.KillFirstEnemy.AddListener(OnKillFirstEnemy);
    }

    void OnDisable()
    {
        GameEventManager.PrepereLevel.AddListener(OnPrepereLevel);
        GameEventManager.PlayerDeath.RemoveListener(OnPlayerDeath);
        GameEventManager.ChangeLevelSpeed.RemoveListener(OnChangeLevelSpeed);
        GameEventManager.KillFirstEnemy.RemoveListener(OnKillFirstEnemy);
    }

    //#endregion

    //#region public methods

    public void StartSpawn()
    {
        tweenSpawn = DOVirtual.DelayedCall(delaySpawn, SpawnEnemy);
        tweenSpawn.OnComplete(() =>
        {
            tweenSpawn.Restart();
        });

        SpawnEnemy();
        SetCurrentSpeed();
    }

    //#endregion

    //#region private methods

    private void SetCurrentSpeed()
    {
        float speed = 1;
        Action<float> callback = (value) =>
        {
            speed = value;
        };
        GameEventManager.GetLevelSpeed?.Invoke(callback);

        if (tweenSpawn == null) return;

        tweenSpawn.timeScale = speed;


        if (tweenMove.tweens.Count > 0)
        {
            foreach (Tween tween in tweenMove.tweens)
            {
                tween.timeScale = speed;
            }
        }
    }

    [NaughtyAttributes.Button]
    private void SpawnEnemy()
    {
        Vector3 position = spawnPosition;
        position.x = UnityEngine.Random.Range(-randomeRangePosition, randomeRangePosition);

        GameObject gameObject = null;

        if (currentSpawnEnemy == spawnObstacle)
        {
            currentSpawnEnemy = 0;
            gameObject = Instantiate(this.obstacle, Vector3.zero, Quaternion.identity);
            gameObject.transform.SetParent(world);
            gameObject.transform.localPosition = position;
            Move(gameObject, modificatorSpeedObstacle);
            obstacles.Add(gameObject);
        }
        else
        {
            gameObject = Instantiate(this.enemy, Vector3.zero, Quaternion.identity);
            gameObject.transform.SetParent(world);
            gameObject.transform.localPosition = position;
            Move(gameObject);
        }
        currentSpawnEnemy += 1;
    }

    private void Spawn(GameObject gameObject, bool isRandomPosition = true)
    {
        Vector3 position = spawnPosition;
        if (isRandomPosition)
        {
            position.x = UnityEngine.Random.Range(-randomeRangePosition, randomeRangePosition);
        }


        GameObject newGameObject = Instantiate(gameObject, position, Quaternion.identity);
        newGameObject.transform.SetParent(world);

        Move(newGameObject);
    }

    private void Move(GameObject gameObject, float modificatorSpeed = 0)
    {
        Vector3 position = new Vector3(gameObject.transform.localPosition.x, endPosition.y, gameObject.transform.localPosition.z);
        tweenMove.tween = gameObject.transform.DOLocalMove(position, tweenMove.duration + modificatorSpeed);
        tweenMove.tween.SetEase(tweenMove.easing);


        float speed = 1;
        Action<float> callback = (value) =>
        {
            speed = value;
        };
        GameEventManager.GetLevelSpeed?.Invoke(callback);

        tweenMove.tween.timeScale = speed;

        tweenMove.tween.OnComplete(() =>
        {
            Destroy(gameObject);
        });

        tweenMove.tweens.Add(tweenMove.tween);
    }

    private void PrepereLevel()
    {
        tweenSpawn.Pause();
        Spawn(pushEnemy, false);

        if (obstacles.Count == 0) return;

        foreach (GameObject gameObject in obstacles)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(false);
            }

        }
    }

    private void StopObstacle()
    {
        if (tweenMove.tweens.Count == 0) return;

        foreach (Tween tween in tweenMove.tweens)
        {
            if (tween.Duration() == tweenMove.duration + modificatorSpeedObstacle)
            {
                tween.Pause();
            }
        }
    }



    //#endregion

    //#region event handlers

    protected void OnChangeLevelSpeed()
    {
        SetCurrentSpeed();
    }

    protected void OnKillFirstEnemy()
    {
        StartSpawn();
        GameEventManager.KillFirstEnemy.RemoveListener(OnKillFirstEnemy);
        GameEventManager.ChangeLevelSpeed.AddListener(OnChangeLevelSpeed);
    }

    protected void OnPlayerDeath()
    {
        tweenSpawn.Pause();

        StopObstacle();
    }

    protected void OnPrepereLevel()
    {
        PrepereLevel();
    }

    //#endregion
}
