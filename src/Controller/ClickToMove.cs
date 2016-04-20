using UnityEngine;
using System.Collections;

/// <summary>
/// Click to move.
/// you can use CharacterController or NavMeshAgent
/// </summary>
namespace DGX.Character.Controller
{
    public class ClickToMove : ScriptableObject {

        private Vector3 position;
        

        // Use this for initialization
        protected override void init ()
        {
            base.init ();
            position = transform.position;
        }
        
        // Update is called once per frame
        void Update () {
        
            if (Input.GetMouseButton (0)) {
                //locate where the player click on the terrain
                locatePosition ();
                GetComponent<Fighter>().onMoveTo(position);
            }
                
        }
        void locatePosition()
        {
            RaycastHit hit;
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay (Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000)) {
                if (hit.collider.tag != "Player" && hit.collider.tag != "Enemy")
                {
                    position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                }
            }
        }
    }
}