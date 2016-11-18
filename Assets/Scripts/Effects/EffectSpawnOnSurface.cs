using UnityEngine;
using System.Collections;

[System.Serializable]
public class EffectSpawnContainer
{
    public string EffectHitTag = string.Empty;
    public GameObject EffectPrefab = null;
}

public class EffectSpawnOnSurface : MonoBehaviour
{
    public EffectSpawnContainer[] ImpactEffectPrefabs;

    public void Spawn(Collision collision)
    {
        foreach (EffectSpawnContainer esc in ImpactEffectPrefabs)
        {
            if (collision.gameObject.tag == esc.EffectHitTag)
            {
                Instantiate(esc.EffectPrefab, collision.contacts[0].point, Quaternion.Euler(collision.contacts[0].normal));
                return;
            }
        }
    }

}
