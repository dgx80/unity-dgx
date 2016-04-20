using UnityEngine;
using System.Collections;
using DGX.Mecanics;

namespace DGX.Machine
{
    public class TonkaTruck : CarScript
    {

        public RotateComponent box;
        
        
        protected override void init()
        {
            base.init();
        }
        protected override void HandleController()
        {
            
            base.HandleController();
            
            
            if(Input.GetKey(KeyCode.U))
            {
                box.IncreaseAngle();
            }
            else if(Input.GetKey(KeyCode.I))
            {
                box.DecreaseAngle();
            }
            
        }
    }
}
