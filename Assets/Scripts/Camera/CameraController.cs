//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//#endregion


public class CameraController : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private bool dontDownCamera;
    [SerializeField] private CameraType type;
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    //#endregion


    //#region life-cycle callbacks

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        GameEventManager.CameraToggle.AddListener(OnCameraToggle);
    }

    void OnEnable()
    {
        GameEventManager.SetCameraFollow.AddListener(OnSetCameraFollow);
    }

    void OnDisable()
    {
        GameEventManager.SetCameraFollow.RemoveListener(OnSetCameraFollow);
    }

    //#endregion

    //#region public methods

    public void SetFollow(Transform transform)
    {
        cinemachineVirtualCamera.Follow = transform;
        cinemachineVirtualCamera.LookAt = transform;
    }

    //#endregion

    //#region private methods
    //#endregion

    //#region event handlers

    protected void OnSetCameraFollow(CameraType type, Transform transform)
    {
        if (this.type == type)
        {
            SetFollow(transform);
        }
    }

    protected void OnCameraToggle(CameraType type, bool isOn)
    {
        if (this.type == type)
        {
            gameObject.SetActive(isOn);
        }
    }

    //#endregion
}
