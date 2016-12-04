using UnityEngine;
using System.Collections;

namespace Controllers.Sails
{
    public class SailController : MonoBehaviour
    {
        public SailMeshController[] SailMeshControllers;

        public void LowerSails()
        {
            foreach (SailMeshController smc in SailMeshControllers)
            {
                smc.NextStage();
            }
        }

        public void HoistSails()
        {
            foreach (SailMeshController smc in SailMeshControllers)
            {
                smc.PreviousStage();
            }
        }

        /// <summary>
        /// Network. set target SailStage. 0 = full sail, 1 = half sail, 2 = no sail
        /// </summary>
        /// <param name="targetStage">The target SailStage.</param>
        public void Network_SetTargetStage(int targetStage)
        {
            foreach (SailMeshController smc in SailMeshControllers)
            {
                int delta = targetStage - smc.CurrentStage;
                if (delta < 0)
                {
                    for (int i = 0; i < (int)Mathf.Abs(delta); i++)
                    {
                        smc.PreviousStage();
                    }
                }
                else if (delta > 0)
                {
                    for (int i = 0; i < (int)Mathf.Abs(delta); i++)
                    {
                        smc.NextStage();
                    }
                }
            }
        }

        public int GetCurrentSailStage()
        {
            if (SailMeshControllers.Length > 0)
            {
                return SailMeshControllers[0].CurrentStage;
            }
            else
            {
                Debug.LogError("There are no SailMeshControllers in the list!");
                return -1;
            }
        }
    }
}
