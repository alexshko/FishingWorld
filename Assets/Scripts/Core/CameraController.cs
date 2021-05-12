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
        public int WaitTimeInSeconds;

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

        private void ResetPriorityOfCurrentCam()
        {
            if (currentActive != mainVcam)
            {
                currentActive.Priority = LastPriority;
                currentActive = mainVcam;
                LastPriority = 0;
            }
        }

        private void SetCurrentCam(CinemachineVirtualCamera CamToRegister)
        {
            if (currentActive != CamToRegister)
            {
                ResetPriorityOfCurrentCam();

                LastPriority = CamToRegister.Priority;
                currentActive = CamToRegister;
                currentActive.Priority = Mathf.Max(mainVcam.Priority, fishVCam.Priority) + 10;
            }
        }

        public async Task SetFocusOnFishingSpot()
        {
            if ((mainVcam) && (fishVCam) && (currentActive != fishVCam))
            {
                ////if the currently used cam is also not the main cam, then we should unregister it first:
                //if (currentActive != mainVcam)
                //{
                //    ResetPriorityOfCurrentCam();
                //}

                //set the current active cammera to fish cammera:
                SetCurrentCam(fishVCam);

                await Task.Delay(WaitTimeInSeconds * 1000);
            }
        }

        public async Task SetFocusOnMainCam()
        {
            if ((mainVcam) && (fishVCam) && (currentActive != mainVcam))
            {
                ResetPriorityOfCurrentCam();
                await Task.Delay(WaitTimeInSeconds * 1000);
            }
        }
    }
}
