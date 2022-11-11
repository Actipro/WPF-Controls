using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides the root window for the application.
	/// </summary>
	public partial class RootWindow {

		private ApplicationOverlayMode currentOverlayMode;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>RootWindow </c> class.
		/// </summary>
		public RootWindow() {
			InitializeComponent();

			this.UpdateOverlayUI();

			this.ViewModel.PropertyChanged += this.OnViewModelPropertyChanged;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Focuses the specified content element.
		/// </summary>
		/// <param name="contentElement">The <see cref="FrameworkElement"/> to focus.</param>
		private void FocusContentElement(FrameworkElement contentElement) {
			var firstFocusableElement = VisualTreeHelperExtended.GetFirstFocusableDescendant(contentElement) as FrameworkElement;
			if (firstFocusableElement != null) {
				// If a DockSite is the first focusable element, try to activate a docking window
				var dockSite = firstFocusableElement as DockSite;
				if (dockSite != null) {
					if (dockSite.Documents.Count > 0) {
						dockSite.ActivatePrimaryDocument();
						return;
					}
					if (dockSite.ToolWindows.Count > 0) {
						dockSite.ToolWindows[0].Activate();
						return;
					}
				}
			}

			// Move focus to the first focusable element (if it is still in the presenter)
			contentElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
		}

		/// <summary>
		/// Occurs when a transition is completed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedPropertyChangedEventArgs{Object}"/> that contains the event data.</param>
		private void OnTransitionPresenterTransitionCompleted(object sender, RoutedPropertyChangedEventArgs<object> e) {
			if (e.OriginalSource == rootPresenter) {
				if (rootPresenter.Content is FrameworkElement contentElement) {
					this.Dispatcher.BeginInvoke(DispatcherPriority.Input, (Action)(() => {
						var currentItemInfo = this.ViewModel.ViewItemInfo;
						if ((currentItemInfo?.CanFocusOnLoad ?? false) && (this.IsActive) && (rootPresenter.Content == contentElement)) {
							// There is no need to focus within the content element if focus is already there
							if (!contentElement.IsKeyboardFocusWithin)
								this.FocusContentElement(contentElement);
						}
					}));
				}
			}
		}

		/// <summary>
		/// Occurs when a property is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> that contains the event data.</param>
		private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
			switch (e?.PropertyName) {
				case nameof(ApplicationViewModel.IsBackstageOpen): {
					var rootPresenterIsVisible = !((this.ViewModel.ViewHasInterop) && (this.ViewModel.IsBackstageOpen));
					rootPresenter.Visibility = (rootPresenterIsVisible ? Visibility.Visible : Visibility.Hidden);
					break;
				}
				case nameof(ApplicationViewModel.OverlayMode):
					this.UpdateOverlayUI();
					break;
			}
		}

		/// <summary>
		/// Updates the overlay UI.
		/// </summary>
		private void UpdateOverlayUI() {
			var chrome = WindowChrome.GetChrome(this);

			// When the overlay mode changes, ensure we clear the overlay content to ensure a switch between the Backstage modes executes the template selector again
			var viewModel = this.ViewModel;
			if (currentOverlayMode != viewModel.OverlayMode) {
				WindowChrome.SetOverlayContent(this, null);
				currentOverlayMode = viewModel.OverlayMode;
			}

			switch (viewModel.OverlayMode) {
				case ApplicationOverlayMode.ExternalSample:
					WindowChrome.SetOverlayAnimationKinds(this, OverlayAnimationKinds.Fade);
					WindowChrome.SetOverlayContent(this, "Loading external sample...");
					this.SetBinding(WindowChrome.IsOverlayVisibleProperty, new Binding() { Source = ViewModel, Path = new PropertyPath(nameof(ApplicationViewModel.IsLoadingExternalSample)), Mode = BindingMode.TwoWay });
					BindingOperations.ClearBinding(this, WindowChrome.UseAlternateTitleBarStyleProperty);
					break;
				default:  // Backstage (all kinds)
					WindowChrome.SetOverlayAnimationKinds(this, OverlayAnimationKinds.FadeSlide);
					WindowChrome.SetOverlayContent(this, viewModel);
					this.SetBinding(WindowChrome.IsOverlayVisibleProperty, new Binding() { Source = ViewModel, Path = new PropertyPath(nameof(ApplicationViewModel.IsBackstageOpen)), Mode = BindingMode.TwoWay });
					this.SetBinding(WindowChrome.UseAlternateTitleBarStyleProperty, new Binding() { Source = ViewModel, Path = new PropertyPath(nameof(ApplicationViewModel.IsBackstageOpen)), Mode = BindingMode.TwoWay });
					break;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when a key is pressed while the control has focus.
		/// </summary>
		/// <param name="e">A <see cref="KeyEventArgs"/> that contains the event data.</param>
		protected override void OnKeyDown(KeyEventArgs e) {
			if (e == null)
				throw new ArgumentNullException("e");

			// Call the base method
			base.OnKeyDown(e);

			if (!e.Handled) {
				switch (e.Key) {
					case Key.Escape:
						// Ensure the Backstage is closed when Esc is pressed
						this.ViewModel.IsBackstageOpen = false;
						break;
					case Key.F9:
						// Write out the focused element
						System.Diagnostics.Debug.WriteLine(DateTime.Now + "RootWindow.OnKeyDown: FocusedElement=" + Keyboard.FocusedElement);
						break;
				}
			}
		}

		/// <summary>
		/// Occurs when a mouse button is released.
		/// </summary>
		/// <param name="e">A <see cref="MouseButtonEventArgs"/> that contains the event data.</param>
		protected override void OnMouseUp(MouseButtonEventArgs e) {
			if (e == null)
				throw new ArgumentNullException("e");

			// Call the base method
			base.OnMouseUp(e);

			// Look for unhandled navigation buttons
			if (!e.Handled) {
				switch (e.ChangedButton) {
					case MouseButton.XButton1:
						this.ViewModel.NavigateViewBackward();
						break;
					case MouseButton.XButton2:
						this.ViewModel.NavigateViewForward();
						break;
				}
			}
		}

		/// <summary>
		/// Gets the view-model for this view.
		/// </summary>
		/// <value>The view-model for this view.</value>
		public ApplicationViewModel ViewModel => (ApplicationViewModel)this.DataContext;

	}

}
