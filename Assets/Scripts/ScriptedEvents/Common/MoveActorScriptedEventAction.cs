using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace FirstWave.ScriptedEvents.Common
{
    public class MoveActorScriptedEventAction : ScriptedEventAction
    {
        public ActorToMove[] actors;
        public Action onComplete;

        public override IEnumerator BeginEvent()
        {
            if (actors == null || actors.Length == 0)
                throw new InvalidOperationException("No actors to move");

            if (actors.Any(a => a.targets == null || a.targets.Length == 0))
                throw new InvalidOperationException("At least one actor has no targets set");

            foreach (var a in actors)
            {
                a.Initialize();
                a.UpdateTarget();
            }

            while (!actors.All(a => a.IsPathDone))
                yield return new WaitForEndOfFrame();

            onComplete?.Invoke();

            Debug.Log("All actors reached final destination");
        }

        private void Update()
        {
            foreach (var a in actors)
            {
                if (!a.IsPathDone)
                    a.Move();
            }
        }
    }
}
