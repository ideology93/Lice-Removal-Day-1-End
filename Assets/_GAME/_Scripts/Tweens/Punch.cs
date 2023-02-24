using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


    public class Punch : TweenBase
    {
        [SerializeField] private Vector3 TargetRotation;
        [SerializeField] private int Vibrato;
        [SerializeField] private float Intensity;

        protected override void SetTweener()
        {
            Tweener = transform
                .DOPunchRotation(TargetRotation, LoopDuration, Vibrato, Intensity)
                .SetLoops(LoopCount, LoopType)
                .SetEase(LoopEase)
                .SetDelay(Delay);
        }
    }

