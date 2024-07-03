using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Entities;
using Users.Domain.ValueObjects;

namespace Users.Domain
{
    public class UserDomainService
    {
        private readonly IUserRepository userRepository;
        private readonly ISmsCodeSender smsSender;

        public UserDomainService(IUserRepository userRepository, ISmsCodeSender smsSender)
        {
            this.userRepository = userRepository;
            this.smsSender = smsSender;
        }
        //聚合根是管理聚合里面所有的对象，可以复杂可以简单
        public void ResetAccessFail(User user)
        {
            user.UserAccessFail.Reset();
        }
        public bool IsLockOut(User user)
        {
            return user.UserAccessFail.IsLockOut();
        }
        public void AccessFail(User user)
        {
            user.UserAccessFail.Fail();
        }
        //校验密码
        public async Task<UserAccessResult> CheckPassword(PhoneNumber phoneNumber, string password)
        {
            UserAccessResult result;
            var user = await userRepository.FindOneAsync(phoneNumber);
            if (user == null)
            {
                result = UserAccessResult.PhoneNumberNotFound;
            }
            else if (IsLockOut(user))
            {
                result = UserAccessResult.Lockout;
            }
            else if (user.HasPassword() == false)
            {
                result = UserAccessResult.NoPassword;
            }
            else if (user.CheckPassword(password))
            {
                result = UserAccessResult.OK;
            }
            else
            {
                result = UserAccessResult.PasswordError;
            }
            if (user != null)
            {
                if (result == UserAccessResult.OK)
                {
                    ResetAccessFail(user);
                }
                else
                {
                    AccessFail(user);
                }
            }
            await userRepository.PublishEventAsync(new UserAccessResultEvent(phoneNumber, result));
            return result;

        }
        //校验手机验证码是否正确
        public async Task<CheckCodeResult> CheckPhoneCodeAsync(PhoneNumber phoneNumber, string code)
        {
            User? user = await userRepository.FindOneAsync(phoneNumber);
            if (user == null)
            {
                return CheckCodeResult.PhoneNumberNotFound;
            }
            else if (!IsLockOut(user))
            {
                return CheckCodeResult.Lockout;
            }

            string? codeInServer = await userRepository.FindPhoneNumberCodeAsync(phoneNumber);
            if (codeInServer == null)
            {
                return CheckCodeResult.CodeError;
            }
            if (codeInServer == code)
            {
                return CheckCodeResult.OK;
            }
            else
            {
                AccessFail(user);
                return CheckCodeResult.CodeError;
            }

        }
    }
}
