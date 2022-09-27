using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : ActionInteraction
{
    

    public void Awake()
    {
        base.Awake();
        transform.gameObject.SetActive(true);
    }

    public override void TakeAction(string actionName)
    {
        //Debug.Log("Trying to take action " + actionName);
        if(actionName == "Open")
        {
            open();
        }
        else if(actionName == "Close")
        {
            close();
        }
    }

    public void open()
    {
        //Debug.Log("Open the door");
        //transform.gameObject.GetComponent<Collider>().enabled = false;
        transform.gameObject.active = false;
    }

    public void close()
    {
        Debug.Log("Closed the door");
    }

}
