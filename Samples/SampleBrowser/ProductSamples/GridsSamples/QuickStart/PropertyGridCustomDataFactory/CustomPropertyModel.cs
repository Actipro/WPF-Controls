using ActiproSoftware.Windows.Controls.Grids.PropertyData;
using ActiproSoftware.Windows.Controls.Grids.PropertyEditors;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomDataFactory;

/// <summary>
/// Represents a <see cref="PropertyDescriptorPropertyModel"/> implementation that supports easy customization of several properties.
/// </summary>
/// <param name="target">The target object that owns the property.</param>
/// <param name="propertyDescriptor">The <see cref="PropertyDescriptor"/> for the property be accessed on the <paramref name="target"/>.</param>
public class CustomPropertyModel(object target, PropertyDescriptor propertyDescriptor) : PropertyDescriptorPropertyModel(target, propertyDescriptor) {

	private bool _customIsValueReadOnly;
	private IEnumerable? _customStandardValues;
	private DataTemplate? _customValueTemplate;
	private object? _customValueTemplateKey;
	private DefaultValueTemplateKind _customValueTemplateKind = DefaultValueTemplateKind.None;
	private DataTemplateSelector? _customValueTemplateSelector;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether the <see cref="Value"/> property is read-only.
	/// </summary>
	/// <remarks>
	/// If set to <c>true</c>, this property value will ensure the <see cref="IPropertyModel.IsValueReadOnly"/> property returns <c>true</c>.
	/// </remarks>
	public bool CustomIsValueReadOnly {
		get => _customIsValueReadOnly;
		set {
			if (SetProperty(ref _customIsValueReadOnly, value)) {
				// Invalidate any cached value for the IPropertyModel property since the custom value has changed
				OnPropertyChanged(nameof(IsValueReadOnly));
			}
		}
	}

	/// <summary>
	/// The standard list of values for the <see cref="Value"/> property.
	/// </summary>
	/// <remarks>
	/// If supplied, this property value will be used as a return value for the <see cref="IPropertyModel.StandardValues"/> property.
	/// </remarks>
	public IEnumerable? CustomStandardValues {
		get => _customStandardValues;
		set {
			if (SetProperty(ref _customStandardValues, value)) {
				// Invalidate any cached value for the IPropertyModel property since the custom value has changed
				OnPropertyChanged(nameof(StandardValues));
			}
		}
	}

	/// <summary>
	/// The custom <see cref="DataTemplate"/> to use for use for editing the property value.
	/// </summary>
	/// <remarks>
	/// If supplied, this property value will be used as a return value for the <see cref="IPropertyModel.ValueTemplate"/> property.
	/// </remarks>
	public DataTemplate? CustomValueTemplate {
		get => _customValueTemplate;
		set {
			if (SetProperty(ref _customValueTemplate, value)) {
				// Invalidate any cached value for the IPropertyModel property since the custom value has changed
				OnPropertyChanged(nameof(ValueTemplate));
			}
		}
	}

	/// <summary>
	/// The custom resource key that references a <see cref="DataTemplate"/> to use for editing the property value.
	/// </summary>
	/// <remarks>
	/// If supplied, this property value will be used as a return value for the <see cref="IPropertyModel.ValueTemplateKey"/> property.
	/// </remarks>
	public object? CustomValueTemplateKey {
		get => _customValueTemplateKey;
		set {
			if (SetProperty(ref _customValueTemplateKey, value)) {
				// Invalidate any cached value for the IPropertyModel property since the custom value has changed
				OnPropertyChanged(nameof(ValueTemplateKey));
			}
		}
	}

	/// <summary>
	/// The custom <see cref="DefaultValueTemplateKind"/> that specifies a default value cell template to use for editing the property value.
	/// </summary>
	/// <remarks>
	/// If supplied, this property value will be used as a return value for the <see cref="IPropertyModel.ValueTemplateKind"/> property.
	/// </remarks>
	public DefaultValueTemplateKind CustomValueTemplateKind {
		get => _customValueTemplateKind;
		set {
			if (SetProperty(ref _customValueTemplateKind, value)) {
				// Invalidate any cached value for the IPropertyModel property since the custom value has changed
				OnPropertyChanged(nameof(ValueTemplateKind));
			}
		}
	}

	/// <summary>
	/// The custom <see cref="DataTemplateSelector"/> to use for use for editing the property value.
	/// </summary>
	/// <remarks>
	/// If supplied, this property value will be used as a return value for the <see cref="IPropertyModel.ValueTemplateSelector"/> property.
	/// </remarks>
	public DataTemplateSelector? CustomValueTemplateSelector {
		get => _customValueTemplateSelector;
		set {
			if (SetProperty(ref _customValueTemplateSelector, value)) {
				// Invalidate any cached value for the IPropertyModel property since the custom value has changed
				OnPropertyChanged(nameof(ValueTemplateSelector));
			}
		}
	}

	/// <inheritdoc/>
	public override bool HasStandardValues
		=> (_customStandardValues is not null) || base.HasStandardValues;

	/// <inheritdoc/>
	protected override bool IsLimitedToStandardValuesCore
		=> (_customStandardValues is not null) || base.IsLimitedToStandardValuesCore;

	/// <inheritdoc/>
	protected override bool IsValueReadOnlyCore
		=> _customIsValueReadOnly || base.IsValueReadOnlyCore;

	/// <inheritdoc/>
	protected override IEnumerable? StandardValuesCore
		=> _customStandardValues ?? base.StandardValuesCore;

	/// <inheritdoc/>
	protected override DataTemplate? ValueTemplateCore
		=> _customValueTemplate ?? base.ValueTemplateCore;

	/// <inheritdoc/>
	protected override object? ValueTemplateKeyCore
		=> _customValueTemplateKey ?? base.ValueTemplateKeyCore;

	/// <inheritdoc/>
	protected override DefaultValueTemplateKind ValueTemplateKindCore
		=> (_customValueTemplateKind == DefaultValueTemplateKind.None)
			? base.ValueTemplateKindCore
			: _customValueTemplateKind;

	/// <inheritdoc/>
	protected override DataTemplateSelector? ValueTemplateSelectorCore
		=> _customValueTemplateSelector ?? base.ValueTemplateSelectorCore;

}
