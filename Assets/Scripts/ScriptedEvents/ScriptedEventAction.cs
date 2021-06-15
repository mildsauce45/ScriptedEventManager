using System;
using System.Collections;
using UnityEngine;

namespace FirstWave.ScriptedEvents
{
    [Serializable]
    public abstract class ScriptedEventAction : MonoBehaviour
    {
        public abstract IEnumerator BeginEvent();
    }
}
