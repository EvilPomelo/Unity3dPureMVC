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
    /// <summary>
    /// 该类继承于IView，拥有一个普通构造函数，一个静态构造函数
    /// 一个静态单例属性，五个全局成员变量(两个对象锁，两个字典(持有IObserver对象和IMediator)，一个单例实例)
    /// 所有方法均可重写，三个观察者相关方法（）
    /// 四个中介者相关方法
    /// 一个初始化方法
    /// </summary>
    public class View : IView
    {
        #region Constructors

        /// <summary>
        /// 该类的构造函数，初始化需要初始化的成员变量
        /// </summary>
        protected View()
        {
            //初始化两个字典存储Mediator和Observer
            m_mediatorMap = new Dictionary<string, IMediator>();
            m_observerMap = new Dictionary<string, IList<IObserver>>();
            //View类初始化函数
            InitializeView();
        }

        #endregion

        #region Public Methods

        #region IView Members

        #region Observer

        /// <summary>
        /// 通过给定的消息名和观察者注册观察者
        /// </summary>
        /// <param name="notificationName"></param>
        /// <param name="observer"></param>
        public virtual void RegisterObserver(string notificationName, IObserver observer)
        {
            lock (m_syncRoot)
            {
                if (!m_observerMap.ContainsKey(notificationName))
                {
                    m_observerMap[notificationName] = new List<IObserver>();
                }

                m_observerMap[notificationName].Add(observer);
            }
        }

        /// <summary>
        /// 给观察者注册obeservers
        /// </summary>
        /// <param name="notification">消息体</param>
        public virtual void NotifyObservers(INotification notification)
        {
            IList<IObserver> observers = null;

            lock (m_syncRoot)
            {
                if (m_observerMap.ContainsKey(notification.Name))
                {
                    //从消息字典中得到所有监听了该消息名的对象
                    IList<IObserver> observers_ref = m_observerMap[notification.Name];
                    //深拷贝该观察者对象集合，防止错误修改后对原本集合造成影响
                    observers = new List<IObserver>(observers_ref);
                }
            }

            //如果该观察者集合不为空
            if (observers != null)
            {
                //依次给观察者发送消息				
                for (int i = 0; i < observers.Count; i++)
                {
                    IObserver observer = observers[i];
                    observer.NotifyObserver(notification);
                }
            }
        }

        /// <summary>
        /// 移除notifyContext对notificationName方法名的监听
        /// </summary>
        /// <param name="notificationName"></param>
        /// <param name="notifyContext"></param>
        public virtual void RemoveObserver(string notificationName, object notifyContext)
        {
            lock (m_syncRoot)
            {
                // 观察者集合中是否有这个消息名
                if (m_observerMap.ContainsKey(notificationName))
                {
                    IList<IObserver> observers = m_observerMap[notificationName];

                    // 遍历该集合
                    for (int i = 0; i < observers.Count; i++)
                    {
                        //如果集合中有观察者所属的对象与传递进来的对象相等
                        if (observers[i].CompareNotifyContext(notifyContext))
                        {
                            //移除该对象的监听
                            observers.RemoveAt(i);
                            break;
                        }
                    }

                    //如果没有监听者则，移除方法名
                    if (observers.Count == 0)
                    {
                        m_observerMap.Remove(notificationName);
                    }
                }
            }
        }

        #endregion

        #region Mediator

        /// <summary>
        /// 注册该中介者
        /// </summary>
        /// <param name="mediator"></param>
        public virtual void RegisterMediator(IMediator mediator)
        {
            lock (m_syncRoot)
            {
                //如果该中介者已注册，则返回
                if (m_mediatorMap.ContainsKey(mediator.MediatorName)) return;

                // 注册该中介者，key为中介者的Name成员变量
                m_mediatorMap[mediator.MediatorName] = mediator;

                // 得到它所关心的消息名
                IList<string> interests = mediator.ListNotificationInterests();

                // Register Mediator as an observer for each of its notification interests
                if (interests.Count > 0)
                {
                    // 创建一个观察者对象
                    IObserver observer = new Observer("handleNotification", mediator);

                    // Register Mediator as Observer for its list of Notification interests
                    for (int i = 0; i < interests.Count; i++)
                    {
                        RegisterObserver(interests[i].ToString(), observer);
                    }
                }
            }

            // 注册完成时调用
            mediator.OnRegister();
        }

        /// <summary>
        /// 通过该mediator名字得到mediator对象
        /// </summary>
        /// <param name="mediatorName"></param>
        /// <returns></returns>
        public virtual IMediator RetrieveMediator(string mediatorName)
        {
            lock (m_syncRoot)
            {
                if (!m_mediatorMap.ContainsKey(mediatorName)) return null;
                return m_mediatorMap[mediatorName];
            }
        }

        /// <summary>
        /// Remove an <c>IMediator</c> from the <c>View</c>
        /// </summary>
        /// <param name="mediatorName">The name of the <c>IMediator</c> instance to be removed</param>
        /// <remarks>This method is thread safe and needs to be thread safe in all implementations.</remarks>
        public virtual IMediator RemoveMediator(string mediatorName)
        {
            IMediator mediator = null;

            lock (m_syncRoot)
            {
                // 检索该mediator名字，没有则返回
                if (!m_mediatorMap.ContainsKey(mediatorName)) return null;
                mediator = (IMediator) m_mediatorMap[mediatorName];

                //取得该mediator关心的所有消息名
                IList<string> interests = mediator.ListNotificationInterests();

                for (int i = 0; i < interests.Count; i++)
                {
                    //改视图取消对这些关心的消息名的观察
                    RemoveObserver(interests[i], mediator);
                }

                // 从字典中移除该视图
                m_mediatorMap.Remove(mediatorName);
            }

            // 调用该视图的删除时调用方法
            if (mediator != null) mediator.OnRemove();
            return mediator;
        }

        //判断View中是否有该视图
        public virtual bool HasMediator(string mediatorName)
        {
            lock (m_syncRoot)
            {
                return m_mediatorMap.ContainsKey(mediatorName);
            }
        }

        #endregion

        #endregion

        #endregion

        #region Accessors

        /// <summary>
        /// 单例属性
        /// </summary>
        public static IView Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null) m_instance = new View();
                    }
                }

                return m_instance;
            }
        }

        #endregion

        #region Protected & Internal Methods

        /// <summary>
        /// 静态构造方法
        /// </summary>
        static View()
        {
        }

        /// <summary>
        /// 初始化View对象的函数，在构造函数中调用
        /// </summary>
        protected virtual void InitializeView()
        {
        }

        #endregion

        #region Members

        // 存储中介者对象名字与实例的字典
        protected IDictionary<string, IMediator> m_mediatorMap;

        //存储方法名和观察者集合的字典
        protected IDictionary<string, IList<IObserver>> m_observerMap;
		
        //m_instance不做优化，单例实例
        protected static volatile IView m_instance;

        //对象锁
        protected readonly object m_syncRoot = new object();

        //对象锁
        protected static readonly object m_staticSyncRoot = new object();

        #endregion
    }
}