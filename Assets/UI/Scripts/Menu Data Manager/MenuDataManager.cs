using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuDataManager : MonoBehaviour
{
    protected GameObject player;
    public GameObject DataCardPrefab;

    public abstract void UpdateData();
}
