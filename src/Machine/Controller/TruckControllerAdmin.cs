using UnityEngine;
using System.Collections;
using DGX.Controller;

namespace DGX.Machine.Controller
{
    public class TruckControllerAdmin : ControllerAdmin {

        public enum eTruckType
        {
            TonkaTruck,
            TonkaLoader,
            TonkaExcavator
        }
        
        
        public GUIText txTruckType;
        public eTruckType TruckType;
        public CarScript Loader;
        public CarScript Truck;
        public CarScript Excavator;
        public GUIText txSpeed;
        
        private const string cTruckType = "Truck Type: ";
        
        protected override void init()
        {
            base.init();
            UpdateController();
        }
        protected override void HandleController()
        {
            base.HandleController();
            txSpeed.text = "Speed:"+currentActor.BackLeftWheel.rpm.ToString();

            if(Input.GetKeyDown(KeyCode.C))
            {
                NextController();
            }
        }
        private void UpdateController()
        {
            switch(TruckType)
            {
            case eTruckType.TonkaLoader:
                txTruckType.text = cTruckType + "Loader";
                SetController(Loader);
                break;
            case eTruckType.TonkaTruck:
                txTruckType.text = cTruckType + "Truck";
                SetController(Truck);
                break;
            case eTruckType.TonkaExcavator:
                txTruckType.text = cTruckType + "Excavator";
                SetController(Excavator);
                break;
            }
        }
        private void NextController()
        {
            switch(TruckType)
            {
            case eTruckType.TonkaLoader:
                TruckType = TruckControllerAdmin.eTruckType.TonkaTruck;
                break;
            case eTruckType.TonkaTruck:
                TruckType = TruckControllerAdmin.eTruckType.TonkaExcavator;
                break;
            case eTruckType.TonkaExcavator:
                TruckType = TruckControllerAdmin.eTruckType.TonkaLoader;
                break;
            }
            UpdateController();
        }        
    }
}