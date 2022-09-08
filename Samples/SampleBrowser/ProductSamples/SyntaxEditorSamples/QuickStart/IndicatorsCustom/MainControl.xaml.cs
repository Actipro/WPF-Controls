using System;
using System.Windows;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsCustom {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Load a language from a language definition
			editor.Document.Language = ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("JavaScript.langdef");
			
			// Register an indicator quick info provider
			editor.Document.Language.RegisterService(new IndicatorQuickInfoProvider());

			// Add some indicators
			this.AddIndicatorForWordAtOffset(editor.ActiveView.CurrentSnapshot.Lines[15].StartOffset + 10);
			this.AddIndicatorForWordAtOffset(editor.ActiveView.CurrentSnapshot.Lines[22].StartOffset + 10);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Adds an indicator.
		/// </summary>
		/// <param name="snapshotRange">The <see cref="TextSnapshotRange"/> of the indicator.</param>
		private void AddIndicator(TextSnapshotRange snapshotRange) {
			// Create an indicator tag
			var tag = new CustomIndicatorTag();
			tag.ContentProvider = new PlainTextContentProvider("Custom indicator created at " + DateTime.Now.ToLongTimeString());

			// Add the indicator tag (use a generic method provided on the indicator manager for custom indicators)
			editor.Document.IndicatorManager.Add<CustomIndicatorTagger, CustomIndicatorTag>(snapshotRange, tag);
		}
		
		/// <summary>
		/// Adds an indicator for the word at the specified offset.
		/// </summary>
		/// <param name="startOffset">The offset to examine.</param>
		private void AddIndicatorForWordAtOffset(int startOffset) { 
			var reader = editor.ActiveView.CurrentSnapshot.GetReader(startOffset);

			if (!reader.IsAtTokenStart) {
				reader.GoToCurrentWordStart();
				startOffset = reader.Offset;
			}

			reader.GoToCurrentWordEnd();
			var endOffset = reader.Offset;

			this.AddIndicator(new TextSnapshotRange(reader.Snapshot, startOffset, endOffset));
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnAddIndicatorButtonClick(object sender, RoutedEventArgs e) {
			// Validate
			if (editor.ActiveView.Selection.IsZeroLength) {
				MessageBox.Show("Please make a selection of at least one character.", "Add Indicator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}

			// Add an indicator
			this.AddIndicator(editor.ActiveView.Selection.SnapshotRange);

			// Focus the editor
			editor.Focus();
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnClearIndicatorsButtonClick(object sender, RoutedEventArgs e) {
			// Clear the tags (use a generic method provided on the indicator manager for custom indicators)
			editor.Document.IndicatorManager.Clear<CustomIndicatorTagger, CustomIndicatorTag>();

			// Focus the editor
			editor.Focus();
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnGoToNextIndicatorButtonClick(object sender, RoutedEventArgs e) {
			// Create search options
			var options = new TagSearchOptions<CustomIndicatorTag>();
			options.CanWrap = true;
			options.SearchUp = false;

			// Find the next indicator (use a generic method provided on the indicator manager for custom indicators)
			var tagRange = editor.Document.IndicatorManager.FindNext<CustomIndicatorTagger, CustomIndicatorTag>(editor.ActiveView.Selection.EndSnapshotOffset, options);
			if (tagRange != null) {
				// Move the caret
				editor.ActiveView.Selection.CaretOffset = tagRange.VersionRange.Translate(editor.ActiveView.CurrentSnapshot).StartOffset;
			}

			// Focus the editor
			editor.Focus();
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnGoToPreviousIndicatorButtonClick(object sender, RoutedEventArgs e) {
			// Create search options
			var options = new TagSearchOptions<CustomIndicatorTag>();
			options.CanWrap = true;
			options.SearchUp = true;

			// Find the previous indicator (use a generic method provided on the indicator manager for custom indicators)
			var tagRange = editor.Document.IndicatorManager.FindNext<CustomIndicatorTagger, CustomIndicatorTag>(editor.ActiveView.Selection.EndSnapshotOffset, options);
			if (tagRange != null) {
				// Move the caret
				editor.ActiveView.Selection.CaretOffset = tagRange.VersionRange.Translate(editor.ActiveView.CurrentSnapshot).StartOffset;
			}

			// Focus the editor
			editor.Focus();
		}
		
	}

}