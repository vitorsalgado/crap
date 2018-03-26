using Moq;
using NUnit.Framework;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PogStore.Cms.Tests.Common
{
	public static class Helper
	{
		public static void SetPropertyValue<T, TProperty>(T instance, Expression<Func<T, TProperty>> propertyExpression, object newValue) where T : class
		{
			if (!(propertyExpression.Body is MemberExpression))
				throw new ArgumentException("expression not supported.");

			var memberExpression = propertyExpression.Body as MemberExpression;
			var propertyName = memberExpression.Member.Name;

			Type type = instance.GetType();
			PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

			if (property == null)
				throw new ArgumentException(
					string.Format("expression does not refer to a valid property in the class {0}.", instance.GetType().ToString()));

			property.SetValue(instance, newValue);
		}

		public static void PrepareControllerForUnitTest(Controller controller)
		{
			var routes = new RouteCollection();

			var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
			request.SetupGet(x => x.ApplicationPath).Returns("/");
			request.SetupGet(x => x.Url).Returns(new Uri("http://www.pogstore.com.br?q=teste", UriKind.Absolute));
			request.SetupGet(x => x.ServerVariables).Returns(new NameValueCollection());

			var response = new Mock<HttpResponseBase>(MockBehavior.Strict);

			var context = new Mock<HttpContextBase>(MockBehavior.Strict);
			context.SetupGet(x => x.Request).Returns(request.Object);
			context.SetupGet(x => x.Response).Returns(response.Object);

			controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
			controller.Url = new UrlHelper(new RequestContext(context.Object, new RouteData()), routes);
			controller.ViewData = new ViewDataDictionary();
		}

		public static void CreateFakeHttpContext(string url, string queryString)
		{
			HttpContext.Current = new HttpContext(
							new HttpRequest("none", url, queryString),
							new HttpResponse(new StreamWriter(new MemoryStream()))
						);
		}

		public static void CreateFakeAjaxHttpContext(string url, string queryString)
		{
			CreateFakeHttpContext(url, queryString);
			HttpContext.Current.Request.Headers.Add(new WebHeaderCollection() {
				{"X-Requested-With", "XMLHttpRequest"}
			});
		}

		public static void ValidateMethodAttributes<T, TMethod>(T obj, Expression<Func<T, TMethod>> methodExpression, params Type[] attributes) where T : class
		{
			var memberExpression = methodExpression.Body as MethodCallExpression;

			foreach (var attribute in attributes)
			{
				var methodAttributes = memberExpression.Method.GetCustomAttributes(attribute, false);
				Assert.IsTrue(methodAttributes.Any(), string.Format("Atribute \"{0}\" não encontrado.", attribute.Name));
			}
		}
	}
}