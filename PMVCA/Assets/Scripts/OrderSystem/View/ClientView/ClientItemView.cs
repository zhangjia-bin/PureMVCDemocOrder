
/*=========================================
* Author: Administrator
* DateTime:2017/6/22 16:47:56
* Description:$safeprojectname$
==========================================*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace OrderSystem
{
    public class ClientItemView : MonoBehaviour
    {
        private Text text = null;
        private Image image = null;
        public ClientItem client = null;
        public IList<Action<object>> actionList = new List<Action<object>>();
        private void Awake()
        {
            text = transform.Find("Id").GetComponent<Text>();
            image = transform.GetComponent<Image>();
        }

        public void InitClient( ClientItem client )
        {
            this.client = client;
            UpdateState(); 
        }

        private void UpdateState(  )
        {
            if (client==null)
            {
                return;
            }
            Color color = Color.white;
            if ( this.client.state.Equals(0) )
            {  color = Color.green;
                print("client.state.Equals(0)");
                //StartCoroutine(WaitingMenu());
                }
            else if (this.client.state.Equals(1))
            {
                color = Color.yellow;
                //StartCoroutine(Serving());
            }

            else if ( this.client.state.Equals(2) )
            {
                color = Color.red;
                StartCoroutine(eatting());
            }
            else if(this.client.state.Equals(3))
            {
                StartCoroutine(AddGuest());
            }
            Debug.Log(client.ToString());
            image.color = color;
            text.text = client.ToString();
            
        }

        IEnumerator Serving(float time = 4)
        {
            yield return  new WaitForSeconds(time);
            actionList[client.state].Invoke(client);
        }

        IEnumerator WaitingMenu(float time = 4)
        {
            yield return new WaitForSeconds(time);
            actionList[client.state].Invoke(client);
        }

        /// <summary>
        /// 来客人了
        /// </summary>
        /// <returns></returns>
        IEnumerator AddGuest(float time = 4)
        {
            yield return new WaitForSeconds(time);
            actionList[0].Invoke(client);

        }
        private IEnumerator eatting( float time = 5 )
        {
            Debug.Log(client.id + "号桌客人正在就餐");
            yield return new WaitForSeconds(time);
            client.state++;
            Debug.Log(client.id + "客人离开饭店");
            actionList[1].Invoke(client);
        }
    }
}