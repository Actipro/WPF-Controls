using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace ActiproSoftware.SampleBrowser.Utilities.ColorBrowser {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		public static RoutedCommand CopyName = new RoutedCommand("CopyName", typeof(MainControl));
		public static RoutedCommand CopyStaticResourceBrush = new RoutedCommand("CopyStaticResourceBrush", typeof(MainControl));
		public static RoutedCommand CopyStaticResourceColor = new RoutedCommand("CopyStaticResourceColor", typeof(MainControl));

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Hide copy button if in XBAP
			if (BrowserInteropHelper.IsBrowserHosted) {
				this.contextMenu1.Visibility = Visibility.Collapsed;
				this.contextMenu2.Visibility = Visibility.Collapsed;
			}

			// Register class command bindings
			CommandManager.RegisterClassCommandBinding(typeof(ListBox), new CommandBinding(System.Windows.Input.ApplicationCommands.Copy, OnCopyExecuted, OnCopyCanExecute));
			CommandManager.RegisterClassCommandBinding(typeof(ListBox), new CommandBinding(MainControl.CopyName, OnCopyNameExecuted, OnCopyCanExecute));
			CommandManager.RegisterClassCommandBinding(typeof(ListBox), new CommandBinding(MainControl.CopyStaticResourceBrush, OnCopyStaticResourceBrushExecuted, OnCopyCanExecute));
			CommandManager.RegisterClassCommandBinding(typeof(ListBox), new CommandBinding(MainControl.CopyStaticResourceColor, OnCopyStaticResourceColorExecuted, OnCopyCanExecute));
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private static void OnCopyCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			ListBox listBox = sender as ListBox;
			if (null != listBox && -1 != listBox.SelectedIndex && !BrowserInteropHelper.IsBrowserHosted) {
				e.CanExecute = true;
				e.Handled = true;
			}
		}

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private static void OnCopyExecuted(object sender, ExecutedRoutedEventArgs e) {
			ListBox listBox = sender as ListBox;
			if (null != listBox && -1 != listBox.SelectedIndex && !BrowserInteropHelper.IsBrowserHosted) {
				NamedColor namedColor = listBox.SelectedItem as NamedColor;
				if (null == namedColor)
					return;

				Clipboard.SetText(namedColor.Color.ToString());
				e.Handled = true;
			}
		}

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private static void OnCopyNameExecuted(object sender, ExecutedRoutedEventArgs e) {
			ListBox listBox = sender as ListBox;
			if (null != listBox && -1 != listBox.SelectedIndex && !BrowserInteropHelper.IsBrowserHosted) {
				NamedColor namedColor = listBox.SelectedItem as NamedColor;
				if (null == namedColor)
					return;

				Clipboard.SetText(namedColor.Name);
				e.Handled = true;
			}
		}

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private static void OnCopyStaticResourceBrushExecuted(object sender, ExecutedRoutedEventArgs e) {
			ListBox listBox = sender as ListBox;
			if (null != listBox && -1 != listBox.SelectedIndex && !BrowserInteropHelper.IsBrowserHosted) {
				NamedColor namedColor = listBox.SelectedItem as NamedColor;
				if (null == namedColor || !namedColor.IsSystemColor)
					return;

				Clipboard.SetText(String.Format("{{StaticResource {{x:Static SystemColors.{0}BrushKey}}}}", namedColor.Name));
				e.Handled = true;
			}
		}
	
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private static void OnCopyStaticResourceColorExecuted(object sender, ExecutedRoutedEventArgs e) {
			ListBox listBox = sender as ListBox;
			if (null != listBox && -1 != listBox.SelectedIndex && !BrowserInteropHelper.IsBrowserHosted) {
				NamedColor namedColor = listBox.SelectedItem as NamedColor;
				if (null == namedColor || !namedColor.IsSystemColor)
					return;

				Clipboard.SetText(String.Format("{{StaticResource {{x:Static SystemColors.{0}ColorKey}}}}", namedColor.Name));
				e.Handled = true;
			}
		}
	}
}