using FluentValidation;
using Labo_Cts_backend.Shared.Models;

namespace Labo_Cts_backend.Shared.IServices
{
    public interface ICommonService
    {
        Task<T> UpdateTimeAndUserForUpdateFields<T>(T entity, string user, bool isCreation) where T : class;
        DateTime GetLocalTime(string timeZoneId);
        ApiResponse<T> ValidateEntity<T>(T entity, IValidator<T> validator);
        Task<ApiResponse<T>> ExecuteSafely<T>(Func<Task<ApiResponse<T>>> operation);
        bool IsValidCode<T>(object identifiant, string fieldName, out ApiResponse<T> response);
    }
}
