using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Primitives;

namespace ActiproSoftware.Windows.WindowsWorkflowIntegration.ExpressionEditing {

	/// <summary>
	/// A custom <see cref="ListBox"/> implementation since clicks on completion list items will normally focus the list item,
	/// but then the workflow designer thinks the editor is closing due to the focus change.  To work around the problem,
	/// this custom control uses a custom <see cref="ListBoxItem"/> that prevents the focus on click, but still allows selection.
	/// </summary>
	public class CustomCompletionListBox : IntelliPromptCompletionListBox {

		#region CustomCompletionListBoxItem

		/// <summary>
		/// A custom <see cref="ListBoxItem"/> that prevents focus on click (base class behavior in <c>OnMouseLeftButtonDown</c>),
		/// but still allows selection.
		/// </summary>
		public class CustomCompletionListBoxItem : IntelliPromptCompletionListBoxItem {

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// PUBLIC PROCEDURES
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <summary>
			/// Occurs when the left mouse button is pressed.
			/// </summary>
			/// <param name="e"></param>
			protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
				e.Handled = true;
				base.OnMouseLeftButtonDown(e);
				this.IsSelected = true;
			}

		}

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates or identifies the element used to display the specified item.
		/// </summary>
		/// <returns>The element that is used to display the given item.</returns>
		protected override DependencyObject GetContainerForItemOverride() {
			return new CustomCompletionListBoxItem();
		}

	}

}
