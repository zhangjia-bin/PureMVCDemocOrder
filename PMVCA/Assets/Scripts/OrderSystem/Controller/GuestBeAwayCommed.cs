using System.Collections;
using System.Collections.Generic;
using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class GuestBeAwayCommed : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
       ClientProxy clientProxy =   Facade.RetrieveProxy(ClientProxy.NAME) as ClientProxy;
       if (notification.Type =="Add")
       {
            ClientItem client =  notification.Body as ClientItem;
            client.state = 0;
            client.population = Random.Range(3, 14);
            clientProxy.AddClient(client);
       }else if (notification.Type == "Remove")
       {
           Debug.Log("客人走了");
           Debug.Log(notification.Body.GetType());
           clientProxy.DeleteClient(notification.Body as ClientItem);
        }
    }
}
