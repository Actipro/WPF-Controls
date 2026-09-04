using System.Reflection;

namespace ActiproSoftware.SampleBrowser.Utilities.SystemParametersBrowser;

/// <summary>
/// Stores information about resource data.
/// </summary>
public class NamedParameter {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="name">The name.</param>
	/// <param name="value">The value.</param>
	protected NamedParameter(string name, object value) {
		Name = name;
		Value = value;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The name.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// The parameters from <see cref="System.Windows.SystemParameters"/>.
	/// </summary>
	public static IEnumerable<NamedParameter> SystemParameters {
		get {
			foreach (var p in typeof(SystemParameters).GetProperties(BindingFlags.Public | BindingFlags.Static)) {
				if (p.PropertyType != typeof(ResourceKey))
					yield return new NamedParameter(p.Name, p.GetValue(obj: null, index: null)!);
			}
		}
	}

	/// <summary>
	/// The value.
	/// </summary>
	public object Value { get; }

}
