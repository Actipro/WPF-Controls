using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Windows;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbFileSystem {
	/// <summary>
	/// Provides access to the shell icon associated with a specified directory or drive.
	/// </summary>
	public static class ShellIconHelper {
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		[StructLayout(LayoutKind.Sequential)]
		private struct SHFILEINFO {
			public IntPtr hIcon;
			public IntPtr iIcon;
			public int dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		};

		private const int SHGFI_ICON = 0x100;
		private const int SHGFI_SMALLICON = 0x1; // Small icon

		[DllImport("shell32.dll")]
		private static extern IntPtr SHGetFileInfo(string pszPath, int dwFileAttributes, ref SHFILEINFO psfi, int cbSizeFileInfo, int uFlags);

		[DllImport("user32.dll")]
		private static extern bool DestroyIcon(IntPtr hIcon);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets a <see cref="BitmapSource"/> for the given directory or drive.
		/// </summary>
		/// <param name="fullPath">The full path.</param>
		/// <returns></returns>
		public static BitmapSource GetSystemImageSource(string fullPath) {
			SHFILEINFO fileInfo = new SHFILEINFO();
			SHGetFileInfo(fullPath, 0, ref fileInfo, Marshal.SizeOf(fileInfo), SHGFI_ICON | SHGFI_SMALLICON);
			BitmapSource imageSource = Imaging.CreateBitmapSourceFromHIcon(fileInfo.hIcon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
			DestroyIcon(fileInfo.hIcon);
			return imageSource;
		}
	}
}
