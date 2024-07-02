using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore一对多
{
    public class Article
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();//类还是要定义成一个属性而不是变量 建议给一个空的List
    }
}
