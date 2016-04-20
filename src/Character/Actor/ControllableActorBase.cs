//
// ControllableActorBase.cs
//
// Author:
//       Jean-Pierre Bouchard <>
//
// Copyright (c) 2015 Razan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using UnityEngine;
using DGX.Controller;

namespace DGX.Character.Actor
{
    public class ControllableActorBase : ActorBase
    {
        private float mInputVertical;
        private float mInputHorizontal;
        private XBoxJoyStick mJoystick;
        private Vector3 mDirection;
        
        
        public ControllableActorBase ()
        {
        }
        protected override void init ()
        {
            base.init();
            mJoystick = new XBoxJoyStick();
        }
        public override void Update ()
        {
            rememberElapsedTime();
            mInputHorizontal = Input.GetAxis("Horizontal");
            mInputVertical = Input.GetAxis("Vertical");
            float speed = (float)Math.Sqrt(mInputHorizontal * mInputHorizontal + mInputVertical * mInputVertical);
            
            SCALABLE_SPEED.applySpeedInfluence(speed);
            setSpeedForward(SCALABLE_SPEED.SPEED);
            setLocomotionValue();
            mDirection.Set (0 - mInputHorizontal, 0f , 0 - mInputVertical);
            //the joystick is not idle
            if (Math.Abs(mInputHorizontal) >= 0.001f || Math.Abs(mInputVertical) >= 0.001f) {    
                updateRotation(mDirection);
            }
            
            if (mJoystick.isJump() == true) {
                onJump();
            }
                
            base.Update();
        }
        private void setLocomotionValue()
        {
            ANIMATOR.SetFloat("horizontal", mInputHorizontal);
            ANIMATOR.SetFloat("vertical", mInputVertical);
            ANIMATOR.SetFloat("forwardX", transform.forward.x);
            ANIMATOR.SetFloat("forwardZ", transform.forward.z);
            ANIMATOR.SetFloat("directionX", mDirection.x);
            ANIMATOR.SetFloat("directionZ", mDirection.z);
            
        }
    }
}

