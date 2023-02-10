#if DEBUG && MS_LOGGING
using ActiproSoftware.SampleBrowser.Logging;
using Microsoft.Extensions.Logging;
using LoggerFactory = ActiproSoftware.Products.Logging.LoggerFactory;
#endif

using System;
using System.IO;
using System.Windows;
using ActiproSoftware.Products.Logging;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;
using ActiproSoftware.Windows.Themes.Generation;

namespace ActiproSoftware.SampleBrowser {
	
	/// <summary>
	/// Represents the application.
	/// </summary>
	public partial class App : Application {

		private static readonly Logger logger;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the <c>App</c> class.
		/// </summary>
		static App() {
			#if DEBUG && MS_LOGGING
			LoggerFactoryAdapter.Configure(builder => {
				builder.AddFilter("ActiproSoftware", Microsoft.Extensions.Logging.LogLevel.Warning);
				builder.AddDebugLogger();
			});
			#endif
			logger = LoggerFactory.DefaultInstance.CreateLogger<App>();
		}

		/// <summary>
		/// Initializes an instance of the <c>App</c> class.
		/// </summary>
		public App() {
			logger?.LogInformation("Initialize component");
			InitializeComponent();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROPERTIES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the application is starting up.
		/// </summary>
		/// <param name="e">A <see cref="StartupEventArgs"/> that contains data related to this event.</param>
		protected override void OnStartup(StartupEventArgs e) {
			// Tell images to chromatically adapt for dark themes when needed
			logger?.LogInformation("Configuring ImageProvider ...");
			ImageProvider.Default.ChromaticAdaptationMode = ImageChromaticAdaptationMode.DarkThemes;
			ImageProvider.Default.UseMonochromeInHighContrast = true;

			// NOTE: This is the ideal place to set the application theme, load native styles, or set customized string resources
			logger?.LogInformation("Configuring ThemeManager ...");
			ThemeManager.BeginUpdate();
			try {
				// The older Aero and Office 2010 themes are in a separate assembly and must be registered if you will use them in the application
				ThemesAeroThemeCatalogRegistrar.Register();

				// Register an optional "Custom (Slate)" theme definition (selectable from the Theme menu)
				ThemeManager.RegisterThemeDefinition(new ThemeDefinition("Custom") {
					Intent = ThemeIntent.Dark,
					BarItemBackgroundGradientKind = GradientKind.Mid,
					BaseGrayscaleHue = 210,
					BaseGrayscaleSaturation = 52,
					ButtonBackgroundGradientKind = GradientKind.Mid,
					ButtonBorderContrastKind = BorderContrastKind.Low,
					ContainerBorderContrastKind = BorderContrastKind.Low,
					ListItemBackgroundGradientKind = GradientKind.Mid,
					PopupBorderContrastKind = BorderContrastKind.Low,
					ScrollBarHasButtons = true,
					ScrollBarThumbCornerRadius = 1.5,
					ScrollBarThumbMargin = 7,
					WindowTitleBarBackgroundKind = WindowTitleBarBackgroundKind.ThemeMinimum,
				});

				// Use the Actipro styles for native WPF controls that look great with Actipro's control products
				ThemeManager.AreNativeThemesEnabled = true;
				logger?.LogDebug("ThemeManager.AreNativeThemesEnabled = {0}", ThemeManager.AreNativeThemesEnabled);

				// Update the SyntaxEditor highlighting style registry and image set if the theme changes backgrounds from light to dark, or vice versa
				ThemeManager.CurrentThemeChanged += (sender, args) => {
					logger?.LogDebug("ThemeManager.CurrentThemeChanged; New theme {0}", ThemeManager.CurrentTheme);
					ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common.SyntaxEditorHelper.UpdateHighlightingStyleRegistryForThemeChange();
					ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common.SyntaxEditorHelper.UpdateImageSetForThemeChange();
				};

				// Set a Metro Light theme
				ThemeManager.CurrentTheme = ThemeNames.MetroLight;
				logger?.LogDebug("ThemeManager.CurrentTheme = {0}", ThemeManager.CurrentTheme);

			}
			finally {
				ThemeManager.EndUpdate();
			}

			// ------------------------------------------------------------------------------------------------------

			logger?.LogInformation("Configuring SyntaxEditor ...");

			// If using SyntaxEditor with languages that support syntax/semantic parsing, use this line at
			//   app startup to ensure that worker threads are used to perform the parsing
			ActiproSoftware.Text.Parsing.AmbientParseRequestDispatcherProvider.Dispatcher =
				new ActiproSoftware.Text.Parsing.Implementation.ThreadedParseRequestDispatcher();
			logger?.LogDebug("AmbientParseRequestDispatcherProvider.Dispatcher = {0}", ActiproSoftware.Text.Parsing.AmbientParseRequestDispatcherProvider.Dispatcher?.GetType().FullName ?? "NULL");

			// Create SyntaxEditor .NET Languages Add-on ambient assembly repository, which supports caching of 
			//   reflection data and improves performance for the add-on...
			//   Be sure to replace the appDataPath with a proper path for your own application (if file access is allowed)
			var appDataPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
				"Actipro Software"), "WPF SampleBrowser Assembly Repository");
			ActiproSoftware.Text.Languages.DotNet.Reflection.AmbientAssemblyRepositoryProvider.Repository =
				new ActiproSoftware.Text.Languages.DotNet.Reflection.Implementation.FileBasedAssemblyRepository(appDataPath);
			logger?.LogDebug(".NET Reflection Repository Path = {0}", appDataPath);

			// Create SyntaxEditor Python Languages Add-on ambient package repository, which supports caching of 
			//   reflection data... Be sure to replace the appDataPath with a proper path for your own application (if file access is allowed)
			appDataPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
				"Actipro Software"), "WPF SampleBrowser Python Package Repository");
			ActiproSoftware.Text.Languages.Python.Reflection.AmbientPackageRepositoryProvider.Repository = 
				new ActiproSoftware.Text.Languages.Python.Reflection.Implementation.FileBasedPackageRepository(appDataPath);
			logger?.LogDebug("Python Package Repository Path = {0}", appDataPath);

			// ------------------------------------------------------------------------------------------------------

			// Call the base method
			base.OnStartup(e);
		}

		/// <summary>
		/// Occurs when the application exits.
		/// </summary>
		/// <param name="e">A <see cref="ExitEventArgs"/> that contains data related to this event.</param>
		protected override void OnExit(ExitEventArgs e) {
			// Shut down the SyntaxEditor ambient parse request dispatcher
			var dispatcher = ActiproSoftware.Text.Parsing.AmbientParseRequestDispatcherProvider.Dispatcher;
			if (dispatcher != null) {
				ActiproSoftware.Text.Parsing.AmbientParseRequestDispatcherProvider.Dispatcher = null;
				dispatcher.Dispose();
			}

			// Prune any SyntaxEditor .NET Languages Add-on cache data that is no longer valid
			var repository = ActiproSoftware.Text.Languages.DotNet.Reflection.AmbientAssemblyRepositoryProvider.Repository;
			if (repository != null)
				repository.PruneCache();

			// ------------------------------------------------------------------------------------------------------

			// Call the base method
			base.OnExit(e);
		}

	}
}
