using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FirstWave.ScriptedEvents.Common
{
    public class LoadSceneScriptedEventAction : ScriptedEventAction
    {
        public string sceneName;

        public override IEnumerator BeginEvent()
        {
            yield return new WaitForEndOfFrame();

            Debug.Log("Loading scene");

            SceneManager.LoadScene(sceneName);
        }
    }
}
