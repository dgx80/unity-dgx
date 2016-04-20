using UnityEngine;
using System.Collections;
using DGX.Mecanics;

namespace DGX.Machine
{
    public class TonkaExcavator : CarScript {

        public RotateComponent frame;
        public RotateComponent arm1;
        public RotateComponent arm2;
        public RotateComponent backet;

        
        private enum eState
        {
            voiture,
            excavation
        }
        
        private eState mState;
        
        protected override void init()
        {
            base.init();
            mState = TonkaExcavator.eState.excavation;
            SwapState();
        }
        protected void SwapState()
        {
            switch(mState)
            {
            case eState.voiture:
                mState = TonkaExcavator.eState.excavation;
                break;
            case eState.excavation:
                mState = TonkaExcavator.eState.voiture;
                break;
            }
        }
        protected override void HandleController()
        {
            base.HandleController();
            if(Input.GetKeyDown(KeyCode.X))
            {
                SwapState();
            }
        
            switch(mState)
            {
            case eState.voiture:
                
                break;
            case eState.excavation:
                        
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                
                float f = 0.5f;
                
                if(horizontal<-f)
                {
                    frame.IncreaseAngle();
                }
                else if(horizontal>f)
                {
                    frame.DecreaseAngle();
                }
                if(vertical>f)
                {
                    arm1.IncreaseAngle();
                }
                else if(vertical<-f)
                {
                    arm1.DecreaseAngle();
                }
                if(Input.GetKey(KeyCode.U))
                {
                    arm2.IncreaseAngle();
                }
                else if(Input.GetKey(KeyCode.I))
                {
                    arm2.DecreaseAngle();
                }
                if(Input.GetKey(KeyCode.O))
                {
                    backet.IncreaseAngle();
                }
                else if(Input.GetKey(KeyCode.P))
                {
                    backet.DecreaseAngle();
                }
            break;
            }
                     
        }
        protected override float HandleMotor()
        {
            float force = 0.0f;
            if(mState == TonkaExcavator.eState.voiture)
            {
                force =base.HandleMotor();
            }
            return force;
        }
        protected override void HandleSteering()
        {
            base.HandleSteering();
    /*        float wheelFactor = SteeringAngleSensibility*Input.GetAxis("Horizontal");
            wheelFactor *= SteeringAngleSensibility;
                    
            FrontLeftWheel.steerAngle += wheelFactor;
            FrontRightWheel.steerAngle += wheelFactor;
            
            if(EnableSteringEffect)
            {
                frontpart.Rotate(0,wheelFactor,0);
            }
    */    }
    }
}