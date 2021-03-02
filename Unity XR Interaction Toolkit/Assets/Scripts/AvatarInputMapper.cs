﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarInputMapper : MonoBehaviour
{

    //Avatar Transforms
    public Transform MainAvatarTransform;
    public Transform AvatarHead;
    public Transform AvatarBody;

    public Transform AvatarHand_Left;
    public Transform AvatarHand_Right;

    //XRRig Transforms
    public Transform XRHead;

    public Transform XRHand_Left;
    public Transform XRHand_Right;

    public Vector3 headPositionOffset;
    public Vector3 handRotationOffset;

    

    // Update is called once per frame
    void Update()
    {
        /*
         * Converting the head and hand positions of the VR device to Hand and hand position for our avatar
         * We also calculate the body position based on the Avatar head position 
         */
        
        //Head and Body sync
        MainAvatarTransform.position = Vector3.Lerp(MainAvatarTransform.position, XRHead.position + headPositionOffset, 0.5f);
        AvatarHead.rotation = Quaternion.Lerp(AvatarHead.rotation, XRHead.rotation, 0.5f);
        AvatarBody.rotation = Quaternion.Lerp(AvatarBody.rotation, Quaternion.Euler(new Vector3(0, AvatarHead.rotation.eulerAngles.y, 0)), 0.05f);

        //Hands sync
        AvatarHand_Right.position = Vector3.Lerp(AvatarHand_Right.position,XRHand_Right.position,0.5f);
        AvatarHand_Right.rotation = Quaternion.Lerp(AvatarHand_Right.rotation,XRHand_Right.rotation,0.5f)*Quaternion.Euler(handRotationOffset);

        AvatarHand_Left.position = Vector3.Lerp(AvatarHand_Left.position,XRHand_Left.position,0.5f);
        AvatarHand_Left.rotation = Quaternion.Lerp(AvatarHand_Left.rotation,XRHand_Left.rotation,0.5f)*Quaternion.Euler(handRotationOffset);
    }
}
