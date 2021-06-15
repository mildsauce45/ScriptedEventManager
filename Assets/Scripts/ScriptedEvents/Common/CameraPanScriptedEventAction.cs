using System;
using System.Collections;
using UnityEngine;

namespace FirstWave.ScriptedEvents.Common
{
    public class CameraPanScriptedEventAction : ScriptedEventAction
    {
        public Camera cameraToPan;
        public Vector3 from;
        public Vector3 to;
        public float panSpeed;
        public bool setStartPosition = true;

        public override IEnumerator BeginEvent()
        {
            if (cameraToPan == null)
                throw new InvalidOperationException("We need a reference to a Camera object in order to work");

            if (setStartPosition)
                cameraToPan.transform.position = from;

            while (cameraToPan.transform.position != to)
            {
                var newPosition = Vector3.MoveTowards(cameraToPan.transform.position, to, Time.deltaTime * panSpeed);

                // At a certain point just clamp down to the target
                if ((newPosition - to).magnitude < 0.025f)
                    newPosition = to;

                cameraToPan.transform.position = newPosition;

                yield return new WaitForEndOfFrame();
            }

            Debug.Log("Camera pan action done");
        }
    }
}
