using UnityEngine;
using System.Collections;

namespace DGX.Mecanics
{
    public class RotateComponent : MonoBehaviour
    {
        private enum eState
        {
            init,
            end,
            initing,
            ending,
            analogic
        };
        
        public enum eRotateAxix
        {
            x,
            y,
            z
        }

        public bool isAnalogic;
        public eRotateAxix RotationAxis;
        public float MaximumAngle;
        public float AngularSpeed;
        public float InitialAngle;
        
        private eState mState;
        private float mAngle;
        private bool bTriggered;
        
        // Use this for initialization
        void Start () {
            mState = eState.init;
            SetInitialeAngle(InitialAngle);
            
            if( isAnalogic )
                mState = eState.analogic;
        }
        
        // Update is called once per frame
        void FixedUpdate () {
            
            
            switch(mState)
            {
            case eState.end:
                if(bTriggered)
                {
                    print("initing");
                    mState = eState.initing;
                    bTriggered = false;
                }
                break;
                    
            case eState.init:
                
                if(bTriggered)
                {
                    mState = eState.ending;
                    bTriggered = false;
                }
                break;
            
            case eState.ending:
                if( DecreaseAngle() )
                {
                    mState = eState.end;
                    bTriggered = false;
                }
                break;
            
            case eState.initing:
                
                if( IncreaseAngle() )
                {    
                    bTriggered = false;
                    mState = eState.init;
                }
                    
                break;
            }
        }
        public void ChangeState()
        {
            print("change state");
            bTriggered = true;
        }
        private void RotateAxis(float fAngle)
        {
            switch(RotationAxis)
            {
            case eRotateAxix.x:
                transform.Rotate( fAngle,0,0);
                break;
            case eRotateAxix.y:
                transform.Rotate( 0,fAngle,0);
                break;
            case eRotateAxix.z:
                transform.Rotate( 0,0, fAngle);
                break;
            }
            
        }
        
        //analogic function
        public bool IncreaseAngle()
        {
            if(mAngle >= MaximumAngle)
            {
                return true;
            }
            bool rep = false;
            //return true if max angular
            if( mAngle + AngularSpeed < MaximumAngle )
            {
                mAngle += AngularSpeed;
                RotateAxis( -AngularSpeed);
            }
            else
            {
                float f = MaximumAngle - mAngle;
                RotateAxis(-f);
                mAngle = MaximumAngle;
                rep = true;
            }
            return rep;
        }
        public bool DecreaseAngle()
        {
            if(mAngle <=0)
            {    
                return true;
            }
            
            bool rep = false;
            
            if( mAngle - AngularSpeed > 0 )
            {
                mAngle -= AngularSpeed;
                RotateAxis( AngularSpeed );
            }
            else
            {
                RotateAxis( mAngle );
                mAngle = 0;
                bTriggered = false;
                rep = true;
            }
            return rep;
        }
        private void SetInitialeAngle( float angle)
        {
            //use only on the init
            mAngle = angle;
        }
    }
}