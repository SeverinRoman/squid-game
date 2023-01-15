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

    void OnEnable()
    {
        GameEventManager.PlayerDeath.AddListener(OnPlayerDeath);
        GameEventManager.KillFirstEnemy.AddListener(OnKillFirstEnemy);
    }

    void OnDisable()
    {
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

        GameObject enemy = Instantiate(this.enemy, position, Quaternion.identity);
        enemy.transform.SetParent(world);

        MoveEnemy(enemy);
    }

    private void MoveEnemy(GameObject enemy)
    {
        Vector3 position = new Vector3(enemy.transform.localPosition.x, endPosition.y, endPosition.z);
        tweenMove.tween = enemy.transform.DOLocalMove(endPosition, tweenMove.duration);
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
            Destroy(enemy);
        });

        tweenMove.tweens.Add(tweenMove.tween);
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
    }

    //#endregion
}
