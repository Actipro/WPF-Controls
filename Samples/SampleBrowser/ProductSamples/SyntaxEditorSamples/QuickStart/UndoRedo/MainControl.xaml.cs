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
using System.Windows.Shapes;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Text.Undo;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.UndoRedo {

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

			// Since the Text assembly's undo stack is .NET 2.0-based and INotifyCollectionChanged is in .NET 3.0, 
			//   we have a ObservableUndoableTextChangeStack wrapper class in the SyntaxEditor assembly that 
			//   can be used for UI databinding
			redoStack.ItemsSource = new ObservableUndoableTextChangeStack(editor.Document.UndoHistory.RedoStack);
			undoStack.ItemsSource = new ObservableUndoableTextChangeStack(editor.Document.UndoHistory.UndoStack);
        }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnAppendButtonClick(object sender, RoutedEventArgs e) {
			editor.Document.AppendText(CustomChangeType.Instance, "\nAppended with custom change type.");
			editor.Focus();
		}

		/// <summary>
		/// Occurs when a mouse is double-clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="MouseButtonEventArgs"/> that contains the event data.</param>
		private void OnRedoListBoxDoubleClick(object sender, MouseButtonEventArgs e) {
			ListBox listBox = (ListBox)sender;
			IUndoableTextChange textChange = listBox.SelectedItem as IUndoableTextChange;
			if (textChange != null) {
				int index = editor.Document.UndoHistory.RedoStack.IndexOf(textChange);
				if (index != -1) {
					editor.Document.UndoHistory.Redo(index + 1);
					editor.Focus();
				}
			}
		}
		
		/// <summary>
		/// Occurs when a mouse is double-clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="MouseButtonEventArgs"/> that contains the event data.</param>
		private void OnUndoListBoxDoubleClick(object sender, MouseButtonEventArgs e) {
			ListBox listBox = (ListBox)sender;
			IUndoableTextChange textChange = listBox.SelectedItem as IUndoableTextChange;
			if (textChange != null) {
				int index = editor.Document.UndoHistory.UndoStack.IndexOf(textChange);
				if (index != -1) {
					editor.Document.UndoHistory.Undo(index + 1);
					editor.Focus();
				}
			}
		}
		
    }

}