
/*=========================================
* Author: Administrator
* DateTime:2017/6/21 18:17:04
* Description:$safeprojectname$
==========================================*/

namespace OrderSystem
{
    public class CookItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public int state { get; set; }
        public string cooking { get; set; }
        public Order cookOrder { get; set; }
        public CookItem( int id , string name , int state = 0 , string cooking = "" ,Order cookOrder = null)
        {
            this.id = id;
            this.name = name;
            this.state = state;
            this.cooking = cooking;
            this.cookOrder = cookOrder; //厨师当前所持有的客人的菜单
        }
        public override string ToString()
        {
            return id + "号厨师\n" + name + "\n" + resultState();
        }
        private string resultState()
        {
            if (state.Equals(0))
                return "休息中";
            return "忙碌中："+ cookOrder.client.id+"做菜中";
        }
    }
}