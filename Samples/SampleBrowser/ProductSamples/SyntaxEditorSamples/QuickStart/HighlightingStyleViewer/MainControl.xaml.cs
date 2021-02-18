using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.HighlightingStyleViewer {

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

			// Create a custom Console Window registry
			IHighlightingStyleRegistry consoleWindowRegistry = new HighlightingStyleRegistry();
			consoleWindowRegistry.Description = "Console Window";
			console.HighlightingStyleRegistry = consoleWindowRegistry;

			// Register the default display item classification types on the ambient and custom registries
			new DisplayItemClassificationTypeProvider().RegisterAll();
			new DisplayItemClassificationTypeProvider(consoleWindowRegistry).RegisterAll();

			// Populate the registry combobox
			registryComboBox.Items.Add(AmbientHighlightingStyleRegistry.Instance);
			registryComboBox.Items.Add(consoleWindowRegistry);
			registryComboBox.SelectedIndex = 0;
        }
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the selection is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="SelectionChangedEventArgs"/> that contains the event data.</param>
		private void OnRegistryComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (classificationTypeListBox.Items.Count > 0)
				classificationTypeListBox.SelectedIndex = 0;
		}

    }

}