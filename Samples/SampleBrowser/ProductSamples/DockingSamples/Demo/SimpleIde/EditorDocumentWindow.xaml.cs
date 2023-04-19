using System;
using System.IO;
using System.Windows;
using System.Windows.Data;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Reflection;
using ActiproSoftware.Text.Languages.JavaScript.Implementation;
using ActiproSoftware.Text.Languages.Python.Implementation;
using ActiproSoftware.Text.Languages.VB.Implementation;
using ActiproSoftware.Text.Languages.Xml.Implementation;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Extensions;
using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.DockingSamples.Demo.SimpleIde {

	/// <summary>
	/// Represents an editor view.
	/// </summary>
	public partial class EditorDocumentWindow : DocumentWindow {
		
		private bool hasPendingParseData;
		
		private CSharpSyntaxLanguage cSharpSyntaxLanguage;
		private JavaScriptSyntaxLanguage javaScriptSyntaxLanguage;
		private PythonSyntaxLanguage pythonSyntaxLanguage;
		private VBSyntaxLanguage vbSyntaxLanguage;
		private XmlSyntaxLanguage xmlSyntaxLanguage;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>EditorDocumentWindow</c> class.
		/// </summary>
		/// <param name="data">The document data.</param>
		/// <param name="text">The text to show in the editor.</param>
		public EditorDocumentWindow(DocumentData data, string text) : this() {
			if (data == null)
				throw new ArgumentNullException("data");

			this.Data = data;
			this.AssignLanguageAndTextForFileType(text);
		}

		/// <summary>
		/// Initializes an instance of the <c>EditorDocumentWindow</c> class.
		/// </summary>
		public EditorDocumentWindow() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Assign a language and default text based on the current file type.
		/// </summary>
		/// <param name="text">The text to show in the editor.</param>
		private void AssignLanguageAndTextForFileType(string text) {
			var requiresDefaultText = (text == null);
			if (!requiresDefaultText)
				editor.Document.SetText(text);

			var extension = Path.GetExtension(this.Data.FileName).ToLowerInvariant();
			editor.Document.Language = this.GetOrCreateLanguage(extension);
			if (requiresDefaultText)
				editor.Document.SetText(this.GetDefaultText(extension));

			// Update symbol selector visibility
			symbolSelectorBorder.Visibility = (editor.Document.Language.GetNavigableSymbolProvider() != null ? Visibility.Visible : Visibility.Collapsed);
			symbolSelector.AreMemberSymbolsSupported = (editor.Document.Language.Key != "Python");
		}

		/// <summary>
		/// Gets the document data.
		/// </summary>
		/// <value>The document data.</value>
		private DocumentData Data {
			get {
				return (DocumentData)this.DataContext;
			}
			set {
				this.DataContext = value;

				this.BindToProperty(DocumentWindow.FileNameProperty, value, "FileName", BindingMode.OneWay);
				this.BindToProperty(DocumentWindow.TitleProperty, value, "Title", BindingMode.OneWay);
			}
		}

		/// <summary>
		/// Returns the default text for the specified extension.
		/// </summary>
		/// <param name="extension">The file extension.</param>
		/// <returns>The default text to use.</returns>
		private string GetDefaultText(string extension) {
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
					return String.Empty;
			}
		}

		/// <summary>
		/// Returns a language for the specified extension.
		/// </summary>
		/// <param name="extension">The file extension.</param>
		/// <returns>The <see cref="ISyntaxLanguage"/> to use.</returns>
		private ISyntaxLanguage GetOrCreateLanguage(string extension) {
			switch (extension) {
				case ".cs":
					if (cSharpSyntaxLanguage == null) {
						cSharpSyntaxLanguage = new CSharpSyntaxLanguage();

						var cSharpProjectAssembly = new CSharpProjectAssembly("Sample");
						var assemblyLoader = new BackgroundWorker();
						assemblyLoader.DoWork += (sender, e) => {
							// Add some common assemblies for reflection (any custom assemblies could be added using various Add overloads instead)
							SyntaxEditorHelper.AddCommonDotNetSystemAssemblyReferences(cSharpProjectAssembly);
						};
						assemblyLoader.RunWorkerAsync();
						cSharpSyntaxLanguage.RegisterProjectAssembly(cSharpProjectAssembly);
					}
					return cSharpSyntaxLanguage;
				case ".js":
					if (javaScriptSyntaxLanguage == null)
						javaScriptSyntaxLanguage = new JavaScriptSyntaxLanguage();
					return javaScriptSyntaxLanguage;
				case ".py":
					if (pythonSyntaxLanguage == null)
						pythonSyntaxLanguage = new PythonSyntaxLanguage();
					return pythonSyntaxLanguage;
				case ".vb":
					if (vbSyntaxLanguage == null) {
						vbSyntaxLanguage = new VBSyntaxLanguage();

						var vbProjectAssembly = new VBProjectAssembly("Sample");
						var assemblyLoader = new BackgroundWorker();
						assemblyLoader.DoWork += (sender, e) => {
							// Add some common assemblies for reflection (any custom assemblies could be added using various Add overloads instead)
							SyntaxEditorHelper.AddCommonDotNetSystemAssemblyReferences(vbProjectAssembly);
						};
						assemblyLoader.RunWorkerAsync();
						vbSyntaxLanguage.RegisterProjectAssembly(vbProjectAssembly);
					}
					return vbSyntaxLanguage;
				case ".xml":
					if (xmlSyntaxLanguage == null)
						xmlSyntaxLanguage = new XmlSyntaxLanguage();
					return xmlSyntaxLanguage;
				default:
					return SyntaxLanguage.PlainText;
			}
		}

		/// <summary>
		/// Occurs when the document's modified state changes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnEditorDocumentIsModifiedChanged(object sender, RoutedEventArgs e) {
			this.Data.IsModified = editor.Document.IsModified;
		}
		
		/// <summary>
		/// Occurs when the document's parse data has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>EventArgs</c> that contains data related to this event.</param>
		private void OnEditorDocumentParseDataChanged(object sender, RoutedEventArgs e) {
			//
			// NOTE: The parse data here is generated in a worker thread... this event handler is called 
			//         back in the UI thread immediately when the worker thread completes... it is best
			//         practice to delay UI updates until the end user stops typing... we will flag that
			//         there is a pending parse data change, which will be handled in the 
			//         UserInterfaceUpdate event
			//

			hasPendingParseData = true;
		}

		/// <summary>
		/// Occurs when a search operation occurs in a view.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="EditorViewSearchEventArgs"/> that contains the event data.</param>
		private void OnEditorViewSearch(object sender, EditorViewSearchEventArgs e) {
			if (this.Data.NotifySearchAction != null)
				this.Data.NotifySearchAction(this, e.ResultSet);
		}
		
		/// <summary>
		/// Occurs after a brief delay following any document text, parse data, or view selection update, allowing consumers to update the user interface during an idle period.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains data related to this event.</param>
		private void OnEditorUserInterfaceUpdate(object sender, RoutedEventArgs e) {
			// If there is a pending parse data change...
			if (hasPendingParseData) {
				// Clear flag
				hasPendingParseData = false;

				if (this.Data.NotifyDocumentOutlineUpdated != null)
					this.Data.NotifyDocumentOutlineUpdated(this);
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the embedded <see cref="SyntaxEditor"/> control.
		/// </summary>
		/// <value>The embedded <see cref="SyntaxEditor"/> control.</value>
		public SyntaxEditor Editor {
			get {
				return editor;
			}
		}

	}

}
