using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a gallery item within a bar gallery control.
	/// </summary>
	/// <typeparam name="TValue">The type of the value associated with this gallery item.</typeparam>
	public class BarGalleryItemViewModel<TValue> : ObservableObjectBase, IBarGalleryItemViewModel {

		private static bool? isEnumValueType;

		private string category;
		private string description;
		private string keyTipText;
		private string label;
		private BarGalleryItemLayoutBehavior layoutBehavior = BarGalleryItemLayoutBehavior.Default;
		private ImageSource imageSource;
		private TValue value;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		protected BarGalleryItemViewModel()
			: this(value: default, category: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value.
		/// </summary>
		/// <param name="value">The item's value.</param>
		protected BarGalleryItemViewModel(TValue value)
			: this(value, category: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value and category.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		protected BarGalleryItemViewModel(TValue value, string category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value, category, and label.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		protected BarGalleryItemViewModel(TValue value, string category, string label) {
			this.value = value;
			this.category = category;
			this.label = label;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		object IBarGalleryItemViewModel.Value {
			get => this.Value;
			set => this.Value = (TValue)value;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Tests if the value type of this class is an enumeration.
		/// </summary>
		/// <value><c>true</c> if the value type of this class is an enumeration; otherwise <c>false</c>.</value>
		private static bool IsEnumValueType => (isEnumValueType ??= typeof(TValue).IsEnum);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IBarGalleryItemViewModel.Category"/>
		public string Category {
			get => category;
			set {
				if (category != value) {
					category = value;
					this.NotifyPropertyChanged(nameof(Category));
				}
			}
		}

		/// <summary>
		/// Gets the coerced value to use as the <see cref="Label"/> when an explicit value has not been defined.
		/// </summary>
		/// <returns>The coerced text label to display.</returns>
		/// <seealso cref="Label"/>
		protected virtual string CoerceLabel() {
			return IsEnumValueType
				? ConvertEnumValueToString(typeof(TValue), Value, useAttributes: true)
				: Value?.ToString();
		}

		/// <summary>
		/// Converts the specified enumeration value to a string representation.
		/// </summary>
		/// <param name="enumValue">The enumeration value.</param>
		/// <param name="useAttributes">Whether to use description or display attributes.</param>
		/// <typeparam name="TEnum">The type of the enumeration.</typeparam>
		/// <returns>A string representation of the specified value or <c>null</c> if the value type is not an enumeration.</returns>
		protected static string ConvertEnumValueToString<TEnum>(TEnum enumValue, bool useAttributes)
			where TEnum : struct, IComparable, IFormattable /* CLS-compliant constraints based on System.Enum interfaces */ {

			return ConvertEnumValueToString(typeof(TEnum), enumValue, useAttributes);
		}

		/// <summary>
		/// Converts the specified enumeration value to a string representation.
		/// </summary>
		/// <param name="enumType">The enumeration <see cref="Type"/> to examine.</param>
		/// <param name="enumValue">The enumeration value.</param>
		/// <param name="useAttributes">Whether to use description or display attributes.</param>
		/// <returns>A string representation of the specified value or <c>null</c> if the value and/or type is not recognized as an enumeration.</returns>
		protected static string ConvertEnumValueToString(Type enumType, object enumValue, bool useAttributes) {
			// Null values are already null strings
			if (enumValue == null)
				return null;

			// Ignore non-enum types
			if ((enumType is null) || (!enumType.IsEnum))
				return null;

			string valueText = enumValue.ToString();

			if ((useAttributes) && (!string.IsNullOrEmpty(valueText))) {
				var fieldInfo = enumType.GetField(valueText);
				if (fieldInfo != null) {
					var attributeText = fieldInfo.GetCustomAttribute<DescriptionAttribute>()?.Description;
					if (attributeText is null) {
						var displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();
						attributeText = displayAttribute?.GetName() ?? displayAttribute?.GetShortName();
					}
					if (!string.IsNullOrEmpty(attributeText))
						return attributeText;
				}
			}

			return valueText;
		}

		/// <inheritdoc cref="IBarGalleryItemViewModel.Description"/>
		public string Description {
			get => description;
			set {
				if (description != value) {
					description = value;
					this.NotifyPropertyChanged(nameof(Description));
				}
			}
		}

		/// <inheritdoc />
		public override sealed bool Equals(object obj) {
			if (obj is IBarGalleryItemViewModel other)
				return Equals(other);
			return false;
		}

		/// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
		public virtual bool Equals(IBarGalleryItemViewModel other) {
			return other is not null
				&& this.GetType() == other.GetType()
				&& ObjectsEqual(this.Value, other.Value)
				&& this.Category == other.Category
				&& this.Description == other.Description
				&& this.ImageSource == other.ImageSource
				&& this.KeyTipText == other.KeyTipText
				&& this.Label == other.Label
				&& this.LayoutBehavior == other.LayoutBehavior;

			static bool ObjectsEqual(object left, object right) {
				if (left is null)
					return right is null;
				else
					return left.Equals(right);
			}
		}

		/// <inheritdoc />
		public override int GetHashCode() {
			#if NETCOREAPP
			return HashCode.Combine(GetType(), Category, Description, ImageSource, KeyTipText, Label, LayoutBehavior, Value);
			#else
			// NOTE: 3 and 61 are prime numbers used for hash collision avoidance
			var hash = 3;
			hash = (hash * 61) + GetType().GetHashCode();
			hash = (hash * 61) + Category?.GetHashCode() ?? 0;
			hash = (hash * 61) + Description?.GetHashCode() ?? 0;
			hash = (hash * 61) + ImageSource?.GetHashCode() ?? 0;
			hash = (hash * 61) + KeyTipText?.GetHashCode() ?? 0;
			hash = (hash * 61) + Label?.GetHashCode() ?? 0;
			hash = (hash * 61) + LayoutBehavior.GetHashCode();
			hash = (hash * 61) + Value?.GetHashCode() ?? 0;
			return hash;
			#endif
		}
		
		/// <inheritdoc cref="IBarGalleryItemViewModel.ImageSource"/>
		public ImageSource ImageSource {
			get => imageSource;
			set {
				if (imageSource != value) {
					imageSource = value;
					NotifyPropertyChanged(nameof(ImageSource));
				}
			}
		}

		/// <inheritdoc cref="IBarGalleryItemViewModel.IsLabelVisible"/>
		public virtual bool IsLabelVisible
			=> layoutBehavior == BarGalleryItemLayoutBehavior.MenuItem;

		/// <inheritdoc cref="IBarGalleryItemViewModel.KeyTipText"/>
		public string KeyTipText {
			get => keyTipText;
			set {
				if (keyTipText != value) {
					keyTipText = value;
					this.NotifyPropertyChanged(nameof(KeyTipText));
				}
			}
		}

		/// <inheritdoc cref="IBarGalleryItemViewModel.Label"/>
		/// <remarks>If the label is not explicitly defined, the value may be coerced.</remarks>
		/// <see cref="CoerceLabel"/>
		public string Label {
			get => label ?? CoerceLabel();
			set {
				if (label != value) {
					label = value;
					this.NotifyPropertyChanged(nameof(Label));
				}
			}
		}

		/// <inheritdoc cref="IBarGalleryItemViewModel.LayoutBehavior"/>
		public BarGalleryItemLayoutBehavior LayoutBehavior {
			get => layoutBehavior;
			set {
				if (layoutBehavior != value) {
					layoutBehavior = value;
					this.NotifyPropertyChanged(nameof(LayoutBehavior));
					this.NotifyPropertyChanged(nameof(IsLabelVisible));
				}
			}
		}

		/// <summary>
		/// Raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event
		/// for the <see cref="Value"/> property and any other properties that are dependent on the value.
		/// </summary>
		protected virtual void OnValueChanged() {
			this.NotifyPropertyChanged(nameof(Value));
			this.NotifyPropertyChanged(nameof(Label));
		}
		
		/// <inheritdoc/>
		public override string ToString() {
			// The label is coerced from the value when label is not explicitly defined,
			// so only include the label in properties if it is explicitly defined
			var properties = $"Value='{this.Value?.ToString() ?? "<null>"}'";
			if (label is not null)
				properties += $", Label='{label}'";

			return $"{this.GetType().FullName}[{properties}]";
		}

		/// <summary>
		/// Gets or sets the value associated with this view model.
		/// </summary>
		/// <value>
		/// An object of type <typeparamref name="TValue"/>.
		/// </value>
		public virtual TValue Value {
			get => value;
			set {
				// Ignore if reference type is the same
				if (((this.value is null) && (value is null))
					|| ((this.value is object oldObject) && (value is object newObject) && (oldObject == newObject)))
					return;

				this.value = value;
				OnValueChanged();
			}
		}

	}

}
