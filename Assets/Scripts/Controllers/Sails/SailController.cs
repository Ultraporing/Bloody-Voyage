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
