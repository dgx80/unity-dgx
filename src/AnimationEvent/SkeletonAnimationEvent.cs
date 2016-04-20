using UnityEngine;
using System.Collections;

namespace DGX.AnimationEvent
{
    public class SkeletonAnimationEvent : ScriptableObject
    {
    	// Update is called once per frame
    	void Update () {
            //transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
    	}
        public void onEventAnimationIdleStart()
        {
            resetRotation();
            resetPosition();
        }
        public void onEventAnimationMovingStart()
        {
            resetRotation();
            resetPosition();
        }
        public void onEventAnimationJumpStart()
        {
            resetPosition();
        }
        private void resetPosition()
        {
            //transform.localPosition = Vector3.zero;
        }
        private void resetRotation()
        {
            //transform.localEulerAngles = Vector3.zero;
        }
    }
}