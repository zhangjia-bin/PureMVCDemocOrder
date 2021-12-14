
/*=========================================
* Author: Administrator
* DateTime:2017/6/21 17:56:03
* Description:$safeprojectname$
==========================================*/

using System.Collections.Generic;
using System.Diagnostics;
using PureMVC.Patterns;
using Debug = UnityEngine.Debug;

namespace OrderSystem
{
    public class WaiterProxy : Proxy
    {
        public new const string NAME = "WaiterProxy";
        public IList<WaiterItem> Waiters
        {
            get { return (IList<WaiterItem>)base.Data; }
        }
        public Queue<WaiterOrder> orders=new Queue<WaiterOrder>();
        public WaiterProxy() : base(NAME, new List<WaiterItem>())
        {
            AddWaiter(new WaiterItem(1, "小丽", 0));
            AddWaiter(new WaiterItem(2, "小红", 0));
            AddWaiter(new WaiterItem(3, "小花", 0));
        }
        public void AddWaiter(WaiterItem item)
        {
            Waiters.Add(item);
        }
        public void RemoveWaiter(WaiterItem item)
        {

            for (int i = 0; i < Waiters.Count; i++)
            {
                if (item.id == Waiters[i].id)
                {
                    Waiters[i].state = 0;
                    SendNotification(OrderSystemEvent.ResfrshWarite);
                    item.isSonging = false;
                    item.isTelling = false;
                    if (orders.Count>0)
                    {
                        ChangeWaiter(orders.Dequeue());
                    }
                    return;
                }
            }
        }
        /// <summary>
        /// 查找空闲状态的服务员
        /// </summary>
        public void ChangeWaiter(WaiterOrder wo)
        {
            //UnityEngine.Debug.LogWarning(Waiters[0].name+""+ Waiters[0].state);
            //UnityEngine.Debug.LogWarning(Waiters[1].name + "" + Waiters[1].state);
            //UnityEngine.Debug.LogWarning(Waiters[2].name + "" + Waiters[2].state);
            WaiterItem item = GetIdleWaiter();
            if (item==null)
            {
                orders.Enqueue(wo);
            }
            if (item != null)
            {
                UnityEngine.Debug.LogWarning("2222");

                item.order = wo.order;
                if (wo.index == 1)
                {
                    item.state = 1;
                    //发送消息改变了服务员的属性
                    SendNotification(OrderSystemEvent.ResfrshWarite, item);

                }
                else if (wo.index == 2)
                {
                    item.state = 3;
                    //发送消息改变了服务员的属性
                    SendNotification(OrderSystemEvent.ResfrshWarite, wo.order);

                }

            }
        }
        private WaiterItem GetIdleWaiter()
        {
            foreach (WaiterItem waiter in Waiters)
                if (waiter.state.Equals((int)E_WaiterState.Idle))
                    return waiter;
            UnityEngine.Debug.LogWarning("暂无空闲服务员请稍等..");
            return null;
        }
    }
}