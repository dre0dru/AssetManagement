using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsLimiter : MonoBehaviour
{
    [SerializeField]
    private int _limit;

    private void Awake()
    {
        Application.targetFrameRate = _limit;
    }
}
