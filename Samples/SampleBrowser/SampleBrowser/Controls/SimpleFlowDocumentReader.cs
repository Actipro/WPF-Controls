using ActiproSoftware.Extensions;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Implements a simple <see cref="FlowDocumentReader"/> without any extra UI.
/// </summary>
public class SimpleFlowDocumentReader : FlowDocumentReader {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SimpleFlowDocumentReader() {
		DefaultStyleKey = typeof(SimpleFlowDocumentReader);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
		base.OnRenderSizeChanged(sizeInfo);

		// Dynamically adjust the page width and viewing mode based on the available width
		Document.PageWidth = (Math.Min(Document.MaxPageWidth, sizeInfo.NewSize.Width) - SystemParameters.VerticalScrollBarWidth).ClampToNonnegative();
		ViewingMode = (sizeInfo.NewSize.Width > Document.MaxPageWidth)
			? FlowDocumentReaderViewingMode.Page
			: FlowDocumentReaderViewingMode.Scroll;
	}

}
