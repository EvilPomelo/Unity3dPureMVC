using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PureMVCHelloWorld
{
    public class MyData
    {
        private int _Level = 0;
        /// <summary>
        /// 等级
        /// </summary>
        public int Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

    }
}