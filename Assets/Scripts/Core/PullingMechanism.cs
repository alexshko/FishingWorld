using alexshko.fishingworld.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace alexshko.fishingworld.Core
{
    [RequireComponent(typeof(GameManagement))]
    public class PullingMechanism : MonoBehaviour
    {
        public PullRodStick pullingSlider;
        public FishResistMeter fishResistSlider;

    }
}
