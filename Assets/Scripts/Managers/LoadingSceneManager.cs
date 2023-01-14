//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
//#endregion


public class LoadingSceneManager : MonoBehaviour
{
    //#region editors fields and properties

    [SerializeField] private float durationLoad = 0;

    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    //#endregion


    //#region life-cycle callbacks

    void OnEnable()
    {
        GameEventManager.LoadScene.AddListener(OnLoadScene);
    }

    void OnDisable()
    {
        GameEventManager.LoadScene.RemoveListener(OnLoadScene);
    }

    //#endregion

    //#region public methods

    private void LoadScene(string sceneName)
    {
        GameEventManager.ToggleScreen?.Invoke(UIScreenType.LoadScreen, true);

        int currentValue = 0;
        Tween tweenWait = DOTween.To(() => currentValue, x => currentValue = x, 0, durationLoad);
        tweenWait.OnComplete(() =>
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        });

    }

    //#endregion

    //#region private methods

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);
            GameEventManager.ChangeProgressValue?.Invoke(progress);

            yield return null;
        }

    }

    //#endregion

    //#region event handlers

    protected void OnLoadScene(string sceneName)
    {
        LoadScene(sceneName);
    }

    //#endregion
}
