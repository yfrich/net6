using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore一对多
{
    public class Comment
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public Article TheArticle { get; set; }
        public long TheArticleId { get; set; }
    }
}
