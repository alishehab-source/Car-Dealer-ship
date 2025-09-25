using CarDealershipAPI.Data;
using CarDealershipAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CarDealershipAPI.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CarDealershipContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly ILogger<Repository<T>> _logger;

        public Repository(CarDealershipContext context, ILogger<Repository<T>> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET Operations
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب البيانات برقم {Id}", id);
                throw new Exception($"خطأ في جلب البيانات برقم {id}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب جميع البيانات");
                throw new Exception("خطأ في جلب جميع البيانات", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب البيانات بالشرط المحدد");
                throw new Exception("خطأ في جلب البيانات بالشرط المحدد", ex);
            }
        }

        public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب أول عنصر بالشرط المحدد");
                throw new Exception("خطأ في جلب أول عنصر بالشرط المحدد", ex);
            }
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في فحص وجود البيانات");
                throw new Exception("خطأ في فحص وجود البيانات", ex);
            }
        }

        public virtual async Task<int> CountAsync()
        {
            try
            {
                return await _dbSet.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في عد البيانات");
                throw new Exception("خطأ في عد البيانات", ex);
            }
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.CountAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في عد البيانات بالشرط المحدد");
                throw new Exception("خطأ في عد البيانات بالشرط المحدد", ex);
            }
        }

        // GET with Include
        public virtual async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet;

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.FirstOrDefaultAsync(entity => EF.Property<int>(entity, "Id") == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب البيانات مع التفاصيل برقم {Id}", id);
                throw new Exception($"خطأ في جلب البيانات مع التفاصيل برقم {id}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet;

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب جميع البيانات مع التفاصيل");
                throw new Exception("خطأ في جلب جميع البيانات مع التفاصيل", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet;

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب البيانات مع التفاصيل والشرط المحدد");
                throw new Exception("خطأ في جلب البيانات مع التفاصيل والشرط المحدد", ex);
            }
        }

        // Paging
        public virtual async Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 10;

                return await _dbSet
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب البيانات مع التقسيم");
                throw new Exception("خطأ في جلب البيانات مع التقسيم", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 10;

                return await _dbSet
                    .Where(predicate)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب البيانات مع التقسيم والشرط");
                throw new Exception("خطأ في جلب البيانات مع التقسيم والشرط", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetPagedAsync<TKey>(int pageNumber, int pageSize, Expression<Func<T, TKey>> orderBy, bool ascending = true)
        {
            try
            {
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 10;

                IQueryable<T> query = ascending ? _dbSet.OrderBy(orderBy) : _dbSet.OrderByDescending(orderBy);

                return await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب البيانات مع التقسيم والترتيب");
                throw new Exception("خطأ في جلب البيانات مع التقسيم والترتيب", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetPagedAsync<TKey>(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, bool ascending = true)
        {
            try
            {
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 10;

                IQueryable<T> query = _dbSet.Where(predicate);
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

                return await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب البيانات مع التقسيم والشرط والترتيب");
                throw new Exception("خطأ في جلب البيانات مع التقسيم والشرط والترتيب", ex);
            }
        }

        // CREATE Operations
        public virtual async Task<T> AddAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "البيانات المرسلة فارغة");

                await _dbSet.AddAsync(entity);
                _logger.LogInformation("تم إضافة عنصر جديد من نوع {EntityType}", typeof(T).Name);
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في إضافة البيانات");
                throw new Exception("خطأ في إضافة البيانات", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null || !entities.Any())
                    throw new ArgumentNullException(nameof(entities), "البيانات المرسلة فارغة");

                await _dbSet.AddRangeAsync(entities);
                _logger.LogInformation("تم إضافة {Count} عنصر من نوع {EntityType}", entities.Count(), typeof(T).Name);
                return entities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في إضافة مجموعة البيانات");
                throw new Exception("خطأ في إضافة مجموعة البيانات", ex);
            }
        }

        // UPDATE Operations
        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "البيانات المرسلة فارغة");

                _dbSet.Update(entity);
                _logger.LogInformation("تم تحديث عنصر من نوع {EntityType}", typeof(T).Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في تحديث البيانات");
                throw new Exception("خطأ في تحديث البيانات", ex);
            }
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null || !entities.Any())
                    throw new ArgumentNullException(nameof(entities), "البيانات المرسلة فارغة");

                _dbSet.UpdateRange(entities);
                _logger.LogInformation("تم تحديث {Count} عنصر من نوع {EntityType}", entities.Count(), typeof(T).Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في تحديث مجموعة البيانات");
                throw new Exception("خطأ في تحديث مجموعة البيانات", ex);
            }
        }

        // DELETE Operations
        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "البيانات المرسلة فارغة");

                _dbSet.Remove(entity);
                _logger.LogInformation("تم حذف عنصر من نوع {EntityType}", typeof(T).Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في حذف البيانات");
                throw new Exception("خطأ في حذف البيانات", ex);
            }
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null || !entities.Any())
                    throw new ArgumentNullException(nameof(entities), "البيانات المرسلة فارغة");

                _dbSet.RemoveRange(entities);
                _logger.LogInformation("تم حذف {Count} عنصر من نوع {EntityType}", entities.Count(), typeof(T).Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في حذف مجموعة البيانات");
                throw new Exception("خطأ في حذف مجموعة البيانات", ex);
            }
        }

        public virtual async Task DeleteByIdAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    Delete(entity);
                    _logger.LogInformation("تم حذف عنصر برقم {Id} من نوع {EntityType}", id, typeof(T).Name);
                }
                else
                {
                    _logger.LogWarning("لم يتم العثور على عنصر برقم {Id} من نوع {EntityType}", id, typeof(T).Name);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في حذف البيانات برقم {Id}", id);
                throw new Exception($"خطأ في حذف البيانات برقم {id}", ex);
            }
        }

        // SAVE Changes
        public virtual async Task<int> SaveChangesAsync()
        {
            try
            {
                var result = await _context.SaveChangesAsync();
                _logger.LogInformation("تم حفظ {Count} تغيير في قاعدة البيانات", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في حفظ البيانات");
                throw new Exception("خطأ في حفظ البيانات", ex);
            }
        }
    }

}
