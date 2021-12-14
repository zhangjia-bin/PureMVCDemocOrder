using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using OrderSystem;

public class HouseProxy : Proxy
{
    public Queue<int> orders = new Queue<int>();
    public new const string NAME = "HouseProxy";

    public IList<HouseItem> Houses 
    {
        get { return (IList<HouseItem>)base.Data; }
    }

    public HouseProxy() : base(NAME, new List<HouseItem>())
    {
     
    }

    public override void OnRegister()
    {
        base.OnRegister();
        AddClient(new HouseItem(1, 0));
        AddClient(new HouseItem(2));
        AddClient(new HouseItem(3, 0));
        AddClient(new HouseItem(4, 0));
        AddClient(new HouseItem(5));
        AddClient(new HouseItem(6, 0));
    }
    public void AddClient(HouseItem item) 
    {
        if (Houses.Count<6) 
        {
            Houses.Add(item);
        }
    }

    public void HouseLiving(ClientItem client) 
    {
        int num = client.population;
        orders.Enqueue(num);
    }


    public void Lieave(HouseItem item) 
    {
        if (item.state==0) 
        {
            if (orders.Count > 0)
            {
                int num = orders.Dequeue();
                item.state++;
                item.num = num;
                SendNotification(OrderSystemEvent.RefreshRoom, item);
            }
            else 
            {
                Debug.Log("没有顾客");
            }
        }
        else 
        {
            Debug.Log("顾客以及入住");
        }
    }
}
