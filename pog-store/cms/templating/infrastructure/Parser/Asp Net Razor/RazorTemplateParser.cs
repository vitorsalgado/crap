using PogStore.Cms.Core.Templating.Parser;
using RazorEngine;
using RazorEngine.Templating;
using System;

namespace PogStore.Cms.Templating.Parser.Asp_Net_Razor
{
	public class RazorTemplateParser : ITemplateParser
	{
		public string Parse(ParseRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}

			var finalHtml = Engine.Razor.RunCompile(
				request.Content, request.Name, request.Model.GetType(), request.Model, null);

			return finalHtml;
		}
	}
}