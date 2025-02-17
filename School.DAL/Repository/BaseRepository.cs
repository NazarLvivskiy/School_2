﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using School.DAL.Entities;

namespace School.DAL.Repository
{
    public class BaseRepository<T>:IRepository<T> where T : EntityBase, new()
    {
        private DbSet<T> table;

        private SchoolDbContext db;

        protected SchoolDbContext DbContext => db;
        public BaseRepository(SchoolDbContext db)
        {
            this.db = db;
            this.table = db.Set<T>();
        }
        
        
        public int Add(T entity)
        {
            table.Add(entity);
            return SaveChanges();
        }

        internal int SaveChanges()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (RetryLimitExceededException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public int Add(IList<T> entities)
        {
            table.AddRange(entities);
            return SaveChanges();
        }

        public int Update(T entity)
        {
            // db.Entry(entity).State = EntityState.Detached;
            table.Update(entity);
            
            return SaveChanges();
        }

        public  int Update(IList<T> entities)
        {
            table.UpdateRange(entities);
            return SaveChanges();
        }

        public int Delete(int id)
        {
            Delete(table.FirstOrDefault(t=>t.Id==id));

            return SaveChanges();
        }

        public int Delete(T entity)
        {
            db.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public T GetOne(int? id) => table.Find(id);

        public virtual IQueryable<T> GetSome(Expression<Func<T, bool>> @where)
            => table.Where(where);

        public virtual List<T> GetAll() => table.ToList();

        public virtual List<T> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy, bool @ascending)
            => (@ascending ? table.OrderBy(orderBy) : table.OrderByDescending(orderBy)).ToList();

        public List<T> ExecuteQuery(string sql) => table.FromSqlRaw(sql).ToList();

        public List<T> ExecuteQuery(string sql, object[] sqlParametersObjects) =>
            table.FromSqlRaw(sql, sqlParametersObjects).ToList();

        public void Dispose()
        {
            db.Dispose();
        }
    }
}