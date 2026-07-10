using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.JavaScript.Implementation;
using ActiproSoftware.Text.Languages.Python.Implementation;
using ActiproSoftware.Text.Languages.VB.Implementation;
using ActiproSoftware.Text.Languages.Xml.Implementation;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Extensions;

namespace ActiproSoftware.ProductSamples.DockingSamples.Demo.SimpleIde;

/// <summary>
/// Represents an editor view.
/// </summary>
public partial class EditorDocumentWindow : DocumentWindow {

	private bool _hasPendingParseData;

	private CSharpSyntaxLanguage? _cSharpSyntaxLanguage;
	private JavaScriptSyntaxLanguage? _javaScriptSyntaxLanguage;
	private PythonSyntaxLanguage? _pythonSyntaxLanguage;
	private VBSyntaxLanguage? _vbSyntaxLanguage;
	private XmlSyntaxLanguage? _xmlSyntaxLanguage;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public EditorDocumentWindow() {
		InitializeComponent();
	}

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="data">The document data.</param>
	/// <param name="text">The text to show in the editor.</param>
	public EditorDocumentWindow(DocumentData data, string? text) : this() {
		if (data is null)
			throw new ArgumentNullException(nameof(data));

		Data = data;
		AssignLanguageAndTextForFileType(text);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Assign a language and default text based on the current file type.
	/// </summary>
	/// <param name="text">The text to show in the editor.</param>
	private void AssignLanguageAndTextForFileType(string? text) {
		var requiresDefaultText = (text is null);
		if (!requiresDefaultText)
			editor.Document.SetText(text!);

		var extension = Path.GetExtension(Data.FileName)?.ToLowerInvariant();
		editor.Document.Language = GetOrCreateLanguage(extension);
		if (requiresDefaultText)
			editor.Document.SetText(GetDefaultText(extension));

		// Update symbol selector visibility
		symbolSelectorBorder.Visibility = (editor.Document.Language.GetNavigableSymbolProvider() is not null ? Visibility.Visible : Visibility.Collapsed);
		symbolSelector.AreMemberSymbolsSupported = (editor.Document.Language.Key != "Python");
	}

	/// <summary>
	/// The document data.
	/// </summary>
	private DocumentData Data {
		get => (DocumentData)DataContext;
		set {
			DataContext = value;

			this.BindToProperty(FileNameProperty, value, nameof(DocumentData.FileName), BindingMode.OneWay);
			this.BindToProperty(TitleProperty, value, nameof(DocumentData.Title), BindingMode.OneWay);
		}
	}

	/// <summary>
	/// Returns the default text for the specified extension.
	/// </summary>
	/// <param name="extension">The file extension.</param>
	private static string GetDefaultText(string? extension) {
		switch (extension) {
			case ".cs":
				return @"using System;

public class Class1 {

	public Class1() {
	}

}
";
			case ".js":
				return @"// JavaScript source code
";
			case ".py":
				return @"# Python source code
";
			case ".vb":
				return @"Imports Microsoft.VisualBasic

Public Class Class1

End Class
";
			case ".xml":
				return @"<?xml version=""1.0"" encoding=""utf-8""?>
";
			default:
				return string.Empty;
		}
	}

	/// <summary>
	/// Returns a language for the specified extension.
	/// </summary>
	/// <param name="extension">The file extension.</param>
	/// <returns>The <see cref="ISyntaxLanguage"/> to use.</returns>
	private ISyntaxLanguage GetOrCreateLanguage(string? extension) {
		switch (extension) {
			case ".cs":
				if (_cSharpSyntaxLanguage is null) {
					_cSharpSyntaxLanguage = new CSharpSyntaxLanguage();

					var cSharpProjectAssembly = new CSharpProjectAssembly("Sample");
					var assemblyLoader = new BackgroundWorker();
					assemblyLoader.DoWork += (sender, e) => {
						// Add some common assemblies for reflection (any custom assemblies could be added using various Add overloads instead)
						SyntaxEditorHelper.AddCommonDotNetSystemAssemblyReferences(cSharpProjectAssembly);
					};
					assemblyLoader.RunWorkerAsync();
					_cSharpSyntaxLanguage.RegisterProjectAssembly(cSharpProjectAssembly);
				}
				return _cSharpSyntaxLanguage;

			case ".js":
				return _javaScriptSyntaxLanguage ??= new JavaScriptSyntaxLanguage();

			case ".py":
				return _pythonSyntaxLanguage ??= new PythonSyntaxLanguage();

			case ".vb":
				if (_vbSyntaxLanguage is null) {
					_vbSyntaxLanguage = new VBSyntaxLanguage();

					var vbProjectAssembly = new VBProjectAssembly("Sample");
					var assemblyLoader = new BackgroundWorker();
					assemblyLoader.DoWork += (sender, e) => {
						// Add some common assemblies for reflection (any custom assemblies could be added using various Add overloads instead)
						SyntaxEditorHelper.AddCommonDotNetSystemAssemblyReferences(vbProjectAssembly);
					};
					assemblyLoader.RunWorkerAsync();
					_vbSyntaxLanguage.RegisterProjectAssembly(vbProjectAssembly);
				}
				return _vbSyntaxLanguage;

			case ".xml":
				return _xmlSyntaxLanguage ??= new XmlSyntaxLanguage();

			default:
				return SyntaxLanguage.PlainText;
		}
	}

	/// <summary>
	/// Occurs when the document's modified state changes.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnEditorDocumentIsModifiedChanged(object sender, RoutedEventArgs e)
		=> Data.IsModified = editor.Document.IsModified;

	/// <summary>
	/// Occurs when the document's parse data has changed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnEditorDocumentParseDataChanged(object sender, RoutedEventArgs e) {
		//
		// NOTE: The parse data here is generated in a worker thread... this event handler is called
		//   back in the UI thread immediately when the worker thread completes... it is best
		//   practice to delay UI updates until the end user stops typing... we will flag that
		//   there is a pending parse data change, which will be handled in the
		//   UserInterfaceUpdate event
		//

		_hasPendingParseData = true;
	}

	/// <summary>
	/// Occurs when a search operation occurs in a view.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnEditorViewSearch(object sender, EditorViewSearchEventArgs e)
		=> Data.NotifySearchAction?.Invoke(this, e.ResultSet);

	/// <summary>
	/// Occurs after a brief delay following any document text, parse data, or view selection update, allowing consumers to update the user interface during an idle period.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnEditorUserInterfaceUpdate(object sender, RoutedEventArgs e) {
		// If there is a pending parse data change...
		if (_hasPendingParseData) {
			// Clear flag
			_hasPendingParseData = false;

			Data.NotifyDocumentOutlineUpdated?.Invoke(this);
		}
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The embedded <see cref="SyntaxEditor"/> control.
	/// </summary>
	public SyntaxEditor Editor
		=> editor; // Defined in XAML

}
