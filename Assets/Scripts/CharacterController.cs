﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Physics")]
    public float moveSpeed;
    public float jumpSpeed;
    public float maxVelocity;
    public float drag;
    private Rigidbody rBody;
    private Vector3 inputVector;

    [Header("Visuals")]
    public Transform model;
    public Animator animator;
    public float yVelocityThreshold;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Register input on X and Z axes of movement
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.z = Input.GetAxis("Vertical");

        //Add force based on player input
        rBody.AddForce(inputVector.normalized * moveSpeed);
        if (Input.GetKeyDown(KeyCode.Space)) rBody.AddForce(Vector3.up * jumpSpeed);
        //Apply drag to X and Z axes
        rBody.velocity = new Vector3(rBody.velocity.x * drag, rBody.velocity.y, rBody.velocity.z * drag);

        //Rotate the model to face its direction
        model.rotation = Quaternion.AngleAxis(-Mathf.Rad2Deg * Mathf.Atan2(rBody.velocity.z, rBody.velocity.x) + 90, Vector3.up);

        //Update animations based on Rigidbody values
        animator.SetFloat("Speed", rBody.velocity.magnitude / maxVelocity);

        if (rBody.velocity.y > yVelocityThreshold)
        {
            animator.SetInteger("YVelocity", 1);
        }
        else if (rBody.velocity.y < -yVelocityThreshold)
        {
            animator.SetInteger("YVelocity", -1);
        }
        else animator.SetInteger("YVelocity", 0);

        Debug.Log(rBody.velocity.y);
    }
}