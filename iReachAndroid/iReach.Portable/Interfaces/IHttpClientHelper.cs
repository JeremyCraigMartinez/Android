using System;
using System.Net.Http;

namespace iReach.Portable.Interfaces
{
	public interface IHttpClientHelper
	{
		HttpMessageHandler MessageHandler { get; }
	}
}

