using System;
using System.Collections;
using UnityEngine;

namespace FirstWave.ScriptedEvents.Common
{
    public class SpawnObjectScriptedEventAction : ScriptedEventAction
    {
        public GameObject objectPrefab;

        public Vector3 spawnLocation;
        public GameObject parent;

        public bool despawn;
        public float lifetime;

        public Action<GameObject> onSpawn;

        public override IEnumerator BeginEvent()
        {
            GameObject go;

            if (parent == null)
                go = Instantiate(objectPrefab, spawnLocation, Quaternion.identity);
            else
            {
                go = Instantiate(objectPrefab, parent.transform);
                go.transform.localPosition = spawnLocation;
            }

            onSpawn?.Invoke(go);

            if (despawn)
            {
                yield return new WaitForSeconds(lifetime);

                Destroy(go);
            }

            yield return new WaitForEndOfFrame();

            Debug.Log("Spawn Object Event over");
        }
    }
}
