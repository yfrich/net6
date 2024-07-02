using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigServices
{
    public interface IConfigReader
    {
        /// <summary>
        /// 如果接口找不到就返回NULL
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetValue(string name);
    }
}
