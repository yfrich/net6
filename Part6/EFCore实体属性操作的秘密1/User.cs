using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Zack.Commons;

namespace EFCore实体属性操作的秘密1
{
    public class User
    {
        public long Id { get; init; }//特征一
        public DateTime CreateDateTime { get; init; }//特征一
        public string UserName { get; private set; }//特征一
        public int Credit { get; private set; }//特征一
        private string? passwordHash;//特征三
        private string? remark;
        public string? Remark
        {
            get
            {
                return this.remark;
            }
        }//特征四
        public string? Tag { get; set; }//特征五
        //给程序员用的
        public User(string yhm)//特征二
        {
            this.UserName = yhm;
            this.CreateDateTime = DateTime.Now;
            this.Credit = 10;
        }
        //给EFCore 从数据库中加载数据然后生成User
        private User()//特征二
        {

        }
        public void ChangeUserName(string newValue)
        {
            this.UserName = newValue;
        }
        public void ChangePasswordHash(string newValue)
        {
            if (newValue.Length < 6)
            {
                throw new ArgumentException("密码太短");
            }
            this.passwordHash = HashHelper.ComputeMd5Hash(newValue);
        }
    }
}
