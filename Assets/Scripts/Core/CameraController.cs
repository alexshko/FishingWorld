using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Threading.Tasks;

namespace alexshko.fishingworld.Core {
    public class CameraController : MonoBehaviour
    {
        public CinemachineVirtualCamera mainVcam;
        public CinemachineVirtualCamera fishVCam;

        //the instance for the Singelton:
        public static CameraController Instance;

        #region variables for the registering of the currently used camera
        private CinemachineVirtualCamera currentActive = null;
        private int LastPriority;
        #endregion

        private void Awake()
        {
            Instance = this;
            currentActive = mainVcam;
        }

        private async Task ResetPriorityOfCurrentCam()
        {
            if (currentActive != mainVcam)
            {
                currentActive.Priority = LastPriority;
                currentActive = mainVcam;
                LastPriority = 0;
                await Task.Delay(1000);
            }
        }

        private async Task SetCurrentCam(CinemachineVirtualCamera CamToRegister)
        {
            if (currentActive != CamToRegister)
            {
                LastPriority = CamToRegister.Priority;
                CamToRegister.Priority = mainVcam.Priority + 10;
                currentActive = CamToRegister;
                await Task.Delay(1000);
            }
        }

        public async Task SetFocusOnFishingSpot()
        {
            if ((mainVcam) && (fishVCam) && (currentActive != fishVCam))
            {
                //if the currently used cam is also not the main cam, then we should unregister it first:
                if (currentActive != mainVcam)
                {
                    await ResetPriorityOfCurrentCam();
                }

                //set the current active cammera to fish cammera:
                await SetCurrentCam(fishVCam);
            }
        }

        public async Task SetFocusOnMainCam()
        {
            if ((mainVcam) && (fishVCam) && (currentActive != mainVcam))
            {
                await ResetPriorityOfCurrentCam();
            }
        }
    }
}
