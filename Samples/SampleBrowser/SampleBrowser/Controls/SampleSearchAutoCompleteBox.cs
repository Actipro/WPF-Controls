using ActiproSoftware.Windows.Controls.Editors;
using ActiproSoftware.Windows.Media.Animation;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Implements an <see cref="AutoCompleteBox"/> for searching samples.
	/// </summary>
	public class SampleSearchAutoCompleteBox : AutoCompleteBox {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SampleSearchAutoCompleteBox"/> class.
		/// </summary>
		public SampleSearchAutoCompleteBox() {
			this.DefaultStyleKey = typeof(SampleSearchAutoCompleteBox);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the end user submits a text entry, generally by pressing <c>Enter</c>.
		/// </summary>
		/// <param name="e">The <c>AutoCompleteBoxEventArgs</c> that contains the event data.</param>
		protected override void OnSubmitted(AutoCompleteBoxEventArgs e) {
			var productItemInfo = e.Item as ProductItemInfo;
			if (productItemInfo != null)
				this.ViewModel.NavigateViewToItemInfo(productItemInfo, TransitionDirection.Forward);
		}
		
		/// <summary>
		/// Occurs when a suggested item is chosen and needs to be converted to text.
		/// </summary>
		/// <param name="e">The <c>AutoCompleteBoxEventArgs</c> that contains the event data.</param>
		protected override void OnSuggestionChosen(AutoCompleteBoxEventArgs e) {
			e.Text = string.Empty;
		}

		/// <summary>
		/// Gets the view-model for this view.
		/// </summary>
		/// <value>The view-model for this view.</value>
		public ApplicationViewModel ViewModel => (ApplicationViewModel)this.DataContext;

	}

}
