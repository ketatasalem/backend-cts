using FluentValidation;
using Labo_Cts_backend.Shared.IServices;
using Labo_Cts_backend.Shared.Models;

namespace Labo_Cts_backend.Shared.Services
{
    public class CommonService() : ICommonService
    {
        public async Task<T> UpdateTimeAndUserForUpdateFields<T>(T entity, string user, bool isCreation) where T : class
        {
            // Déterminer les noms des propriétés selon le contexte (création ou mise à jour)
            string datePropertyName = isCreation ? "CreDat" : "UpdDat";
            string userPropertyName = isCreation ? "CreUsr" : "UpdUsr";

            var dateProperty = typeof(T).GetProperty(datePropertyName);
            var userProperty = typeof(T).GetProperty(userPropertyName);

            if (dateProperty != null &&
                (dateProperty.PropertyType == typeof(DateTime) || dateProperty.PropertyType == typeof(DateTime?)))
            {
                dateProperty.SetValue(entity, GetLocalTime("Africa/Tunis"));
            }

            if (userProperty != null && userProperty.PropertyType == typeof(string))
            {
                userProperty.SetValue(entity, user);
            }

            await Task.CompletedTask;
            return entity;
        }


        public DateTime GetLocalTime(string timeZoneId)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        }

        public ApiResponse<T> ValidateEntity<T>(T entity, IValidator<T> validator)
        {
            var validationResult = validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                return new ApiResponse<T>
                {
                    Success = false,
                    Message = "La validation a échoué",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            return new ApiResponse<T> { Success = true };
        }

        public async Task<ApiResponse<T>> ExecuteSafely<T>(Func<Task<ApiResponse<T>>> operation)
        {
            try
            {
                return await operation();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Exception: {ex.Message}");
                System.Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");

                return new ApiResponse<T>
                {
                    Success = false,
                    Message = $"Une exception s'est produite: {ex.Message}",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public bool IsValidCode<T>(object identifiant, string fieldName, out ApiResponse<T> response)
        {
            if (identifiant is int intId && intId <= 0)
            {
                response = new ApiResponse<T>
                {
                    Success = false,
                    Message = $"{fieldName} est invalide. Il doit être un entier positif.",
                    StatusCode = StatusCodes.Status400BadRequest
                };
                return false;
            }

            if (identifiant is string strId && string.IsNullOrEmpty(strId))
            {
                response = new ApiResponse<T>
                {
                    Success = false,
                    Message = $"{fieldName} est requis et ne peut pas être vide.",
                    StatusCode = StatusCodes.Status400BadRequest
                };
                return false;
            }

            response = null;
            return true;
        }

    }
}
