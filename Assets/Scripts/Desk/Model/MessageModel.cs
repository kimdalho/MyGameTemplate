using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class MessageModel 
{
    public uint id;
    public string description;

    public MessageModel(uint _id, string _desc)
    {
        this.id = _id;
        this.description = _desc;
    }

}
