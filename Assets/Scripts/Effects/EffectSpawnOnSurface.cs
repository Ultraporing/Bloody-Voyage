using UnityEngine;
using System.Collections;

[System.Serializable]
public class EffectSpawnContainer
{
    public PhysicMaterial EffectHitMaterial = null;
    public GameObject EffectPrefab = null;
}

public class EffectSpawnOnSurface : MonoBehaviour
{
    public EffectSpawnContainer[] ImpactEffectPrefabs;

    public void Spawn(Collision collision)
    {
        foreach (EffectSpawnContainer esc in ImpactEffectPrefabs)
        {
            if (collision.collider.material.name.Replace(" (Instance)", string.Empty) == esc.EffectHitMaterial.name)
            {
                Instantiate(esc.EffectPrefab, collision.contacts[0].point, Quaternion.Euler(collision.contacts[0].normal));
                return;
            }
        }
    }

}
