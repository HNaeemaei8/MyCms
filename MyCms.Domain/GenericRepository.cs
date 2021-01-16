using Microsoft.EntityFrameworkCore;
using MyCms.Domain.Context;
using MyCms.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyCms.Domain
{
    class GenericRepository<TEntity> where TEntity:class
    {
        private MyCmsContext _ctx;
        private DbSet<TEntity> _dbSet;
        public GenericRepository(MyCmsContext ctx)
        {
            ctx = _ctx;
            _dbSet = _ctx.Set<TEntity>();
        }
        public TEntity Get(object id)
        {
            return _dbSet.Find(id);
        }
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby =null,
            string includes="") 
        {
            IQueryable<TEntity> query = _dbSet;
            if (where !=null)
            {
                query = query.Where(where);
            }
            if (orderby != null)
            {
                query = orderby(query);
            }
            foreach (string s in includes.Split(','))
            {
                query = query.Include(s);
            }
            return query;
          // return _dbSet.ToList();
        }
    }

    public class My
    {
      //  GenericRepository<User> users = new GenericRepository<User>();
    }
}
