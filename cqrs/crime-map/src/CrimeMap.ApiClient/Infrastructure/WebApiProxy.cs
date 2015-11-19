using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CrimeMap.Common;

namespace CrimeMap.ApiClient {

	public abstract class WebApiProxy {

		protected const string MEDIA_TYPE_APPLICATION_JSON = "application/json";
		private static Uri _apiUrl;

		protected static Uri ApiUrl {
			get {
				if (_apiUrl == null) {
					var url = ConfigurationManager.AppSettings["CrimeMap.ApiAddress"];

					if (string.IsNullOrEmpty(url))
						throw new InvalidOperationException("Api Url is not configured.");

					var uri = new Uri(url);
					_apiUrl = uri;
				}

				return _apiUrl;
			}
		}

		protected HttpClient GetHttpClientJsonReady(Uri baseAddress) {
			HttpClient httpClient = new HttpClient();
			httpClient.BaseAddress = baseAddress;
			httpClient.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_APPLICATION_JSON));

			return httpClient;
		}

		protected static async Task EnsureNotInternalServerError(HttpResponseMessage httpResponse) {
			if (httpResponse.StatusCode == HttpStatusCode.InternalServerError) {
				var error = await httpResponse.Content.ReadAsAsync<ErrorResponse>();
				throw new ProxyException(error);
			}
		}

		protected async Task<BaseResponse> SendCommand<T>(T command, string requestUri) {
			var httpClient = this.GetHttpClientJsonReady(ApiUrl);
			var httpResponse = await httpClient.PostAsJsonAsync(requestUri, command);

			await EnsureNotInternalServerError(httpResponse);
			var response = await httpResponse.Content.ReadAsAsync<BaseResponse>();

			return response;
		}

		protected async Task<TResponse> Fetch<TRequest, TResponse>(TRequest request, string requestUri) {
			var httpClient = this.GetHttpClientJsonReady(ApiUrl);
			var httpResponse = await httpClient.PostAsJsonAsync(requestUri, request);

			await EnsureNotInternalServerError(httpResponse);
			var response = await httpResponse.Content.ReadAsAsync<TResponse>();

			return response;
		}

	}
}
