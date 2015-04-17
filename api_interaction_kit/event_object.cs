using System;

namespace api_interaction_kit
{
	public class event_object
	{
		public Object data;

		public event_object () { data = new Object (); }
		public event_object (Object d) { data = d; }
	}
}

