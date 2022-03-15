#if MS_LOGGING

using ActiproSoftware.Products.Logging;
using Microsoft.Extensions.Logging;
using System;
using ActiproLogLevel = ActiproSoftware.Products.Logging.LogLevel;
using IMSExtensionsLogger = Microsoft.Extensions.Logging.ILogger;

namespace ActiproSoftware.SampleBrowser.Logging {

	/// <summary>
	/// Defines an adapter to allow Microsoft's <c>ILogger</c> to work with Actipro's <see cref="Logger"/>.
	/// </summary>
	internal class LoggerAdapter : Logger {

		private readonly IMSExtensionsLogger wrappedLogger;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="LoggerAdapter"/> class.
		/// </summary>
		/// <param name="wrappedLogger">The Microsoft-based logger.</param>
		internal LoggerAdapter(IMSExtensionsLogger wrappedLogger) {
            this.wrappedLogger = wrappedLogger ?? throw new ArgumentNullException(nameof(wrappedLogger));
        }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		public override IDisposable BeginScope(string messageFormat, params object[] args) {
            return wrappedLogger.BeginScope(messageFormat, args);
        }

		/// <inheritdoc/>
		public override bool IsEnabled(ActiproLogLevel logLevel) {
            return wrappedLogger.IsEnabled(logLevel.ToMicrosoftLogLevel());
        }

		/// <inheritdoc/>
		public override void Log(ActiproLogLevel logLevel, Exception exception, string message, params object[] args) {
            wrappedLogger.Log(logLevel.ToMicrosoftLogLevel(), exception, message, args);
        }

		/// <inheritdoc/>
		public override void Log(ActiproLogLevel logLevel, Func<string> messageFactory, Exception exception) {
			var msLogLevel = logLevel.ToMicrosoftLogLevel();
			if (wrappedLogger.IsEnabled(msLogLevel))
				wrappedLogger.Log(msLogLevel, exception, messageFactory?.Invoke());
		}

	}
}

#endif