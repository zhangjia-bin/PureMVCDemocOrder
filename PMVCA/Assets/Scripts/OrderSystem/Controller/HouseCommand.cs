using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using System;
using OrderSystem;

public class HouseCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        HouseProxy houseProxy = Facade.RetrieveProxy(HouseProxy.NAME) as HouseProxy;
        if (notification.Type == "RUZHU")
        {
            ClientItem client = notification.Body as ClientItem;
            houseProxy.HouseLiving(client);
        }
        else if (notification.Type == "LIKAI") 
        {
            Debug.Log("客人走了");
            Debug.Log(notification.Body.GetType());
            HouseItem item= notification.Body as HouseItem;
            houseProxy.Lieave(item);
        }
    }
}
