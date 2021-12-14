using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HouseItem 
{
    public HouseItem(int id, int state=0)
    {
        this.id = id;
        this.state = state;
    }

    public int id { get; set; }
    public int num { get; set; }
    public int state { get; set; }

    public override string ToString()
    {
        return id+"号房"+"\n"+ num + "个人"+"\n"+ ReturnState(state);
    }
    private string ReturnState(int state) 
    {
        if (state.Equals(0)) 
            return "空闲中";
        return "客房以入驻"+num+"人";

    }
}
