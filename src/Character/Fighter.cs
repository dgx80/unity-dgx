using UnityEngine;
using System.Collections;
using DGX.Character.Controller;

namespace DGX.Character
{
    public class Fighter : ScriptableObject
    {
        private GameObject mOpponent;
        public int damage;
        
        public float range = 1.8f;
        public int health = 200;
        public float runSpeed = 7;
        public float fieldOfView = 1;

        private bool started;
        private bool ended;
        //private Character.Anim.FighterAnimation fighterAnimation;
        private FighterStateMachine mStateMachine;
        private CharacterController mController;
        private UnityEngine.Camera mCamera;

        // Use this for initialization
        protected override void init ()
        {
            base.init ();
            UnityEngine.Camera.main.enabled = true;
            mCamera = GetComponentInChildren<UnityEngine.Camera> ();
            mController = GetComponent<CharacterController> ();
            //fighterAnimation = GetComponent<Character.Anim.FighterAnimation> ();
            mStateMachine = new DGX.Character.FighterStateMachine(
                this,
                runSpeed
                );
            mStateMachine.OWNER = gameObject.name;
            mStateMachine.setIsDebug(debug);
        }
        
        // Update is called once per frame
        void Update ()
        {
            //handleAttackFinished ();
            //impact ();
            //dieAction ();
            mStateMachine.handleUpdate ();
        }
        public void onAttack()
        {
            if (inRange ()) {
                if(mStateMachine.onAttack(mOpponent, damage)) {
                }
                //fighterAnimation.onAttack ();
                //ClickToMove.attack = true;
                //if (opponent != null) {
                //    transform.LookAt (opponent.transform.position);
                //}
            }
        }
        public void onIdle()
        {
            LOGGER.log("onIdle");
            mStateMachine.onIdle ();
        }
        
        public void onMoveTo(UnityEngine.Vector3 position)
        {
            mStateMachine.onRunTo (position);
        }
/*        private void handleAttackFinished()
        {
            if (fighterAnimation.isAttackAnimationEnded()) {
                ClickToMove.attack = false;
                impacted = false;
            }
        }

        public void impact()
        {
            if (opponent != null && fighterAnimation.isAttackPlaying() && !impacted) {

                if (fighterAnimation.isImpactTime()) {
                    opponent.GetComponent<Mob>().getHit(damage);
                    impacted = true;
                }
            }
        }
*/      
        public void getHit(int damage)
        {
            if (health > 0) {
                health -= damage;
                if (health <= 0) {
                    health = 0;
                    mStateMachine.onDie();
                }
            }
            LOGGER.logTag ("health", health);
        }
        public bool inRange()
        {
            if (mOpponent != null) {
                float distance = Vector3.Distance (mOpponent.transform.position, transform.position);
                bool b = (distance <= range);
                return b;
            }
            return false;
        }
        
        public void goToOpponent()
        {
            if (!mOpponent) {
                return;
            }
            onMoveTo (mOpponent.transform.position);
            //transform.LookAt (opponent.transform.position);
            //m_controller.SimpleMove (transform.forward * speed);
        }
        public GameObject OPPONENT
        {
            set
            {
                mOpponent = value;
            }
            get
            {
                return mOpponent;
            }
        }
        
        public bool Initialized()
        {
            if(mStateMachine != null) {
                return true;
            }
            return false;
        }
        public UnityEngine.Camera CAMERA
        {
            get
            {
                return mCamera;
            }
        }
             
        public FighterStateMachine.eCharacterState CHARACTER_STATE
        {
            get 
            {
                return mStateMachine.CHARACTER_STATE;
            }
        }
        public void debugFieldOfView()
        {
            Vector3 rot1 = Quaternion.Euler(0, -30, 0) * transform.forward;
            Vector3 rot2 = Quaternion.Euler(0, 30, 0) * transform.forward;
            //rot2.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 30, transform.eulerAngles.z);
            
            Debug.DrawRay(CAMERA.transform.position, rot1 * 10, Color.green);
            Debug.DrawRay(CAMERA.transform.position, rot2 * 10, Color.green);
            //Debug.DrawRay(transform.position, rot2.eulerAngles * 10, Color.blue);
        }
    }
    
}