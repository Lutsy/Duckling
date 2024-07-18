using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationTriggerEvent : MonoBehaviour
{
    public UnityEvent animTriggerEvent;

    public void PlayAnimationEvent()
    {
        animTriggerEvent.Invoke();
    }
}