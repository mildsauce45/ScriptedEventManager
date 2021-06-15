using System;
using System.Collections;
using UnityEngine;

namespace FirstWave.ScriptedEvents
{
    public class ScriptedEventManager : MonoBehaviour
    {
        public string eventName;
        public ScriptedEventTrigger eventStartTrigger = ScriptedEventTrigger.Start;
        public ScriptedEventAction[] actions;
        public Action<ScriptedEventManager> EventCompleted;

        private IScriptedEventCondition condition;

        void Start()
        {
            condition = GetComponent<IScriptedEventCondition>();

            if (eventStartTrigger == ScriptedEventTrigger.Start || (eventStartTrigger == ScriptedEventTrigger.Condition && condition.ShouldStartEvent()))
                StartScriptedEvent();
        }

        public Coroutine StartScriptedEvent() =>
            StartCoroutine(StartEventInternal());

        private IEnumerator StartEventInternal()
        {
            for (int i = 0; i < actions.Length; i++)
                yield return actions[i].BeginEvent();

            EventCompleted?.Invoke(this);
        }
    }
}
