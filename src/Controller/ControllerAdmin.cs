using System.Collections;
using DGX.Machine;

namespace DGX.Controller
{
    public class ControllerAdmin : UnityEngine.MonoBehaviour {

		public DgxCamera.CameraSet CameraKit;
        protected Machine.CarScript currentActor = null;
        
        // Use this for initialization
        void Start () {
            init();
        }
        
        // Update is called once per frame
        void Update () {
            
            HandleController();
        
        }
        protected virtual void init()
        {
        }
        protected void SetController(CarScript actor)
        {
            if(currentActor != null)
            {
                currentActor.EnableController = false;
            }
            CameraKit.SetTarget(actor);
            currentActor = actor;
            currentActor.EnableController = true;
        }
        protected virtual void HandleController()
        {
        }
    }
}