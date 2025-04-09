using FluentValidation.Results;
using System.Net;
using System.Text.Json.Serialization;

namespace DeveloperStore.App.Models
{
    public interface IResult
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        bool Success { get; }

        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public int PageSize { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        HttpStatusCode StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        IReadOnlyList<string> Erros { get; }

        void AddMessage(IList<ValidationFailure> failures);
        
        void AddMessage(string failure);
        
        IList<string> GetMessages();
    }

    public interface IResultResponse : IResult
    {

    }

    public interface IResultResponse<T> : IResult
    {
        T Data { get; set; }
    }
}

