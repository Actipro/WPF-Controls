using System;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Implements a simple <see cref="FlowDocumentReader"/> without any extra UI.
	/// </summary>
	public class SimpleFlowDocumentReader : FlowDocumentReader {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleFlowDocumentReader"/> class.
		/// </summary>
		public SimpleFlowDocumentReader() {
			this.DefaultStyleKey = typeof(SimpleFlowDocumentReader);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Called when the rendered size of a control changes. 
		/// </summary>
		/// <param name="sizeInfo">Specifies the size changes.</param>
		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
			// Call the base methods
			base.OnRenderSizeChanged(sizeInfo);

			// Dynamically adjust the page width and viewing mode based on the available width
			this.Document.PageWidth = Math.Max(0.0, Math.Min(this.Document.MaxPageWidth, sizeInfo.NewSize.Width) - SystemParameters.VerticalScrollBarWidth);
			this.ViewingMode = (sizeInfo.NewSize.Width > this.Document.MaxPageWidth ? FlowDocumentReaderViewingMode.Page : FlowDocumentReaderViewingMode.Scroll);
		}

	}

}
