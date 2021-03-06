﻿using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using PocketGauger.UnitTests.TestData;

namespace PocketGauger.UnitTests.IntegrationTests
{
    public abstract class IntegrationTestBase
    {
        protected PocketGaugerFiles PocketGaugerFiles { get; private set; }

        [SetUp]
        public void BaseSetupForEachTest()
        {
            PocketGaugerFiles = new PocketGaugerFiles();
        }

        protected void AddPocketGaugerFile(string fileName)
        {
            PocketGaugerFiles.Add(fileName, GetEmbeddedResource(fileName));
        }

        private static Stream GetEmbeddedResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var path = FormattableString.Invariant($"{typeof(ExpectedMeterDetailsData).Namespace}.{resourceName.ToUpperInvariant()}");

            return assembly.GetManifestResourceStream(path);
        }

        [TearDown]
        public void TearDown()
        {
            PocketGaugerFiles?.Dispose();
        }
    }
}
