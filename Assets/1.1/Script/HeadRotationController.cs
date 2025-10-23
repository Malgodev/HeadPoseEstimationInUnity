using System.Collections.Generic;
using Mediapipe.Tasks.Components.Containers;
using Mediapipe.Tasks.Vision.PoseLandmarker;
using Mediapipe.Unity.Sample.PoseLandmarkDetection;
using UnityEngine;

public class HeadRotationController : MonoBehaviour
{
    [SerializeField] private PoseLandmarkerRunner runner;
    [SerializeField] private GameObject prefab;
    [SerializeField] private List<GameObject> poseLandmarkers = new();

    private PoseLandmarkerResult currentResult;
    private readonly object resultLock = new();
    private bool hasNewResult;

    [SerializeField] private float scale;
    
    private void OnEnable()
    {
        runner.OnResult += HandleOnResult;
    }

    private void OnDisable()
    {
        runner.OnResult -= HandleOnResult;
    }

    // Called by MediaPipe (background thread)
    private void HandleOnResult(PoseLandmarkerResult result)
    {
        lock (resultLock)
        {
            result.CloneTo(ref currentResult);
            hasNewResult = true;
        }
    }

    private void Update()
    {
        PoseLandmarkerResult resultCopy = default;
        bool hasNew = false;
        
        lock (resultLock)
        {
            if (hasNewResult)
            {
                resultCopy = currentResult;
                hasNewResult = false;
                hasNew = true;
            }
        }

        if (!hasNew || resultCopy.poseWorldLandmarks == null) return;

        NormalizedLandmarks landmarks = resultCopy.poseLandmarks[0];

        if (landmarks.landmarks == null) return;
        
        if (landmarks.landmarks.Count != poseLandmarkers.Count)
        {
            for (int i = poseLandmarkers.Count; i < landmarks.landmarks.Count; i++)
            {
                GameObject newGO = Instantiate(prefab, this.transform);
                poseLandmarkers.Add(newGO);
            }
        }

        for (int i = 0; i < landmarks.landmarks.Count; i++)
        {
            var lm = landmarks.landmarks[i];
            poseLandmarkers[i].transform.localPosition = lm.GetPosition(scale);
        }
    }
    
    [Flags]
    public enum BodyParts : short
    {
        None = 0,
        Face = 1,
        // Torso = 2,
        LeftArm = 4,
        LeftHand = 8,
        RightArm = 16,
        RightHand = 32,
        LowerBody = 64,
        All = 127,
    }

    // private const int _LandmarkCount = 33;
    // private static readonly int[] _LeftLandmarks = new int[] {
    //     1, 2, 3, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31
    // };
    // private static readonly int[] _RightLandmarks = new int[] {
    //     4, 5, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32
    // };
    // private static readonly List<(int, int)> _Connections = new List<(int, int)> {
    //     // Left Eye
    //     (0, 1),
    //     (1, 2),
    //     (2, 3),
    //     (3, 7),
    //     // Right Eye
    //     (0, 4),
    //     (4, 5),
    //     (5, 6),
    //     (6, 8),
    //     // Lips
    //     (9, 10),
    //     // Left Arm
    //     (11, 13),
    //     (13, 15),
    //     // Left Hand
    //     (15, 17),
    //     (15, 19),
    //     (15, 21),
    //     (17, 19),
    //     // Right Arm
    //     (12, 14),
    //     (14, 16),
    //     // Right Hand
    //     (16, 18),
    //     (16, 20),
    //     (16, 22),
    //     (18, 20),
    //     // Torso
    //     (11, 12),
    //     (12, 24),
    //     (24, 23),
    //     (23, 11),
    //     // Left Leg
    //     (23, 25),
    //     (25, 27),
    //     (27, 29),
    //     (27, 31),
    //     (29, 31),
    //     // Right Leg
    //     (24, 26),
    //     (26, 28),
    //     (28, 30),
    //     (28, 32),
    //     (30, 32),
    // };
}