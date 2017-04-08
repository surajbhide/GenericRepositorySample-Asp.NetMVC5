using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF.Core;

namespace EF.Data
{
    public class Repository<T> where T : BaseEntity
    {
        private readonly EFDbContext context;
        private IDbSet<T> entities;
        string errorMessage = String.Empty;

        public Repository(EFDbContext con) => context = con;

        private IDbSet<T> Entities
        {
            get
            {
                if (entities == null)
                    entities = context.Set<T>();
                return entities;
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public T GetById(object id) => this.Entities.Find(id);

        public void Insert(T entity)
        {
            this.Entities.Add(entity);
        }

        public void Update(T entity)
        {
            Entities.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            Entities.Remove(entity);
        }

    }
}
