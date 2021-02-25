using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
//link to official documentation for XR Input: https://docs.unity3d.com/Manual/xr_input.html
public class MovementController : MonoBehaviour
{
    /*
    Creating a list of objects of type XRContoller, we will assign them though the inspector
    In the inspector set the size to 2
    Drag your left and right hand controllers - Controllers gameobject comes with script called XRController 
    through this the objects we created we access controllers XRController component
    which allows us to read the user controller inputs 
    */
    [SerializeField] private List<XRController> controllers;

    //XR Rig -> Camera Offset -> Attach the Main Camera Gamobject though the inspector 
    [SerializeField] private GameObject head;

    [SerializeField] private float speed = 3f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Foreach loop goes through all the elements in a given list 
        //In this case the give list is controllers list
        foreach (XRController xRController in controllers)
        {
            //Debug.Log(xRController.name);//Prints the name of controllers gameobject

            //Prints the name of controller in detail for example what device are you using
            //If Oculus -> output would be Oculus Touch Controller - Right/Left
            Debug.Log(xRController.inputDevice.name);

            //getting access to joystick input value we define out value of type vector 2 which hold the value of joystick input
            //The out put value looks like (0.5, 0.1), if the user is not interacting with joystick then it's at 0.0
            if(xRController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis,out Vector2 positionVector))
            {
                Debug.Log(positionVector);
                if (positionVector.magnitude > 0.15f)//Check if we have big input value, avoiding accidental touches 
                {
                    Move(positionVector);
                }
            }
        }
    }

    private void Move(Vector2 positionVector)
    {
        //Setting the joystick value as the forward vector
        Vector3 direction = new Vector3(positionVector.x, 0, positionVector.y);
        Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0);

        //Roting the input direction by horizontal head rotation
        direction = Quaternion.Euler(headRotation) * direction;

        //Apply speed and move the player
        Vector3 movement = direction * speed;
        transform.position = transform.position + (Vector3.ProjectOnPlane(Time.deltaTime * movement, Vector3.up));
    }
}
