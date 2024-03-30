using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeGrab : PlayerBase
{
    [SerializeField] private float downStart;
    [SerializeField] private float downEnd;
    [SerializeField] private float fwd;
    [SerializeField] private float downHitY;
    [SerializeField] private float offset_1;
    [SerializeField] private float offset_2;
    
    private bool isHanging;
    
    private void FixedUpdate()
    {
        LedgeGrab();
    }

    private void LedgeGrab()
    {
        if(rb.velocity.y < 0 && !isHanging)
        {
            RaycastHit downHit;
            Vector3 lineDownStart = (transform.position + Vector3.up * downStart) + transform.forward * fwd;
            Vector3 lineDownEnd = (transform.position + Vector3.up * downEnd) + transform.forward * fwd;
            Physics.Linecast(lineDownStart, lineDownEnd, out downHit,LayerMask.GetMask("Ledge"));
            Debug.DrawLine(lineDownStart, lineDownEnd);

            if (downHit.collider != null)
            {
                RaycastHit fwdHit = new RaycastHit();
                Vector3 lineFwdStart = new Vector3(transform.position.x, downHit.point.y - downHitY, transform.position.z);
                Vector3 lineFwdEnd = new Vector3(transform.position.x, downHit.point.y - downHitY, transform.position.z) + transform.forward;
                Physics.Linecast(lineFwdStart, lineFwdEnd, out fwdHit,LayerMask.GetMask("Ledge"));
                Debug.DrawLine(lineFwdStart, lineFwdEnd);

                if (downHit.collider != null)
                {
                    rb.useGravity = false;

                    rb.velocity = Vector3.zero;

                    isHanging = true;

                    Vector3 hangPos = new Vector3(fwdHit.point.x, downHit.point.y, fwdHit.point.z);
                    Vector3 offSet = transform.forward * offset_1 + transform.up * offset_2;
                    hangPos += offSet;
                    transform.position = hangPos;
                    transform.forward = fwdHit.normal;
                }
            }
        }
    }
}
