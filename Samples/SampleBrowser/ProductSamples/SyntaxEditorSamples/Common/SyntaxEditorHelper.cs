using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Languages.DotNet.Reflection;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using System.Reflection;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;

/// <summary>
/// Provides some helper methods.
/// </summary>
public static class SyntaxEditorHelper {

	private const string LanguagesPath = "ActiproSoftware.ProductSamples.SyntaxEditorSamples.Languages.";

	public const string DefinitionPath = LanguagesPath + "Definitions.";
	public const string SnippetsPath = LanguagesPath + "Snippets.";
	public const string ThemesPath = LanguagesPath + "Themes.";
	public const string XmlSchemasPath = LanguagesPath + "XmlSchemas.";

	/// <summary>
	/// Adds common "System" assembly references to a .NET <see cref="IProjectAssembly"/> to enable IntelliPrompt for commonly used types.
	/// </summary>
	/// <param name="projectAssembly">The .NET project assembly.</param>
	public static void AddCommonDotNetSystemAssemblyReferences(IProjectAssembly projectAssembly) {
		#if NET
		ArgumentNullException.ThrowIfNull(projectAssembly);
		#else
		if (projectAssembly is null)
			throw new ArgumentNullException(nameof(projectAssembly));
		#endif

		// Iterate the assemblies in the AppDomain and load all "System" assemblies
		foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
			if (
				assembly.FullName?.StartsWith("System", StringComparison.OrdinalIgnoreCase) == true
				|| assembly.FullName?.StartsWith("mscorlib", StringComparison.OrdinalIgnoreCase) == true
			) {
				projectAssembly.AssemblyReferences.Add(assembly);
			}
		}
	}

	/// <summary>
	/// Creates an <see cref="ICodeSnippetFolder"/> and initializes it with specified code snippets from embedded resources.
	/// </summary>
	/// <param name="folderName">The folder name.</param>
	/// <param name="paths">The array of resource paths to load.</param>
	private static CodeSnippetFolder LoadCodeSnippetFolderFromResources(string folderName, string[] paths) {
		var folder = new CodeSnippetFolder(folderName);
		var serializer = new CodeSnippetSerializer();

		foreach (var path in paths) {
			using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
			if (stream is not null) {
				var snippets = serializer.LoadFromStream(stream);
				if (snippets is not null) {
					foreach (var snippet in snippets)
						folder.Items.Add(snippet);
				}
			}
		}

		return folder;
	}

	/// <summary>
	/// Initializes an existing <see cref="ISyntaxLanguage"/> from a language definition (.langdef file) from a resource stream.
	/// </summary>
	/// <param name="fileName">The file name.</param>
	public static void InitializeLanguageFromResourceStream(ISyntaxLanguage language, string fileName) {
		var path = DefinitionPath + fileName;
		using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
		if (stream is not null) {
			var serializer = new SyntaxLanguageDefinitionSerializer() {
				// Enable the use of common classification types (like Comment and String)
				//   for consistent highlighting styles
				UseBuiltInClassificationTypes = true,
			};
			serializer.InitializeFromStream(language, stream);
		}
	}

	/// <summary>
	/// Loads a language definition (.langdef file) from a resource stream.
	/// </summary>
	/// <param name="fileName">The file name.</param>
	/// <returns>The <see cref="ISyntaxLanguage"/> that was loaded.</returns>
	public static ISyntaxLanguage LoadLanguageDefinitionFromResourceStream(string fileName) {
		var path = DefinitionPath + fileName;
		using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
		if (stream is not null) {
			var serializer = new SyntaxLanguageDefinitionSerializer() {
				// Enable the use of common classification types (like Comment and String)
				//   for consistent highlighting styles
				UseBuiltInClassificationTypes = true,
			};
			return serializer.LoadFromStream(stream);
		}
		else {
			// Fallback to plain text
			return SyntaxLanguage.PlainText;
		}
	}

	/// <summary>
	/// Creates an <see cref="ICodeSnippetFolder"/> and initializes it with some sample code snippets from embedded resources.
	/// </summary>
	public static ICodeSnippetFolder LoadSampleCSharpCodeSnippetsFromResources() {
		// NOTE: If you have file system access, the static CodeSnippetFolder.LoadFrom(path) method easily
		//   loads snippets within a specified file path and should be used instead

		var childPaths = new string[] {
			SnippetsPath + "CSharp.Sample_Child_Folder.while.snippet",
		};
		var childFolder = LoadCodeSnippetFolderFromResources("Sample Child Folder", childPaths);

		var rootPaths = new string[] {
			SnippetsPath + "CSharp.for.snippet",
			SnippetsPath + "CSharp.switch.snippet",
		};
		var rootFolder = LoadCodeSnippetFolderFromResources("Root", rootPaths);
		rootFolder.Folders.Add(childFolder);
		return rootFolder;
	}

	/// <summary>
	/// Creates an <see cref="ICodeSnippetFolder"/> and initializes it with some sample code snippets from embedded resources.
	/// </summary>
	public static ICodeSnippetFolder LoadSampleJavascriptCodeSnippetsFromResources() {
		// NOTE: If you have file system access, the static CodeSnippetFolder.LoadFrom(path) method easily
		//   loads snippets within a specified file path and should be used instead

		var rootPaths = new string[] {
			SnippetsPath + "Javascript.JavascriptFor.snippet",
			SnippetsPath + "Javascript.JavascriptWhile.snippet",
		};
		var rootFolder = LoadCodeSnippetFolderFromResources("Root", rootPaths);
		return rootFolder;
	}

	/// <summary>
	/// Creates an <see cref="ICodeSnippetFolder"/> and initializes it with some sample code snippets from embedded resources.
	/// </summary>
	public static ICodeSnippetFolder LoadSampleVBCodeSnippetsFromResources() {
		// NOTE: If you have file system access, the static CodeSnippetFolder.LoadFrom(path) method easily
		//   loads snippets within a specified file path and should be used instead

		var childPaths = new string[] {
			SnippetsPath + "VB.Sample_Child_Folder.VBWhile.snippet",
		};
		var childFolder = LoadCodeSnippetFolderFromResources("Sample Child Folder", childPaths);

		var rootPaths = new string[] {
			SnippetsPath + "VB.VBFor.snippet",
			SnippetsPath + "VB.VBSelect.snippet",
		};
		var rootFolder = LoadCodeSnippetFolderFromResources("Root", rootPaths);
		rootFolder.Folders.Add(childFolder);
		return rootFolder;
	}

}
