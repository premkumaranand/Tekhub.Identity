using System.Collections.Generic;

namespace Tekhub.Identity.Web.Common.App
{
    public interface ICollectionSessionManager<TEntity>
    {
        void Add(TEntity entity);
        void AddRange(List<TEntity> entity);
        bool Contains(TEntity entity);
        IList<TEntity> GetAll();
        void Clear();
    }
}
