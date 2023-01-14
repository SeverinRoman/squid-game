//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class PoolObject : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private GameObjectType type;
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    public PoolManager poolManager;

    //#endregion


    //#region life-cycle callbacks
    //#endregion

    //#region public methods
    [NaughtyAttributes.Button]
    public void ReturnToPool()
    {
        poolManager.ReturnPoolObject(type, gameObject);
    }

    public void SetPoolConfig(GameObjectType type, PoolManager poolManager)
    {
        this.type = type;
        this.poolManager = poolManager;
    }

    //#endregion

    //#region private methods
    //#endregion

    //#region event handlers
    //#endregion
}
