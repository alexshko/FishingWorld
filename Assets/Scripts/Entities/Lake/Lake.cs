using alexshko.fishingworld.Enteties.Fishes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace alexshko.fishingworld.Enteties
{
    public class Lake : FishDictionary
    {
        public VisualEffect RippleEffectRef;

        Vector3 pointOfRippleEffect = Vector3.zero;
        public Vector3 PointOFRippleEffect { 
            get { return pointOfRippleEffect; } 
            set {
                if (Vector3.Distance(value,Vector3.zero) < 0.1f)
                {
                    RippleEffectRef.enabled = false;
                }
                //only if the position of the effect moves by 0.1f then we should reposition the effect:
                else if (Vector3.Distance(pointOfRippleEffect, value) > 0.1f)
                {
                    RippleEffectRef.enabled = true;

                    pointOfRippleEffect = value;
                    RippleEffectRef.transform.position = value + new Vector3(0,.02f,0);
                    RippleEffectRef.Reinit();
                }
            }
        }
    }
}
