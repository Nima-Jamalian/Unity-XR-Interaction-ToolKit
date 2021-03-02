using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;//Accessing the XR library 
using UnityEngine.Events;//Using Unity Event in order to create custom event when a button is pressed 
public class InputCapture : MonoBehaviour
{
    /*
     * In order access the controller input value first we need get access to the Input device 
     * We can also specify which device controller we want to get access to using XRNode
     */
    List<InputDevice> devices;
    public XRNode controllerNode;

    /*
     *Using unity event system we can create two event for OnPress and OnRealease button 
     */
    [Tooltip("Event when the button starts being pressed")]
    public UnityEvent OnPress;

    [Tooltip("Event when the button starts being released")]
    public UnityEvent OnRelease;

    //keep track of whether we are pressing the button
    bool isPressed = false;

    private void Awake()
    {
        //initializing devices
        devices = new List<InputDevice>();
    }

    void GetDevice()
    {
        //Getting the access to the input device
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);

    }

    // Start is called before the first frame update
    void Start()
    {
        GetDevice();
    }

    // Update is called once per frame
    void Update()
    {
        GetDevice();
        foreach (var device in devices)
        {
            Debug.Log(device.name + " " + device.characteristics);//getting the device name 

            if (device.isValid)
            {
                bool inputValue;
                //Checking when the primary button is pressed using CommonUsages class
                //You can check the key mapping at: https://docs.unity3d.com/Manual/xr_input.html
                //Simply change the code for CommonUsages.primaryButton to access whatever button you want
                if (device.TryGetFeatureValue(CommonUsages.primaryButton, out inputValue) && inputValue)
                {
                    if (!isPressed)
                    {
                        isPressed = true;
                        Debug.Log("OnPress event is called");

                        OnPress.Invoke();
                    }

                }
                else if (isPressed)
                {
                    isPressed = false;
                    OnRelease.Invoke();
                    Debug.Log("OnRelease event is called");

                }
            }

        }
    }
}
