using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDynamicProperties;

/// <summary>
/// Represents a <see cref="PropertyDescriptorPropertyModel"/> implementation that supports easy customization of several properties.
/// </summary>
/// <param name="target">The target object that owns the property.</param>
/// <param name="propertyDescriptor">The <see cref="PropertyDescriptor"/> for the property be accessed on the <paramref name="target"/>.</param>
public class CustomPropertyModel(object target, PropertyDescriptor propertyDescriptor) : PropertyDescriptorPropertyModel(target, propertyDescriptor) {

	private bool? _isVisible;
	private bool _isUpdating;

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Invalidates whether the property is read-only.
	/// </summary>
	private void InvalidateIsReadOnly() {
		OnPropertyChanged(nameof(IsValueReadOnly));
		OnPropertyChanged(nameof(IsReadOnly));
	}

	/// <summary>
	/// Invalidates whether the property is visible.
	/// </summary>
	private void InvalidateIsVisible() {
		_isVisible = null;
		OnPropertyChanged(nameof(IsVisible));
	}

	/// <summary>
	/// Invalidates the property's standard values.
	/// </summary>
	private void InvalidateStandardValues() {
		OnPropertyChanged(nameof(StandardValues));
		OnPropertyChanged(nameof(StandardValuesAsStrings));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override bool HasStandardValuesCore {
		get {
			var provider = Target as IDynamicPropertyStateProvider;
			if ((Name is not null) && (provider?.GetPropertyHasStandardValues(Name) == true))
				return true;

			return base.HasStandardValuesCore;
		}
	}

	/// <inheritdoc/>
	protected override bool IsLimitedToStandardValuesCore {
		get {
			var provider = Target as IDynamicPropertyStateProvider;
			if ((Name is not null) && (provider?.GetPropertyHasStandardValues(Name) == true))
				return true;

			return base.IsLimitedToStandardValuesCore;
		}
	}

	/// <inheritdoc/>
	protected override bool IsValueReadOnlyCore {
		get {
			var provider = Target as IDynamicPropertyStateProvider;
			if ((Name is not null) && (provider is not null))
				return provider.GetPropertyReadOnly(Name);

			return base.IsValueReadOnlyCore;
		}
	}

	/// <summary>
	/// Indicates whether the property should be visible.
	/// </summary>
	public bool IsVisible {
		get {
			if (!_isVisible.HasValue) {
				var provider = Target as IDynamicPropertyStateProvider;
				_isVisible = (provider is null) || (Name is null) || provider.GetPropertyVisibility(Name);
			}

			return _isVisible.Value;
		}
	}

	/// <inheritdoc/>
	protected override void OnPropertyChanged(PropertyChangedEventArgs e) {
		base.OnPropertyChanged(e);

		if ((!_isUpdating) && (e.PropertyName == nameof(ValueAsString))) {
			try {
				_isUpdating = true;

				InvalidateIsReadOnly();
				InvalidateIsVisible();

				var provider = Target as IDynamicPropertyStateProvider;
				if ((Name is not null) && (provider?.GetPropertyHasStandardValues(Name) == true))
					InvalidateStandardValues();
			}
			finally {
				_isUpdating = false;
			}
		}
	}

	/// <inheritdoc/>
	protected override IEnumerable? StandardValuesCore {
		get {
			var provider = Target as IDynamicPropertyStateProvider;
			return ((Name is not null) && (provider is not null))
				? provider.GetPropertyStandardValues(Name)
				: base.StandardValuesCore;
		}
	}

	/// <inheritdoc/>
	protected override object? ValueCore {
		get => base.ValueCore;
		set {
			if (!_isUpdating)
				base.ValueCore = value;
		}
	}

}
