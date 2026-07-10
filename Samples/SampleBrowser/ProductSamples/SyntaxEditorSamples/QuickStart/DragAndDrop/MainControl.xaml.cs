using ActiproSoftware.Windows.Controls.SyntaxEditor;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DragAndDrop;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Load a language from a language definition
		editor.Document.Language = Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");

		var lt = Environment.NewLine;

		// Initialize the toolbox with snippets of text
		var textSnippets = new[] {
			new TextSnippet(displayText: "Class", snippet: $"class ClassName {{{lt}{lt}}}{lt}"),
			new TextSnippet(displayText: "Enum", snippet: $"enum EnumName {{{lt}{lt}}}{lt}"),
			new TextSnippet(displayText: "For Loop", snippet: $"for (var index = 0; index < count; index++) {{{lt}{lt}}}{lt}"),
			new TextSnippet(displayText: "For Loop (Reverse)", snippet: $"for (var index = count - 1; index >= 0; index--) {{{lt}{lt}}}{lt}"),
			new TextSnippet(displayText: "Interface", snippet: $"interface InterfaceName {{{lt}{lt}}}{lt}"),
			new TextSnippet(displayText: "Method", snippet: $"void MethodName() {{{lt}\tthrow new NotImplementedException();{lt}}}{lt}"),
			new TextSnippet(displayText: "Multi-Line Comment", snippet: $"/*{lt} * Multi-line comment{lt} */{lt}"),
			new TextSnippet(displayText: "Namespace", snippet: $"namespace NamespaceName {{{lt}{lt}}}{lt}"),
			new TextSnippet(displayText: "Property", snippet: $"object PropertyName {{{lt}\tget => throw new NotImplementedException();{lt}\tset => throw new NotImplementedException();{lt}}}{lt}"),
			new TextSnippet(displayText: "Property (Read-Only)", snippet: $"object PropertyName {{{lt}\tget => throw new NotImplementedException();{lt}}}{lt}"),
			new TextSnippet(displayText: "Region", snippet: $"#region{lt}{lt}#endregion{lt}"),
		};
		foreach (var textSnippet in textSnippets.OrderBy(s => s.DisplayText))
			toolbox.Items.Add(textSnippet);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs before text is cut or copied to the clipboard, and also before a drag occurs.
	/// </summary>
	private void OnEditorCutCopyDrag(object sender, CutCopyDragEventArgs e) {
		// NOTE: This event can be used to monitor drag operations and optionally override the text that is used
		Debug.WriteLine($"OnEditorCutCopyDrag; Action={e.Action}; Text={e.DataStore.GetText()}; HasHtmlData={e.DataStore.GetDataPresent(DataFormats.Html)}; HasRtfData={e.DataStore.GetDataPresent(DataFormats.Rtf)}");

		if ((e.Action == CutCopyDragAction.Drag) && (shouldOverrideDragText.IsChecked == true)) {
			// Override the default text with custom text
			e.DataStore.SetText(overrideDragText.Text, DataStoreTextKind.Default);
		}
	}

	/// <summary>
	/// Occurs when a paste or drag/drop operation occurs over the control, allowing for customization of the text to be inserted.
	/// </summary>
	private void OnEditorPasteDragDrop(object sender, PasteDragDropEventArgs e) {
		// NOTE: This event can be used to monitor drag operations and optionally override the text that is used
		Debug.WriteLine($"OnEditorPasteDragDrop; Action={e.Action}; Text={e.Text}; IDataObject={e.DataStore.ToDataObject()?.GetType().Name}");

		if ((e.Action == PasteDragDropAction.DragDrop) && (shouldCancelDrop.IsChecked == true)) {
			// Cancel by coercing the effect
			e.DragEventArgs!.Effects = DragDropEffects.None;
			MessageBox.Show("Drop Canceled");
			return;
		}

		if (shouldOverrideDropText.IsChecked == true) {
			// Override the dropped text with custom text
			e.Text = overrideDropText.Text;

			// Make sure the effect indicates that copy is allowed (since text may not have been available when effects were initialized)
			if (
				e.DragEventArgs!.Effects == DragDropEffects.None
				&& e.DragEventArgs.AllowedEffects.HasFlag(DragDropEffects.Copy)
			) {
				e.DragEventArgs.Effects = DragDropEffects.Copy;
			}
		}
		else if (e.Text is null) {

			// NOTE: The PasteDragDropEventArgs.Text property is initialized to the text automatically extracted
			//   from the drag source. If the property is NULL, that indicates the text could not be determined.
			//   Custom drag sources can be analyzed here and their representative text assigned to the
			//   PasteDragDropEventArgs.Text property to control the dropped text.

			if (e.DataStore.GetDataPresent(typeof(TextSnippet).FullName!)) {
				//
				// Custom TextSnippet Object
				//
				var textSnippet = e.DataStore.GetData(typeof(TextSnippet).FullName!) as TextSnippet;
				e.Text = textSnippet!.Snippet;
				e.DragEventArgs!.Effects = DragDropEffects.Copy;
			}
			else if (e.DataStore.GetDataPresent(DataFormats.FileDrop)) {
				//
				// FileDrop
				//
				if (e.DataStore.GetData(DataFormats.FileDrop) is string[] { Length: 1 } allFiles) {
					var filePath = allFiles[0];
					if (!string.IsNullOrWhiteSpace(filePath)) {
						if (e.Action == PasteDragDropAction.DragDrop) {
							try {
								if (editor.Document.IsReadOnly) {
									// Cancel the default drop behavior
									e.DragEventArgs!.Effects = DragDropEffects.None;

									// Prompt to open the file to replace the current file
									if (MessageBoxResult.Yes == MessageBox.Show($"Do you want open the file '{Path.GetFileName(filePath)}'?", "Open File?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No)) {
										editor.Document.LoadFile(filePath);
									}
								}
								else {
									// Prompt to insert the context of the dropped file
									if (MessageBoxResult.Yes == MessageBox.Show($"Do you want to insert the contents of the file '{Path.GetFileName(filePath)}'?", "Insert File Contents?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No)) {
										// Indicate the text to be inserted
										e.Text = File.ReadAllText(filePath);
										// The default effect for FileDrop is 'None', so assign an effect to allow the custom text to be inserted.
										e.DragEventArgs!.Effects = DragDropEffects.Copy;
									}
								}
							}
							catch (Exception ex) {
								Debug.WriteLine(ex);
								MessageBox.Show("Error processing file.  " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
							}
						}
						else {
							// Customize the drag operation to indicate the 'Copy' effect will be used.
							e.DragEventArgs!.Effects = DragDropEffects.Copy;
						}
					}
				}
			}
			else {
				//
				// Unknown Drag Source
				//
				var dataObject = e.DataStore.ToDataObject();
				var customData = $"Optionally build custom text to be inserted for any data source; SourceType={dataObject?.GetType().Name}";
				e.Text = customData;
			}
		}
	}

	private void OnToolboxPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
		//
		// Attempt to initiate a Drag operation from the Toolbox
		//

		// Find the UIElement at the mouse position
		var element = toolbox.InputHitTest(e.GetPosition(toolbox)) as UIElement;
		Debug.WriteLine($"OnToolboxPreviewMouseLeftButtonDown; Element={element?.GetType().Name}");
		if (element is not null) {
			// Find the object associated with the UIElement
			var data = DependencyProperty.UnsetValue;
			while (data == DependencyProperty.UnsetValue) {
				data = toolbox.ItemContainerGenerator.ItemFromContainer(element);
				if (data == DependencyProperty.UnsetValue)
					element = VisualTreeHelper.GetParent(element) as UIElement;
				if (element == toolbox)
					return;
			}

			if (data is TextSnippet textSnippet) {
				// Initialize the drag using IDataObject
				var dataObject = new DataObject(DataFormats.Text, textSnippet.Snippet!);
				DragDrop.DoDragDrop(toolbox, dataObject, DragDropEffects.Copy);
			}
		}
	}

	private void OnStringDragSourcePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
		if (sender is UIElement element) {
			// Drag data can be initialized directly from a string
			DragDrop.DoDragDrop(element, $"Custom String Generated at {DateTime.Now:t}", DragDropEffects.Copy);
		}
	}

	private void OnCustomDragSourcePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
		if (sender is UIElement element) {
			// Drag data can be set to a custom object as long as the object can be serialized
			var customObject = new TextSnippet(displayText: "Custom Object", snippet: "This is the text of a custom TextSnippet object.");
			DragDrop.DoDragDrop(element, customObject, DragDropEffects.Copy);
		}
	}
}
