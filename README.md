# [Fixed] Unity-Bug-Report-Playable-IN-50605

**Fixed in 2023.3.0b5.**

## About this issue

When manually evaluating a PlayableGraph, the `FrameData.frameId` is not consecutive.

According to Unity's Script Reference, "The frameId is incremented by `1` for every call to Playable.PrepareFrame." 

However, when manually evaluating a PlayableGraph, the frameId is incremented by `2` for every call to Playable.PrepareFrame.

Note:
- In the `PlayableGraph::Evaluate` method, the `BumpFrameID` method is called twice, causing this issue.
- https://docs.unity3d.com/ScriptReference/Playables.FrameData-frameId.html

## How to reproduce

1. Open the "Sample" scene.
2. Enter Play mode.
3. Observe the content of the logged messages in the Console window.

Expected output: frameId - lastFrameId = 1.

Actual output: frameId - lastFrameId = 2.
