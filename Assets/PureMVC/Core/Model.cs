/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;
using System.Collections.Generic;

using PureMVC.Interfaces;

#endregion

namespace PureMVC.Core
{
    /// <summary>
    /// IModel的单例实现
    /// </summary>
    public class Model : IModel
    {
		#region Constructors

		/// <summary>
		/// 初始化字典，并调用Model初始化方法
		/// </summary>
		protected Model()
		{
			m_proxyMap = new Dictionary<string, IProxy>();
			InitializeModel();
		}

		#endregion
		///Model层的公共方法
		#region Public Methods
		
		#region IModel Members
		/// <summary>
		/// 在Model的字典中注册该proxy
		/// </summary>
		/// <param name="proxy"></param>
		public virtual void RegisterProxy(IProxy proxy)
		{
			lock (m_syncRoot)
			{
				m_proxyMap[proxy.ProxyName] = proxy;
			}

			proxy.OnRegister();
		}

		/// <summary>
		/// 查找该哈希表中是否有名字为proxyName的对象
		/// </summary>
		/// <param name="proxyName"></param>
		/// <returns>返回值为Iproxy</returns>
		public virtual IProxy RetrieveProxy(string proxyName)
		{
			lock (m_syncRoot)
			{
				if (!m_proxyMap.ContainsKey(proxyName)) return null;
				return m_proxyMap[proxyName];
			}
		}

		/// <summary>
		/// 检查该哈希表中是否有名字为proxyName的对象
		/// </summary>
		/// <param name="proxyName"></param>
		/// <returns></returns>
		public virtual bool HasProxy(string proxyName)
		{
			lock (m_syncRoot)
			{
				return m_proxyMap.ContainsKey(proxyName);
			}
		}

		/// <summary>
		/// 从Model中的字典删除该proxyName的索引
		/// </summary>
		/// <param name="proxyName"></param>
		/// <returns></returns>
		public virtual IProxy RemoveProxy(string proxyName)
		{
			IProxy proxy = null;

			lock (m_syncRoot)
			{
				if (m_proxyMap.ContainsKey(proxyName))
				{
					proxy = RetrieveProxy(proxyName);
					m_proxyMap.Remove(proxyName);
				}
			}

			if (proxy != null) proxy.OnRemove();
			return proxy;
		}

		#endregion

		#endregion

		#region Accessors

		/// <summary>
		/// 获得该Model的单例
		/// </summary>
		public static IModel Instance
		{
			get
			{
				if (m_instance == null)
				{
					lock (m_staticSyncRoot)
					{
						if (m_instance == null) m_instance = new Model();
					}
				}

				return m_instance;
			}
		}

		#endregion

		#region Protected & Internal Methods

		/// <summary>
		/// 静态的构造方法
		/// </summary>
		static Model()
		{
		}

		/// <summary>
		/// 初始化这个模型，构造方法中调用
		/// </summary>
		protected virtual void InitializeModel()
		{
		}

		#endregion

		#region Members

		/// <summary>
		/// 根据ProxyName存储IProxy的字典
		/// </summary>
		protected IDictionary<string, IProxy> m_proxyMap;

		/// <summary>
		/// IModel，volatile关键字代表该变量是易变的，直接从地址读值，不需要编译器优化(例如从寄存器中读值)
		/// </summary>
		protected static volatile IModel m_instance;

		/// <summary>
		/// 该对象用于锁
		/// </summary>
		protected readonly object m_syncRoot = new object();

		/// <summary>
		/// 用于锁住单例的调用
		/// </summary>
		protected static readonly object m_staticSyncRoot = new object();

		#endregion
    }
}
