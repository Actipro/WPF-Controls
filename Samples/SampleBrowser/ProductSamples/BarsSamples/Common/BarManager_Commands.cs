using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using System.Windows;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	partial class BarManager {

		private ICommand insertTableCommand;
		private ICommand notImplementedCommand;
		private ICommand searchForTextCommand;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the composite command for decreasing font size.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand DecreaseFontSizeCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for setting flow direction.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand FlowDirectionCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for increasing font size.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand IncreaseFontSizeCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for inserting a symbol.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand InsertSymbolCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the command to insert a table.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		/// <remarks>The command parameter must be a <see cref="Size"/> where the width and height are whole numbers indicating the number of table rows and columns, respectively.</remarks>
		public ICommand InsertTableCommand {
			get {
				if (insertTableCommand == null) {
					insertTableCommand = new DelegateCommand<Size>(
						p => {
							ThemedMessageBox.Show(
								string.Format("This is where a table of size {0}x{1} would be inserted.", p.Width, p.Height), "Not Implemented", 
								MessageBoxButton.OK, MessageBoxImage.Information);
						}
					);
				}

				return insertTableCommand;
			}
		}

		/// <summary>
		/// Gets the composite command for creating a new, blank document.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand NewBlankDocumentCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for creating a new document with default content.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand NewDefaultDocumentCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets a special command associated with controls that are for demonstration purposes only and provide no implemented functionality.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand NotImplementedCommand {
			get {
				if (notImplementedCommand == null) {
					notImplementedCommand = new DelegateCommand<object>(
						p => {
							ThemedMessageBox.Show(
								"This control is for user interface demonstration purposes only and no application functionality has been implemented for it.", "Not Implemented", 
								MessageBoxButton.OK, MessageBoxImage.Information);
						}
					);
				}

				return notImplementedCommand;
			}
		}
		
		/// <summary>
		/// Gets the command executed to perform a text search.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand SearchForTextCommand {
			get {
				if (searchForTextCommand == null) {
					searchForTextCommand = new DelegateCommand<object>(
						p => {
							if (this.ControlViewModels[BarControlKeys.SearchForText] is BarTextBoxViewModel viewModel) {
								ThemedMessageBox.Show(
									string.Format("Search for the text '{0}' here.", viewModel.Text), "Not Implemented", 
									MessageBoxButton.OK, MessageBoxImage.Information);
							}
						}
					);
				}

				return searchForTextCommand;
			}
		}

		/// <summary>
		/// Gets the composite command for setting the font color.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetFontColorCommand { get; } = new PreviewableCompositeCommand();

		/// <summary>
		/// Gets the composite command for setting the font family.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetFontFamilyCommand { get; } = new PreviewableCompositeCommand();

		/// <summary>
		/// Gets the composite command for setting the font size.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetFontSizeCommand { get; } = new PreviewableCompositeCommand();

		/// <summary>
		/// Gets the composite command for setting the numbering style.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetNumberingCommand { get; } = new PreviewableCompositeCommand();

		/// <summary>
		/// Gets the composite command for setting text alignment.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetTextAlignmentCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for setting text highlight color.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetTextHighlightColorCommand { get; } = new PreviewableCompositeCommand();

		/// <summary>
		/// Gets the composite command for setting a text style.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetTextStyleCommand { get; } = new PreviewableCompositeCommand();

		/// <summary>
		/// Gets the composite command for setting an underline style.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetUnderlineCommand { get; } = new PreviewableCompositeCommand();

		/// <summary>
		/// Gets the composite command to stop text highlighting.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand StopHighlightingCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for toggling the visibility of the ribbon application button.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ToggleApplicationButtonCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for toggling the bold font weight.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ToggleBoldCommand { get; } = new CompositeCommand();
		
		/// <summary>
		/// Gets the composite command for toggling the visibility of the ribbon footer.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ToggleFooterCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for toggling the italic font style.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ToggleItalicCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for toggling the numbering style.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ToggleNumberingCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for toggling the visibility of the ribbon quick access toolbar.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ToggleQuickAccessToolBarCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for toggling the strikethrough font style.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ToggleStrikethroughCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for toggling the underline font style.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ToggleUnderlineCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for handling an undefined font size.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand UnknownFontSizeCommand { get; } = new CompositeCommand();

	}

}
