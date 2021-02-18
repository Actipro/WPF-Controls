using ActiproSoftware.Windows.Controls.Shell;
using ActiproSoftware.Windows.Data.Filtering;
using System.IO;

namespace ActiproSoftware.SampleBrowser {
	
	/// <summary>
	/// Provides a filter for a code viewer.
	/// </summary>
	public class CodeViewerTreeFilter : DataFilterBase {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Examines the specified item to see if meets filter conditions.
		/// </summary>
		/// <param name="item">The item to examine.</param>
		/// <param name="context">An optional object that provides contextual information for the filter request.</param>
		/// <returns>A <see cref="DataFilterResult"/> that indicates the filter result.</returns>
		public override DataFilterResult Filter(object item, object context) {
			var shellObject = item as ShellObjectViewModel;
			if (shellObject != null) {
				if (shellObject.IsFolder)
					return DataFilterResult.IncludedWithDescendants;
				else {
					var parsingName = shellObject.RelativeParsingName;
					if (!string.IsNullOrEmpty(parsingName)) {
						var extension = Path.GetExtension(parsingName);
						if (!string.IsNullOrEmpty(extension)) {
							extension = extension.ToUpperInvariant();
							switch (extension) {
								case ".CS":
								case ".XAML":
									return DataFilterResult.Included;
							}
						}
					}
				}
			}

			return DataFilterResult.Excluded;
		}
		
	}

}
