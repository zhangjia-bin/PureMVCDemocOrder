
/*=========================================
* Author: Administrator
* DateTime:2017/6/21 12:43:57
* Description:$safeprojectname$
==========================================*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

namespace OrderSystem
{
    public class CookView : MonoBehaviour
    {
        public UnityAction CallCook = null;
        public UnityAction<CookItem> ServerFood = null;
       

        private ObjectPool<CookItemView> objectPool = null;
        private List<CookItemView> cooks = new List<CookItemView>();
        private Transform parent = null;

        private void Awake()
        {
            parent = this.transform.Find("Content");
            var prefab = Resources.Load<GameObject>("Prefabs/UI/CookItem");
            objectPool = new ObjectPool<CookItemView>(prefab , "CookPool");
        }

        public void UpdateCook( IList<CookItem> cooks )
        {
            for ( int i = 0 ; i < this.cooks.Count ; i++ )
                objectPool.Push(this.cooks[i]);

            this.cooks.AddRange(objectPool.Pop(cooks.Count));
            ResfrshCook(cooks);
        }

        public void ResfrshCook(IList<CookItem> cooks)
        {
            for (int i = 0; i < this.cooks.Count; i++)
            {
                this.cooks[i].transform.SetParent(parent);
                var item = cooks[i];
                this.cooks[i].transform.Find("Id").GetComponent<Text>().text = item.ToString();
                Color color = this.cooks[i].GetComponent<Image>().color;
                if (item.state.Equals(0))
                {
                    color = Color.green;
                }
                else if (item.state.Equals(1))
                {
                    color = Color.yellow;
                    StartCoroutine(Cooking(cooks[i]));
                }
                else if (item.state.Equals(2))
                {
                    color = Color.red;
                    
                }
                this.cooks[i].GetComponent<Image>().color = color;
            }
        }
        /// <summary>
        /// 炒菜完成
        /// </summary>
        /// <param name="item"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        IEnumerator Cooking(CookItem item,float time = 4)
        {
            item.state = -1;
            yield return  new WaitForSeconds(time);
            item.state = 0;
            Debug.Log(item.cookOrder);
            ServerFood.Invoke(item);
            
        }
    }
}