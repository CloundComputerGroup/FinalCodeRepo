using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.EntityRepository
{
    public abstract class EntityRepositoryBase<T> where T : class
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDbSet<T> dbset;

        protected EntityRepositoryBase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.dbset = DataContext.Set<T>();
        }

        protected IBankContext DataContext
        {
            get { return unitOfWork.Context; }
        }

        protected void Reload(T entity)
        {
            ((DbContext)DataContext).Entry(entity).Reload();
        }

        public virtual T Add(T entity)
        {
            return dbset.Add(entity);
        }
        public virtual void Update(T entity)
        {
            ((DbContext)unitOfWork.Context).Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void SetModified(object entity)
        {

            if (entity != null)
                ((DbContext)unitOfWork.Context).Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void SetModified(object entity, params string[] properties)
        {
            var entry = ((DbContext)unitOfWork.Context).Entry(entity);
            entry.State = System.Data.Entity.EntityState.Unchanged;
            foreach (var property in properties)
            {
                entry.Property(property).IsModified = true;
            }
            ((DbContext)unitOfWork.Context).Configuration.ValidateOnSaveEnabled = false;

        }

        public void SetUnChanged(object entity)
        {
            if (entity != null)
                ((DbContext)unitOfWork.Context).Entry(entity).State = System.Data.Entity.EntityState.Unchanged;
        }

        public void SetDetached(object entity)
        {
            if (entity != null)
                ((DbContext)unitOfWork.Context).Entry(entity).State = System.Data.Entity.EntityState.Detached;
        }

        public void SetAdded(object entity)
        {
            if (entity != null)
                ((DbContext)unitOfWork.Context).Entry(entity).State = System.Data.Entity.EntityState.Added;
        }

        public void SetDeleted(object entity)
        {
            if (entity != null)
                ((DbContext)unitOfWork.Context).Entry(entity).State = System.Data.Entity.EntityState.Deleted;
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }
        public void Delete(Func<T, Boolean> where)
        {
            IEnumerable<T> objects = dbset.Where(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }
        public virtual T GetById(long id)
        {
            return dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }
        public virtual IEnumerable<T> GetMany(Func<T, bool> where)
        {
            return dbset.Where(where).ToList();
        }
        public T Get(Func<T, Boolean> where)
        {
            return dbset.Where(where).FirstOrDefault();
        }
    }
}
