using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MVCCoreApp.Models;
using Login = MVCCoreApp.Models.Login;

namespace MVCCoreApp.Repositories
{
    public class DriverRepository : IRepository<Driver>
    {
        private readonly JourneyDbContext context;
        public DriverRepository(JourneyDbContext context)
        {
            this.context = context;
        }

        public bool ValidateCredentials(Login login)
        {
            bool result = false;
            DbSet<Login> logins = context.Logins;
            Login vlogin = logins.Where(l => l.UserId == login.UserId && l.Password == login.Password && l.UserType == login.UserType).FirstOrDefault();
            if (vlogin != null)
            {
                result = true;
            }
            return result;
        }
        public bool SetCredentials(Login login)
        {
            DbSet<Login> logins = context.Logins;
            logins.Add(login);
            context.SaveChanges();
            return true;
        }
        public bool Add(Driver entity)
        {
            try
            {
                DbSet<Driver> drivers = context.Drivers;
                drivers.Add(entity);
                int r = context.SaveChanges();
                if (r > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(Driver entity)
        {
            throw new NotImplementedException();
        }

        public List<Driver> GetAll()
        {
            throw new NotImplementedException();
        }

        public Driver Search(object id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Driver entity)
        {
            throw new NotImplementedException();
        }
    }
}