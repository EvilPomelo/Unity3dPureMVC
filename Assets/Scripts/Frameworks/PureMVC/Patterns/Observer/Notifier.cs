/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using PureMVC.Interfaces;

#endregion

namespace PureMVC.Patterns
{
	/// <summary>
	/// 该类的作用主要是定义消息的操作方法
	/// 一个成员变量及对应属性用于持有Facade单例的引用
	/// </summary>
    public class Notifier : INotifier
    {
        #region Public Methods

        #region INotifier Members(通过三组不同参数的SendNotification调用facade单例中的SendNotification)

        /// <summary>
        /// 通过外观模式调用发送消息的方法，只发送消息名
        /// </summary>
        /// <param name="notificationName"></param>
        public virtual void SendNotification(string notificationName) 
		{
			m_facade.SendNotification(notificationName);
		}

	    /// <summary>
	    /// 通过外观模式调用发送消息的方法，发送消息名和消息体
	    /// </summary>
	    /// <param name="notificationName"></param>
	    /// <param name="body"></param>
		public virtual void SendNotification(string notificationName, object body)
		{
			m_facade.SendNotification(notificationName, body);
		}

	    /// <summary>
	    /// 通过外观模式调用发送消息的方法，发送消息名,消息体和消息类型
	    /// </summary>
	    /// <param name="notificationName"></param>
	    /// <param name="body"></param>
	    /// <param name="type"></param>
		public virtual void SendNotification(string notificationName, object body, string type)
		{
            m_facade.SendNotification(notificationName, body, type);
		}

        #endregion

        #endregion

        #region Accessors(声明Facade属性，暴露给外部)

        /// <summary>
        /// Facade属性
        /// </summary>
        protected IFacade Facade
		{
			get { return m_facade; }
		}

		#endregion

		#region Members （给m_facade私有变量赋值为Facade单例）

	    /// <summary>
	    /// 获取外观模式的单例
	    /// </summary>
	    private IFacade m_facade = Patterns.Facade.Instance;

	    #endregion
    }
}
