namespace Cake.Issues.Reporting.Generic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;
    using RazorLight;
    using RazorLight.Compilation;

    /// <summary>
    /// Generator for creating text based issue reports.
    /// </summary>
    internal class GenericIssueReportGenerator : IssueReportFormat
    {
        private readonly GenericIssueReportFormatSettings genericIssueReportFormatSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericIssueReportGenerator"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public GenericIssueReportGenerator(ICakeLog log, GenericIssueReportFormatSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.genericIssueReportFormatSettings = settings;
        }

        /// <inheritdoc />
        protected override FilePath InternalCreateReport(IEnumerable<IIssue> issues)
        {
            this.Log.Information("Creating report '{0}'", this.Settings.OutputFilePath.FullPath);

            try
            {
               IMetadataReferenceManager manager = new DefaultMetadataReferenceManager();
               var engine = new RazorLightEngineBuilder()
                    .UseMemoryCachingProvider().AddMetadataReferences(MetadataResolver.GetMetadataReferences().ToArray())
                    .Build();

               var result = engine.CompileRenderAsync(Guid.NewGuid().ToString(), this.genericIssueReportFormatSettings.Template, issues, typeof(IEnumerable<IIssue>), this.genericIssueReportFormatSettings.Options.ToExpando()).Result;

               File.WriteAllText(this.Settings.OutputFilePath.FullPath, result);

               return this.Settings.OutputFilePath;
            }
            catch (Exception e)
            {
                this.Log.Error(e.Message);

                throw;
            }
        }
    }
}
