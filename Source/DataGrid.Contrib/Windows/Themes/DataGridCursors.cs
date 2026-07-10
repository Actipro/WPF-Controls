using System.IO.Packaging;
using System.Reflection;

namespace ActiproSoftware.Windows.Themes;

/// <summary>
/// Represents a set of cursors for use with the <c>DataGrid</c>.
/// </summary>
public static class DataGridCursors {

	private static Cursor? _columnResizeCursor;
	private static Cursor? _rowResizeCursor;

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Gets an absolute pack URI for the assembly resource identified by <paramref name="relativeUri"/>.
	/// </summary>
	/// <param name="relativeUri">The relative path to the resource.</param>
	private static Uri GetResourceUri(Uri relativeUri) {
		if (relativeUri.IsAbsoluteUri)
			throw new ArgumentException("value must be a relative URI", nameof(relativeUri));

		return PackUriHelper.Create(
			new Uri("application:///", UriKind.Absolute),
			new Uri("/" + Assembly.GetExecutingAssembly().GetName().Name + ";component" + relativeUri, UriKind.Relative)
		);
	}

	private static Cursor LoadCursorFromResource(string relativePath, Cursor fallback) {
		try {
			var uri = GetResourceUri(new Uri(relativePath, UriKind.Relative));
			var resourceStream = Application.GetResourceStream(uri);
			if (resourceStream is not null)
				return new Cursor(resourceStream.Stream);
		}
		catch { }
		return fallback;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns an Excel-style mouse cursor to display when resizing a <see cref="DataGrid"/> column.
	/// </summary>
	public static Cursor ColumnResizeCursor
		=> _columnResizeCursor ??= LoadCursorFromResource("/Products/DataGrid/Contrib/Cursors/ColumnResize.cur", Cursors.SizeWE);

	/// <summary>
	/// Returns an Excel-style mouse cursor to display when resizing a <see cref="DataGrid"/> row.
	/// </summary>
	public static Cursor RowResizeCursor
		=> _rowResizeCursor ??= LoadCursorFromResource("/Products/DataGrid/Contrib/Cursors/RowResize.cur", Cursors.SizeNS);

}
