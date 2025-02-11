using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Context;
using Labo_Cts_backend.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Labo_Cts_backend.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LaboratoireCtsContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(LaboratoireCtsContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<PagedResult<T>> GetPagedAsync(FilterQuery filter)
        {
            var query = _dbSet.AsQueryable();

            // Appliquer les filtres dynamiquement
            foreach (var filterItem in filter.Filters)
            {
                var columnName = NormalizePropertyName(filterItem.Key); // Normalisation du nom
                var filterValue = filterItem.Value;

                var property = typeof(T).GetProperty(columnName);
                if (property != null)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        // Filtre LIKE pour les chaînes de caractères
                        query = query.Where(x => EF.Functions.Like(EF.Property<string>(x, columnName), $"%{filterValue}%"));
                    }
                    else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
                    {
                        // Filtre direct pour les entiers
                        if (int.TryParse(filterValue, out int intValue))
                        {
                            query = query.Where(x => EF.Property<int>(x, columnName) == intValue);
                        }
                    }
                    else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
                    {
                        // Filtre direct pour les décimaux
                        if (decimal.TryParse(filterValue, out decimal decimalValue))
                        {
                            query = query.Where(x => EF.Property<decimal>(x, columnName) == decimalValue);
                        }
                    }
                    else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                    {
                        // Filtre direct pour les dates
                        if (DateTime.TryParse(filterValue, out DateTime dateValue))
                        {
                            query = query.Where(x => EF.Property<DateTime>(x, columnName) == dateValue);
                        }
                    }
                    else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                    {
                        // Filtre direct pour les booléens
                        if (bool.TryParse(filterValue, out bool boolValue))
                        {
                            query = query.Where(x => EF.Property<bool>(x, columnName) == boolValue);
                        }
                    }
                    // Ajoutez d'autres types au besoin (double, float, etc.)
                }
            }

            // Pagination
            var totalRecords = await query.CountAsync();
            var pagedData = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = pagedData,
                TotalCount = totalRecords,
                PageSize = filter.PageSize,
                CurrentPage = filter.PageNumber
            };
        }

        private string NormalizePropertyName(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) return propertyName;

            return char.ToUpper(propertyName[0]) + propertyName.Substring(1); // Majuscule sur la 1ère lettre
        }
    }

}
