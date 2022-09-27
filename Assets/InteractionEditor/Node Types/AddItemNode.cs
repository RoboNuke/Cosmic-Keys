using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Text.RegularExpressions;

public class AddItemNode : InteractionNode
{
    private List<ItemData> itemsToGive = new List<ItemData>();
    private List<string> itemList = new List<string>();
    private List<ItemData> itemDataList = new List<ItemData>();
    private Hashtable nameToData = new Hashtable();
    private Hashtable nameToCount = new Hashtable();

    public AddItemNode(InteractionGraphView graphView) : base(graphView)
    {
        nodeType = InteractionNodeTypes.ADD_ITEM;
        
        string[] assetNames = AssetDatabase.FindAssets("t:ItemData");
        
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var item = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
            //Debug.Log(item.ItemName);
            itemDataList.Add(item);
            itemList.Add(item.ItemName);
        }
        
    }

    public override void initPorts()
    {
        
        _graphView.AddStaticPort(this, "Output");
        var button = new Button(() => {AddTextPort(); });
        button.text = "New Item";
        titleContainer.Add(button);
        
    }

    public void AddTextPort(string itemName = "Enter Item Name", string initCount = "0")
    {
        var holder = new TextField { name = "Holder", value = "" };
        holder.contentContainer.Clear();
        var intField = new TextField
        {
            name = "Int Field",
            value = initCount
        };
        intField.style.minWidth = 30f;
        intField.RegisterValueChangedCallback(evt => StoreCount(evt.newValue, holder, intField));


        var textField = new TextField
        {
            name = "Test Name",
            value = itemName
        };
        if (itemName != "Enter Item Name")
        {
            textField.style.backgroundColor = Color.green;
            intField.style.backgroundColor = Color.green;
            textField.isReadOnly = true;
        }
        textField.isDelayed = true;
        textField.RegisterValueChangedCallback(evt => 
                ItemNameHandle(evt.newValue, textField, intField));
        
        contentContainer.Add(textField);


        Button deleteButton = new Button();
        deleteButton = new Button(() => RemoveItem(textField, deleteButton, holder))
        {
            text = "X"
        };
        
        holder.contentContainer.Add(intField);
        holder.contentContainer.Add(textField);
        holder.contentContainer.Add(deleteButton);

        mainContainer.Add(holder);
        
        RefreshExpandedState();
        RefreshPorts();

    }
    public void StoreCount(string newVal, TextField holder, TextField tf)
    {
        int newValue;
        if (int.TryParse(newVal, out newValue) && ((TextField)holder.contentContainer[1]).isReadOnly)
        {
            nameToCount[((TextField)holder.contentContainer[1]).value] = newValue;
            tf.style.backgroundColor = Color.green;
        }
        else
        {
            tf.style.backgroundColor = Color.red;
        }

    }
    public void RemoveItem(TextField tf, Button but, TextField holder)
    {
        if (nameToData.ContainsKey(tf.value))
        {
            itemsToGive.Remove((ItemData)nameToData[tf.value]);
            nameToData.Remove(tf.value);
        }
        contentContainer.Remove(holder);
        outputContainer.Remove(tf);
    }

    public void ItemNameHandle(string newValue, TextField tf, TextField intf)
    {
        int index = itemList.FindIndex(a => a == newValue);
        int num;
        if (index >= 0 && !nameToData.ContainsKey(itemDataList[index].ItemName) && int.TryParse(intf.value, out num))
        {
            itemsToGive.Add(itemDataList[index]);
            nameToData.Add(itemDataList[index].ItemName, itemDataList[index]);
            tf.isReadOnly = true;
            tf.style.backgroundColor = Color.green;
            nameToCount[itemDataList[index].ItemName] = num;
            
        }
        else
            tf.style.backgroundColor = Color.red;
    }

    public override void loadData(InteractionNodeData data)
    {
        GUID = data.Guid;
        itemsToGive = data.ItemsToGive;
        nameToCount = data.ItemNameToCount;
        foreach(ItemData item in itemsToGive)
        {
            nameToData[item.ItemName] = item;
            AddTextPort(item.ItemName, nameToCount[item.ItemName].ToString());
        }
    }

    public override InteractionNodeData getNodeData()
    {
        var nodeData = new InteractionNodeData();
        nodeData.nodeName = "Item Drop Node";
        nodeData.Guid = GUID;
        nodeData.Position = GetPosition().position;
        nodeData.ItemsToGive = itemsToGive;
        nodeData.NodeType = nodeType;
        nodeData.ItemNameToCount = nameToCount;
        return nodeData;
    }

    public override void addConnection(string portName)
    {

    }
}
