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
    /// <summary>
    /// PureMVC Model的接口定义
    /// </summary>
    /// <remarks>
    /// 在IMode中定义了一系列与Iproxy相关的方法
    /// </remarks>
    public interface IModel
    {
        /// <summary>
        /// 注册一个<c>IProxy</c>实例给<c>Model</c>
        /// </summary>
        /// <param name="proxy">一个Iprxy的示例 </param>
		void RegisterProxy(IProxy proxy);

        /// <summary>
        /// 从Model中取得一个名字为proxy<c>IProxy</c>的实例
        /// </summary>
        /// <param name="proxyName">名字为所需取得的Proxy实例</param>
        /// <returns>返回先前注册过的名字为proxyName实例</returns>
		IProxy RetrieveProxy(string proxyName);

        /// <summary>
        /// 从Model中删除一个名字为proxyName的<c>Iproxy</c>实例
        /// </summary>
        /// <param name="proxyName">需要删除的名字</param>
        ///<returns>返回删除的Iproxy实例</returns>
        IProxy RemoveProxy(string proxyName);

		/// <summary>
		/// 查看该proxyName是否被注册
		/// </summary>
		/// <param name="proxyName">需要查询的名字</param>
		/// <returns>>返回一个Bool值代表是否查询到</returns>
		bool HasProxy(string proxyName);
    }
}
