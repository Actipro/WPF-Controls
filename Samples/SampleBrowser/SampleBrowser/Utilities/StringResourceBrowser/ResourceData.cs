using ActiproSoftware.Properties;
using System.Reflection;

namespace ActiproSoftware.SampleBrowser.Utilities.StringResourceBrowser;

/// <summary>
/// Stores information about string resource data.
/// </summary>
/// <param name="srType">The <see cref="SRBase"/> <see cref="Type"/>.</param>
/// <param name="enumType">The enumeration <see cref="Type"/>.</param>
/// <param name="name">The name of the resource.</param>
public class ResourceData(Type srType, Type enumType, string name) {

	private readonly Type _srType = srType;

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The enumeration <see cref="Type"/>.
	/// </summary>
	public Type EnumType { get; } = enumType;

	/// <summary>
	/// The name of the resource.
	/// </summary>
	public string Name { get; } = name;

	/// <summary>
	/// The value of the resource.
	/// </summary>
	public string? Value {
		get {
			// The 'GetString' method expects a value from the enum
			#if NET
			if (Enum.TryParse(EnumType, Name, out var enumValue))
				return _srType.InvokeMember("GetString", BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod, binder: null, target: null, args: [enumValue]) as string;
			#else
			foreach (var enumValue in Enum.GetValues(EnumType)) {
				if (Name.Equals(Enum.GetName(EnumType, enumValue), StringComparison.OrdinalIgnoreCase))
					return _srType.InvokeMember("GetString", BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod, binder: null, target: null, args: [enumValue]) as string;
			}
			#endif
			return null;
		}
	}

}
