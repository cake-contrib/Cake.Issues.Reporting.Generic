namespace Cake.Issues.Reporting.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Cake.Core;
    using Microsoft.CodeAnalysis;
    using Microsoft.CSharp;
    using Newtonsoft.Json;

    /// <summary>
    /// Custom resolver for RazorEngine.
    /// Required since we're embedding references with Costura.Fody.
    /// </summary>
    internal static class MetadataResolver
    {
        /// <summary>
        /// Gets the metadata references.
        /// </summary>
        /// <returns>MetadataReferences List.</returns>
        public static List<MetadataReference> GetMetadataReferences() =>
            new List<MetadataReference>()
            {
                GetMetadataReference(typeof(Type)),
                GetMetadataReference(typeof(TimeZoneInfo)),
                GetMetadataReference("netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"),
                GetMetadataReference(typeof(CSharpCodeProvider)),
                GetMetadataReference(typeof(JsonToken)),
                GetMetadataReference(typeof(CakeContext)),
                GetMetadataReference(typeof(IIssue)),
                GetMetadataReference(typeof(MetadataResolver)),
            };

        private static MetadataReference GetMetadataReference(Type type) =>
            MetadataReference.CreateFromFile(type.GetTypeInfo().Assembly.Location);

        private static MetadataReference GetMetadataReference(string assemblyName) =>
            MetadataReference.CreateFromFile(Assembly.Load(assemblyName).Location);
    }
}