/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;

using PureMVC.Core;
using PureMVC.Interfaces;
using PureMVC.Patterns;

#endregion

namespace PureMVC.Patterns
{
 
	/// <summary>
	/// 该类将Controller，Model，View三个类通过外观模式联系起来
	/// 5个成员变量，除开controller，model，view三个单例对象的引用，还有facade对象的单例引用，以及一个对象锁
	/// 一个属性，赋予Facade单例类的访问权限
	/// 四个个初始化方法，在InitializeFacade中调用model，controller，view的初始化方法
	/// 一个普通无参构造函数调用InitializeFacade，一个静态构造函数
	/// 三个不同参数的SendNotification函数，通过传入不同参数调用一个通用的消息发送函数NotifyObservers
	/// 四个Mediator操作方法，分别为注册，删除，检索，判断是否存在
	/// 四个Command操作方法，分别为注册，删除，检索，判断是否存在
	/// 四个Proxy操作方法，分别为注册，删除，检索，判断是否存在
	/// </summary>
    public class Facade : IFacade
	{
		#region Constructors

		/// <summary>
		/// 构造函数中调用初始化函数
		/// </summary>
        protected Facade() 
        {
			InitializeFacade();
		}

		#endregion

		#region Public Methods

		#region IFacade Members

		#region Proxy

		/// <summary>
		/// 注册一个代理类
		/// </summary>
		/// <param name="proxy"></param>
		public virtual void RegisterProxy(IProxy proxy)
		{
			m_model.RegisterProxy(proxy);
		}

		/// <summary>
		/// 检索一个代理类
		/// </summary>
		/// <param name="proxyName"></param>
		/// <returns></returns>
        public virtual IProxy RetrieveProxy(string proxyName)
		{
			return m_model.RetrieveProxy(proxyName);
		}

		//
        public virtual IProxy RemoveProxy(string proxyName)
		{
			return m_model.RemoveProxy(proxyName);
		}

		/// <summary>
		/// 检查该模型是否注册
		/// </summary>
		/// <param name="proxyName"></param>
		/// <returns></returns>
        public virtual bool HasProxy(string proxyName)
		{
			// The model is initialized in the constructor of the singleton, so this call should be thread safe.
			// This method is thread safe on the model.
			return m_model.HasProxy(proxyName);
		}

		#endregion

		#region Command

		/// <summary>
		/// 注册命令
		/// </summary>
		/// <param name="notificationName"></param>
		/// <param name="commandType"></param>
        public virtual void RegisterCommand(string notificationName, Type commandType)
		{
			m_controller.RegisterCommand(notificationName, commandType);
		}

		/// <summary>
		/// 移除注册
		/// </summary>
		/// <param name="notificationName"></param>
        public virtual void RemoveCommand(string notificationName)
		{
			m_controller.RemoveCommand(notificationName);
		}

		/// <summary>
		/// 检查命令是否注册
		/// </summary>
		/// <param name="notificationName"></param>
		/// <returns></returns>
        public virtual bool HasCommand(string notificationName)
		{
			return m_controller.HasCommand(notificationName);
		}

		#endregion

		#region Mediator

		/// <summary>
		/// 在View中注册一个Mediator类
		/// </summary>
		/// <param name="mediator"></param>
        public virtual void RegisterMediator(IMediator mediator)
		{
			m_view.RegisterMediator(mediator);
		}

		/// <summary>
		/// 通过mediator名字检索这个该mediator在view中是否注册
		/// </summary>
		/// <param name="mediatorName"></param>
		/// <returns></returns>
        public virtual IMediator RetrieveMediator(string mediatorName)
		{
			return m_view.RetrieveMediator(mediatorName);
		}

		/// <summary>
		/// 在View中移除该Mediator
		/// </summary>
		/// <param name="mediatorName"></param>
		/// <returns></returns>
        public virtual IMediator RemoveMediator(string mediatorName)
		{
			return m_view.RemoveMediator(mediatorName);
		}

		/// <summary>
		/// 判断该mediator名字是否注册
		/// </summary>
		/// <param name="mediatorName"></param>
		/// <returns></returns>
        public virtual bool HasMediator(string mediatorName)
		{
			return m_view.HasMediator(mediatorName);
		}

		#endregion

		#region Observer

		/// <summary>
		/// 视图对象发送消息给观察的对象
		/// </summary>
		/// <param name="notification"></param>
        public virtual void NotifyObservers(INotification notification)
		{
			m_view.NotifyObservers(notification);
		}

		#endregion

		#endregion

		#region INotifier Members（三个发送消息函数）

		/// <summary>
		/// 发送消息，参数包含消息名
		/// </summary>
		/// <param name="notificationName"></param>
        public virtual void SendNotification(string notificationName)
		{
			NotifyObservers(new Notification(notificationName));
		}

		/// <summary>
		/// 发送消息，参数包含消息名，消息体
		/// </summary>
		/// <param name="notificationName"></param>
		/// <param name="body"></param>
        public virtual void SendNotification(string notificationName, object body)
		{
			NotifyObservers(new Notification(notificationName, body));
		}

		/// <summary>
		/// 发送消息，参数包含消息名，消息体，消息类型
		/// </summary>
		/// <param name="notificationName"></param>
		/// <param name="body"></param>
		/// <param name="type"></param>
        public virtual void SendNotification(string notificationName, object body, string type)
		{
			NotifyObservers(new Notification(notificationName, body, type));
		}

		#endregion

		#endregion

		#region Accessors

		/// <summary>
		/// Facade单例属性
		/// </summary>
		public static IFacade Instance
		{
			get
			{
				if (m_instance == null)
				{
					lock (m_staticSyncRoot)
					{
						if (m_instance == null) m_instance = new Facade();
					}
				}

				return m_instance;
			}
		}

		#endregion

		#region Protected & Internal Methods

		/// <summary>
		/// Facade静态构造函数
		/// </summary>
        static Facade()
        {
        }

   		/// <summary>
   		/// 初始化Facade对象
   		/// </summary>
        protected virtual void InitializeFacade()
        {
			InitializeModel();
			InitializeController();
			InitializeView();
		}

        /// <summary>
        /// 用于初始化实例
        /// </summary>
		protected virtual void InitializeController()
        {
			if (m_controller != null) return;
			m_controller = Controller.Instance;
		}

        /// <summary>
        /// 初始化Model实例
        /// </summary>
        protected virtual void InitializeModel()
        {
			if (m_model != null) return;
			m_model = Model.Instance;
		}
		
       /// <summary>
       /// 用于初始化View实例
       /// </summary>
        protected virtual void InitializeView()
        {
			if (m_view != null) return;
			m_view = View.Instance;
		}

		#endregion

		#region Members

        //Controller类成员变量
		protected IController m_controller;

        //Model类成员变量
        protected IModel m_model;

        // View类成员变量
        protected IView m_view;

        // Facade(本类静态成员变量)
        protected static volatile IFacade m_instance;

		// 对象锁
		protected static readonly object m_staticSyncRoot = new object();

		#endregion
	}
}
