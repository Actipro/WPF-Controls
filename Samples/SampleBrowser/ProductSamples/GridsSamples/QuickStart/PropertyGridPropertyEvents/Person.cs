using ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors;
using ActiproSoftware.Windows.Controls.Grids.PropertyEditors;
using System.ComponentModel.DataAnnotations;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyEvents;

/// <summary>
/// Represents a test object for demonstration purposes.
/// </summary>
[ReadOnly(true)]
[TypeConverter(typeof(ExpandableObjectConverter))]
public class Person : ObservableObjectBase {

	private DateTime? _birthday;
	private string? _firstName;
	private string? _lastName;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The age of the person.
	/// </summary>
	[DefaultValue("(not set)")]
	[Description("Indicates the age of the person, based on the birthday.")]
	[Display(Order = 4)]
	public string Age {
		get {
			if (Birthday is not null) {
				var age = (int)((DateTime.Now - Birthday.Value).TotalDays / 365.0);
				if (age == 1)
					return "1 year old";
				else if (age < 0)
					return "(invalid)";
				else
					return string.Format("{0} years old", age);
			}

			return "(not set)";
		}
	}

	/// <summary>
	/// The birthday of the person.
	/// </summary>
	[DefaultValue(null)]
	[Description("Indicates the birthday of the person.")]
	[Editor(typeof(NullableDatePropertyEditor), typeof(PropertyEditor))]
	[Display(Order = 3)]
	public DateTime? Birthday {
		get => _birthday;
		set {
			if (SetProperty(ref _birthday, value))
				OnPropertyChanged(nameof(Age));
		}
	}

	/// <summary>
	/// The children of the person.
	/// </summary>
	[Description("Lists the children of the person.")]
	[TypeConverter(typeof(ChildrenCollectionConverter))]
	[Display(Order = 5)]
	public List<Person> Children { get; } = [];

	/// <summary>
	/// The first name of the person.
	/// </summary>
	[DefaultValue(null)]
	[Description("Indicates the first name of the person.")]
	[NotifyParentProperty(true)]
	[Display(Order = 2)]
	public string? FirstName {
		get => _firstName;
		set => SetProperty(ref _firstName, value);
	}

	/// <summary>
	/// The last name of the person.
	/// </summary>
	[DefaultValue(null)]
	[Description("Indicates the last name of the person.")]
	[NotifyParentProperty(true)]
	[Display(Order = 1)]
	public string? LastName {
		get => _lastName;
		set => SetProperty(ref _lastName, value);
	}

	/// <inheritdoc/>
	public override string ToString()
		=> string.Format("{0}, {1}", LastName, FirstName);

}
