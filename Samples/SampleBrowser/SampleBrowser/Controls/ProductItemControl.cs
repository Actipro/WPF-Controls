using ActiproSoftware.Windows.Input;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Renders the user interface of a <see cref="ProductItemInfo"/>.
	/// </summary>
	[ContentProperty("Child")]
	public class ProductItemControl : Control {
		
		private DelegateCommand<object> toggleIsSideBarVisibleCommand;

		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="Child"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Child"/> dependency property.</value>
		public static readonly DependencyProperty ChildProperty = DependencyProperty.Register("Child", typeof(UIElement), typeof(ProductItemControl), new FrameworkPropertyMetadata(null, OnChildPropertyValueChanged));
		
		/// <summary>
		/// Identifies the <see cref="IsSideBarVisible"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsSideBarVisible"/> dependency property.</value>
		public static readonly DependencyProperty IsSideBarVisibleProperty = DependencyProperty.Register("IsSideBarVisible", typeof(bool), typeof(ProductItemControl), new FrameworkPropertyMetadata(true));
		
		/// <summary>
		/// Identifies the <see cref="SideBarContent"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="SideBarContent"/> dependency property.</value>
		public static readonly DependencyProperty SideBarContentProperty = DependencyProperty.Register("SideBarContent", typeof(UIElement), typeof(ProductItemControl), new FrameworkPropertyMetadata(null));
		
		/// <summary>
		/// Identifies the <see cref="SideBarWidth"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="SideBarWidth"/> dependency property.</value>
		public static readonly DependencyProperty SideBarWidthProperty = DependencyProperty.Register("SideBarWidth", typeof(double), typeof(ProductItemControl), new FrameworkPropertyMetadata(400.0));
		
		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ProductItemControl"/> class.
		/// </summary>
		public ProductItemControl() {
			this.DefaultStyleKey = typeof(ProductItemControl);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the property value has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> containing data related to this event.</param>
		private static void OnChildPropertyValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
			var control = (ProductItemControl)sender;
			var oldChild = e.OldValue as UIElement;
			var newChild = e.NewValue as UIElement;

			if (oldChild != null)
				control.RemoveLogicalChild(oldChild);
			if (newChild != null)
				control.AddLogicalChild(newChild);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the child element.
		/// </summary>
		/// <value>
		/// The child element.
		/// </value>
		public UIElement Child {
			get {
				return (UIElement)this.GetValue(ChildProperty);
			}
			set {
				this.SetValue(ChildProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets whether the sidebar is visible.
		/// </summary>
		/// <value>
		/// <c>true</c> if the sidebar is visible; otherwise, <c>false</c>.
		/// </value>
		public bool IsSideBarVisible {
			get {
				return (bool)this.GetValue(IsSideBarVisibleProperty);
			}
			set {
				this.SetValue(IsSideBarVisibleProperty, value);
			}
		}
		
		/// <summary>
		/// Gets an enumerator for logical child elements of this element.
		/// </summary>
		/// <value>An enumerator for logical child elements of this element.</value>
		protected override IEnumerator LogicalChildren {
			get {
				var child = this.Child;
				if (child != null)
					yield return child;
			}
		}

		/// <summary>
		/// Notifies the UI that it has been unloaded.
		/// </summary>
		public virtual void NotifyUnloaded() {}
		
		/// <summary>
		/// Gets or sets the sidebar content element.
		/// </summary>
		/// <value>
		/// The sidebar content element.
		/// </value>
		public UIElement SideBarContent {
			get {
				return (UIElement)this.GetValue(SideBarContentProperty);
			}
			set {
				this.SetValue(SideBarContentProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the sidebar width.
		/// </summary>
		/// <value>The sidebar width.</value>
		public double SideBarWidth {
			get {
				return (double)this.GetValue(SideBarWidthProperty);
			}
			set {
				this.SetValue(SideBarWidthProperty, value);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that toggles whether the sidebar is open.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that toggles whether the sidebar is open.
		/// </value>
		public ICommand ToggleIsSideBarVisibleCommand {
			get {
				if (toggleIsSideBarVisibleCommand == null) {
					toggleIsSideBarVisibleCommand = new DelegateCommand<object>((param) => { 
						this.IsSideBarVisible = !this.IsSideBarVisible;
					});
				}

				return toggleIsSideBarVisibleCommand;
			}
		}
		
	}

}
