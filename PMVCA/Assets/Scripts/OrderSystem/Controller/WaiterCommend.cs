using System.Collections;
using System.Collections.Generic;
using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class WaiterCommend : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        WaiterProxy waiterProxy = Facade.RetrieveProxy(WaiterProxy.NAME) as WaiterProxy;
        if (notification.Type == "SHANGCAI")
        {
            
            Debug.Log("寻找服务员上菜");
            waiterProxy.ChangeWaiter(new WaiterOrder(notification.Body as Order, 1) );
        }else if (notification.Type == "Over")
        {
            Debug.Log("服务员没事干");
            waiterProxy.RemoveWaiter(notification.Body as WaiterItem);
        }else if(notification.Type == "XIADAN")
        {
            waiterProxy.ChangeWaiter(new WaiterOrder( notification.Body as Order,2));          
        }
    }
}
