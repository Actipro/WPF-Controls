using System.Reflection;

namespace ActiproSoftware.SampleBrowser.Utilities.StringResourceBrowser;

/// <summary>
/// Stores information about a product with string resources.
/// </summary>
public class ProductResource {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="assembly">The assembly.</param>
	public ProductResource(Assembly assembly) {
		Assembly = assembly;

		var name = Assembly.GetName().Name!;
		if (name.StartsWith("ActiproSoftware.", StringComparison.OrdinalIgnoreCase))
			name = name.Substring("ActiproSoftware.".Length);
		if (name.EndsWith(".Wpf", StringComparison.OrdinalIgnoreCase))
			name = name.Substring(0, name.Length - ".Wpf".Length);
		Name = name;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The assembly.
	/// </summary>
	public Assembly Assembly { get; }

	/// <summary>
	/// Indicates whether assembly is valid for customizing string resources.
	/// </summary>
	public bool IsValid
		=> (SRType is not null) && (SRNameType is not null);

	/// <summary>
	/// The name of the resource.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// The string resource name type.
	/// </summary>
	public Type? SRNameType
		=> Type.GetType(string.Format("ActiproSoftware.Properties.{0}.SRName, {1}", Name, Assembly.FullName));

	/// <summary>
	/// The string resource type.
	/// </summary>
	public Type? SRType
		=> Type.GetType(string.Format("ActiproSoftware.Properties.{0}.SR, {1}", Name, Assembly.FullName));

}
