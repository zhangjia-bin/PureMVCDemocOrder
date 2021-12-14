﻿
/*=========================================
* Author: Administrator
* DateTime:2017/6/21 12:43:50
* Description:$safeprojectname$
==========================================*/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace OrderSystem
{
    public class ClientView : MonoBehaviour
    {
        public UnityAction<ClientItem> CallWaiter = null;
        public UnityAction<Order> Order = null;
        public UnityAction Pay = null;

        private ObjectPool<ClientItemView> objectPool = null;
        private List<ClientItemView> clients = new List<ClientItemView>();
        private Transform parent = null;

        private void Awake()
        {
            parent = this.transform.Find("Content");
            var prefab = Resources.Load<GameObject>("Prefabs/UI/ClientItem");
            objectPool = new ObjectPool<ClientItemView>(prefab , "ClientPool");
        }

        public void UpdateClient( IList<ClientItem> clients,  IList<Action<object>> objs)
        {
            
            for (int i = 0; i < this.clients.Count; i++)
                objectPool.Push(this.clients[i]);

            this.clients.AddRange(objectPool.Pop(clients.Count));
            
            for ( int i = 0 ; i < this.clients.Count ; i++ )
            {
                var client = this.clients[i];
                client.transform.SetParent(parent);
                client.InitClient(clients[i]);
                client.actionList = objs;
                client.GetComponent<Button>().onClick.RemoveAllListeners();
                client.GetComponent<Button>().onClick.AddListener(( ) => { if(client.client.state ==0) CallWaiter(client.client); });
            }
        }
        public void UpdateState( ClientItem client )
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].client.id.Equals(client.id))
                {
                    clients[i].InitClient(client);
                    return;
                }
            }
        }

        public void RefrshClient(IList<ClientItem> Reclients)
        {
            for (int i = 0; i < Reclients.Count; i++)
            {
                UpdateState(Reclients[i]);
            }
        }
    }
}