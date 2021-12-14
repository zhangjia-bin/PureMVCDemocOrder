using System.Collections;
using System.Collections.Generic;
using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class GetAndExitOrderCommed : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        OrderProxy orderProxy = PureMVC.Patterns.Facade.Instance.RetrieveProxy("OrderProxy") as OrderProxy;
        MenuProxy menuProxy = PureMVC.Patterns.Facade.Instance.RetrieveProxy("MenuProxy") as MenuProxy;
       // WaiterProxy waiterProxy = PureMVC.Patterns.Facade.Instance.RetrieveProxy("WaiterProxy") as WaiterProxy;
        Debug.Log(notification.Type);
        if (notification.Type =="Get")
        {
            Order order = new Order(notification.Body as ClientItem,menuProxy.Menus);
            orderProxy.AddOrder(order);
            SendNotification(OrderSystemEvent.UPMENU, order);
           // waiterProxy.ChangeWaiter();//上菜改变服务员的属性
        }
        else if (notification.Type =="Exit") //删除菜单 
        {
            Order order = new Order(notification.Body as ClientItem, menuProxy.Menus);
            orderProxy.RemoveOrder(order); 
        }
    }
}
