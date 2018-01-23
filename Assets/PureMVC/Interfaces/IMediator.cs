/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;
using System.Collections.Generic;

#endregion

namespace PureMVC.Interfaces
{
    public interface IMediator
    {
        //中介者名
        string MediatorName { get; }
		
        //持有的视图组件的引用
        object ViewComponent { get; set; }

        /// <summary>
        /// 关注的方法名集合，只有这几个方法名他才会去执行方法
        /// </summary>
        /// <returns></returns>
        IList<string> ListNotificationInterests();
		
        /// <summary>
        /// 调用该notification
        /// </summary>
        /// <param name="notification"></param>
        void HandleNotification(INotification notification);
		
        //该中介者注册时调用
        void OnRegister();

        //该中介者移除时调用
        void OnRemove();
    }
}