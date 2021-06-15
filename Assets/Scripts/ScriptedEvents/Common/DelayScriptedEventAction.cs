using System.Collections;
using UnityEngine;

namespace FirstWave.ScriptedEvents.Common
{
    public class DelayScriptedEventAction : ScriptedEventAction
    {
        public float delay;

        public override IEnumerator BeginEvent()
        {
            yield return new WaitForSeconds(delay);

            Debug.Log("Done waiting");
        }
    }
}
