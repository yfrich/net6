using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4_4;

internal record Student(int Id, string Name)
{
    public string? NickName { get; set; }
    /// <summary>
    /// 指定额外的构造函数
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="NickName"></param>
    public Student(int Id, string Name, string NickName) : this(Id, Name)
    {
        this.NickName = NickName;
    }
}
