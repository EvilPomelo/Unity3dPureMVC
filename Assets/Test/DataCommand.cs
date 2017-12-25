using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;

namespace PureMVCHelloWorld
{
    public class DataCommand : SimpleCommand
    {
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="notification"></param>
        public override void Execute(INotification notification)
        {
            DataProxy datapro = (DataProxy)Facade.RetrieveProxy(DataProxy.NAME);
            datapro.AddLevel(10);
        }
    }
}