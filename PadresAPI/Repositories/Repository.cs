using Microsoft.EntityFrameworkCore;
using PadresAPI.Models;

namespace PadresAPI.Repositories
{
    public class Repository<T> where T : class
    {
        public Sistem21PrimariaContext context { get; }

        public Repository(Sistem21PrimariaContext context)
        {
            this.context = context;
        }

        public DbSet<T> Get()
        {
            return context.Set<T>();
        }

        public T? Get(object id)
        {
            return context.Find<T>(id);
        }

        public void Insert(T entidad)
        {
            context.Add(entidad);
            context.SaveChanges();
        }

        public void Update(T entidad)
        {
            context.Update(entidad);
            context.SaveChanges();
        }

        public void Delte(T entidad)
        {
            context.Remove(entidad);
            context.SaveChanges();
        }
    }
}
