using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeSystem : MonoBehaviour
{
    public enum AttributeType : int {
        STR=0,
        DEX=1,
        CON=2,
        INT=3,
        WIS=4,
        CHA=5
    }

    public int[] attributes = new int[6];
    public int[] attributeModifiers = new int[6];

    // Start is called before the first frame update
    void Start()
    {
        // will need to read these values from some kind of save file???
        attributes = new int[] { 20, 20, 20, 20, 20, 20};
        attributeModifiers = new int[] {0, 0, 0, 0, 0, 0};

    }

    public void AddAttributeModifier(AttributeType attribute, int modifier)
    {
        attributeModifiers[(int)attribute] += modifier;
    }

    public void SetAttribute(AttributeType attribute, int newValue)
    {
        attributes[(int)attribute] = newValue;
    }

    public int GetAttribute(AttributeType attribute)
    {
        return attributes[(int)attribute] + attributeModifiers[(int)attribute];
    }

    public int GetBaseAttribute(AttributeType attribute)
    {
        return attributes[(int)attribute];
    }
    
    public int GetAttributeModifiers(AttributeType attribute)
    {
        return attributeModifiers[(int)attribute];
    }

    public int AttributeCheck(AttributeType attribute)
    {
        return (attributes[(int)attribute] + attributeModifiers[(int)attribute] - 10) / 2;
    }
}
