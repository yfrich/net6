using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore实体属性操作的秘密1
{
    /// <summary>
    /// 枚举类型如何存储到数据库 默认存储int类型，可以设定修改为string类型
    /// </summary>
    internal class ValueObject1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CurrencyName Currency { get; set; }
    }
    enum CurrencyName
    {
        CNY, USD, NZD
    }
}
