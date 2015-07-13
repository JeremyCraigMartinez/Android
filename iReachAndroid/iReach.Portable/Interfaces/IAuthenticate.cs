using System;
using System.Collections.Generic;

namespace iReach.Portable.Interfaces
{
	public interface IAuthenticate
	{
		// Takes a command object Action
		void LoginAsync(Action<bool, Dictionary<string, string>> loginResult);
	}
}

