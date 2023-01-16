//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//#endregion


public class Score : MonoBehaviour
{
    //#region editors fields and properties

    [SerializeField] private TMP_Text text;
    [SerializeField] private float stepIncrease = 10;

    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    private float currentScore = 0;

    //#endregion


    //#region life-cycle callbacks

    void OnEnable()
    {
        GameEventManager.ScoreIncrease.AddListener(OnScoreIncrease);
    }

    void OnDisable()
    {
        GameEventManager.ScoreIncrease.RemoveListener(OnScoreIncrease);
    }
    //#endregion

    //#region public methods
    //#endregion

    //#region private methods
    [NaughtyAttributes.Button]
    private void ScoreIncrease()
    {
        currentScore += stepIncrease;
        text.text = currentScore.ToString();
    }

    //#endregion

    //#region event handlers

    protected void OnScoreIncrease()
    {
        ScoreIncrease();
    }

    //#endregion
}
