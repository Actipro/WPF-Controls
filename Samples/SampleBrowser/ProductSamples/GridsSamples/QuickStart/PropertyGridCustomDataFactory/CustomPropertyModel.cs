using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;
using ActiproSoftware.Windows.Controls.Grids.PropertyEditors;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomDataFactory {

	/// <summary>
	/// Represents a <see cref="PropertyDescriptorPropertyModel"/> implementation that supports easy customization of several properties.
	/// </summary>
	public class CustomPropertyModel : PropertyDescriptorPropertyModel {

		private bool						customIsValueReadOnly;
		private IEnumerable					customStandardValues;
		private DataTemplate				customValueTemplate;
		private object						customValueTemplateKey;
		private DefaultValueTemplateKind	customValueTemplateKind			= DefaultValueTemplateKind.None;
		private DataTemplateSelector		customValueTemplateSelector;
		
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
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets whether the <see cref="Value"/> property is read-only.
		/// </summary>
		/// <value>
		/// <c>true</c> if the <see cref="Value"/> property is read-only; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// If set to <c>true</c>, this property value will ensure the <see cref="IPropertyModel.IsValueReadOnly"/> property returns <c>true</c>.
		/// </remarks>
		public bool CustomIsValueReadOnly {
			get {
				return customIsValueReadOnly;
			}
			set {
				if (customIsValueReadOnly != value) {
					customIsValueReadOnly = value;
					this.NotifyPropertyChanged("IsValueReadOnly");
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
		/// If supplied, this property value will be used as a return value for the <see cref="IPropertyModel.StandardValues"/> property.
		/// </remarks>
		public IEnumerable CustomStandardValues {
			get {
				return customStandardValues;
			}
			set {
				if (customStandardValues != value) {
					customStandardValues = value;
					this.NotifyPropertyChanged("StandardValues");
				}
			}
		}

		/// <summary>
		/// Gets or sets the custom <see cref="DataTemplate"/> to use for use for editing the property value.
		/// </summary>
		/// <value>The custom <see cref="DataTemplate"/> to use for use for editing the property value.</value>
		/// <remarks>
		/// If supplied, this property value will be used as a return value for the <see cref="IPropertyModel.ValueTemplate"/> property.
		/// </remarks>
		public DataTemplate CustomValueTemplate {
			get {
				return customValueTemplate;
			}
			set {
				if (customValueTemplate != value) {
					customValueTemplate = value;
					this.NotifyPropertyChanged("ValueTemplate");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the custom resource key that references a <see cref="DataTemplate"/> to use for editing the property value.
		/// </summary>
		/// <value>The custom resource key that references a <see cref="DataTemplate"/> to use for editing the property value.</value>
		/// <remarks>
		/// If supplied, this property value will be used as a return value for the <see cref="IPropertyModel.ValueTemplateKey"/> property.
		/// </remarks>
		public object CustomValueTemplateKey {
			get {
				return customValueTemplateKey;
			}
			set {
				if (customValueTemplateKey != value) {
					customValueTemplateKey = value;
					this.NotifyPropertyChanged("ValueTemplateKey");
				}
			}
		}

		/// <summary>
		/// Gets or sets the custom <see cref="DefaultValueTemplateKind"/> that specifies a default value cell template to use for editing the property value.
		/// </summary>
		/// <value>The custom <see cref="DefaultValueTemplateKind"/> that specifies a default value cell template to use for editing the property value.</value>
		/// <remarks>
		/// If supplied, this property value will be used as a return value for the <see cref="IPropertyModel.ValueTemplateKind"/> property.
		/// </remarks>
		public DefaultValueTemplateKind CustomValueTemplateKind {
			get {
				return customValueTemplateKind;
			}
			set {
				if (customValueTemplateKind != value) {
					customValueTemplateKind = value;
					this.NotifyPropertyChanged("ValueTemplateKind");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the custom <see cref="DataTemplateSelector"/> to use for use for editing the property value.
		/// </summary>
		/// <value>The custom <see cref="DataTemplateSelector"/> to use for use for editing the property value.</value>
		/// <remarks>
		/// If supplied, this property value will be used as a return value for the <see cref="IPropertyModel.ValueTemplateSelector"/> property.
		/// </remarks>
		public DataTemplateSelector CustomValueTemplateSelector {
			get {
				return customValueTemplateSelector;
			}
			set {
				if (customValueTemplateSelector != value) {
					customValueTemplateSelector = value;
					this.NotifyPropertyChanged("ValueTemplateSelector");
				}
			}
		}
		
		/// <summary>
		/// Gets whether the property supports <see cref="StandardValues"/>.
		/// </summary>
		/// <value>
		/// <c>true</c> if the property supports <see cref="StandardValues"/>; otherwise, <c>false</c>.
		/// </value>
		public override bool HasStandardValues {
			get {
				return (customStandardValues != null ? true : base.HasStandardValues);
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
				return (customStandardValues != null) || (base.IsLimitedToStandardValuesCore);
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
				return customIsValueReadOnly || base.IsValueReadOnlyCore;
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
				return customStandardValues ?? base.StandardValuesCore;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DataTemplate"/> to use for use for editing the property value.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use for use for editing the property value.</value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override DataTemplate ValueTemplateCore {
			get {
				return customValueTemplate ?? base.ValueTemplateCore;
			}
		}
		
		/// <summary>
		/// Gets the resource key that references a <see cref="DataTemplate"/> to use for editing the property value.
		/// </summary>
		/// <value>The resource key that references a <see cref="DataTemplate"/> to use for editing the property value.</value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override object ValueTemplateKeyCore {
			get {
				return customValueTemplateKey ?? base.ValueTemplateKeyCore;
			}
		}

		/// <summary>
		/// Gets the <see cref="DefaultValueTemplateKind"/> that specifies a default value cell template to use for editing the property value.
		/// </summary>
		/// <value>The <see cref="DefaultValueTemplateKind"/> that specifies a default value cell template to use for editing the property value.</value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override DefaultValueTemplateKind ValueTemplateKindCore {
			get {
				if (customValueTemplateKind != DefaultValueTemplateKind.None)
					return customValueTemplateKind;
				else
					return base.ValueTemplateKindCore;
			}
		}

		/// <summary>
		/// Gets the <see cref="DataTemplateSelector"/> to use for use for editing the property value.
		/// </summary>
		/// <value>The <see cref="DataTemplateSelector"/> to use for use for editing the property value.</value>
		/// <remarks>
		/// This property does the actual work of retrieving the value for the cached version of this property.
		/// </remarks>
		protected override DataTemplateSelector ValueTemplateSelectorCore {
			get {
				return customValueTemplateSelector ?? base.ValueTemplateSelectorCore;
			}
		}

	}

}
