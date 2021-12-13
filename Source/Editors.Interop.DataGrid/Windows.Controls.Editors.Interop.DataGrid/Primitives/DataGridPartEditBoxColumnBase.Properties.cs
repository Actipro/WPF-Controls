using System.Windows;
using ActiproSoftware.Windows.Controls.Editors.Primitives;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.Primitives {

	public partial class DataGridPartEditBoxColumnBase<T> {

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="CommitTriggers"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="CommitTriggers"/> dependency property.</value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static readonly DependencyProperty CommitTriggersProperty = DependencyProperty.Register("CommitTriggers", typeof(PartEditBoxCommitTriggers), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(PartEditBoxCommitTriggers.Default, NotifyPropertyChangeForRefreshContent));

		/// <summary>
		/// Identifies the <see cref="HasPopup"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="HasPopup"/> dependency property.</value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static readonly DependencyProperty HasPopupProperty = DependencyProperty.Register("HasPopup", typeof(bool), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(true, NotifyPropertyChangeForRefreshContent));

		/// <summary>
		/// Identifies the <see cref="IsArrowKeyPartNavigationEnabled"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsArrowKeyPartNavigationEnabled"/> dependency property.</value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static readonly DependencyProperty IsArrowKeyPartNavigationEnabledProperty = DependencyProperty.Register("IsArrowKeyPartNavigationEnabled", typeof(bool), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(true, NotifyPropertyChangeForRefreshContent));

		/// <summary>
		/// Identifies the <see cref="IsEditable"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsEditable"/> dependency property.</value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register("IsEditable", typeof(bool), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(true, NotifyPropertyChangeForRefreshContent));

		/// <summary>
		/// Identifies the <see cref="IsNullAllowed"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsNullAllowed"/> dependency property.</value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static readonly DependencyProperty IsNullAllowedProperty = DependencyProperty.Register("IsNullAllowed", typeof(bool), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(false, NotifyPropertyChangeForRefreshContent));
		
		/// <summary>
		/// Identifies the <see cref="IsUndoEnabled"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsUndoEnabled"/> dependency property.</value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static readonly DependencyProperty IsUndoEnabledProperty = DependencyProperty.Register("IsUndoEnabled", typeof(bool), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(true, NotifyPropertyChangeForRefreshContent));

		/// <summary>
		/// Identifies the <see cref="PlaceholderText"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="PlaceholderText"/> dependency property.</value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register("PlaceholderText", typeof(string), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(null, NotifyPropertyChangeForRefreshContent));

		/// <summary>
		/// Identifies the <see cref="SpinnerVisibility"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="SpinnerVisibility"/> dependency property.</value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static readonly DependencyProperty SpinnerVisibilityProperty = DependencyProperty.Register("SpinnerVisibility", typeof(SpinnerVisibility), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(SpinnerVisibility.Collapsed, NotifyPropertyChangeForRefreshContent));

		/// <summary>
		/// Identifies the <see cref="SpinWrapping"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="SpinWrapping"/> dependency property.</value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static readonly DependencyProperty SpinWrappingProperty = DependencyProperty.Register("SpinWrapping", typeof(SpinWrapping), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(SpinWrapping.NoWrap, NotifyPropertyChangeForRefreshContent));
		
		/// <summary>
		/// Identifies the <see cref="TextAlignment"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="TextAlignment"/> dependency property.</value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(TextAlignment.Left, NotifyPropertyChangeForRefreshContent));

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Applies standard values to the specified target element.
		/// </summary>
		/// <param name="targetElement">The target element.</param>
		protected virtual void ApplyStandardValues(FrameworkElement targetElement) {
			this.ApplyValue(CommitTriggersProperty, targetElement, PartEditBoxBase<T>.CommitTriggersProperty);
			this.ApplyValue(HasPopupProperty, targetElement, PartEditBoxBase<T>.HasPopupProperty);
			this.ApplyValue(IsArrowKeyPartNavigationEnabledProperty, targetElement, PartEditBoxBase<T>.IsArrowKeyPartNavigationEnabledProperty);
			this.ApplyValue(IsEditableProperty, targetElement, PartEditBoxBase<T>.IsEditableProperty);
			this.ApplyValue(IsNullAllowedProperty, targetElement, PartEditBoxBase<T>.IsNullAllowedProperty);
			this.ApplyValue(IsUndoEnabledProperty, targetElement, PartEditBoxBase<T>.IsUndoEnabledProperty);
			this.ApplyValue(PlaceholderTextProperty, targetElement, PartEditBoxBase<T>.PlaceholderTextProperty);
			this.ApplyValue(SpinnerVisibilityProperty, targetElement, PartEditBoxBase<T>.SpinnerVisibilityProperty);
			this.ApplyValue(SpinWrappingProperty, targetElement, PartEditBoxBase<T>.SpinWrappingProperty);
			this.ApplyValue(TextAlignmentProperty, targetElement, PartEditBoxBase<T>.TextAlignmentProperty);
		}

		/// <summary>
		/// Gets or sets the triggers that will force this control to commit any changes.
		/// </summary>
		/// <value>
		/// A <see cref="PartEditBoxCommitTriggers"/> indicating the triggers that will force this control to commit any changes.
		/// The default value is <c>PartEditBoxCommitTriggers.Default</c>.
		/// </value>
		public PartEditBoxCommitTriggers CommitTriggers {
			get {
				return (PartEditBoxCommitTriggers)this.GetValue(CommitTriggersProperty);
			}
			set {
				this.SetValue(CommitTriggersProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether the control has a popup available.
		/// </summary>
		/// <value>
		/// <c>true</c> if the control has a popup available; otherwise <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool HasPopup {
			get {
				return (bool)this.GetValue(HasPopupProperty);
			}
			set {
				this.SetValue(HasPopupProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether the left/right arrow keys can be used to move between and select editable parts.
		/// </summary>
		/// <value>
		/// <c>true</c> if the left/right arrow keys can be used to move between and select editable parts; otherwise <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsArrowKeyPartNavigationEnabled {
			get {
				return (bool)this.GetValue(IsArrowKeyPartNavigationEnabledProperty);
			}
			set {
				this.SetValue(IsArrowKeyPartNavigationEnabledProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether the edit box's text area is editable.
		/// </summary>
		/// <value>
		/// <c>true</c> if the edit box's text area is editable; otherwise <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		/// <remarks>
		/// When <c>false</c>, the edit box behaves more like a <c>ComboBox</c>.
		/// </remarks>
		public bool IsEditable {
			get {
				return (bool)this.GetValue(IsEditableProperty);
			}
			set {
				this.SetValue(IsEditableProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether null values are allowed to be entered by the user.
		/// </summary>
		/// <value>
		/// <c>true</c> if null values are allowed to be entered by the user; otherwise <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool IsNullAllowed {
			get {
				return (bool)this.GetValue(IsNullAllowedProperty);
			}
			set {
				this.SetValue(IsNullAllowedProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets whether undo/redo support is enabled for the text-editing portion of the control.
		/// </summary>
		/// <value>
		/// <c>true</c> if undo/redo support is enabled for the text-editing portion of the control.
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsUndoEnabled {
			get {
				return (bool)this.GetValue(IsUndoEnabledProperty);
			}
			set {
				this.SetValue(IsUndoEnabledProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the text that is displayed in the control until the value is changed by a user action or some other operation.
		/// </summary>
		/// <value>The text that is displayed in the control until the value is changed by a user action or some other operation.</value>
		public string PlaceholderText {
			get {
				return (string)this.GetValue(PlaceholderTextProperty);
			}
			set {
				this.SetValue(PlaceholderTextProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets a value indicating if and when the control has a spinner available.
		/// </summary>
		/// <value>
		/// A value indicating if and when the control has a spinner available.
		/// The default value is <c>SpinnerVisibility.Collapsed</c>.
		/// </value>
		public SpinnerVisibility SpinnerVisibility {
			get {
				return (SpinnerVisibility)this.GetValue(SpinnerVisibilityProperty);
			}
			set {
				this.SetValue(SpinnerVisibilityProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the wrapping behavior used when spinning past a minimum or maximum value in the active part.
		/// </summary>
		/// <value>
		/// The wrapping behavior used when spinning past a minimum or maximum value in the active part.
		/// The default value is <c>SpinWrapping.NoWrap</c>.
		/// </value>
		public SpinWrapping SpinWrapping {
			get {
				return (SpinWrapping)this.GetValue(SpinWrappingProperty);
			}
			set {
				this.SetValue(SpinWrappingProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets the text alignment of the text editing area.
		/// </summary>
		/// <value>
		/// The text alignment of the text editing area.
		/// The default value is <c>TextAlignment.Left</c>.
		/// </value>
		public TextAlignment TextAlignment {
			get {
				return (TextAlignment)this.GetValue(TextAlignmentProperty);
			}
			set {
				this.SetValue(TextAlignmentProperty, value);
			}
		}

	}

}
