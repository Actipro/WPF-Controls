using ActiproSoftware.Windows.Controls.SyntaxEditor.Primitives;

namespace ActiproSoftware.Windows.WindowsWorkflowIntegration.ExpressionEditing;

/// <summary>
/// A custom <see cref="ListBox"/> implementation since clicks on completion list items will normally focus the list item,
/// but then the workflow designer thinks the editor is closing due to the focus change.  To work around the problem,
/// this custom control uses a custom <see cref="ListBoxItem"/> that prevents the focus on click, but still allows selection.
/// </summary>
public class CustomCompletionListBox : IntelliPromptCompletionListBox {

	// --------------------------------------------------------------------------------------------------
	// NESTED TYPES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// A custom <see cref="ListBoxItem"/> that prevents focus on click (base class behavior in <c>OnMouseLeftButtonDown</c>),
	/// but still allows selection.
	/// </summary>
	public class CustomCompletionListBoxItem : IntelliPromptCompletionListBoxItem {

		// --------------------------------------------------------------------------------------------------
		// PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		/// <inheritdoc/>
		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
			e.Handled = true;
			base.OnMouseLeftButtonDown(e);
			IsSelected = true;
		}

	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override DependencyObject GetContainerForItemOverride()
		=> new CustomCompletionListBoxItem();

}
