using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EffectManager : MonoBehaviour
{
    [System.Serializable]
    private class EffectConfig
    {
        [SerializeField] public EffectType type;
        [SerializeField] public GameObject effect;
    }

    [SerializeField] private List<EffectConfig> effectConfigs;
    [SerializeField] private GameObject world;


    void OnEnable()
    {
        GameEventManager.SpawnEffect.AddListener(OnSpawnEffect);
    }

    void OnDisable()
    {
        GameEventManager.SpawnEffect.RemoveListener(OnSpawnEffect);
    }

    private void SpawnEffect(EffectType type, Vector3 position)
    {
        foreach (EffectConfig effectConfig in effectConfigs)
        {
            if (effectConfig.type == type)
            {
                GameObject effect = Instantiate(effectConfig.effect, position, Quaternion.identity);
                effect.transform.SetParent(world.transform);
            }
        }
    }

    protected void OnSpawnEffect(EffectType type, Vector3 position)
    {
        SpawnEffect(type, position);
    }

}
