using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Input;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.BarsIntegration {

	/// <summary>
	/// Provides the main window for this sample.
	/// </summary>
	public partial class MainWindow {

		private ICommand dockWindowCommand;
		private ICommand floatWindowCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainWindow</c> class.
		/// </summary>
		public MainWindow() {
			UpdateAppFocusProperties(apply: true);

			InitializeComponent();

			this.Loaded += this.OnLoaded;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Returns the active <see cref="DockingWindow"/>.
		/// </summary>
		/// <returns>The active <see cref="DockingWindow"/>.</returns>
		private DockingWindow GetActiveDockingWindow() {
			return dockSite.ActiveWindow ?? dockSite.PrimaryDocument;
		}

		/// <summary>
		/// Occurs when the element is loaded
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLoaded(object sender, RoutedEventArgs e) {
			// Activate the first document if there is no active docking window
			if (dockSite.ActiveWindow == null)
				dockSite.DocumentWindows.FirstOrDefault()?.Activate();
		}

		/// <summary>
		/// Updates application focus properties so that menus interaction works better when opened from a floating window.
		/// </summary>
		/// <param name="apply">Whether to apply focus properties.</param>
		/// <returns>
		/// <c>true</c> if a change was made; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// These kinds of properties would normally be set once in application startup logic.
		/// However since this is simply a sample, we are setting them at window creation, and clearing them on window close.
		/// </remarks>
		private static bool UpdateAppFocusProperties(bool apply) {
			if (apply) {
				if (HwndSource.DefaultAcquireHwndFocusInMenuMode) {
					HwndSource.DefaultAcquireHwndFocusInMenuMode = false;
					Keyboard.DefaultRestoreFocusMode = RestoreFocusMode.None;

					return true;
				}
			}
			else if (!HwndSource.DefaultAcquireHwndFocusInMenuMode) {
				HwndSource.DefaultAcquireHwndFocusInMenuMode = true;
				Keyboard.DefaultRestoreFocusMode = RestoreFocusMode.Auto;

				return true;
			}

			return false;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the command to dock a docking window.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand DockWindowCommand {
			get {
				if (dockWindowCommand == null) {
					dockWindowCommand = new DelegateCommand<object>(
						p => {
							var dockingWindow = (p as DockingWindow) ?? GetActiveDockingWindow();

							switch (dockingWindow) {
								case ToolWindow toolWindow:
									toolWindow.Dock();
									break;
								case DocumentWindow documentWindow:
									documentWindow.MoveToMdi(dockSite.PrimaryDockHost);
									documentWindow.Activate();
									break;
							}
						}
					);
				}

				return dockWindowCommand;
			}
		}
		
		/// <summary>
		/// Gets the command to float a docking window.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand FloatWindowCommand {
			get {
				if (floatWindowCommand == null) {
					floatWindowCommand = new DelegateCommand<object>(
						p => {
							var dockingWindow = (p as DockingWindow) ?? GetActiveDockingWindow();

							dockingWindow?.Float();
						}
					);
				}

				return floatWindowCommand;
			}
		}

		/// <inheritdoc/>
		protected override void OnClosed(EventArgs e) {
			base.OnClosed(e);

			UpdateAppFocusProperties(apply: false);
		}

	}

}