using ActiproSoftware.Windows.Controls.Shell;
using ActiproSoftware.Windows.Data.Filtering;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides a filter for a code viewer.
/// </summary>
public class CodeViewerTreeFilter : DataFilterBase {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override DataFilterResult Filter(object item, object? context) {
		if (item is ShellObjectViewModel shellObject) {
			if (shellObject.IsFolder)
				return DataFilterResult.IncludedWithDescendants;
			else if (shellObject.RelativeParsingName is { Length: > 0 } parsingName) {
				var extension = Path.GetExtension(parsingName)?.ToUpperInvariant();
				switch (extension) {
					case ".CS":
					case ".XAML":
						return DataFilterResult.Included;
				}
			}
		}

		return DataFilterResult.Excluded;
	}

}
