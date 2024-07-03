using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EFCore实体属性操作的秘密1
{
    internal class Person
    {
        public long Id { get; init; }
        public string Name { get; private set; }
        public void ChangeName(string name)
        {
            this.Name = name;
        }
        //private string name;
        //public string Name
        //{
        //    get
        //    {
        //        Console.WriteLine("get被调用");
        //        return this.name;
        //    }
        //    set
        //    {
        //        Console.WriteLine("set被调用");
        //        this.name = value;
        //    }
        //}
        /*
        private string xingming;
        public string Name
        {
            get
            {
                Console.WriteLine("get被调用");
                return this.xingming;
            }
            set
            {
                Console.WriteLine("set被调用");
                this.xingming = value;
            }
        }
        */
    }
}
