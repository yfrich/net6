using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBooks;

public class Book
{
    public long Id { get; set; }
    public string Titile { get; set; }
    public string AuthorName { get; set; }

    public double Price { get; set; }
    public DateTime PubDate { get; set; }

    public string TestMoreDB { get; set; }

}
