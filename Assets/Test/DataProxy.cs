using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns;
using UnityEngine;
namespace PureMVCHelloWorld
{
    public class DataProxy : Proxy
    {
        //声明本类的名称
        public new const string NAME = "DataProxy";
        //引用实体类
        private MyData _MyData = null;

        /// <summary>
        /// 增加玩家的等级
        /// </summary>
        public DataProxy() : base(NAME)
        {
            _MyData = new MyData();
        }

        /// <summary>
        /// 增加玩家的等级
        /// </summary>
        /// <param name="addNumber"></param>
        public void AddLevel(int addNumber)
        {
            _MyData.Level += addNumber;
            SendNotification("Msg_AddLevel",_MyData);
        }

    }
}