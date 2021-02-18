using System.Collections;
using System.ComponentModel;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDynamicProperties {

	/// <summary>
	/// Represents a <see cref="PropertyDescriptorPropertyModel"/> implementation that supports easy customization of several properties.
	/// </summary>
	public class CustomPropertyModel : PropertyDescriptorPropertyModel {

		private bool? isVisible;
		private bool isUpdating;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <see cref="CustomPropertyModel"/> class.
		/// </summary>
		/// <param name="target">The target object that owns the property.</param>
		/// <param name="propertyDescriptor">The <see cref="PropertyDescriptor"/> for the property be accessed on the <paramref name="target"/>.</param>
		public CustomPropertyModel(object target, PropertyDescriptor propertyDescriptor) : base(target, propertyDescriptor) {}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Invalidates whether the property is read-only.
		/// </summary>
		private void InvalidateIsReadOnly() {
			this.NotifyPropertyChanged("IsValueReadOnly");
			this.NotifyPropertyChanged("IsReadOnly");
		}
		
		/// <summary>
		/// Invalidates whether the property is visible.
		/// </summary>
		private void InvalidateIsVisible() {
			isVisible = null;
			this.NotifyPropertyChanged("IsVisible");
		}
		
		/// <summary>
		/// Invalidates the property's standard values.
		/// </summary>
		private void InvalidateStandardValues() {
			this.NotifyPropertyChanged("StandardValues");
			this.NotifyPropertyChanged("StandardValuesAsStrings");
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets whether the property supports <see cref="StandardValues"/>.
		/// </summary>
		/// <value>
		/// <c>true</c> if the property supports <see cref="StandardValues"/>; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override bool HasStandardValuesCore {
			get {
				var provider = this.Target as IDynamicPropertyStateProvider;
				if ((provider != null) && (provider.GetPropertyHasStandardValues(this.Name)))
					return true;

				return base.HasStandardValuesCore;
			}
		}
		
		/// <summary>
		/// Gets whether the <see cref="Value"/> property can only be set to one of the values defined by the <see cref="StandardValues"/> property.
		/// </summary>
		/// <value>
		/// <c>true</c> if the <see cref="Value"/> property can only be set to one of the values defined by the <see cref="StandardValues"/> property; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override bool IsLimitedToStandardValuesCore {
			get {
				var provider = this.Target as IDynamicPropertyStateProvider;
				if ((provider != null) && (provider.GetPropertyHasStandardValues(this.Name)))
					return true;

				return base.IsLimitedToStandardValuesCore;
			}
		}
		
		/// <summary>
		/// Gets whether the <see cref="Value"/> property is read-only.
		/// </summary>
		/// <value>
		/// <c>true</c> if the <see cref="Value"/> property is read-only; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override bool IsValueReadOnlyCore {
			get {
				var provider = this.Target as IDynamicPropertyStateProvider;
				if (provider != null)
					return provider.GetPropertyReadOnly(this.Name);

				return base.IsValueReadOnlyCore;
			}
		}
		
		/// <summary>
		/// Gets a value indicating whether the property should be visible.
		/// </summary>
		/// <value><c>true</c> if the property should be visible; otherwise, <c>false</c>.</value>
		public bool IsVisible {
			get {
				if (!this.isVisible.HasValue) {
					var provider = this.Target as IDynamicPropertyStateProvider;
					if (provider != null)
						this.isVisible = provider.GetPropertyVisibility(this.Name);
					else
						this.isVisible = true;
				}

				return this.isVisible.Value;
			}
		}

		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event.
		/// </summary>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> that contains the event data.</param>
		protected override void OnPropertyChanged(PropertyChangedEventArgs e) {
			// Call the base method
			base.OnPropertyChanged(e);

			if ((!isUpdating) && (e.PropertyName == "ValueAsString")) {
				try {
					isUpdating = true;

					this.InvalidateIsReadOnly();
					this.InvalidateIsVisible();

					var provider = this.Target as IDynamicPropertyStateProvider;
					if ((provider != null) && (provider.GetPropertyHasStandardValues(this.Name)))
						this.InvalidateStandardValues();
				}
				finally {
					isUpdating = false;
				}
			}
		}
		
		/// <summary>
		/// Gets the standard list of values for the <see cref="Value"/> property.
		/// </summary>
		/// <value>
		/// The standard list of values for the <see cref="Value"/> property.
		/// </value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override IEnumerable StandardValuesCore {
			get {
				var provider = this.Target as IDynamicPropertyStateProvider;
				if (provider != null) {
					var values = provider.GetPropertyStandardValues(this.Name);
					if (values != null)
						return values;
				}

				return base.StandardValuesCore;
			}
		}

		/// <summary>
		/// Gets or sets the property value.
		/// </summary>
		/// <value>The property value.</value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override object ValueCore {
			get {
				return base.ValueCore;
			}
			set {
				if (!isUpdating)
					base.ValueCore = value;
			}
		}

	}

}
