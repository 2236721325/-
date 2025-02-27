﻿using Shared.Commons;
using System.Linq.Expressions;

namespace Shared.BaseServices
{
    public interface IBaseService<T> where T : class, new()
    {
        Task<int> AddAsync(T entity);
        Task<int> AddRangeAsync(IEnumerable<T> entities);
        Task<int> RemoveAsync(int id);
        Task<int> RemoveAsync(T entity);
        Task<int> RemoveRangeAsync(IEnumerable<T> entities);
        Task<int> UpdateAsync(T entity);
        Task<int> UpdateRangeAsync(IEnumerable<T> entities);
        ValueTask<T> FindAsync(int id);
        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<T>> Query(bool disableTracking = true);
        /// <summary>
        /// 自定义条件查询
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<IQueryable<T>> Query(Expression<Func<T, bool>> func, bool disableTracking = true);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<IQueryable<T>> Query(int pageIndex, int size, RefAsync<int> total, bool disableTracking = true);
        /// <summary>
        /// 自定义分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="size"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<IQueryable<T>> Query(Expression<Func<T, bool>> func, int pageIndex, int size, RefAsync<int> total, bool disableTracking = true);

    }
}
