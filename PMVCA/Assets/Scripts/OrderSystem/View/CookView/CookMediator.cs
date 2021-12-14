
/*=========================================
* Author: Administrator
* DateTime:2017/6/20 19:22:49
* Description:$safeprojectname$
==========================================*/

using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

namespace OrderSystem
{
    public class CookMediator : Mediator
    {
        private CookProxy cookProxy = null;
        public new const string NAME = "CookMediator";
        public CookView CookView
        {
            get { return (CookView) base.ViewComponent; }
        }

        public CookMediator( CookView view ) : base(NAME , view)
        {
            CookView.CallCook += ( ) => { SendNotification(OrderSystemEvent.CALL_COOK); };
            CookView.ServerFood += item => { SendNotification(OrderSystemCommed.SERVER_FOOD,item); };
           // CookView.refrshCook += () => { SendNotification(OrderSystemEvent.ResfrshCook); };
        }

        public override void OnRegister( )
        {
            base.OnRegister();
            cookProxy = Facade.RetrieveProxy(CookProxy.NAME) as CookProxy;
            if(null == cookProxy)
                throw new Exception(CookProxy.NAME + "is null.");
            CookView.UpdateCook(cookProxy.Cooks);
        }

        public override IList<string> ListNotificationInterests( )
        {
            IList<string> notifications = new List<string>(); 
            notifications.Add(OrderSystemEvent.CALL_COOK);
            notifications.Add(OrderSystemCommed.SERVER_FOOD);
            notifications.Add(OrderSystemEvent.ResfrshCook);
            return notifications;
        }

        public override void HandleNotification( INotification notification )
        {
            switch (notification.Name)
            {
                case OrderSystemEvent.CALL_COOK:
                    Order order = notification.Body as Order;
                    if( null == order )
                        throw new Exception("order is null ,please check it.");
                    //todo 分配一个厨师开始做菜
                    Debug.Log("厨师接收到前台的订单,开始炒菜:" + order.names);
                    SendNotification(OrderSystemCommed.CookCooking,order);
                    break;
                case OrderSystemCommed.SERVER_FOOD:
                    Debug.Log("厨师通知服务员上菜");
                    CookItem cook = notification.Body as CookItem;
                    //  Debug.Log(cook.cookOrder.GetType());
                    SendNotification(OrderSystemEvent.ResfrshCook);//刷新一下厨师界面
                    SendNotification(OrderSystemCommed.selectWaiter, cook.cookOrder, "SHANGCAI");
                    cook.cookOrder = null;
                    break;
                case OrderSystemEvent.ResfrshCook:
                    cookProxy = Facade.RetrieveProxy(CookProxy.NAME) as CookProxy;
                    Debug.Log("刷新厨师状态");
                    if (null == cookProxy)
                        throw new Exception(CookProxy.NAME + "is null.");
                    CookView.ResfrshCook(cookProxy.Cooks);
                    break;;

            }
        }
    }
}