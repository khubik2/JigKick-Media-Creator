﻿//************************************************************************************************
// Copyright © 2010 Steven M. Cohn. All Rights Reserved.
//
//************************************************************************************************

namespace iTuner
{
	using System;
	using System.Text;


	/// <summary>
	/// Represents the displayable information for a single USB disk.
	/// </summary>

	public class UsbDisk
	{
		private const int KB = 1024;
		private const int MB = KB * 1000;
		private const int GB = MB * 1000;


		/// <summary>
		/// Initialize a new instance with the given values.
		/// </summary>
		/// <param name="name">The Windows drive letter assigned to this device.</param>

		internal UsbDisk (string name)
		{
			this.Name = name;
			this.Model = String.Empty;
			this.Volume = String.Empty;
			this.FreeSpace = 0;
			this.Size = 0;
		}


		/// <summary>
		/// Gets the available free space on the disk, specified in bytes.
		/// </summary>

		public ulong FreeSpace
		{
			get;
			internal set;
		}


		/// <summary>
		/// Get the model of this disk.  This is the manufacturer's name.
		/// </summary>
		/// <remarks>
		/// When this class is used to identify a removed USB device, the Model
		/// property is set to String.Empty.
		/// </remarks>

		public string Model
		{
			get;
			internal set;
		}


		/// <summary>
		/// Gets the name of this disk.  This is the Windows identifier, drive letter.
		/// </summary>

		public string Name
		{
			get;
			private set;
		}


		/// <summary>
		/// Gets the total size of the disk, specified in bytes.
		/// </summary>

		public ulong Size
		{
			get;
			internal set;
		}


		/// <summary>
		/// Get the volume name of this disk.  This is the friently name ("Stick").
		/// </summary>
		/// <remarks>
		/// When this class is used to identify a removed USB device, the Volume
		/// property is set to String.Empty.
		/// </remarks>

		public string Volume
		{
			get;
			internal set;
		}


		/// <summary>
		/// Pretty print the disk.
		/// </summary>
		/// <returns></returns>

		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(Name);
			builder.Append(" (");
			builder.Append(Model);
			builder.Append(")");

			return builder.ToString();
		}


		private string FormatByteCount (ulong bytes)
		{
			string format = null;

			if (bytes < KB)
			{
				format = String.Format("{0} Bytes", bytes);
			}
			else if (bytes < MB)
			{
				bytes = bytes / KB;
				format = String.Format("{0} KB", bytes.ToString("N"));
			}
			else if (bytes < GB)
			{
				double dree = bytes / MB;
				format = String.Format("{0} MB", dree.ToString("N1"));
			}
			else
			{
				double gree = bytes / GB;
				format = String.Format("{0} GB", gree.ToString("N1"));
			}

			return format;
		}
	}
}
