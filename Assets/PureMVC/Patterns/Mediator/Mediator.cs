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
        /// Constructs a new mediator with the specified name and view component
        /// </summary>
        /// <param name="mediatorName">The name of the mediator</param>
        /// <param name="viewComponent">The view component to be mediated</param>
        public Mediator(string mediatorName, object viewComponent)
        {
            m_mediatorName = (mediatorName != null) ? mediatorName : NAME;
            m_viewComponent = viewComponent;
        }

        #endregion

        #region Public Methods

        #region IMediator Members

        /// <summary>
        /// List the <c>INotification</c> names this <c>Mediator</c> is interested in being notified of
        /// </summary>
        /// <returns>The list of <c>INotification</c> names </returns>
        public virtual IList<string> ListNotificationInterests()
        {
            return new List<string>();
        }

        /// <summary>
        /// Handle <c>INotification</c>s
        /// </summary>
        /// <param name="notification">The <c>INotification</c> instance to handle</param>
        /// <remarks>
        ///     <para>
        ///        Typically this will be handled in a switch statement, with one 'case' entry per <c>INotification</c> the <c>Mediator</c> is interested in. 
        ///     </para>
        /// </remarks>
        public virtual void HandleNotification(INotification notification)
        {
        }

        /// <summary>
        /// Called by the View when the Mediator is registered
        /// </summary>
        public virtual void OnRegister()
        {
        }

        /// <summary>
        /// Called by the View when the Mediator is removed
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