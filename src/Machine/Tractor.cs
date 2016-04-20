using System;
using UnityEngine;
using System.Collections;
using DGX.Mecanics;
using DGX.Controller;

namespace DGX.Machine
{
    public class Tractor : CarScript
    {
        public RotateComponent hydrolic;
    //    public Presse trailer;
        
        protected override void init()
        {
            base.init();
        }
        protected override void HandleController()
        {    
            base.HandleController();
            
            
            if(Input.GetKey(jKey.Button7))
            {
                hydrolic.IncreaseAngle();
            }
            else if(Input.GetKey(jKey.Button8))
            {
                hydrolic.DecreaseAngle();
            }
            
        }
        protected override float HandleMotor()
        {
            float force = base.HandleMotor();
            
        //    trailer.ApplyMotorForce(force);
            return force;
        }
    }
}
