using System;
using System.Collections;
using UnityEngine;

namespace FirstWave.ScriptedEvents.Common
{
    public class DestroyObjectScriptedEventAction : ScriptedEventAction
    {
        public GameObject toDestroy;
        public AudioClip toPlay;

        public override IEnumerator BeginEvent()
        {
            if (toDestroy == null)
                throw new InvalidOperationException("You need to specify a game object to destroy.");

            Destroy(toDestroy);

            if (toPlay != null)
            {
                AudioSource.PlayClipAtPoint(toPlay, Camera.main.transform.position);
                yield return new WaitForSeconds(toPlay.length);
            }

            yield return new WaitForEndOfFrame();

            Debug.Log("Destroy object complete.");
        }
    }
}
