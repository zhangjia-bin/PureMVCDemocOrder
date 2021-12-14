using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OrderSystem;
using System;

public class HouseItemView : MonoBehaviour
{
    private Text text = null;
    private Image image = null;
    public HouseItem house=null;
    public IList<Action<object>> actionList = new List<Action<object>>();

    private void Awake()
    {
        text = transform.Find("ID").GetComponent<Text>();
        image = transform.GetComponent<Image>();
    }
    public void InitClient(HouseItem house) 
    {
        this.house = house;
        UpdateState();
    }
    private void UpdateState()
    {
        if (house == null)
        {
            return;
        }
        Color color = Color.white;
        if (this.house.state.Equals(0))
        {
            color = Color.green;
        }
        else if (this.house.state.Equals(1))
        {
            color = Color.yellow;
            StartCoroutine(EnterHouse(house));
        }
        else if (this.house.state.Equals(2))
        {
            color = Color.red;
        }

        Debug.Log("客房部的:" + house.ToString());
        image.color = color;
        text.text = house.ToString();
    }
    IEnumerator EnterHouse(HouseItem item,float time = 6) 
    {
        Debug.Log("客户部:"+house.id+"号客人"+"入驻");
        item.state = -1;
        yield return new WaitForSeconds(time);
        item.state=0;
        Debug.Log(house.id+"号客人离开客房部");
        actionList[0].Invoke(item);
    }
}
