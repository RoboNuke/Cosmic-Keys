using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockpickDoor : SkillCheckInteract
{

    public override void onSuccess()
    {
        Debug.Log("Door Opened");
        transform.gameObject.GetComponent<DoorController>().open();
    }

    public override void onFailed()
    {
        Debug.Log("Door Remains Closed");
        //transform.gameObject.GetComponent<DoorController>().open();
    }
}
