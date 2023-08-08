# Unity-Bug-Report-Playable-IN-50605
According to Unity's Script Reference, "The frameId is incremented by `1` for every call to Playable.PrepareFrame." However, when manually evaluating a PlayableGraph, the frameId is incremented by `2` for every call to Playable.PrepareFrame.
