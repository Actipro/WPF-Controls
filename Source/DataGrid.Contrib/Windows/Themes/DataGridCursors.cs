using System;
using System.Diagnostics.CodeAnalysis;
using System.IO.Packaging;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ActiproSoftware.Windows.Themes {

	/// <summary>
	/// Represents a set of cursors for use with the <c>DataGrid</c>.
	/// </summary>
	public static class DataGridCursors {

		private static Cursor columnResizeCursor;
		private static Cursor rowResizeCursor;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets an absolute pack URI for the assembly resource identified by <paramref name="relativeUri"/>.
		/// </summary>
		/// <param name="relativeUri">The relative path to the resource.</param>
		/// <returns></returns>
		private static Uri GetResourceUri(Uri relativeUri) {
			if (relativeUri.IsAbsoluteUri)
				throw new ArgumentException("value must be a relative URI", "relativeUri");
			return PackUriHelper.Create(
				new Uri("application:///", UriKind.Absolute),
				new Uri("/" + Assembly.GetExecutingAssembly().GetName().Name + ";component" + relativeUri, UriKind.Relative));
		}

		#endregion // NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets an Excel-style mouse cursor to display when resizing a <see cref="DataGrid"/> column.
		/// </summary>
		/// <value>An Excel-style mouse cursor to display when resizing a <see cref="DataGrid"/> column.</value>
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static Cursor ColumnResizeCursor {
			get {
				if (columnResizeCursor == null) {
					try {
						var uri = GetResourceUri(new Uri("/Products/DataGrid/Contrib/Cursors/ColumnResize.cur", UriKind.Relative));
						var resourceStream = Application.GetResourceStream(uri);
						if (resourceStream != null)
							columnResizeCursor = new Cursor(resourceStream.Stream);
						else
							columnResizeCursor = Cursors.SizeWE;
					}
					catch {
						columnResizeCursor = Cursors.SizeWE;
					}
				}
				return columnResizeCursor;
			}
		}

		/// <summary>
		/// Gets an Excel-style mouse cursor to display when resizing a <see cref="DataGrid"/> row.
		/// </summary>
		/// <value>An Excel-style mouse cursor to display when resizing a <see cref="DataGrid"/> row.</value>
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static Cursor RowResizeCursor {
			get {
				if (rowResizeCursor == null) {
					try {
						var uri = GetResourceUri(new Uri("/Products/DataGrid/Contrib/Cursors/RowResize.cur", UriKind.Relative));
						var resourceStream = Application.GetResourceStream(uri);
						if (resourceStream != null)
							rowResizeCursor = new Cursor(resourceStream.Stream);
						else
							rowResizeCursor = Cursors.SizeNS;
					}
					catch {
						rowResizeCursor = Cursors.SizeNS;
					}
				}
				return rowResizeCursor;
			}
		}

		#endregion // PUBLIC PROCEDURES
	}
}