/***
 * 
 *    Title: "SUIFW" UI框架项目
 *           主题： 游戏开始
 *    Description: 
 *           功能： 游戏项目的入口
 *                  
 *    Date: 2017
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PureMVCDemo
{
	public class StartGame : MonoBehaviour {


		void Start ()
		{
            //启动PureMVC框架。
            //this.gameobejct 表示 UI 界面的根节点。
		    new ApplicationFacade(this.gameObject);
		}
		
	}
}