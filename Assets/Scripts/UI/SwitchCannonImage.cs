using UnityEngine;
using System.Collections;
using Controllers.Cannons;

namespace UI
{
    public class SwitchCannonImage : SwitchImageOnTrigger
    {
        public CannonBankPosition Position;
        public CannonBankSide Side;
    }
}

