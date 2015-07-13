using System;
using System.Windows.Input;

namespace iReach.Portable
{
	public interface IRemoveCommand
	{
		ICommand RemoveCommand { get; }
		bool canRemoveCommand (int index );
	}
}

