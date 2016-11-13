using UnityEngine;
using System.Collections;
using System;

namespace Controllers.Cannons
{
    [Serializable]
    public enum CannonBankSide
    {
        Left, Right
    }

    [Serializable]
    public enum CannonBankPosition
    {
        Back, Middle, Front
    }

    [Serializable]
    public class CannonBankControllers
    {
        public CannonBankPosition Position;
        public CannonBankSide Side;
        public CannonBankController CannonBank;
    }

    public class CannonBankController : MonoBehaviour
    {
        public CannonBaseController[] CannonBaseControllers;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
