
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VShow_BackEnd.Services.Models;

namespace VShow_BackEnd.Library.Concretes
{
    /// <summary>
    /// ส่วนของการส่งข้อมูลไปหา Server อื่นๆในรูปแบบ Resful API [GET, POST, PUT, DELETE]
    /// </summary>
    public class HttpApiService
    {
        private Dictionary<string, string> Headers = new Dictionary<string, string>();

        public void Authorization(string authorization)
        {
            Headers.Add("Authorization", authorization);
        }

        public async Task<HttpResponse<string>> Get(string url)
        {
            return await Send(url, HttpMethod.Get, null);
        }

        public async Task<HttpResponse<T>> Get<T>(string url)
        {
            return await Send<T>(url, HttpMethod.Get, null);
        }

        public async Task<HttpResponse<string>> Post(string url, object data)
        {
            return await Send(url, HttpMethod.Post, data);
        }

        public async Task<HttpResponse<T>> Post<T>(string url, object data)
        {
            return await Send<T>(url, HttpMethod.Post, data);
        }

        public async Task<HttpResponse<string>> Put(string url, object data)
        {
            return await Send(url, HttpMethod.Put, data);
        }

        public async Task<HttpResponse<T>> Put<T>(string url, object data)
        {
            return await Send<T>(url, HttpMethod.Put, data);
        }

        public async Task<HttpResponse<string>> Delete(string url)
        {
            return await Send(url, HttpMethod.Delete, null);
        }

        public async Task<HttpResponse<T>> Delete<T>(string url)
        {
            return await Send<T>(url, HttpMethod.Delete, null);
        }

        public async Task<HttpResponse<string>> PostFile(string url, List<IFormFile> files)
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post
            };
            var formDataContent = new MultipartFormDataContent();
            foreach (var file in files)
            {
                var content = new StreamContent(file.OpenReadStream());
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                formDataContent.Add(content, file.Name, file.FileName);
            }
            httpRequest.Content = formDataContent;
            var httpResponse = await Send(httpRequest);
            return httpResponse;
        }

        public async Task<HttpResponse<T>> PostFile<T>(string url, List<IFormFile> files)
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post
            };
            var formDataContent = new MultipartFormDataContent();
            foreach (var file in files)
            {
                var content = new StreamContent(file.OpenReadStream());
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                formDataContent.Add(content, file.Name, file.FileName);
            }
            httpRequest.Content = formDataContent;

            foreach (var header in Headers) httpRequest.Headers.Add(header.Key, header.Value);
            var httpResponse = await Send<T>(httpRequest);
            return httpResponse;
        }

        public async Task<HttpResponse<string>> Send(string url, HttpMethod method, object data, Dictionary<string, string> headers = null)
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = method
            };
            if (method.Method.Equals(HttpMethod.Post.Method) || method.Method.Equals(HttpMethod.Put.Method))
            {
                var requestData = JsonSerializer.Serialize(data);
                httpRequest.Content = new StringContent(requestData, Encoding.UTF8, "application/json");
            }
            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers) httpRequest.Headers.Add(header.Key, header.Value);
            }
            foreach (var header in Headers) httpRequest.Headers.Add(header.Key, header.Value);
            return await Send(httpRequest);
        }

        public async Task<HttpResponse<T>> Send<T>(string url, HttpMethod method, object data, Dictionary<string, string> headers = null)
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = method
            };
            if (method.Method.Equals(HttpMethod.Post.Method) || method.Method.Equals(HttpMethod.Put.Method))
            {
                var requestData = JsonSerializer.Serialize(data);
                httpRequest.Content = new StringContent(requestData, Encoding.UTF8, "application/json");
            }
            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers) httpRequest.Headers.Add(header.Key, header.Value);
            }
            foreach (var header in Headers) httpRequest.Headers.Add(header.Key, header.Value);
            return await Send<T>(httpRequest);
        }

        public async Task<HttpResponse<string>> Send(HttpRequestMessage httpRequest)
        {
            var returnResponse = new HttpResponse<string>();
            string stringResponse = string.Empty;
            try
            {
                var httpClient = new HttpClient();
                var httpResponse = await httpClient.SendAsync(httpRequest);
                stringResponse = await httpResponse.Content.ReadAsStringAsync();
                returnResponse.StatusCode = httpResponse.StatusCode;
                returnResponse.ReasonPhrase = httpResponse.ReasonPhrase;
                returnResponse.RequestMessage = httpResponse.RequestMessage;
                returnResponse.Headers = httpResponse.Headers;
                returnResponse.Content = stringResponse;
            }
            catch (Exception ex)
            {
                returnResponse.OutputMessage = stringResponse;
                returnResponse.ErrorMessage = ex.Message;
            }
            return returnResponse;
        }

        public async Task<HttpResponse<T>> Send<T>(HttpRequestMessage httpRequest)
        {
            var returnResponse = new HttpResponse<T>();
            string stringResponse = string.Empty;
            try
            {
                var httpClient = new HttpClient();
                var httpResponse = await httpClient.SendAsync(httpRequest);
                stringResponse = await httpResponse.Content.ReadAsStringAsync();
                returnResponse.StatusCode = httpResponse.StatusCode;
                returnResponse.ReasonPhrase = httpResponse.ReasonPhrase;
                returnResponse.RequestMessage = httpResponse.RequestMessage;
                returnResponse.Headers = httpResponse.Headers;
                returnResponse.Content = JsonSerializer.Deserialize<T>(stringResponse);
            }
            catch (Exception ex)
            {
                returnResponse.OutputMessage = stringResponse;
                returnResponse.ErrorMessage = ex.Message;
            }
            return returnResponse;
        }


    }
}
