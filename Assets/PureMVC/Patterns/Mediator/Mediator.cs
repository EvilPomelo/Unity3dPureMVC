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

namespace PureMVC.Patterns
{
    /// <summary>
    /// 两个成员变量对应两个属性，一个成员常量
    /// 三个构造函数，初始化成员变量视图名字和组件
    /// 四个继承的公共方法
    /// </summary>
    public class Mediator : Notifier, IMediator
    {
        #region Constants

        //成员常量
        public const string NAME = "Mediator";

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数，默认姓名为NAME，viewComponent为null
        /// </summary>
        public Mediator()
            : this(NAME, null)
        {
        }

        /// <summary>
        /// 构造函数，需传入姓名参数，viewComponent为null
        /// </summary>
        public Mediator(string mediatorName)
            : this(mediatorName, null)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mediatorName"></param>
        /// <param name="viewComponent"></param>
        public Mediator(string mediatorName, object viewComponent)
        {
            m_mediatorName = (mediatorName != null) ? mediatorName : NAME;
            m_viewComponent = viewComponent;
        }

        #endregion

        #region Public Methods

        #region IMediator Members

        /// <summary>
        /// 感兴趣的消息名列表
        /// </summary>
        /// <returns></returns>
        public virtual IList<string> ListNotificationInterests()
        {
            return new List<string>();
        }

        /// <summary>
        /// 获得消息名的句柄
        /// </summary>
        /// <param name="notification"></param>
        public virtual void HandleNotification(INotification notification)
        {
        }

        /// <summary>
        /// 注册时调用
        /// </summary>
        public virtual void OnRegister()
        {
        }

        /// <summary>
        /// 移除时调用
        /// </summary>
        public virtual void OnRemove()
        {
        }

        #endregion

        #endregion

        #region Accessors


        // 得到视图对象的名字
        public virtual string MediatorName
        {
            get { return m_mediatorName; }
        }

        // 得到该视图对象
        public virtual object ViewComponent
        {
            get { return m_viewComponent; }
            set { m_viewComponent = value; }
        }

        #endregion

        #region Members

        //mediatord的名字
        protected string m_mediatorName;

        //持有的视图对象
        protected object m_viewComponent;

        #endregion
    }
}