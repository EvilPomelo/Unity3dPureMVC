using PureMVC.Patterns;
using PureMVCHelloWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationFacade : Facade {
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="goRootNode"></param>
    public ApplicationFacade(GameObject goRootNode)
    {
        //控制层注册
        RegisterCommand("Reg_StartDataCommand", typeof(DataCommand));
        //视图层注册
        RegisterMediator(new DataMediator(goRootNode));
        //模型层注册
        RegisterProxy(new DataProxy());
    }
}
