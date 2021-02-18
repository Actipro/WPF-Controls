using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.Primitives;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid {

	/// <summary>
	/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="EnumEditBox"/> control.
	/// </summary>
	public class DataGridEnumColumn : DataGridPartEditBoxColumnBase<object> {
	
		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="EnumSortComparer"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="EnumSortComparer"/> dependency property.</value>
		public static readonly DependencyProperty EnumSortComparerProperty = DependencyProperty.Register("EnumSortComparer", typeof(IComparer<Enum>), typeof(DataGridEnumColumn), new PropertyMetadata(null));

		/// <summary>
		/// Identifies the <see cref="EnumType"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="EnumType"/> dependency property.</value>
		public static readonly DependencyProperty EnumTypeProperty = DependencyProperty.Register("EnumType", typeof(Type), typeof(DataGridEnumColumn), new PropertyMetadata(null));

		/// <summary>
		/// Identifies the <see cref="UseDisplayAttributes"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="UseDisplayAttributes"/> dependency property.</value>
		public static readonly DependencyProperty UseDisplayAttributesProperty = DependencyProperty.Register("UseDisplayAttributes", typeof(bool), typeof(DataGridEnumColumn), new PropertyMetadata(false));
		
		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <see cref="DataGridEnumColumn"/> class.
		/// </summary>
		public DataGridEnumColumn() {
			this.IsArrowKeyPartNavigationEnabled = false;
			this.IsEditable = false;
			this.SpinWrapping = SpinWrapping.SimpleWrap;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Applies standard values to the specified target element.
		/// </summary>
		/// <param name="targetElement">The target element.</param>
		protected override void ApplyStandardValues(FrameworkElement targetElement) {
			base.ApplyStandardValues(targetElement);
			if (targetElement is EnumEditBox) {
				this.ApplyValue(EnumSortComparerProperty, targetElement, EnumEditBox.EnumSortComparerProperty);
				this.ApplyValue(EnumTypeProperty, targetElement, EnumEditBox.EnumTypeProperty);
				this.ApplyValue(UseDisplayAttributesProperty, targetElement, EnumEditBox.UseDisplayAttributesProperty);
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="IComparer{Enum}"/> used to sort the enumeration values.
		/// </summary>
		/// <value>
		/// The <see cref="IComparer{Enum}"/> used to sort the enumeration values; otherwise <see langword="null"/> to indicate no sorting, which will use the order the enumeration values are defined.
		/// </value>
		public IComparer<Enum> EnumSortComparer {
			get { 
				return (IComparer<Enum>)this.GetValue(EnumSortComparerProperty); 
			}
			set { 
				this.SetValue(EnumSortComparerProperty, value); 
			}
		}

		/// <summary>
		/// Gets or sets the enumeration type.
		/// </summary>
		/// <value>The enumeration type.</value>
		public Type EnumType {
			get {
				// WINRTNOTE: Have to use a try-cast here since UWP sometimes temporarily passes a COM object
				return this.GetValue(EnumTypeProperty) as Type;
			}
			set { 
				this.SetValue(EnumTypeProperty, value); 
			}
		}

		/// <summary>
		/// Gets the type of the associated <c>PartEditBoxBase</c>-derived control.
		/// </summary>
		/// <returns>The type of the associated <c>PartEditBoxBase</c>-derived control.</returns>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected override Type GetEditBoxType() {
			return typeof(EnumEditBox);
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether enumeration values should be displayed using an associated <c>DisplayAttribute</c>, if any.
		/// </summary>
		/// <value>
		/// <c>true</c> if enumeration values should be displayed using an associated <c>DisplayAttribute</c>, if any; otherwise <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool UseDisplayAttributes {
			get { 
				return (bool)this.GetValue(UseDisplayAttributesProperty); 
			}
			set { 
				this.SetValue(UseDisplayAttributesProperty, value); 
			}
		}

	}

}
