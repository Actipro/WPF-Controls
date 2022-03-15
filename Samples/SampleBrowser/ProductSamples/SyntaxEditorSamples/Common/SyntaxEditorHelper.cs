using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Languages.DotNet.Implementation;
using ActiproSoftware.Text.Languages.DotNet.Reflection;
using ActiproSoftware.Text.Languages.JavaScript.Implementation;
using ActiproSoftware.Text.Languages.Python.Implementation;
using ActiproSoftware.Text.Languages.Xml.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using ActiproSoftware.Windows.Themes;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common {

	/// <summary>
	/// Provides some helper methods.
	/// </summary>
	public static class SyntaxEditorHelper {

		public const string DefinitionPath = "ActiproSoftware.ProductSamples.SyntaxEditorSamples.Languages.Definitions.";
		public const string SnippetsPath = "ActiproSoftware.ProductSamples.SyntaxEditorSamples.Languages.Snippets.";
		public const string ThemesPath = "ActiproSoftware.ProductSamples.SyntaxEditorSamples.Languages.Themes.";
		public const string XmlSchemasPath = "ActiproSoftware.ProductSamples.SyntaxEditorSamples.Languages.XmlSchemas.";

		private static bool isDarkThemeActive;

		/// <summary>
		/// Adds common "System" assembly references to a .NET <see cref="IProjectAssembly"/> to enable IntelliPrompt
		/// for commonly used types.
		/// </summary>
		/// <param name="projectAssembly">The .NET project assembly.</param>
		public static void AddCommonDotNetSystemAssemblyReferences(IProjectAssembly projectAssembly) {
			if (projectAssembly is null)
				throw new ArgumentNullException(nameof(projectAssembly));

			// Iterate the assemblies in the AppDomain and load all "System" assemblies
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				if ((assembly.FullName.StartsWith("System", StringComparison.OrdinalIgnoreCase)) ||
					(assembly.FullName.StartsWith("mscorlib", StringComparison.OrdinalIgnoreCase))) {
					projectAssembly.AssemblyReferences.Add(assembly);
				}
			}
		}

		/// <summary>
		/// Returns whether a dark theme is currently active.
		/// </summary>
		/// <returns>
		/// <c>true</c> if a dark theme is currently active; otherwise, <c>false</c>.
		/// </returns>
		private static bool GetIsDarkThemeActive() {
			var isDark = ThemeManager.IsDarkTheme(ThemeManager.CurrentTheme);
			return isDark;
		}
		
		/// <summary>
		/// Creates an <see cref="ICodeSnippetFolder"/> and initializes it with specified code snippets from embedded resources.
		/// </summary>
		/// <param name="folderName">The folder name.</param>
		/// <param name="paths">The array of resource paths to load.</param>
		/// <returns>The <see cref="ICodeSnippetFolder"/> that was loaded.</returns>
		private static ICodeSnippetFolder LoadCodeSnippetFolderFromResources(string folderName, string[] paths) {
			ICodeSnippetFolder folder = new CodeSnippetFolder(folderName);
			CodeSnippetSerializer serializer = new CodeSnippetSerializer();

			foreach (string path in paths) {
				using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path)) {
					if (stream != null) {
						IEnumerable<ICodeSnippet> snippets = serializer.LoadFromStream(stream);
						if (snippets != null) {
							foreach (ICodeSnippet snippet in snippets)
								folder.Items.Add(snippet);
						}
					}
				}
			}

			return folder;
		}
		
		/// <summary>
		/// Initializes an existing <see cref="ISyntaxLanguage"/> from a language definition (.langdef file) from a resource stream.
		/// </summary>
		/// <param name="filename">The filename.</param>
		public static void InitializeLanguageFromResourceStream(ISyntaxLanguage language, string filename) {
			string path = DefinitionPath + filename;
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path)) {
				if (stream != null) {
					SyntaxLanguageDefinitionSerializer serializer = new SyntaxLanguageDefinitionSerializer();
					serializer.InitializeFromStream(language, stream);
				}
			}
		}

		/// <summary>
		/// Loads a language definition (.langdef file) from a resource stream.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <returns>The <see cref="ISyntaxLanguage"/> that was loaded.</returns>
		public static ISyntaxLanguage LoadLanguageDefinitionFromResourceStream(string filename) {
			string path = DefinitionPath + filename;
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path)) {
				if (stream != null) {
					SyntaxLanguageDefinitionSerializer serializer = new SyntaxLanguageDefinitionSerializer();
					return serializer.LoadFromStream(stream);
				}
				else
					return SyntaxLanguage.PlainText;
			}
		}
		
		/// <summary>
		/// Creates an <see cref="ICodeSnippetFolder"/> and initializes it with some sample code snippets from embedded resources.
		/// </summary>
		/// <returns>The <see cref="ICodeSnippetFolder"/> that was loaded.</returns>
		public static ICodeSnippetFolder LoadSampleCSharpCodeSnippetsFromResources() {
			// NOTE: If you have file system access, the static CodeSnippetFolder.LoadFrom(path) method easily
			//       loads snippets within a specified file path and should be used instead

			string[] childPaths = new string[] {
				SnippetsPath + "CSharp.Sample_Child_Folder.while.snippet",
			};
			ICodeSnippetFolder childFolder = LoadCodeSnippetFolderFromResources("Sample Child Folder", childPaths);

			string[] rootPaths = new string[] {
				SnippetsPath + "CSharp.for.snippet",
				SnippetsPath + "CSharp.switch.snippet",
			};
			ICodeSnippetFolder rootFolder = LoadCodeSnippetFolderFromResources("Root", rootPaths);
			rootFolder.Folders.Add(childFolder);
			return rootFolder;
		}
		
		/// <summary>
		/// Creates an <see cref="ICodeSnippetFolder"/> and initializes it with some sample code snippets from embedded resources.
		/// </summary>
		/// <returns>The <see cref="ICodeSnippetFolder"/> that was loaded.</returns>
		public static ICodeSnippetFolder LoadSampleJavascriptCodeSnippetsFromResources() {
			// NOTE: If you have file system access, the static CodeSnippetFolder.LoadFrom(path) method easily
			//       loads snippets within a specified file path and should be used instead

			string[] rootPaths = new string[] {
				SnippetsPath + "Javascript.JavascriptFor.snippet",
				SnippetsPath + "Javascript.JavascriptWhile.snippet",
			};
			ICodeSnippetFolder rootFolder = LoadCodeSnippetFolderFromResources("Root", rootPaths);
			return rootFolder;
		}
		
		/// <summary>
		/// Creates an <see cref="ICodeSnippetFolder"/> and initializes it with some sample code snippets from embedded resources.
		/// </summary>
		/// <returns>The <see cref="ICodeSnippetFolder"/> that was loaded.</returns>
		public static ICodeSnippetFolder LoadSampleVBCodeSnippetsFromResources() {
			// NOTE: If you have file system access, the static CodeSnippetFolder.LoadFrom(path) method easily
			//       loads snippets within a specified file path and should be used instead

			string[] childPaths = new string[] {
				SnippetsPath + "VB.Sample_Child_Folder.VBWhile.snippet",
			};
			ICodeSnippetFolder childFolder = LoadCodeSnippetFolderFromResources("Sample Child Folder", childPaths);

			string[] rootPaths = new string[] {
				SnippetsPath + "VB.VBFor.snippet",
				SnippetsPath + "VB.VBSelect.snippet",
			};
			ICodeSnippetFolder rootFolder = LoadCodeSnippetFolderFromResources("Root", rootPaths);
			rootFolder.Folders.Add(childFolder);
			return rootFolder;
		}

		/// <summary>
		/// Updates the image set for a theme change.
		/// </summary>
		public static void UpdateImageSetForThemeChange() {
			switch (ThemeManager.CurrentTheme) {
				case ThemeNames.AeroNormalColor:
				case ThemeNames.Office2010Black:
				case ThemeNames.Office2010Blue:
				case ThemeNames.Office2010Silver:
					// Only needed if these older Aero-style themes are supported in your app
					CommonImageSourceProvider.DefaultImageSet = CommonImageSet.Classic;
					break;
				default:
					CommonImageSourceProvider.DefaultImageSet = (GetIsDarkThemeActive() ? CommonImageSet.MetroDark : CommonImageSet.MetroLight);
					break;
			}
		}

		/// <summary>
		/// Updates the highlighting style registry for a theme change.
		/// </summary>
		public static void UpdateHighlightingStyleRegistryForThemeChange() {
			var oldIsDarkThemeActive = isDarkThemeActive;
			isDarkThemeActive = GetIsDarkThemeActive();

			if (isDarkThemeActive != oldIsDarkThemeActive) {
				// Unregister all classification types
				var classificationTypes = AmbientHighlightingStyleRegistry.Instance.ClassificationTypes.ToArray();
				foreach (var classificationType in classificationTypes)
					AmbientHighlightingStyleRegistry.Instance.Unregister(classificationType);

				// Re-register common classification types for general display (plain text, margins, etc.) and add-ons (which also include common ones like keywords, strings, etc.)
				new DisplayItemClassificationTypeProvider().RegisterAll();
				new DotNetClassificationTypeProvider().RegisterAll();
				new JavaScriptClassificationTypeProvider().RegisterAll();
				new PythonClassificationTypeProvider().RegisterAll();
				new XmlClassificationTypeProvider().RegisterAll();
				new CompareFilesClassificationTypeProvider().RegisterAll();

				// Load HTML, Markdown, XAML, and Formula languages just so their custom classification types get re-registered
				LoadLanguageDefinitionFromResourceStream("Html.langdef");
				LoadLanguageDefinitionFromResourceStream("Markdown.langdef");
				LoadLanguageDefinitionFromResourceStream("Xaml.langdef");
				LoadLanguageDefinitionFromResourceStream("CustomFormula.langdef");
				// NOTE: Any other languages that are active would need to reload to ensure their custom classification types get re-registered as well

				if (isDarkThemeActive) {
					// Load a dark theme, which has some example pre-defined styles for some of the more common syntax languages
					string path = ThemesPath + "Dark.vssettings";
					using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path)) {
						if (stream != null)
							AmbientHighlightingStyleRegistry.Instance.ImportHighlightingStyles(stream);
					}
				}
			}
		}
		
	}
}
