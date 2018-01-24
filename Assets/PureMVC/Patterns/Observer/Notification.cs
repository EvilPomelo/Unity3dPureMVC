/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;

using PureMVC.Interfaces;

#endregion

namespace PureMVC.Patterns
{
    /// <summary>
    /// 该类的功能主要是定义一个消息对象里面存放的内容
    /// 三个成员变量对应三个属性(消息名,消息体，消息类型）
    /// 三个构造函数
    /// 一个ToString方法
    /// </summary>
    public class Notification : INotification
    {
        #region Constructors(三个构造方法，分别是只赋值方法名，赋值方法名与传递对象，赋值方法名，传递对象与方法类型)

        /// <summary>
        /// 只指定名字的构造函数
        /// </summary>
        /// <param name="name"></param>
        public Notification(string name)
            : this(name, null, null)
        { }

        /// <summary>
        /// 指定名字和消息体的构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="body"></param>
        public Notification(string name, object body)
            : this(name, body, null)
        { }

        /// <summary>
        /// 指定名字，消息体，消息类型的构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="body"></param>
        /// <param name="type"></param>
        public Notification(string name, object body, string type)
        {
            m_name = name;
            m_body = body;
            m_type = type;
        }

        #endregion

        #region Public Methods(重写的TosSring方法）

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string msg = "Notification Name: " + Name;
            msg += "\nBody:" + ((Body == null) ? "null" : Body.ToString());
            msg += "\nType:" + ((Type == null) ? "null" : Type);
            return msg;
        }

        #endregion

        #region Accessors(从父类重写的虚属性)

        /// <summary>
        /// 消息名属性
        /// </summary>
        public virtual string Name
        {
            get { return m_name; }
        }
		
        /// <summary>
        /// 消息体属性
        /// </summary>
        public virtual object Body
        {
            get
            {
                return m_body;
            }
            set
            {
                m_body = value;
            }
        }
		
        /// <summary>
        /// 消息类型属性
        /// </summary>
        public virtual string Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }

        #endregion

        #region Members(与属性关联的私有成员)

        /// <summary>
        /// 消息实例的名字
        /// </summary>
        private string m_name;

        /// <summary>
        /// 消息类型
        /// </summary>
        private string m_type;

        /// <summary>
        /// 消息体
        /// </summary>
        private object m_body;

        #endregion
    }
}