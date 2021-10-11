using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;//IHttpContextAccessor burdan geliyor
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection; //24.satırdaki getservice burdan gelir.
using Core.Extensions;
using Business.Constants;

namespace Business.BusinessAspects.Autofac
{
    //JWT için
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor; 
        //her istek yapan kişi için bir httpcontext oluşur, herkese bir thread oluşur. bu accessor da bir interface olarak geliyor

        public SecuredOperation(string roles) //rolleri istiyoruz
        {
            //rollerimiz virgülle geliyor attribute olduğu için
            _roles = roles.Split(',');//split: stringi virgüle göre ayırıp array e atıyor (managerda virgülle verdiğimiz yer)
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))//claimlerinin içinde ilgili rol varsa
                {
                    return; //return et yani methodu çalıştırmaya devam et
                }
            }
            throw new Exception(Messages.AuthorizationDenied); //yetki yok hatası ver
        }
    }
}
