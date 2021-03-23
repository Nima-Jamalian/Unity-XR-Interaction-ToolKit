using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffest;
    public Vector3 trckingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffest);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trckingRotationOffset);
    }
}
public class VRRig : MonoBehaviour
{
    [SerializeField] private VRMap head;
    [SerializeField] private VRMap leftHand;
    [SerializeField] private VRMap rightHand;
    [SerializeField] private float turnSmoothness = 5f;
    [SerializeField] private Transform headConstraint;
    [SerializeField] private Vector3 headBodyOffset;

    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}
