﻿using System;
using System.IO;
using Sushi.Descriptors;
using Sushi.Extensions;
using Sushi.JavaScript;
using Sushi.JavaScript.Enum;
using Sushi.TypeScript;
using Sushi.TypeScript.Enum;

namespace Sushi.Tests
{
    public class TestBase
    {
        protected static readonly string FilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Compiled\");

        /// <summary>
        ///     Compile a model for JavaScript.
        /// </summary>
        protected static void CompileJavaScript(ConversionKernel kernel,
            JavaScriptVersion version,
            Func<ClassDescriptor, bool> predicate = null,
            string fileName = "ecmascript",
            bool minify = false,
            Wrap wrap = Wrap.None)
        {
            var converter = kernel.CreateConverterForJavaScript(version, wrap);
            var converted = converter.Convert(predicate);

            fileName += $".{version.ToString().ToLowerInvariant()}";

            if (wrap != Wrap.None)
                fileName += "." + wrap;

            if (minify)
                fileName += ".min";

            converter.WriteToFile(converted, FilePath, fileName, minify);
        }

        /// <summary>
        ///     Compile a model for TypeScript.
        /// </summary>
        protected static void CompileTypeScript(ConversionKernel kernel, string fileName = "typescript", Func<ClassDescriptor, bool> predicate = null, bool minify = false)
        {
            var converter = kernel.CreateConverterForTypeScript(TypeScriptSpecification.TypeScript);
            var converted = converter.Convert(predicate);

            if (minify)
                fileName += ".min";

            converter.WriteToFile(converted, FilePath, fileName, minify);
        }

        /// <summary>
        ///     Compile a model for DefinitelyTyped.
        /// </summary>
        protected static void CompileDefinitelyTyped(ConversionKernel kernel, string fileName = "reference", bool minify = false)
        {
            // Make sure the XML documentation is loaded
            var assemblyName = typeof(TestBase).Assembly.GetProjectName();
            kernel.LoadXmlDocumentation();

            var converter = kernel.CreateConverterForTypeScript(TypeScriptSpecification.Declaration);
            var converted = converter.Convert();

            if (minify)
                fileName += ".min";

            converter.WriteToFile(converted, FilePath, fileName, minify);
        }
    }
}
