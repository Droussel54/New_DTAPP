//using System.Data.Entity;
//using New_DTAPP.Data;
//using Microsoft.EntityFrameworkCore;
//using New_DTAPP.Models;
//using New_DTAPP.Repository.Interfaces;
//using System.Security.Cryptography;

//namespace New_DTAPP.Repository
//{
//    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntityId> 
//    {
//        protected readonly NewDtappContext _context;

//        protected Repository(NewDtappContext dbContext)
//        {
//            _context = new NewDtappContext();
//        }
 
//        public virtual bool Exist(int id)
//        {
//            return _context.Set<TEntity>().SingleOrDefaultAsync(p => id == id) != null;
//        }
//    }
//}
