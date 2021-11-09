using System.Windows;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid {

	public partial class DataGridMaskedStringColumn {

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="IsCaseAutoCorrected"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsCaseAutoCorrected"/> dependency property.</value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "AutoCorrected")]
		public static readonly DependencyProperty IsCaseAutoCorrectedProperty = DependencyProperty.Register("IsCaseAutoCorrected", typeof(bool), typeof(DataGridMaskedStringColumn), new PropertyMetadata(false, NotifyPropertyChangeForRefreshContent));

		/// <summary>
		/// Identifies the <see cref="IsCaseSensitive"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsCaseSensitive"/> dependency property.</value>
		public static readonly DependencyProperty IsCaseSensitiveProperty = DependencyProperty.Register("IsCaseSensitive", typeof(bool), typeof(DataGridMaskedStringColumn), new PropertyMetadata(false, NotifyPropertyChangeForRefreshContent));
		
		/// <summary>
		/// Identifies the <see cref="Mask"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Mask"/> dependency property.</value>
		public static readonly DependencyProperty MaskProperty = DependencyProperty.Register("Mask", typeof(string), typeof(DataGridMaskedStringColumn), new PropertyMetadata(null, NotifyPropertyChangeForRefreshContent));
		
		/// <summary>
		/// Identifies the <see cref="MaskKind"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="MaskKind"/> dependency property.</value>
		public static readonly DependencyProperty MaskKindProperty = DependencyProperty.Register("MaskKind", typeof(MaskKind), typeof(DataGridMaskedStringColumn), new PropertyMetadata(MaskKind.Regex, NotifyPropertyChangeForRefreshContent));

		/// <summary>
		/// Identifies the <see cref="PromptChar"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="PromptChar"/> dependency property.</value>
		public static readonly DependencyProperty PromptCharProperty = DependencyProperty.Register("PromptChar", typeof(char), typeof(DataGridMaskedStringColumn), new PropertyMetadata('\u2022', NotifyPropertyChangeForRefreshContent));
		
		/// <summary>
		/// Identifies the <see cref="PromptVisibility"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="PromptVisibility"/> dependency property.</value>
		public static readonly DependencyProperty PromptVisibilityProperty = DependencyProperty.Register("PromptVisibility", typeof(MaskPromptVisibility), typeof(DataGridMaskedStringColumn), new PropertyMetadata(MaskPromptVisibility.FocusedOnly, NotifyPropertyChangeForRefreshContent));

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Applies standard values to the specified target element.
		/// </summary>
		/// <param name="targetElement">The target element.</param>
		protected virtual void ApplyStandardValues(FrameworkElement targetElement) {
			this.ApplyValue(IsCaseAutoCorrectedProperty, targetElement, MaskedTextBox.IsCaseAutoCorrectedProperty);
			this.ApplyValue(IsCaseSensitiveProperty, targetElement, MaskedTextBox.IsCaseSensitiveProperty);
			this.ApplyValue(MaskProperty, targetElement, MaskedTextBox.MaskProperty);
			this.ApplyValue(MaskKindProperty, targetElement, MaskedTextBox.MaskKindProperty);
			this.ApplyValue(PromptCharProperty, targetElement, MaskedTextBox.PromptCharProperty);
			this.ApplyValue(PromptVisibilityProperty, targetElement, MaskedTextBox.PromptVisibilityProperty);
		}

		/// <summary>
		/// Gets or sets whether characters entered will have their case auto-corrected to match the defined mask.
		/// </summary>
		/// <value>
		/// <c>true</c> if characters entered will have their case auto-corrected to match the defined mask; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		/// <remarks>
		/// When this property is set to <c>true</c>, then the <see cref="IsCaseSensitive"/> property is ignored.
		/// </remarks>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "AutoCorrected")]
		public bool IsCaseAutoCorrected {
			get {
				return (bool)this.GetValue(IsCaseAutoCorrectedProperty);
			}
			set {
				this.SetValue(IsCaseAutoCorrectedProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets whether the mask is case sensitive.
		/// </summary>
		/// <value>
		/// <c>true</c> if the mask is case sensitive; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		/// <remarks>
		/// When the <see cref="IsCaseAutoCorrected"/> property is set to <c>true</c>, this property is ignored.
		/// </remarks>
		public bool IsCaseSensitive {
			get {
				return (bool)this.GetValue(IsCaseSensitiveProperty);
			}
			set {
				this.SetValue(IsCaseSensitiveProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the text that designates the input mask.
		/// </summary>
		/// <value>
		/// The text that designates the input mask.
		/// The default value is <see langword="null"/>.
		/// </value>
		public string Mask {
			get {
				return (string)this.GetValue(MaskProperty);
			}
			set {
				this.SetValue(MaskProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the kind of input mask.
		/// </summary>
		/// <value>
		/// The kind of input mask.
		/// The default value is <c>Regex</c>.
		/// </value>
		public MaskKind MaskKind {
			get {
				return (MaskKind)this.GetValue(MaskKindProperty);
			}
			set {
				this.SetValue(MaskKindProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets the character used to indicate required input.
		/// </summary>
		/// <value>
		/// The character used to indicate required input.
		/// The default value is <c>\u2022</c>.
		/// </value>
		public char PromptChar {
			get {
				return (char)this.GetValue(PromptCharProperty);
			}
			set {
				this.SetValue(PromptCharProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the visibility of mask prompts.
		/// </summary>
		/// <value>
		/// A <see cref="MaskPromptVisibility"/> that indicates the visibility of mask prompts.
		/// The default value is <c>FocusedOnly</c>.
		/// </value>
		public MaskPromptVisibility PromptVisibility {
			get {
				return (MaskPromptVisibility)this.GetValue(PromptVisibilityProperty);
			}
			set {
				this.SetValue(PromptVisibilityProperty, value);
			}
		}
		
	}

}
