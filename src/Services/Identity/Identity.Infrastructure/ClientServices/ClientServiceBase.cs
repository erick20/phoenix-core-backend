using Identity.Application.Exceptions;
using Identity.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Identity.Infrastructure.ClientServices
{
    internal abstract class ClientServiceBase
    {
        private const string HttpClientTimeoutIntervalName = "HttpClientTimeoutInterval";

        private readonly ILogger<ClientServiceBase> _logger;

        private readonly HttpClient httpClient;

        protected ClientServiceBase(HttpClient httpClient, ILogger<ClientServiceBase> logger, IOptions<IClientServiceBase> clientServiceBase)
        {
            this.httpClient = httpClient;
            this.BaseAddressUrl = clientServiceBase.Value.BaseAddress;
            this.ConfigHttpClient();
            _logger = logger;
        }

        protected string BaseAddressUrl { get; init; }

        protected async Task<TRT> PostAsJsonAsync<T, TRT>(string requestUrl, T content)
        {
            httpClient.DefaultRequestHeaders.Clear();
            var response = await this.httpClient.PostAsJsonAsync(requestUrl, content);

            var result = await this.GetRequestResult<TRT>(response);

            return result;
        }

        protected async Task<TRT> PostAsJsonAsync<TRT>(string requestUrl, object content, string accesstoken = null, string innertoken = null, string apiVersion = "1.0")
        {
            httpClient.DefaultRequestHeaders.Clear();
            if (!string.IsNullOrWhiteSpace(accesstoken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            }

            httpClient.DefaultRequestHeaders.Add("InnerAuthorization", innertoken);
            httpClient.DefaultRequestHeaders.Add("Api-Version", apiVersion);

            //string body = Newtonsoft.Json.JsonConvert.SerializeObject(content);
            //var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await this.httpClient.PostAsJsonAsync(requestUrl, content);

            var result = await this.GetRequestResult<TRT>(response);

            return result;
        }

        //protected async Task<TRT> PostAsXmlAsync<T, TRT>(string requestUrl, T content)
        //{
        //    var response = await this.httpClient.PostAsXmlAsync(requestUrl, content);

        //    var result = await this.GetRequestResult<TRT>(response);

        //    return result;
        //}

        protected async Task<TRT> PutAsJsonAsync<T, TRT>(string requestUrl, T content)
        {
            httpClient.DefaultRequestHeaders.Clear();
            var response = await this.httpClient.PutAsJsonAsync(requestUrl, content);

            var result = await this.GetRequestResult<TRT>(response);

            return result;
        }

        protected async Task<TRT> PutAsJsonAsync<TRT>(string requestUrl, object content, string accesstoken = null, string innertoken = null, string apiVersion = "1.0")
        {
            httpClient.DefaultRequestHeaders.Clear();
            if (!string.IsNullOrWhiteSpace(accesstoken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            }

            httpClient.DefaultRequestHeaders.Add("InnerAuthorization", innertoken);
            httpClient.DefaultRequestHeaders.Add("Api-Version", apiVersion);

            //string body = Newtonsoft.Json.JsonConvert.SerializeObject(content);
            //var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await this.httpClient.PutAsJsonAsync(requestUrl, content);

            var result = await this.GetRequestResult<TRT>(response);

            return result;
        }

        protected async Task PutAsJsonAsync(string requestUrl, object content, string accesstoken = null, string innertoken = null, string apiVersion = "1.0")
        {
            httpClient.DefaultRequestHeaders.Clear();
            if (!string.IsNullOrWhiteSpace(accesstoken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            }

            httpClient.DefaultRequestHeaders.Add("InnerAuthorization", innertoken);
            httpClient.DefaultRequestHeaders.Add("Api-Version", apiVersion);

            //string body = Newtonsoft.Json.JsonConvert.SerializeObject(content);
            //var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await this.httpClient.PutAsJsonAsync(requestUrl, content);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpException(Convert.ToInt32(response.StatusCode), response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        //protected async Task<TRT> PutAsXmlAsyncM<T, TRT>(string requestUrl, T content)
        //{
        //    var response = await this.httpClient.PutAsXmlAsync(requestUrl, content);

        //    var result = await this.GetRequestResult<TRT>(response);

        //    return result;
        //}

        protected async Task<T> GetAsJsonAsync<T>(string requestUrl)
        {
            httpClient.DefaultRequestHeaders.Clear();
            var response = await this.httpClient.GetAsync(requestUrl);

            var result = await this.GetRequestResult<T>(response);

            return result;
        }
        protected async Task<T> GetAsJsonAsync<T>(string requestUrl, string accesstoken = null, string innertoken = null, string apiVersion = "1.0")
        {
            httpClient.DefaultRequestHeaders.Clear();
            if (!string.IsNullOrWhiteSpace(accesstoken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            }

            httpClient.DefaultRequestHeaders.Add("InnerAuthorization", innertoken);
            httpClient.DefaultRequestHeaders.Add("Api-Version", apiVersion);

            var response = await this.httpClient.GetAsync(requestUrl);

            var result = await this.GetRequestResult<T>(response);

            return result;
        }

        private void ConfigHttpClient()
        {
            if (string.IsNullOrEmpty(this.BaseAddressUrl))
            {
                throw new NullReferenceException("Base Address Url is missing.");
            }

            this.httpClient.BaseAddress = new Uri(this.BaseAddressUrl);

            //TODO set timeout intervall from configuration
            //if (ConfigurationManager.AppSettings.AllKeys.Contains(HttpClientTimeoutIntervalName) && int.TryParse(ConfigurationManager.AppSettings[HttpClientTimeoutIntervalName], out int timeoutInterval))
            //{
                this.httpClient.Timeout = TimeSpan.FromSeconds(40);
            //}

            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            //this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        }

        private async Task<T> GetRequestResult<T>(HttpResponseMessage response)
        {
            T result;

            try
            {
                response.EnsureSuccessStatusCode();

                var resp = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<T>(resp);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpException(Convert.ToInt32(response.StatusCode), response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
