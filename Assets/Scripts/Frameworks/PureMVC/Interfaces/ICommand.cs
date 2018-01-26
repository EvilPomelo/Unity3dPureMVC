/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
#region Using

using System;

#endregion

namespace PureMVC.Interfaces
{

    public interface ICommand
    {
        /// <summary>
        /// 根据传入消息体执行方法
        /// </summary>
		void Execute(INotification notification);
    }
}
