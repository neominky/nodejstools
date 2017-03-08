// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.NodejsTools.Npm;

namespace Microsoft.NodejsTools.NpmUI
{
    internal abstract class PackageCatalogEntryViewModel
    {
        private readonly SemverVersion? version;
        private readonly SemverVersion? localVersion;

        protected PackageCatalogEntryViewModel(
            string name,
            SemverVersion? version,
            IEnumerable<SemverVersion> availableVersions,
            string author,
            string description,
            IEnumerable<string> homepages,
            string keywords,
            SemverVersion? localVersion
        )
        {
            this.Name = name;
            this.version = version;
            this.AvailableVersions = availableVersions != null ? availableVersions.ToList() : new List<SemverVersion>();
            this.Author = author;
            this.Description = description;
            this.Homepages = homepages != null ? homepages.ToList() : new List<string>();
            this.Keywords = keywords;
            this.localVersion = localVersion;
        }

        public virtual string Name { get; }
        public IEnumerable<SemverVersion> AvailableVersions { get; }
        public string Author { get; }
        public string Description { get; }
        public IEnumerable<string> Homepages { get; }
        public string Keywords { get; }
    }

    internal class ReadOnlyPackageCatalogEntryViewModel : PackageCatalogEntryViewModel
    {
        public ReadOnlyPackageCatalogEntryViewModel(IPackage package, IPackage localInstall)
            : base(
                package.Name ?? string.Empty,
                package.Version,
                package.AvailableVersions,
                package.Author == null ? string.Empty : package.Author.ToString(),
                package.Description ?? string.Empty,
                package.Homepages,
                (package.Keywords != null && package.Keywords.Any())
                    ? string.Join(", ", package.Keywords)
                    : Resources.NoKeywordsInPackage,
                localInstall != null ? (SemverVersion?)localInstall.Version : null
            )
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                throw new ArgumentNullException("package.Name");
            }
        }
    }
}
