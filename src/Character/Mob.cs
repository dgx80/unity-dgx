using UnityEngine;
using System.Collections;

namespace DGX.Character
{
    public class Mob : ScriptableObject {

        public float speed;
        public float range;
        public CharacterController controller;
        public Transform player;
        public Fighter opponent;
        private Character.Anim.MoveableAnimation moveableAnimation;
        //private bool impacted;
        public int health;
        public int damage;
        private Character.Anim.FighterAnimation fighterAnimation;

        // Use this for initialization
        protected override void init ()
        {
            base.init ();
            health = 100;
            opponent = player.GetComponent<Fighter> ();
            fighterAnimation = GetComponent<Character.Anim.FighterAnimation> ();
            moveableAnimation = GetComponent<Character.Anim.MoveableAnimation> ();
        }
     
        // Update is called once per frame
        void Update () 
        {
            if  (!isDead ()) {
                if (!inRange ()) {
                    chase ();
                } else {
                    //attack();

                    //if(fighterAnimation.isAttackAnimationEnded()) {
                    //    impacted = false;
                    //}
                }
            } else {
                dieAction();
            }
        }
        private void attack()
        {
            fighterAnimation.playingAttack();
            if (fighterAnimation.isImpactTime()) {
                //impacted = true;
                opponent.getHit(damage);
            }
        }
            
        private bool inRange()
        {
            if (Vector3.Distance (transform.position, player.position) < range) {
                return true;
            }
            return false;
        }
        private void chase()
        {
            transform.LookAt (player.position);
            controller.SimpleMove (transform.forward * speed);
            moveableAnimation.playingRun ();
        }
        public void getHit(int damage)
        {
            health -= damage;
            if (health < 0) {
                health = 0;
            }
            LOGGER.logTag ("mob-health", health);
        }
        private bool isDead()
        {
            return(health <= 0);
        }
        private void dieAction()
        {
            fighterAnimation.playingDying ();

            if (fighterAnimation.isDieEnded()) {
                Destroy(gameObject); 
            }
        }

        void OnMouseOver()
        {
            player.GetComponent<Fighter> ().OPPONENT = gameObject;
        }
    }
}