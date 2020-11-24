using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LimbSyst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public struct PlayerInfo
    {
        bool earL, earR;
        bool eyeL, eyeR;
        bool handL, handR;
        bool legL, legR;

        int earTotal, eyeTotal, handTotal, legTotal;

        public void UpdateBody()
        {
            earTotal = addBoth(earL, earR);
            eyeTotal = addBoth(eyeL, eyeR);
            handTotal = addBoth(handL, handR);
            legTotal = addBoth(legR, legL);
        }

        int addBoth(bool p1, bool p2)
        {
            return Convert.ToInt32(p1) + Convert.ToInt32(p2);
        }

        public void Reset()
        {
            earL = earR = eyeL = eyeR = handL = handR = legL = legR = true;
            UpdateBody();
        }
    }
}
