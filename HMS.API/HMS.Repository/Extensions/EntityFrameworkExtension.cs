using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.Repository.Extensions
{
    public static class EntityFrameworkExtension
    {
        public static TEntity FindTracked<TEntity>(this DbContext context, params object[] keyValues)
            where TEntity : class
        {
            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var key = entityType.FindPrimaryKey();
            var stateManager = context.GetDependencies().StateManager;
            var entry = stateManager.TryGetEntry(key, keyValues);
            return entry?.Entity as TEntity;
        }
    }
}
