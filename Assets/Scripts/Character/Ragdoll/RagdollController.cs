//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class RagdollController : MonoBehaviour
{

    [System.Serializable]
    private class RagdollConfig
    {
        [SerializeField] public BodyType type;
        [SerializeField] public Rigidbody body;
    }

    //#region editors fields and properties
    [SerializeField] private bool isOnStartRagdoll = false;
    [SerializeField] private Animator animator;

    [SerializeField] private List<RagdollConfig> ragdollConfigs;


    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties
    private List<Collider> _colliders = new List<Collider>();
    //#endregion


    //#region life-cycle callbacks
    void Start()
    {
        foreach (RagdollConfig RagdollConfig in ragdollConfigs)
        {
            Collider collider = RagdollConfig.body.gameObject.GetComponent<Collider>();
            _colliders.Add(collider);
        }

        ToggleRagdoll(isOnStartRagdoll);
    }
    //#endregion

    //#region public methods

    public void ToggleRagdoll(bool isOn)
    {
        if (isOn)
        {
            animator.enabled = false;
            ChangeRootCollider(false);
            ChangeKinematicAll(false);
            ChangeEnabledCollidersAll(true);
        }
        else if (!isOn)
        {
            animator.enabled = true;
            ChangeRootCollider(true);
            ChangeKinematicAll(true);
            ChangeEnabledCollidersAll(false);
        }
    }

    public void Impulse(Vector3 impulse)
    {
        AddImpulseAll(impulse);
    }

    public void ChangeLayer(LayerType type)
    {
        ChangeAllLayer(type);
    }

    //#endregion

    //#region private methods

    private void ChangeAllLayer(LayerType type)
    {
        foreach (RagdollConfig RagdollConfig in ragdollConfigs)
        {
            RagdollConfig.body.gameObject.layer = (int)type;
        }
    }

    private void ChangeRootCollider(bool isOn)
    {
        if (gameObject.TryGetComponent<Collider>(out Collider collider))
        {
            collider.enabled = isOn;
        }
        if (gameObject.TryGetComponent<Rigidbody>(out Rigidbody body))
        {
            // body.useGravity = isOn;
        }
    }

    private void ChangeKinematic(BodyType type, bool isOn)
    {
        foreach (RagdollConfig RagdollConfig in ragdollConfigs)
        {
            if (RagdollConfig.type == type)
            {
                RagdollConfig.body.isKinematic = isOn;
                break;
            }
        }
    }

    private void ChangeKinematicAll(bool isOn)
    {
        foreach (RagdollConfig RagdollConfig in ragdollConfigs)
        {
            RagdollConfig.body.isKinematic = isOn;
        }
    }

    private void AwakeAll()
    {
        foreach (RagdollConfig RagdollConfig in ragdollConfigs)
        {
            RagdollConfig.body.WakeUp();
        }
    }

    private void AddImpulseAll(Vector3 impulse)
    {
        foreach (RagdollConfig RagdollConfig in ragdollConfigs)
        {
            RagdollConfig.body.AddForce(impulse, ForceMode.Impulse);
        }
    }

    private void ChangeEnabledCollidersAll(bool isOn)
    {
        foreach (Collider collider in _colliders)
        {
            collider.enabled = isOn;
        }
    }

    //#endregion

    //#region event handlers
    //#endregion
}
