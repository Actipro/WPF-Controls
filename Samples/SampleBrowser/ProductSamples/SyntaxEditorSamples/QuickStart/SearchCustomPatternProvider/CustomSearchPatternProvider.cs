using System;
using System.Text;
using ActiproSoftware.Text.RegularExpressions;
using ActiproSoftware.Text.Searching;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.SearchCustomPatternProvider {
	
	/// <summary>
	/// Implements a custom <see cref="ISearchPatternProvider"/> that can provide regular expression find/replace patterns based on a supplied pattern.
	/// </summary>
	public class CustomSearchPatternProvider : ISearchPatternProvider {

		private static CustomSearchPatternProvider instance;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>CustomSearchPatternProvider</c> class.
		/// </summary>
		private CustomSearchPatternProvider() {
			// Initialize
			this.Key = "Custom";
			this.Description = "Numbers (custom)";
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the description of the search pattern provider.
		/// </summary>
		/// <value>The description of the search pattern provider.</value>
		public string Description { get; private set; }
		
		/// <summary>
		/// Returns the regular expression match pattern to use for find operations, based on the supplied pattern that uses this provider.
		/// </summary>
		/// <param name="pattern">The pattern to convert using this provider.</param>
		/// <returns>The regular expression match pattern to use for find operations.</returns>
		public string GetFindPattern(string pattern) {
			// Convert _ characters to digits and leave * and + as their regex equivalent
			string[] parts = pattern.Split(new char[] { '_', '*', '+' });

			int index = 0;
			StringBuilder result = new StringBuilder();
			foreach (string part in parts) {
				if (part.Length > 0)
					result.Append(String.Format("\"{0}\"", RegexHelper.Escape(part)));

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
		
		/// <summary>
		/// Returns the regular expression match pattern to use for replace operations, based on the supplied pattern that uses this provider.
		/// </summary>
		/// <param name="pattern">The pattern to convert using this provider.</param>
		/// <returns>The regular expression match pattern to use for replace operations.</returns>
		public string GetReplacePattern(string pattern) {
			return RegexHelper.Escape(pattern);
		}

		/// <summary>
		/// Gets the static instance of this provider.
		/// </summary>
		/// <value>The static instance of this provider.</value>
		public static CustomSearchPatternProvider Instance { 
			get {
				if (instance == null)
					instance = new CustomSearchPatternProvider();
				
				return instance;
			}
		}

		/// <summary>
		/// Gets the string key that uniquely identifies the search pattern provider.
		/// </summary>
		/// <value>The string key that uniquely identifies the search pattern provider.</value>
		public string Key { get; private set; }
		
		/// <summary>
		/// Gets whether this pattern requires case sensitivity.
		/// </summary>
		/// <value>
		/// <c>true</c> if this pattern requires case sensitivity; otherwise, <c>false</c>.
		/// </value>
		public bool RequiresCaseSensitivity { 
			get {
				return false;
			}
		}

	}

}

