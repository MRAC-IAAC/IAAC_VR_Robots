using UnityEngine;
using System.Collections.Generic;
using RosSharp.RosBridgeClient;

public class IAAC_TestSubscriber : UnitySubscriber<RosSharp.RosBridgeClient.MessageTypes.Std.String>
{
    protected override void ReceiveMessage(RosSharp.RosBridgeClient.MessageTypes.Std.String message)
    {
        Debug.Log(message.data);
    }
}

