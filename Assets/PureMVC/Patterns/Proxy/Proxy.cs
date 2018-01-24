/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;

using PureMVC.Interfaces;
using PureMVC.Patterns;

#endregion

namespace PureMVC.Patterns
{
    /// <summary>
    /// 
    /// </summary>
    public class Proxy : Notifier, IProxy
    {
		#region Constants

		//Proxy对象默认名字
        public static string NAME = "Proxy";

		#endregion

		#region Constructors

		/// <summary>
		/// 构造函数，参数为空，使用默认参数
		/// </summary>
        public Proxy() 
            : this(NAME, null)
        {
		}

        /// <summary>
        /// 构造函数中给代理类指定一个自定义名字
        /// </summary>
        /// <param name="proxyName"></param>
        public Proxy(string proxyName)
            : this(proxyName, null)
        {
		}

        /// <summary>
        /// 构造一个代理类，使用
        /// </summary>
        /// <param name="proxyName"></param>
        /// <param name="data"></param>
		public Proxy(string proxyName, object data)
		{

			m_proxyName = (proxyName != null) ? proxyName : NAME;
			if (data != null) m_data = data;
		}

		#endregion

		#region Public Methods

		#region IProxy Members

		/// <summary>
		/// 代理类注册时调用
		/// </summary>
		public virtual void OnRegister()
		{
		}

		/// <summary>
		/// 代理类删除时调用
		/// </summary>
		public virtual void OnRemove()
		{
		}

		#endregion

		#endregion

		#region Accessors

		/// <summary>
		/// 代理类名字，只读
		/// </summary>
		/// <returns></returns>
		public virtual string ProxyName
		{
			get { return m_proxyName; }
		}

		/// <summary>
		/// 代理类属性
		/// </summary>
		public virtual object Data
		{
			get { return m_data; }
			set { m_data = value; }
		}

		#endregion

		#region Members

		//代理类的名字
		protected string m_proxyName;
		
		//代理类管理的对象
		protected object m_data;

		#endregion
	}
}
