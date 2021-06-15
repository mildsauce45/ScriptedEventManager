using System;
using System.Collections;
using System.Linq;
using FirstWave.ScriptedEvents.Common;
using UnityEngine;

namespace FirstWave.ScriptedEvents.Specialized
{
    /// <summary>
    /// Base class for composing larger scripted events purely in code if you prefer that method
    /// </summary>
    public abstract class ComposableScriptedEventAction : ScriptedEventAction
    {
        protected GameObject go;

        protected void InitEvent()
        {
            go = new GameObject("ComposableEventObject");
        }

        protected void EndEvent()
        {
            Destroy(go);
        }

        /// <summary>
        /// Helper method to introduce a delay in your scripted event
        /// </summary>
        protected IEnumerator Delay(float secs)
        {
            var delayAction = go.AddComponent<DelayScriptedEventAction>();
            delayAction.delay = secs;

            yield return delayAction.BeginEvent();

            Destroy(delayAction);
        }


        /// <summary>
        /// Helper method to introduce a camera pan to your scripted event
        /// </summary>
        protected IEnumerator Pan(float panSpeed, Vector3 to, Vector3? from = null)
        {
            var panAction = go.AddComponent<CameraPanScriptedEventAction>();
            panAction.setStartPosition = from.HasValue;
            panAction.cameraToPan = Camera.main;
            panAction.from = from ?? Vector3.zero;
            panAction.to = to;
            panAction.panSpeed = panSpeed;

            yield return panAction.BeginEvent();

            Destroy(panAction);
        }

        /// <summary>
        /// Helper method to spawn an object at the given location
        /// </summary>
        protected IEnumerator Spawn(GameObject prefab, Vector3 location, GameObject parent = null, bool destroy = false, float lifetime = 1f, Action<GameObject> onSpawn = null)
        {
            var spawn = go.AddComponent<SpawnObjectScriptedEventAction>();
            spawn.objectPrefab = prefab;
            spawn.spawnLocation = location;
            spawn.parent = parent;
            spawn.despawn = destroy;
            spawn.onSpawn = onSpawn;
            spawn.lifetime = lifetime;

            yield return spawn.BeginEvent();

            Destroy(spawn);
        }

        /// <summary>
        /// Helper method to destroy an object
        /// </summary>
        protected IEnumerator Despawn(GameObject obj, AudioClip audioClip = null)
        {
            var despawn = go.AddComponent<DestroyObjectScriptedEventAction>();
            despawn.toDestroy = obj;
            despawn.toPlay = audioClip;

            yield return despawn.BeginEvent();

            Destroy(despawn);
        }

        protected IEnumerator MoveActor(GameObject toMove, Vector2 target, float moveSpeed = 3.5f)
        {
            var path = go.AddComponent<MoveActorScriptedEventAction>();

            var actor = new ActorToMove
            {
                obj = toMove,
                moveSpeed = moveSpeed,
                targets = new Vector2[] { target }
            };

            path.actors = new[] { actor };

            yield return path.BeginEvent();

            Destroy(path);
        }

        protected IEnumerator MoveActorsAlongPath(GameObject[] toMove, System.Collections.Generic.IEnumerable<Vector2>[] moveTargets, float[] moveSpeed)
        {
            var path = go.AddComponent<MoveActorScriptedEventAction>();

            var actors = new ActorToMove[toMove.Length];

            for (int i = 0; i < toMove.Length; i++)
            {
                var actor = new ActorToMove
                {
                    obj = toMove[i],
                    moveSpeed = moveSpeed[i],
                    targets = moveTargets[i].ToArray()
                };

                actors[i] = actor;
            }

            path.actors = actors;

            yield return path.BeginEvent();

            Destroy(path);
        }
    }
}
