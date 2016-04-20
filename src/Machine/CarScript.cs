using UnityEngine;
using System.Collections;

namespace DGX.Machine
{
    public class CarScript : MonoBehaviour {

        public enum eTractionType
        {
            ForwardTraction,
            BackwardTraction,
            IntegralTraction
        };
        
        public bool EnableController;
        public bool EnableSteringEffect;
        public eTractionType TractionType;
        public WheelCollider BackLeftWheel;
        public WheelCollider BackRightWheel;
        public WheelCollider FrontLeftWheel;
        public WheelCollider FrontRightWheel;
        public float MotorPower;
        public float MotorMaximumSpeed;
        public float SteeringAngleSensibility;
        public float BrakeTorque;
        public float springs = 0.2f;
        public float dampers;
        public float suspensionDistance;
        public float targetPosition;
        public float wheelWeight = 2.0f;
        public float wheelRadius = 0.5f;
        public bool AutomaticBrake = false;
        
        public Transform wheelLeft;
        public Transform wheelRight;
        public Vector3 masseCenter = new Vector3(0.0f, -0.25f, 0.0f); // offset of centre of mass
        
        private bool HandBrake = false;
        
        // Use this for initialization
        void Start () 
        {
            GetComponent<Rigidbody>().centerOfMass += masseCenter;
            SetWheelParam(BackLeftWheel);
            SetWheelParam(BackRightWheel);
            SetWheelParam(FrontLeftWheel);
            SetWheelParam(FrontRightWheel);
            
            init();
        }
        protected void SetWheelParam(WheelCollider col)
        {
            
            col.suspensionDistance = suspensionDistance;
            JointSpring js = col.suspensionSpring;
            js.spring = springs;
            js.damper = dampers;
            js.targetPosition = targetPosition;
            col.suspensionSpring = js;
            col.radius = wheelRadius;
            col.mass = wheelWeight;        
        }
        
        // Update is called once per frame
        void Update () {
            if(EnableController)
            {
                HandleController();
            }
        }
        protected virtual void init()
        {
        }
        protected virtual void HandleController()
        {
            
            HandleMotor();
            HandleSteering();
            HandleBrake();
        }
        
        protected virtual float HandleMotor()
        {
            float force = -Input.GetAxis("Vertical");
            float rpm = FrontLeftWheel.rpm;
            
            if( force > 0.1f)
            {
                if(AutomaticBrake==true)
                {
                    SetHandBrake(false);
                }
                force = force*MotorPower;
                
                if( rpm > MotorMaximumSpeed )
                {
                    force = 0;
                }
                else if( rpm < -MotorMaximumSpeed )
                {
                    force = 0;
                }
                
            }
            else if(force < -0.1f)
            {
                if(AutomaticBrake==true)
                {
                    SetHandBrake(false);
                }
                force = force* MotorPower;
                
                if( rpm > MotorMaximumSpeed )
                {
                    force = 0;
                }
                else if( rpm < -MotorMaximumSpeed )
                {
                    force = 0;
                }
            }
            else
            {
                force = 0;
            }
            
            ApplyMotorForce(force);
            
            return force;
        }
        public void ApplyMotorForce(float force)
        {
            switch( TractionType )
            {
            case eTractionType.BackwardTraction:
                
                BackLeftWheel.motorTorque = force;
                BackRightWheel.motorTorque = force;
                break;
            case eTractionType.ForwardTraction:
                FrontLeftWheel.motorTorque = force;
                FrontRightWheel.motorTorque = force;
                break;
            case eTractionType.IntegralTraction:
                BackLeftWheel.motorTorque = force;
                BackRightWheel.motorTorque = force;
                FrontLeftWheel.motorTorque = force;
                FrontRightWheel.motorTorque = force;
                break;
            }
        }
        protected virtual void HandleSteering()
        {
            float wheelAngle = SteeringAngleSensibility*Input.GetAxis("Horizontal");
            
            float difAngle = wheelAngle - FrontLeftWheel.steerAngle; 
            
            FrontLeftWheel.steerAngle = wheelAngle;
            FrontRightWheel.steerAngle = wheelAngle;
            
            if(EnableSteringEffect)
            {
                wheelLeft.Rotate(0,difAngle,0);
                wheelRight.Rotate(0,difAngle,0);
            }
        }
        protected void SetHandBrake(bool bHandBrake)
        {
            if(HandBrake == bHandBrake)
                return;
            HandBrake = bHandBrake;
            if(bHandBrake)
            {
        //        print("Handbrake\n");
            }
            else
            {
        //        print("Handbrake released\n");
            }
        }
        protected virtual void HandleBrake()
        {
            if(HandBrake)
            {
                FrontLeftWheel.brakeTorque = BrakeTorque;
                FrontRightWheel.brakeTorque = BrakeTorque;
                BackLeftWheel.brakeTorque = BrakeTorque;
                BackRightWheel.brakeTorque = BrakeTorque;
            }
            if( Input.GetKey(KeyCode.B) )
            {
                FrontLeftWheel.brakeTorque = BrakeTorque;
                FrontRightWheel.brakeTorque = BrakeTorque;
                BackLeftWheel.brakeTorque = BrakeTorque;
                BackRightWheel.brakeTorque = BrakeTorque;
                
                if(AutomaticBrake)
                {
                    if(BackLeftWheel.rpm < 0.1f && BackLeftWheel.rpm > -0.1f )
                    {
                        SetHandBrake(true);
                    }
                }
            }
            else
            {
                FrontLeftWheel.brakeTorque = 0;
                FrontRightWheel.brakeTorque = 0;
                BackLeftWheel.brakeTorque = 0;
                BackRightWheel.brakeTorque = 0;
            }
        }
        
    }
}
