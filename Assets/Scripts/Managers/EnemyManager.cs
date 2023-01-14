//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//#endregion


public class EnemyManager : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private float delaySpawn;
    [SerializeField] private float randomeDelaySpawn;

    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float randomeRangePosition;

    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform world;

    [Header("Tweens")]
    [SerializeField] private TweenConfig tweenMove;

    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties
    private Tween tweenSpawn;
    //#endregion


    //#region life-cycle callbacks

    void Awake()
    {
        SetCurrentSpeed();
        StartSpawn();
    }

    void OnEnable()
    {
        GameEventManager.ChangeLevelSpeed.AddListener(OnChangeLevelSpeed);
    }

    void OnDisable()
    {
        GameEventManager.ChangeLevelSpeed.RemoveListener(OnChangeLevelSpeed);
    }

    //#endregion

    //#region public methods

    public void StartSpawn()
    {
        tweenSpawn = DOVirtual.DelayedCall(UnityEngine.Random.Range(delaySpawn - randomeDelaySpawn, delaySpawn + randomeDelaySpawn), SpawnEnemy);
        tweenSpawn.OnComplete(() =>
        {
            tweenSpawn.Restart();
        });

        SpawnEnemy();
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
        randomeDelaySpawn /= speed;
        delaySpawn /= speed;
        tweenMove.duration /= speed;
    }

    [NaughtyAttributes.Button]
    private void SpawnEnemy()
    {
        Vector3 position = spawnPosition;
        position.x = UnityEngine.Random.Range(-randomeRangePosition, randomeRangePosition);

        GameObject enemy = Instantiate(this.enemy, position, Quaternion.identity);
        enemy.transform.SetParent(world);

        MoveEnemy(enemy);
    }

    private void MoveEnemy(GameObject enemy)
    {
        Vector3 position = new Vector3(enemy.transform.localPosition.x, endPosition.y, endPosition.z);
        tweenMove.tween = enemy.transform.DOLocalMove(endPosition, tweenMove.duration);
        tweenMove.tween.SetEase(tweenMove.easing);

        tweenMove.tween.OnComplete(() =>
        {
            Destroy(enemy);
        });
    }


    //#endregion

    //#region event handlers

    protected void OnChangeLevelSpeed()
    {
        SetCurrentSpeed();
    }

    //#endregion
}
