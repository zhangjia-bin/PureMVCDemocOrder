
/*=========================================
* Author: Administrator
* DateTime:2017/6/21 12:39:43
* Description:$safeprojectname$
==========================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

namespace OrderSystem
{
    public class WaiterView : MonoBehaviour
    {
        public UnityAction CallWaiter = null;
        public UnityAction<Order> Order = null;
        public UnityAction Pay = null;
        public UnityAction<Order> CallCook = null;
        public UnityAction<WaiterItem> ServerFood = null;

        private ObjectPool<WaiterItemView> objectPool = null;
        private List<WaiterItemView> waiters = new List<WaiterItemView>();
        private Transform parent = null;
        public System.Action<object> Over { get; internal set; }

        private void Awake()
        {
            parent = this.transform.Find("Content");
            var prefab = Resources.Load<GameObject>("Prefabs/UI/WaiterItem");
            objectPool = new ObjectPool<WaiterItemView>(prefab , "WaiterPool");
        }
        public void UpdateWaiter( IList<WaiterItem> waiters )
        {
            for ( int i = 0 ; i < this.waiters.Count ; i++ )
                objectPool.Push(this.waiters[i]);

            this.waiters.AddRange(objectPool.Pop(waiters.Count));
            Move(waiters);
        }

        public void Move(IList<WaiterItem> waiters,Order order=null)
        {
            for (int i = 0; i < this.waiters.Count; i++)
            {
                this.waiters[i].transform.SetParent(parent);
                var item = waiters[i];
                this.waiters[i].transform.Find("Id").GetComponent<Text>().text = item.ToString();
                Color color = Color.white;
                if (item.state.Equals(0))
                    color = Color.green;
                else if (item.state.Equals(1)) { color = Color.yellow;
                    item.isTelling = true;
                    StartCoroutine(WaiterServing(item));
                }
                else if (item.state.Equals(2))
                    color = Color.red;
                else if (item.state.Equals(3))
                {
                    item.isSonging = true;
                    color = Color.blue;
                    StartCoroutine(WaiterTellCook(item, order));
                }
                    
                this.waiters[i].GetComponent<Image>().color = color;
            }
        }

        IEnumerator WaiterTellCook(WaiterItem item,Order order)
        {
            yield return new WaitForSeconds(2);
            CallCook.Invoke(order);
            Over.Invoke(item);
        }

        IEnumerator WaiterServing(WaiterItem item,float time = 4)
        {
            yield return  new WaitForSeconds(time);
           ServerFood.Invoke(item);
            Over.Invoke(item);
        }
    }
}