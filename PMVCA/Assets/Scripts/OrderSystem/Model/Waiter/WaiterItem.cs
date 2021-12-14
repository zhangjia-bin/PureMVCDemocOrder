
/*=========================================
* Author: Administrator
* DateTime:2017/6/21 17:55:55
* Description:$safeprojectname$
==========================================*/

namespace OrderSystem
{
    public enum E_WaiterState
    {
        Idle,
        Busy
    }
    public class WaiterOrder
    {
        public Order order;
        public int index;

        public WaiterOrder(Order order, int index)
        {
            this.order = order;
            this.index = index;
        }
    }
    public class WaiterItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public int state;
        public bool inJob = false;
        public bool isSonging = false;
        public bool isTelling = false;
        public Order order { get; set; }

        public WaiterItem( int id , string name , int state = 0 , Order order = null)
        {
            this.id = id;
            this.name = name;
            this.state = state;
            this.order = order;
        }
        public override string ToString()
        {
            return id + "号服务员\n" + name + "\n" + resultState();
        }
        private string resultState( )
        {
            if (state.Equals(0))
                return "休息中";
            return "忙碌中" + order.client.id + "送菜中";
        }
    }
}