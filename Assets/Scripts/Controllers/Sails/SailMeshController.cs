using UnityEngine;
using System.Collections;

namespace Controllers.Sails
{
    [RequireComponent(typeof(MeshRenderer))]
    public class SailMeshController : MonoBehaviour
    {
        public Vector3[] PositionStages = new Vector3[2];
        public Vector3[] ScaleStages = new Vector3[2];
        [Range(0,2)]
        public int CurrentStage = 0;
        private MeshRenderer MeshRenderer = null;

        // Use this for initialization
        private void Awake()
        {
            MeshRenderer = GetComponent<MeshRenderer>();

        }
	
        public void NextStage()
        {
            if (CurrentStage == 0)
            {
                CurrentStage++;

                transform.localPosition = PositionStages[CurrentStage];
                transform.localScale = ScaleStages[CurrentStage];
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

                transform.localPosition = PositionStages[CurrentStage];
                transform.localScale = ScaleStages[CurrentStage];
            }
            else if (CurrentStage == 2)
            {
                CurrentStage--;
                transform.localPosition = PositionStages[CurrentStage];
                transform.localScale = ScaleStages[CurrentStage];
                MeshRenderer.enabled = true;
            }
        }
    }
}
