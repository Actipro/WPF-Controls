using System;
using System.Windows;
using System.Windows.Data;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.Editors;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CurrentLineHighlighting {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Load a language from a language definition
			editor.Document.Language = ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("Css.langdef");

			// Register the default display item classification types on the ambient registry
			DisplayItemClassificationTypeProvider provider = new DisplayItemClassificationTypeProvider();
			provider.RegisterAll();

			// Get the style for the current line and bind it to the edit box
			IHighlightingStyle style = AmbientHighlightingStyleRegistry.Instance[provider.CurrentLine];
			Binding valueBinding = new Binding();
			valueBinding.Source = style;
			valueBinding.Path = new PropertyPath("Background");
			valueBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			colorEditbox.SetBinding(ColorEditBox.ValueProperty, valueBinding);
		}

	}

}