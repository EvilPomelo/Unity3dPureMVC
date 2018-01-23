/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;

#endregion

/// <summary>
/// 消息接口，只包含了3个属性，Name(消息名)，Body(消息传递对象)，Type(消息类型)，以及一个ToString方法。
/// </summary>
namespace PureMVC.Interfaces
{

    public interface INotification
    {
        /// <summary>
        /// 消息名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 消息体
        /// </summary>
        object Body { get; set; }
		
        /// <summary>
        /// 消息类型
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// 要求子类重写tostring方法
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}