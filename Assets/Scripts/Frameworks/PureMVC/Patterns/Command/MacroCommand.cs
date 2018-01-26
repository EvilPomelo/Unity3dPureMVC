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
	/// 这个类用来批量执行命令
	/// 一个成员变量m_subCommands
	/// 一个构造函数
	/// 三个方法（初始化，增加命令，执行命令）
	/// </summary>
    public class MacroCommand : Notifier, ICommand
	{
		#region Constructors

		/// <summary>
		/// 构造函数
		/// </summary>
		public MacroCommand()
		{
			m_subCommands = new List<Type>();
			InitializeMacroCommand();
		}

		#endregion

		#region Public Methods

		#region ICommand Members

		/// <summary>
		/// 从列表中一次取出命令依次执行
		/// </summary>
		/// <param name="notification"></param>
		public virtual void Execute(INotification notification)
		{
			while (m_subCommands.Count > 0)
			{
				Type commandType = m_subCommands[0];
				object commandInstance = Activator.CreateInstance(commandType);

				if (commandInstance is ICommand)
				{
					((ICommand) commandInstance).Execute(notification);
				}

				m_subCommands.RemoveAt(0);
			}
		}

		#endregion

		#endregion

		#region Protected & Internal Methods

		/// <summary>
		/// 命令初始化函数
		/// </summary>
		protected virtual void InitializeMacroCommand()
		{
		}

        /// <summary>
        /// 增加命令
        /// </summary>
        /// <param name="commandType"></param>
        protected void AddSubCommand(Type commandType)
		{
            m_subCommands.Add(commandType);
		}

		#endregion

		#region Members
		
		//命令列表
		private IList<Type> m_subCommands;

		#endregion
	}
}
