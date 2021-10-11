using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new NorthwindContext())
            {
                var result = from operationClaim in context.OperationClaims //operation claimler ile
                             join userOperationClaim in context.UserOperationClaims//user operation claimlere join atıyor
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id //onların içinde Id si bizim gönderdiğimiz user a eşit olanı buluyor
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name }; // ve bunları return ediyoruz
                return result.ToList();

            }
        }
    }
}
