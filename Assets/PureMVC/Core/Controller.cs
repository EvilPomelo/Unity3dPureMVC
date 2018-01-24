/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;
using System.Collections.Generic;

using PureMVC.Interfaces;
using PureMVC.Patterns;

#endregion

namespace PureMVC.Core
{
    public class Controller : IController
	{
		#region Constructors

		/// <summary>
		/// Controller构造函数
		/// </summary>
		protected Controller()
		{
			m_commandMap = new Dictionary<string, Type>();	
			InitializeController();
		}

		#endregion

		#region Public Methods

		#region IController Members

		/// <summary>
		/// 根据传入消息体执行方法
		/// </summary>
		/// <param name="note"></param>
		public virtual void ExecuteCommand(INotification note)
		{
			Type commandType = null;

			lock (m_syncRoot)
			{
				if (!m_commandMap.ContainsKey(note.Name)) return;
				commandType = m_commandMap[note.Name];
			}

			object commandInstance = Activator.CreateInstance(commandType);

			if (commandInstance is ICommand)
			{
				((ICommand) commandInstance).Execute(note);
			}
		}

		/// <summary>
		/// 注册命令
		/// </summary>
		/// <param name="notificationName"></param>
		/// <param name="commandType"></param>
		public virtual void RegisterCommand(string notificationName, Type commandType)
		{
			lock (m_syncRoot)
			{
				if (!m_commandMap.ContainsKey(notificationName))
				{
					m_view.RegisterObserver(notificationName, new Observer("executeCommand", this));
				}

				m_commandMap[notificationName] = commandType;
			}
		}

		/// <summary>
		/// 判断是否有该消息名的实例
		/// </summary>
		/// <param name="notificationName"></param>
		/// <returns></returns>
		public virtual bool HasCommand(string notificationName)
		{
			lock (m_syncRoot)
			{
				return m_commandMap.ContainsKey(notificationName);
			}
		}

		/// <summary>
		/// 移除注册的命令实例
		/// </summary>
		/// <param name="notificationName"></param>
		public virtual void RemoveCommand(string notificationName)
		{
			lock (m_syncRoot)
			{
				if (m_commandMap.ContainsKey(notificationName))
				{
					// remove the observer

					// This call needs to be monitored carefully. Have to make sure that RemoveObserver
					// doesn't call back into the controller, or a dead lock could happen.
					m_view.RemoveObserver(notificationName, this);
					m_commandMap.Remove(notificationName);
				}
			}
		}

		#endregion

		#endregion

		#region Accessors

		/// <summary>
		/// controller类单例属性
		/// </summary>
		public static IController Instance
		{
			get
			{
				if (m_instance == null)
				{
					lock (m_staticSyncRoot)
					{
						if (m_instance == null) m_instance = new Controller();
					}
				}

				return m_instance;
			}
		}

		#endregion

		#region Protected & Internal Methods

		/// <summary>
		/// 静态构造函数
		/// </summary>
		static Controller()
		{
		}

		/// <summary>
		/// 在初始化函数中初始化view的单例
		/// </summary>
		protected virtual void InitializeController()
		{
			m_view = View.Instance;
		}

		#endregion

		#region Members

		/// <summary>
        /// View对象单例
        /// </summary>
		protected IView m_view;
		
        /// <summary>
        /// 字典类实例
        /// </summary>
        protected IDictionary<string, Type> m_commandMap;

        /// <summary>
        /// controller类单例
        /// </summary>
		protected static volatile IController m_instance;

		/// <summary>
		/// 对象锁
		/// </summary>
		protected readonly object m_syncRoot = new object();

		/// <summary>
		/// 用来锁住单例实例
		/// </summary>
		protected static readonly object m_staticSyncRoot = new object();

		#endregion
	}
}
