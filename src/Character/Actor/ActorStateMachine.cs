//
// ActorStateMachine.cs
//
// Author:
//       Razan <>
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
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using DGX.Character.Action;

namespace DGX.Character.Actor
{
    public class ActorStateMachine : DGX.Action.SynchronousStateMachine
    {
        #region DECLARATION
        
        public enum eCharacterState
        {
            idle = 0,
            move,
            jump
        }
        #endregion
        #region ATTRIBUTES
        private ActorActionIdle mActionIdle;
        private ActorActionMoving mActionMoving;
        
        #endregion
        
        #region CONSTRUCTOR
        
        public ActorStateMachine (
            ActorBase actor,
            ScalableSpeed scalableSpeed
        ) : base()
        {
            CharacterController characterController = actor.GetComponent<CharacterController> ();
            Animator animator = actor.GetComponentInChildren<Animator> ();
            
            mActionIdle = new ActorActionIdle ((int)eCharacterState.idle);
            mActionMoving = new ActorActionMoving ((int)eCharacterState.move, actor.transform, characterController, scalableSpeed);
            
            addAction (mActionIdle);
            addAction (mActionMoving);
            
            startNewAction ((int)eCharacterState.idle, false);
        }
        #endregion
        
        #region EVENTS
        
        #endregion
        #region UPDATE
        
        #endregion
        #region ACTIONS
        
        public bool onMoveTo(UnityEngine.Vector3 position)
        {
            mActionMoving.VECTOR_TARGET = position;
            onMoving();
            return true;
        }
        public void onMoving()
        {
            startNewCharacterState(eCharacterState.move, false);    
        }
        public bool onJump()
        {
            return startNewCharacterState(eCharacterState.jump, false);
        }
             
        public bool onIdle()
        {
            return startNewCharacterState(eCharacterState.idle, false);
        }
        public void updateRotation(Vector3 direction)
        {
            mActionMoving.DIRECTION = direction;
        }
        
        #endregion
        #region PROPERTIES
        
        public eCharacterState CHARACTER_STATE
        {
            get {return (eCharacterState)ACTION_CURRENT_ID;}
        }
        #endregion
        #region UTILITY
        protected bool startNewCharacterState(eCharacterState state, bool forceResetAction)
        {
            bool bChanged = startNewAction((int)state, false);
            if (bChanged) {
                LOGGER.logTag ("start new state", state.ToString ());
            }
            return bChanged;
        }
        
        #endregion
    }
}

