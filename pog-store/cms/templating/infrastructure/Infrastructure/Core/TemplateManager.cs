using PogStore.Cms.Templating.Repository;
using RazorEngine.Templating;
using System;
using System.Collections.Concurrent;

namespace PogStore.Cms.Templating.Core
{
	public class TemplateManager : ITemplateManager
	{
		private readonly ITemplateRepository _templateRepository;

		private readonly ConcurrentDictionary<ITemplateKey, ITemplateSource> _dynamicTemplates =
			new ConcurrentDictionary<ITemplateKey, ITemplateSource>();

		public TemplateManager(ITemplateRepository templateRepository)
		{
			this._templateRepository = templateRepository;
		}

		public void AddDynamic(ITemplateKey key, ITemplateSource source)
		{
			_dynamicTemplates.AddOrUpdate(key, source, (k, oldSource) =>
			{
				if (oldSource.Template != source.Template)
				{
					throw new InvalidOperationException("The same key was used for another template!");
				}

				return source;
			});
		}

		public ITemplateKey GetKey(string name, ResolveType resolveType, ITemplateKey context)
		{
			return new NameOnlyTemplateKey(name, resolveType, context);
		}

		public ITemplateSource Resolve(ITemplateKey key)
		{
			ITemplateSource result;

			if (_dynamicTemplates.TryGetValue(key, out result))
			{
				return result;
			}

			var htmlContent = _templateRepository.Get(key.Name);

			if (htmlContent == null)
			{
				throw new InvalidOperationException("");
			}

			return new LoadedTemplateSource(htmlContent);
		}
	}
}