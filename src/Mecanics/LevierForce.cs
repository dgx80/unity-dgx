using UnityEngine;
using System.Collections;

namespace DGX.Mecanics
{
    public class LevierForce : MonoBehaviour
    {
        private enum eState
        {
            knThrowing = 0,
            knPulling,
            knReleased,
            knReadyToFill,
            knReadyToShot
        };
        
        public float Teta;
        public float InitSpeed;
        public float PullingSpeed;
        public float acceleration;
        public Transform ProjectilePrefab;
        public float CatapulteForce;
        public float AngularForce;
        public bool AutoLauch;
        public float TimeCycle;
        
        
        private Transform projectile = null;
        private Transform levier;
        private eState mState = eState.knReleased;
        private float mAngle=0; 
        private float mCurrentSpeed = 0;
        private float m_nTimer;
        private bool LPressing = false;
        #region callback    
        // Use this for initialization
        protected void Start () {
            
            m_nTimer = 0;
            levier = transform.FindChild("levier");
            
            StartReleased();
        
        }
        
        // Update is called once per frame
        protected void Update () {
        
            
            if(AutoLauch)
            {
                m_nTimer += Time.deltaTime;
        
                if( m_nTimer >= TimeCycle )
                {
                    NextState();
                    m_nTimer = 0;
                }
            }
            switch(mState)
            {
            case eState.knReadyToShot:
            case eState.knReadyToFill:
            case eState.knReleased:
                
                if(!LPressing && Input.GetKey(KeyCode.L))
                {
                    LPressing = true;
                    NextState();
                }
                else if(LPressing && !Input.GetKey(KeyCode.L))
                {
                    LPressing = false;
                }
                    
                break;    
            case eState.knPulling:
                
                if(mAngle- PullingSpeed > 0)
                {
                    mAngle -= PullingSpeed;
                    
                    RotateLevier(-PullingSpeed);
                }
                else
                {
                    StopPulling();
                }
                break;
            case eState.knThrowing:
                
                mCurrentSpeed += acceleration;
                
                if(mAngle+mCurrentSpeed < Teta)
                {    
                    
                    mAngle += mCurrentSpeed;
                    
                    RotateLevier(mCurrentSpeed);
                }
                else
                {
                    StopThrowing();
                }
                break;
            }
        }
        
        #endregion
        
        #region catapulte
            
        private void RotateLevier(float fAngle)
        {
            levier.transform.Rotate(fAngle,0,0);
        }
        
        public void NextState()
        {
            switch(mState)
            {
            case eState.knReadyToShot:

                StartThrowing();
                
                break;
            case eState.knReadyToFill:
                
                    projectile = (Transform)Instantiate(ProjectilePrefab,GameObject.Find("SpawnPoint_fill").transform.position, Quaternion.identity);
                    if(projectile)
                    {
                        projectile.transform.parent = levier;
                    }
                    StartReadyToShot();
                
                break;    
            case eState.knReleased:

                StartPulling();
                
                break;
            
            }
        }
        #endregion
        
        #region State
        private void StartReadyToShot()
        {
            mState = eState.knReadyToShot;
        }
        private void StartThrowing()
        {
            mState = eState.knThrowing;
            mCurrentSpeed = InitSpeed;
            mAngle = 0;
        }
        private void StartReadyToFill()
        {
            mState = eState.knReadyToFill;
        }
        private void StartPulling()
        {
            mState = eState.knPulling;
        }
        private void StartReleased()
        {
            mAngle = Teta;
            mState = eState.knReleased;
        }
        private void StopPulling()
        {
            
            RotateLevier(-mAngle);
            mAngle = 0;
            StartReadyToFill();
        }
        private void StopThrowing()
        {        
            float x,y,z;
        
            x = transform.rotation.eulerAngles.x;
            y = transform.rotation.eulerAngles.y;
            z = transform.rotation.eulerAngles.z;
            float kForward = ProjectilePrefab.GetComponent<Rigidbody>().mass*CatapulteForce;
            
            if(projectile)
                Destroy(projectile.gameObject);
            
            projectile = (Transform)Instantiate(ProjectilePrefab,GameObject.Find("SpawnPoint_throw").transform.position, Quaternion.identity);
            
            

            projectile.GetComponent<Rigidbody>().AddForce( transform.forward*kForward);
            projectile.GetComponent<Rigidbody>().AddForce( transform.up*AngularForce);
            RotateLevier(-(mAngle - Teta));
            StartReleased();
            
        }
        #endregion
        
    }
}
