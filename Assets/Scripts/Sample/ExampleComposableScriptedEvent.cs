using System.Collections;
using FirstWave.ScriptedEvents.Specialized;
using UnityEngine;

public class ExampleComposableScriptedEvent : ComposableScriptedEventAction
{
    public GameObject square;
    public GameObject circle;

    public override IEnumerator BeginEvent()
    {
        InitEvent();

        yield return MoveActor(square, new Vector2(5, 0));
        yield return MoveActor(square, new Vector2(-5, 0), 5.5f);

        yield return MoveActorsAlongPath(new[]
        {
            square,
            circle
        },
        new[]
        {
            new [] { new Vector2(5, 0), new Vector2(-5, 0) },
            new [] { new Vector2(5, 5), new Vector2(5, -5), new Vector2(-5, -5), new Vector2(-5, 5) }
        },
        new[] { 2.5f, 4.5f });

        yield return Despawn(square);

        EndEvent();

        yield return new WaitForEndOfFrame();
    }
}
