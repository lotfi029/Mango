using Mango.Web.Abstracts;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace Mango.Web.Service;

public class BaseService(IHttpClientFactory _httpClientFactory) : IBaseService
{
    public async Task<Result<T>> SendAsync<T>(Request request)
    {

        HttpClient client = _httpClientFactory.CreateClient();
        using var message = BuildHttpRequestMessage(request);

        try
        {
            using var response = await client.SendAsync(message);
            var responseContent = await response.Content.ReadAsStringAsync();
            

            var returnedResult = response.StatusCode switch
            {
                HttpStatusCode.OK or HttpStatusCode.Created or HttpStatusCode.NoContent =>
                    Result.Success(JsonConvert.DeserializeObject<T>(responseContent)!),
                _ => Result.Fail<T>(ParseError(responseContent, response.StatusCode))
            };

            return returnedResult;
        }
        catch (Exception ex)
        {
            return Result.Fail<T>(Error.FromException(ex));
        }
    }
    public async Task<Result> SendAsync(Request request)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        using var message = BuildHttpRequestMessage(request);

        try
        {
            using var response = await client.SendAsync(message);
            var responseContent = await response.Content.ReadAsStringAsync();

            return response.StatusCode switch
            {
                HttpStatusCode.OK or HttpStatusCode.Created or HttpStatusCode.NoContent =>
                    Result.Success(),
                _ => Result.Fail(ParseError(responseContent, response.StatusCode))
            };
        }
        catch (Exception ex)
        {
            return Result.Fail(Error.FromException(ex));
        }
    }

    private HttpRequestMessage BuildHttpRequestMessage(Request request)
    {
        var message = new HttpRequestMessage
        {
            RequestUri = new Uri(request.Url),
            Method = (sbyte)request.ApiType switch
            {
                1 => HttpMethod.Post,
                2 => HttpMethod.Put,
                3 => HttpMethod.Delete,
                _ => HttpMethod.Get
            }
        };

        message.Headers.Add("accept", "application/json");

        if (!string.IsNullOrWhiteSpace(request.AccessToken))
        {
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", request.AccessToken);
        }

        if (request.Data is not null)
        {
            message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
        }

        return message;
    }

    private static Error ParseError(string content, HttpStatusCode statusCode)
    {
        try
        {
            var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(content);

            return new(problemDetails?.Title ?? "Error", problemDetails?.Detail ?? "an error eccure", statusCode);

        }
        catch
        {
            return new("Error", "an error eccure", statusCode);
        }
    }
}




//public class BaseService(IHttpClientFactory _httpClientFactory) : IBaseService
//{
//    public async Task<Result> SendAsync<T>(Request<T> request)
//    {
//        HttpClient client = _httpClientFactory.CreateClient();
//        HttpRequestMessage message = new();
//        message.Headers.Add("accept", "application/json");

//        //response.Headers.Add("Authorization", $"Bearer {request.AccessToken}");

//        message.RequestUri = new Uri(request.Url);
//        if (request.Data is not null)
//        {
//            message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
//        }

//        HttpResponseMessage response = new();
//        try
//        {
//            response = (sbyte)request.ApiType switch
//            {
//                1 => await client.PostAsync(request.Url, message.Content),
//                2 => await client.PutAsync(request.Url, message.Content),
//                3 => await client.DeleteAsync(request.Url),
//                _ => await client.GetAsync(request.Url),
//            };

//            string responseContent = await response.Content.ReadAsStringAsync();
//            var error = JsonConvert.DeserializeObject<Error>(responseContent);

//            var returnedResult = response.StatusCode switch
//            {
//                HttpStatusCode.OK => Result.Success(),
//                HttpStatusCode.Created => Result.Success(),
//                HttpStatusCode.NoContent => Result.Success(),
//                HttpStatusCode.BadRequest => Result.Fail(error ?? Error.BadRequest("", "")),
//                HttpStatusCode.Unauthorized => Result.Fail(error ?? Error.Unauthorized("", "")),
//                HttpStatusCode.Forbidden => Result.Fail(error ?? Error.Forbidden("", "")),
//                HttpStatusCode.NotFound => Result.Fail(error ?? Error.NotFound("", "")),
//                _ => Result.Fail(error ?? Error.InternalServerError("", ""))
//            };

//            return returnedResult;
//        }
//        catch (Exception ex)
//        {
//            return Result.Fail(Error.FromException(ex));
//        }
//    }
//}