using UnityEngine;
using System.Collections;

namespace DGX.DgxCamera
{
    public class SmoothCamera2D : MonoBehaviour {
        
        public float dampTime = 0.15f;
        private Vector3 velocity = Vector3.zero;
        public Transform target;
        public UnityEngine.Camera myCamera;
        public float heigth = 4f;
        // Update is called once per frame
        void Update () 
        {
            if (target)
            {
                Vector3 point = myCamera.WorldToViewportPoint(target.position);
                Vector3 delta = target.position - myCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10)); //(new Vector3(0.5, 0.5, point.z));
                Vector3 destination = transform.position + delta;
                destination.y = 4;
                
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }
            
        }
    }
}

