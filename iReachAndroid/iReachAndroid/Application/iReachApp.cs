using System;
using iReach.Core.Helpers;
using System.IO;
using System.Threading.Tasks;
using iReach.Core.Database;
using System.Collections.Generic;
using SQLite;
using Android.App;
using Android.Runtime;
using iReach.Core;

namespace iReachAndroid
{
	[Application]			
	public class iReachApp : Application
	{
		public iReachApi api;

		public iReachApp(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
		{
		}
		public override void OnCreate ()
		{

			base.OnCreate ();
			var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		
			var dbFile = Path.Combine(docFolder, "food.db"); // FILE NAME TO USE WHEN COPIED
			Console.WriteLine("DataPath: " + dbFile);

			if (!System.IO.File.Exists(dbFile)) {
				var s = Resources.OpenRawResource(Resource.Raw.food);  // DATA FILE RESOURCE ID
				FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write);
				ReadWriteStream(s, writeStream);
			}


			

		}
		// readStream is the stream you need to read
		// writeStream is the stream you want to write to
		private void ReadWriteStream(Stream readStream, Stream writeStream)
		{
			int Length = 256;
			Byte[] buffer = new Byte[Length];
			int bytesRead = readStream.Read(buffer, 0, Length);
			// write the required bytes
			while (bytesRead > 0)
			{
				writeStream.Write(buffer, 0, bytesRead);
				bytesRead = readStream.Read(buffer, 0, Length);
			}
			readStream.Close();
			writeStream.Close();
		}
		
	}
}

