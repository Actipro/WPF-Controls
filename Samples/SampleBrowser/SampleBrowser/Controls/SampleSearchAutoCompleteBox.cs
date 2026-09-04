using ActiproSoftware.Windows.Controls.Editors;
using ActiproSoftware.Windows.Media.Animation;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Implements an <see cref="AutoCompleteBox"/> for searching samples.
/// </summary>
public class SampleSearchAutoCompleteBox : AutoCompleteBox {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SampleSearchAutoCompleteBox() {
		DefaultStyleKey = typeof(SampleSearchAutoCompleteBox);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnSubmitted(AutoCompleteBoxEventArgs e) {
		if (e.Item is ProductItemInfo productItemInfo)
			ViewModel.NavigateViewToItemInfo(productItemInfo, TransitionDirection.Forward);
	}

	/// <inheritdoc/>
	protected override void OnSuggestionChosen(AutoCompleteBoxEventArgs e)
		=> e.Text = string.Empty;

	/// <summary>
	/// The view-model for this view.
	/// </summary>
	public ApplicationViewModel ViewModel
		=> (ApplicationViewModel)DataContext;

}
