using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PureMVC.Interfaces;

namespace PureMVCHelloWorld
{
    public class DataMediator : Mediator
    {
        //定义名称
        public new const string NAME = "DataMediator";
        //定义两个显示控件
        private Text TxtLevel;
        private Button BtnDisplayLevelNum;

        /// <summary>
        /// 构造函数,给控件赋值
        /// </summary>
        /// <param name="goRootNode"></param>
        public DataMediator(GameObject goRootNode)
        {
            TxtLevel = goRootNode.transform.Find("Text").GetComponent<Text>();
            BtnDisplayLevelNum = goRootNode.transform.Find("Button").GetComponent<Button>();
            BtnDisplayLevelNum.onClick.AddListener(BtnClick);
        }

        public void BtnClick()
        {
            SendNotification("Reg_StartDataCommand");
        }

        /// <summary>
        /// 本视图层，允许接收的消息
        /// </summary>
        /// <returns></returns>
        public override IList<string> ListNotificationInterests()
        {
            IList<string> listResult = new List<string>();
            //可以接收消息的集合
            listResult.Add("Msg_AddLevel");
            return listResult;
        }

        /// <summary>
        /// 处理所有其它类，发给本类要处理的消息
        /// </summary>
        /// <param name="notification"></param>
        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case "Msg_AddLevel":
                    MyData mydata = notification.Body as MyData;
                    TxtLevel.text = mydata.Level.ToString();
                    break;
                default:
                    break;
            }
        }
    }
}