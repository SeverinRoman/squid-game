using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;


[System.Serializable]
public class TweenConfig
{
    public float duration = 1;
    public Ease easing = Ease.Linear;

    [Header("Loop")]
    public bool onLoop = false;
    [ShowIf("onLoop")][AllowNesting] public int loops = -1;
    [ShowIf("onLoop")][AllowNesting] public LoopType loopType = LoopType.Incremental;

    [Header("Transform")]
    public bool isTransform = false;
    [ShowIf("isTransform")][AllowNesting] public Pose transformPositionRotation;
    [ShowIf("isTransform")][AllowNesting] public Vector3 transformScale;

    [Header("Jump")]
    public bool isJump = false;
    [ShowIf("isJump")][AllowNesting] public float jumpPower;
    [ShowIf("isJump")][AllowNesting] public int numJump;

    [Header("Shake")]
    public bool isShake = false;
    [ShowIf("isShake")][AllowNesting] public float strength;
    [ShowIf("isShake")][AllowNesting] public int vibrato;
    [ShowIf("isShake")][AllowNesting] public int randomess;

    public Sequence sequence = null;
    public Tween tween = null;
}
