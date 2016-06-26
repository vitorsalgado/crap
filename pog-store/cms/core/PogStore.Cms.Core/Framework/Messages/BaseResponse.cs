using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace PogStore.Cms.Core.Framework
{
	[DataContract]
	public abstract class BaseResponse
	{
		private List<ValidationMessage> _validationMessages;

		protected BaseResponse()
		{
			_validationMessages = new List<ValidationMessage>();
			Ack = AckType.SUCCESS;
		}

		[DataMember]
		public string CorrelationId { get; set; }

		[DataMember]
		public AckType Ack { get; set; }

		[DataMember]
		public string Message { get; set; }

		[DataMember]
		public List<ValidationMessage> ValidationMessages
		{
			get
			{
				return _validationMessages;
			}
		}

		[DataMember]
		public string Version
		{
			get
			{
				return string.Format("{0}.{1}",
					CurrentAssemblyName.Version.Major,
					CurrentAssemblyName.Version.Minor);
			}
		}

		[DataMember]
		public string Build
		{
			get
			{
				return CurrentAssemblyName.Version.Build.ToString();
			}
		}

		[IgnoreDataMember]
		private static AssemblyName CurrentAssemblyName
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName();
			}
		}
	}
}