using UnityEngine;
using System.Collections;
using DGX.Mecanics;

namespace DGX.Machine
{

    public class TonkaLoader: CarScript {

        public RotateComponent frontpart;
        public RotateComponent backet;
        public RotateComponent pelle;
        
        protected override void init()
        {
            base.init();
        }
        protected override void HandleController()
        {
            base.HandleController();
            
            if(Input.GetKey(KeyCode.U))
            {
                backet.IncreaseAngle();
            }
            else if(Input.GetKey(KeyCode.I))
            {
                backet.DecreaseAngle();
            }
            if(Input.GetKey(KeyCode.O))
            {
                pelle.IncreaseAngle();
            }
            else if(Input.GetKey(KeyCode.P))
            {
                pelle.DecreaseAngle();
            }
            
        }
        protected override void HandleSteering()
        {
            float wheelFactor = -Input.GetAxis("Horizontal");
            
            if(wheelFactor > 0.1f)
            {
                if(EnableSteringEffect)
                {
                    frontpart.IncreaseAngle();
                }
            }
            else if(wheelFactor < -0.1f)
            {
                if(EnableSteringEffect)
                {
                    frontpart.DecreaseAngle();
                }
            }
            
        }                
    }
}