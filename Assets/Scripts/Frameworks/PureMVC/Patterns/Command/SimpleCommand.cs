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
	
    public class SimpleCommand : Notifier, ICommand
    {
		#region Public Methods

		#region ICommand Members

		/// <summary>
		/// 执行方法
		/// </summary>
		/// <param name="notification"></param>
		public virtual void Execute(INotification notification)
		{
		}

		#endregion

		#endregion
	}
}
