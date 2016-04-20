using System.Collections;
using System;
using System.Collections.Generic;
using DGX.Machine;

namespace DGX.DgxCamera
{
    public class CameraSet : UnityEngine.MonoBehaviour
    {

        public enum eCameraType
        {
            thirdPersonCamera = 0,
            farCamera,
            sideCamera,
            UpperCamera,
            orbitCamera,
            none
        };
        public bool ActiveTarget;
        public bool IsActive;
        public CarScript Target;
        public eCameraType CameraSelection;
        public UnityEngine.Camera  ThirdPersonCamera;
        public UnityEngine.Camera  SideCamera;
        public UnityEngine.Camera  OrbitPersonCamera;
        public UnityEngine.Camera  FarCamera;
        public UnityEngine.Camera  UpperCamera;
        public UnityEngine.GUIText tx;
        
        private UnityEngine.Camera CurrentCamera;
        
        // Use this for initialization
        void Start () {
            
            
            if( IsActive)
            {
                SetCameraSelection( CameraSelection );
            }
            else
            {
                SetCameraSelection( eCameraType.none );
            }
            if(ActiveTarget)
            {
                SetTarget(Target);
            }
        }
        public void SetTarget(CarScript actor)
        {
            transform.position = actor.transform.position;
            transform.rotation = actor.transform.rotation;
            transform.parent = actor.transform;
        }
        // Update is called once per frame
        void Update () {
            

            if( UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.V) )
            {
                NextCamnera();
                
            }
        }
        private void NextCamnera()
        {
            if( CameraSelection +1 == eCameraType.none )
            {
                SetCameraSelection(0);
            }
            else
            {
                SetCameraSelection(CameraSelection+1);
            }
            
        }
        private void CloseCamera()
        {
            ThirdPersonCamera.enabled = false;
            SideCamera.enabled = false;
            OrbitPersonCamera.enabled = false;
            FarCamera.enabled = false;
            UpperCamera.enabled = false;
            IsActive = false;
            CurrentCamera = null;
        }
        
        private void SetCameraSelection(eCameraType type)
        {
            CloseCamera();
            string s = "camera:";
                
            CameraSelection = type;
            
            switch(CameraSelection)
            {
            case eCameraType.thirdPersonCamera:
                CurrentCamera = ThirdPersonCamera;
                s += " third";
                break;
            case eCameraType.orbitCamera:
                
                CurrentCamera = OrbitPersonCamera;
                s += " orbit";
                break;
                
            case eCameraType.farCamera:
                s += " far";
                CurrentCamera = FarCamera;
                break;
                
            case eCameraType.UpperCamera:
                s += " upper";
                CurrentCamera = UpperCamera;
                
                break;
            case eCameraType.sideCamera:
                s += " side";
                CurrentCamera = SideCamera;
                break;
            
            }
            tx.text = s;
            if( CurrentCamera != null )
            {
                IsActive = true;
                CurrentCamera.enabled = true;
            }
        }
    }
}