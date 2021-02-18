using System.Windows.Documents;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Implements a simple <see cref="FlowDocument"/> with basic styling.
	/// </summary>
	public class SimpleFlowDocument : FlowDocument {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleFlowDocument"/> class.
		/// </summary>
		public SimpleFlowDocument() {
			this.DefaultStyleKey = typeof(SimpleFlowDocument);
		}

	}

}
