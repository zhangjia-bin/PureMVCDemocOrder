using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using OrderSystem;
using System;

public class HouseView : MonoBehaviour
{
    public UnityAction<HouseItem> CallHouse = null;

    private ObjectPool<HouseItemView> objectPool = null;
    private List<HouseItemView> houses = new List<HouseItemView>();
    private Transform parent = null;
    private void Awake()
    {
        parent = this.transform.Find("Content");
        var prefab = Resources.Load<GameObject>("Prefabs/UI/HouseItem");
        objectPool = new ObjectPool<HouseItemView>(prefab, "HousePool");
    }
    public void UpdateHouse1(IList<HouseItem> houses,IList<Action<object>> actions) 
    {
        for (int i = 0; i < this.houses.Count; i++)
            objectPool.Push(this.houses[i]);

        this.houses.AddRange(objectPool.Pop(houses.Count));

        for (int i = 0; i < this.houses.Count; i++)
        {
            var house = this.houses[i];
            house.transform.SetParent(parent);
            house.InitClient(houses[i]);
            house.actionList = actions;
            house.GetComponent<Button>().onClick.RemoveAllListeners();
            house.GetComponent<Button>().onClick.AddListener(() => { if (house.house.state == 0) CallHouse(house.house); });
        }
    }
    public void UpdateStateHouse(HouseItem house) 
    {
        for (int i = 0; i < houses.Count; i++)
        {
            if (houses[i].house.id.Equals(house.id))
            {
                houses[i].InitClient(house);
                return;
            }
        }
    }
}
