using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a gallery item whose value and label based on the value in an enumeration.
	/// </summary>
	/// <typeparam name="TEnum">The enumeration type of the value associated with this gallery item.</typeparam>
	public class EnumBarGalleryItemViewModel<TEnum> : BarGalleryItemViewModel<TEnum>
		where TEnum : struct, IComparable, IFormattable /* CLS-compliant constraints based on System.Enum interfaces */ {

		/// <summary>
		/// Gets the default sort order for enumeration items that do not define an order using the <see cref="DisplayAttribute.Order"/> property.
		/// </summary>
		public const int DefaultOrder = 10000;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public EnumBarGalleryItemViewModel()
			: this(value: default, category: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value.
		/// </summary>
		/// <param name="value">The item's value.</param>
		public EnumBarGalleryItemViewModel(TEnum value)
			: this(value, category: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value and category.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public EnumBarGalleryItemViewModel(TEnum value, string category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value, category, and label.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public EnumBarGalleryItemViewModel(TEnum value, string category, string label)
			: base(value, category, label) {

			ThrowIfNotEnumType(typeof(TEnum));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Throws an exception if <typeparamref name="TEnum"/> is not an enumeration since generic type constraints are not specific enough.
		/// </summary>
		/// <param name="enumType">The type to examine.</param>
		/// <exception cref="ArgumentException">Thrown if <typeparamref name="TEnum"/> is not an enumeration.</exception>
		private static void ThrowIfNotEnumType(Type enumType) {
			if (!enumType.IsEnum)
				throw new ArgumentException($"The type {nameof(TEnum)} must be an enumeration.");
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a collection of gallery item view models representing the values defined for an enumeration type.
		/// </summary>
		/// <exception cref="InvalidOperationException"/>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<EnumBarGalleryItemViewModel<TEnum>> CreateCollection() {
			var enumType = typeof(TEnum);
			ThrowIfNotEnumType(enumType);

			var collection = new HashSet<EnumBarGalleryItemViewModel<TEnum>>();
			foreach (var field in enumType.GetFields().Where(f => f.IsStatic)) {

				try {
					// Ignore fields explicitly marked as non-browsable
					bool isNonBrowsable = field.GetCustomAttribute<EditorBrowsableAttribute>()?.State == EditorBrowsableState.Never;
					if (isNonBrowsable)
						continue;

					var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();

					var value = (TEnum)field.GetValue(null);
					var category = displayAttribute?.GetGroupName();
					var label = ConvertEnumValueToString(value, useAttributes: true);

					var viewModel = new EnumBarGalleryItemViewModel<TEnum>(value, category, label) {
						Description = displayAttribute?.GetDescription(),
						Order = displayAttribute?.GetOrder() ?? DefaultOrder,
					};
					collection.Add(viewModel);
				}
				catch (InvalidOperationException ex) {
					// Wrap the exception with details on which field generated the error since it can be
					// difficult to debug localization issues on DisplayAttribute without knowing which
					// field generated an exception.
					throw new InvalidOperationException($"Invalid operation creating view model for enumeration field '{enumType.FullName}.{field.Name}'.  {ex.Message}", ex);
				}
			}

			return collection.OrderBy(vm => vm.Order);
		}

		/// <inheritdoc/>
		public override bool IsLabelVisible => true;

		/// <summary>
		/// Gets or sets the sort order for this item, where lower values appear before higher values.
		/// </summary>
		/// <value>An integer value.</value>
		public int Order { get; set; }

	}

}
