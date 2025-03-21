using FluentValidation.Results;
using System.Net;
using System.Text.Json.Serialization;

namespace DeveloperStore.Domain.Services.Models
{
    public abstract class Result : IResult
    {
        [JsonIgnore]
        public bool Success { get { return GetSucess(); } }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        [JsonIgnore]
        public int PageSize { get; set; }
             
        public IReadOnlyList<string> Erros => Messages;

        private List<string> Messages { get; set; }

        public Result()
        {
            this.Messages = [];
        }

        public void AddMessage(IList<ValidationFailure> failures)
        {
            foreach (var item in failures)
            {
                this.Messages.Add(item.ErrorMessage);
            }
        }

        public void AddMessage(string failure)
        {
            this.Messages.Add(failure);
        }

        public IList<string> GetMessages()
        {
            return this.Messages;
        }

        private bool GetSucess()
        {
            if (this.Messages?.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class ResultResponse : Result, IResultResponse
    {
        public ResultResponse(string Message)
        {
            this.AddMessage(Message);
        }
        public ResultResponse()
        {

        }
    }

    public class ResultResponse<T>(T value) : Result, IResultResponse<T>
    {
        public ResultResponse() : this(default)
        {            
        }

        public T Data { get; set; } = value;
    }
}
