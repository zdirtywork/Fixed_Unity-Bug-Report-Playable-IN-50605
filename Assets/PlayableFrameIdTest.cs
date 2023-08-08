using UnityEngine;
using UnityEngine.Playables;

// ## About this issue
// 
// When manually evaluating a PlayableGraph, the FrameData.frameId is not consecutive.
// 
// According to Unity's Script Reference, "The frameId is incremented by 1 for every call to Playable.PrepareFrame."
// However, when manually evaluating a PlayableGraph, the frameId is incremented by 2 for every call to Playable.PrepareFrame.
// 
// Note:
// - In the PlayableGraph::Evaluate method, the BumpFrameID method is called twice, causing this issue.
// - https://docs.unity3d.com/ScriptReference/Playables.FrameData-frameId.html
// 
// ## How to reproduce
// 
// 1. Open the "Sample" scene.
// 2. Enter Play mode.
// 3. Observe the content of the logged messages in the Console window.
// 
// Expected output: frameId - lastFrameId = 1.
// Actual output: frameId - lastFrameId = 2.

public class MyBehaviour : PlayableBehaviour
{
    public ulong lastFrameId;


    public override void PrepareFrame(Playable playable, FrameData info)
    {
        base.PrepareFrame(playable, info);

        if (lastFrameId != 0)
        {
            Debug.Log($"frameId - lastFrameId = {info.frameId - lastFrameId}");
        }

        lastFrameId = info.frameId;
    }
}


public class PlayableFrameIdTest : MonoBehaviour
{
    private PlayableGraph _graph;


    private void Start()
    {
        _graph = PlayableGraph.Create(GetType().Name);
        _graph.SetTimeUpdateMode(DirectorUpdateMode.Manual);

        var sp = ScriptPlayable<MyBehaviour>.Create(_graph);
        var spo = ScriptPlayableOutput.Create(_graph, "SP Output");
        spo.SetSourcePlayable(sp);

        _graph.Play();
    }

    private void Update()
    {
        _graph.Evaluate(Time.deltaTime);
    }

    private void OnDestroy()
    {
        if (_graph.IsValid())
        {
            _graph.Destroy();
        }
    }
}
