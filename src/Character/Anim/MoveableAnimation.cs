/// <summary>
/// Moveable character.
/// </summary>
/// 
using System;
using UnityEngine;

namespace DGX.Character.Anim
{
    public class MoveableAnimation : ScriptableObject
    {
        public AnimationClip runClip;
        public AnimationClip idleClip;

        public void playingRun()
        {
            ANIMATION.CrossFade(runClip.name);
        }
        public void playingIdle()
        {
            ANIMATION.CrossFade(idleClip.name);
        }
    }
}

