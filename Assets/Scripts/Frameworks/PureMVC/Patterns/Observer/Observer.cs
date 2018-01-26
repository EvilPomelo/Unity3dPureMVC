/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;
using System.Reflection;

using PureMVC.Interfaces;

#endregion

namespace PureMVC.Patterns
{
	/// <summary>
	/// 三个成员变量（传递的方法名，上下文对象，对象锁），两个属性（传递的方法名，上下文对象）
	/// 一个构造函数
	/// </summary>
	public class Observer : IObserver
	{
        #region Constructors(构造方法中给m_notifyMethod和m_notifyContext赋值)

       /// <summary>
       ///Obeserver的构造方法
       /// </summary>
       /// <param name="notifyMethod"></param>
       /// <param name="notifyContext"></param>
        public Observer(string notifyMethod, object notifyContext)
		{
			m_notifyMethod = notifyMethod;
			m_notifyContext = notifyContext;
		}

		#endregion

		#region Public Methods

		#region IObserver Members

		/// <summary>
		/// 发送消息给所有感兴趣的对象
		/// </summary>
		/// <param name="notification"></param>
		public virtual void NotifyObserver(INotification notification)
		{
			object context;
			string method;

			// Retrieve the current state of the object, then notify outside of our thread safe block
			lock (m_syncRoot)
			{
				context = NotifyContext;
				method = NotifyMethod;
			}
			//获取contex的类型
			Type t = context.GetType();
			//指定BindingFlags的值，最后根据f来判断获取哪一个方法
			BindingFlags f = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
			MethodInfo mi = t.GetMethod(method, f);
			mi.Invoke(context, new object[] { notification });
		}

		/// <summary>
		/// 比较传入对象
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public virtual bool CompareNotifyContext(object obj)
		{
			lock (m_syncRoot)
			{
				// Compare on the current state
				return NotifyContext.Equals(obj);
			}
		}

        #endregion

        #endregion

        #region Accessors(NotifyMethod与NotifyContext属性)

        /// <summary>
        /// 方法
        /// </summary>
        public virtual string NotifyMethod
		{
			private get
			{
				return m_notifyMethod;
			}
			set
			{
				m_notifyMethod = value;
			}
		}

		/// <summary>
		/// The notification context (this) of the interested object
		/// </summary>
		/// <remarks>This accessor is thread safe</remarks>
		public virtual object NotifyContext
		{
			private get
			{
				return m_notifyContext;
			}
			set
			{
				m_notifyContext = value;
			}
		}

        #endregion

        #region Members(私有成员变量m_notifyMethod和m_notifyContext)

        /// <summary>
        /// 持有的需要传递的方法名
        /// </summary>
        private string m_notifyMethod;

		/// <summary>
		/// 持有及需传递的上下文对象
		/// </summary>
		private object m_notifyContext;

		/// <summary>
		/// 对象锁
		/// </summary>
		protected readonly object m_syncRoot = new object();

		#endregion
	}
}
