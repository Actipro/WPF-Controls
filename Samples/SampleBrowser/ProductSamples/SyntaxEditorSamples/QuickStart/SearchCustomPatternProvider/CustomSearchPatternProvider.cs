using ActiproSoftware.Text.RegularExpressions;
using ActiproSoftware.Text.Searching;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.SearchCustomPatternProvider;

/// <summary>
/// Implements a custom <see cref="ISearchPatternProvider"/> that can provide regular expression find/replace patterns based on a supplied pattern.
/// </summary>
public class CustomSearchPatternProvider : ISearchPatternProvider {

	private static CustomSearchPatternProvider? _instance;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	private CustomSearchPatternProvider() {
		// Initialize
		Key = "Custom";
		Description = "Numbers (custom)";
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="ISearchPatternProvider.Description"/>
	public string Description { get; }

	/// <inheritdoc cref="ISearchPatternProvider.GetFindPattern"/>
	public string GetFindPattern(string pattern) {
		// Convert _ characters to digits and leave * and + as their regex equivalent
		var parts = pattern.Split(['_', '*', '+']);

		var index = 0;
		var result = new StringBuilder();
		foreach (var part in parts) {
			if (part.Length > 0)
				result.Append(string.Format("\"{0}\"", MatchingRegexCode.EscapePattern(part)));

			index += part.Length;

			if (index < pattern.Length) {
				if (pattern[index] == '_')
					result.Append("[0-9]");
				else
					result.Append(pattern[index]);
			}

			index++;
		}

		return result.ToString();
	}

	/// <inheritdoc cref="ISearchPatternProvider.GetReplacePattern"/>
	public string GetReplacePattern(string pattern)
		=> ReplacementRegexCode.EscapePattern(pattern)!;

	/// <summary>
	/// The static instance of this provider.
	/// </summary>
	public static CustomSearchPatternProvider Instance
		=> _instance ??= new CustomSearchPatternProvider();

	/// <summary>
	/// The string key that uniquely identifies the search pattern provider.
	/// </summary>
	public string Key { get; }

	/// <inheritdoc cref="ISearchPatternProvider.RequiresCaseSensitivity"/>
	public bool RequiresCaseSensitivity
		=> false;

}
