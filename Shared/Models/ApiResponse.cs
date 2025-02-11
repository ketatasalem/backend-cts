namespace Labo_Cts_backend.Shared.Models
{
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indicates if the request was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The main data returned by the API.
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// A human-readable message describing the result.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// A list of validation or processing errors.
        /// </summary>
        public List<string>? Errors { get; set; }

        /// <summary>
        /// Optional status code for finer control.
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// Default constructor for success responses.
        /// </summary>
        /// <param name="data">The data to return.</param>
        /// <param name="message">A success message.</param>
        public ApiResponse(T data, string message = "")
        {
            Success = true;
            Data = data;
            Message = message;
            Errors = null;
        }

        /// <summary>
        /// Constructor for error responses.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="errors">List of error details.</param>
        public ApiResponse(string message, List<string>? errors = null)
        {
            Success = false;
            Data = default;
            Message = message;
            Errors = errors ?? new List<string>();
        }

        /// <summary>
        /// Empty constructor for serialization/deserialization.
        /// </summary>
        public ApiResponse() { }
    }
}
