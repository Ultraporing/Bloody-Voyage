using UnityEngine;
using System.Collections;

namespace Controllers.Sails
{
    [RequireComponent(typeof(MeshRenderer))]
    public class SailMeshController : MonoBehaviour
    {
        public Vector3 PositionFullToHalfMultiplicator = new Vector3();
        public Vector3 ScaleFullToHalfMultiplicator = new Vector3();
        [Range(0,2)]
        public int CurrentStage = 0;
        private MeshRenderer MeshRenderer = null;

        // Use this for initialization
        void Start () {
            MeshRenderer = GetComponent<MeshRenderer>();

        }
	
        public void NextStage()
        {
            if (CurrentStage == 0)
            {
                CurrentStage++;

                transform.position = new Vector3(
                    transform.position.x * PositionFullToHalfMultiplicator.x, 
                    transform.position.y * PositionFullToHalfMultiplicator.y, 
                    transform.position.z * PositionFullToHalfMultiplicator.z);

                transform.localScale = new Vector3(
                    transform.localScale.x / ScaleFullToHalfMultiplicator.x,
                    transform.localScale.y / ScaleFullToHalfMultiplicator.y,
                    transform.localScale.z / ScaleFullToHalfMultiplicator.z);
            }
            else if (CurrentStage == 1)
            {
                CurrentStage++;
                MeshRenderer.enabled = false;
            }
        }

        public void PreviousStage()
        {
            if (CurrentStage == 1)
            {
                CurrentStage--;

                transform.position = new Vector3(
                    transform.position.x / PositionFullToHalfMultiplicator.x,
                    transform.position.y / PositionFullToHalfMultiplicator.y,
                    transform.position.z / PositionFullToHalfMultiplicator.z);

                transform.localScale = new Vector3(
                    transform.localScale.x * ScaleFullToHalfMultiplicator.x,
                    transform.localScale.y * ScaleFullToHalfMultiplicator.y,
                    transform.localScale.z * ScaleFullToHalfMultiplicator.z);
            }
            else if (CurrentStage == 2)
            {
                CurrentStage--;
                MeshRenderer.enabled = true;
            }
        }
    }
}
