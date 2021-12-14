using System.Collections;
using System.Collections.Generic;
using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class CookCommend : SimpleCommand
{
   
    public override void Execute(INotification notification)
    {
        CookProxy cookProxy = Facade.RetrieveProxy(CookProxy.NAME) as CookProxy; //厨师的代理
        Order order = notification.Body as Order;
        cookProxy.CookCooking(order);
    }
}
