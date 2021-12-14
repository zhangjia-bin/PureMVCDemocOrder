using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using OrderSystem;

public class HouseMediator : Mediator
{
    private HouseProxy houseProxy = null;
    public new const string NAME = "HouseMediator";

    public HouseView View
    {
        get { return (HouseView)base.ViewComponent; }
    }
    public HouseMediator(HouseView view) : base(NAME, view) 
    {
        View.CallHouse += data => { SendNotification(OrderSystemEvent.Leave, data); };

    }
    public override void OnRegister()
    {
        base.OnRegister();
        houseProxy = Facade.RetrieveProxy(HouseProxy.NAME) as HouseProxy;
        if (null== houseProxy) 
            throw new Exception("获取" + HouseProxy.NAME + "代理失败");

            IList<Action<object>> actionList = new List<Action<object>>()
            {
              item=>SendNotification(OrderSystemEvent.RefreshRoom,item),
            };

        View.UpdateHouse1(houseProxy.Houses, actionList);
    }

    public override IList<string> ListNotificationInterests()
    {
        IList<string> noti = new List<string>();
        noti.Add(OrderSystemEvent.LiveIn);
        noti.Add(OrderSystemEvent.Leave);
        noti.Add(OrderSystemEvent.RefreshRoom);
        return noti;
    }

    public override void HandleNotification(INotification notification)
    {
        Debug.Log(notification.Name+"这是客房部");
        switch (notification.Name)
        {
            case OrderSystemEvent.LiveIn:
                ClientItem client = notification.Body as ClientItem;
                if(null==client)
                    throw new Exception("order is null ,please check it.");
                SendNotification(OrderSystemEvent.SelectRoom, client, "RUZHU");
                break;
            case OrderSystemEvent.Leave:
                HouseItem house= notification.Body as HouseItem;
                SendNotification(OrderSystemEvent.RefreshRoom, house);
                SendNotification(OrderSystemEvent.SelectRoom, house, "LIKAI");
                house.num = 0;
                break;
            case OrderSystemEvent.RefreshRoom:
                HouseItem house1 = notification.Body as HouseItem;
                View.UpdateStateHouse(house1);
                break;

        }
    }
}
