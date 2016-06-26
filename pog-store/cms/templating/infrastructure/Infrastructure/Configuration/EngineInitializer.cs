using PogStore.Cms.Templating.Core;
using PogStore.Cms.Templating.Repository;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System;

namespace PogStore.Cms.Templating.Configuration
{
	public static class EngineInitializer
	{
		public static void SetUp()
		{
			var config = new TemplateServiceConfiguration();

			config.BaseTemplateType = typeof(BaseWebTemplate<>);
			config.TemplateManager = new TemplateManager(new TemplateRepository());
			config.Namespaces.Add("System");
			config.Namespaces.Add("System.Web");
			config.Namespaces.Add("System.Web.Mvc");
			config.Namespaces.Add("PogStore.Cms.Templating");

			AppDomain.CurrentDomain.Load("System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			AppDomain.CurrentDomain.Load("System.Web.Mvc");

			var service = RazorEngineService.Create(config);

			Engine.Razor = service;
		}
	}
}