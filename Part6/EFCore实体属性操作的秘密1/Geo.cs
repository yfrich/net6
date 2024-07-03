using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore实体属性操作的秘密1
{
    /// <summary>
    /// 值对象类型
    /// </summary>
    internal record Geo
    {
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        //领域内的处理 也可以用无参构造，次构造方法名对应属性对不上的情况下
        public Geo(double latitude, double longitude)
        {
            if (longitude < -180 || longitude > 180)
            {
                throw new ArgumentException("longitdue invalid");
            }
            if (latitude < -90 || latitude > 90)
            {
                throw new ArgumentException("latitude invalid");
            }
            this.Longitude = longitude;
            this.Latitude = latitude;
        }
    }
}
