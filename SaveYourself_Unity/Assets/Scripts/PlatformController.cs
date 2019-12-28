using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private Animator ac;
    
    void Start()
    {
        ac = GetComponent<Animator>();
    }
    void OnEnable()
    {
//        triggerBall = ball;
        EventManager.StartListening ("EVENT_HOLD_BALL", HoldingBall);
        EventManager.StartListening ("EVENT_AIR_BALL", BallInAir);
    }

    private void OnDisable()
    {
//        triggerBall = null;
        EventManager.StopListening ("EVENT_HOLD_BALL", HoldingBall);
        EventManager.StopListening ("EVENT_AIR_BALL", BallInAir);
    }

    void HoldingBall()
    {
        ac.SetBool("action", true);
//        Debug.Log(("platform for ball grabbing"));
    }

    void BallInAir()
    {
        ac.SetBool("action", false);
//        Debug.Log(("platform for ball throwing"));
    }
}