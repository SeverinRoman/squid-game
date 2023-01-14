//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class CollisionController : MonoBehaviour
{
    //#region editors fields and properties
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties

    //#endregion


    //#region life-cycle callbacks

    //#endregion

    //#region public methods


    //#endregion

    //#region private methods



    //#endregion

    //#region event handlers

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherGameObject = collision.gameObject;

        switch (otherGameObject.layer)
        {
            case ((int)LayerType.Ground):

                break;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        GameObject otherGameObject = collision.gameObject;

        switch (otherGameObject.layer)
        {
            case ((int)LayerType.Ground):
                break;
        }

    }

    //#endregion
}
