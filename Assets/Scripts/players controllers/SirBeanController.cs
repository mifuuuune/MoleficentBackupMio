using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class SirBeanController : BasicController
{
    public float Force = 100f;

    protected override void StatusUpdate(float CurrentInput)
    {
        base.StatusUpdate(CurrentInput);
        anim.SetBool("UsingAbility", false);

        if (!IsGrounded)
        {
            Jump();

            if ((!SpecialJumped) && (Input.GetButtonDown("Jump")))
            {
                SpecialJump();
            }
        }
        else
        {
            SpecialJumped = false;
            anim.SetBool("SpecialJumping", false);

            if (CurrentInput > 0)
            {
                MovementSpeed = 4.5f;
                Run();
            }
            else Idle();

            if (Input.GetMouseButton(0))
            {
                CmdUseAbility();
                return;
            }

            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                Jump();
                return;
            }
        }
    }

    protected override void SpecialJump()
    {
        base.SpecialJump();
        rb.AddForce(Vector3.up * (JumpForce - (rb.velocity.y * rb.mass)), ForceMode.Impulse);
        anim.SetBool("SpecialJumping", true);
    }

    [Command]
    protected override void CmdUseAbility()
    {
        base.CmdUseAbility();
        //Debug.Log(AimRayCast().tag);

        try
        {
            if (ProximityRayCast().tag == "Boulder" && ProximityRayCast() != null)
            {

                anim.SetBool("UsingAbility", true);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(ProximityRayCast().transform.position.x - transform.position.x, 0, ProximityRayCast().transform.position.z - transform.position.z)), RotationSpeed);
                //ProximityRayCast().transform.Translate(transform.forward * 1.5f * Time.deltaTime);
                ProximityRayCast().GetComponent<Rigidbody>().AddForce(transform.forward * Force, ForceMode.Impulse);

            }
            else if (ProximityRayCast().tag == "Rock" && ProximityRayCast() != null)
            {

                anim.SetBool("UsingAbility", true);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(ProximityRayCast().transform.position.x - transform.position.x, 0, ProximityRayCast().transform.position.z - transform.position.z)), RotationSpeed);
                ProximityRayCast().GetComponent<Rigidbody>().AddForce(transform.forward * Force, ForceMode.Impulse);

            }
        }
        catch(NullReferenceException ex)
        {
            //Debug.Log("No object in range");
        }
       
    }
}