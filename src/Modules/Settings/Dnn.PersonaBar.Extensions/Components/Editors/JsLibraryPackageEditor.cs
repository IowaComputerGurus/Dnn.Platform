﻿using System;
using System.Linq;
using Dnn.PersonaBar.Extensions.Components.Dto;
using Dnn.PersonaBar.Extensions.Components.Dto.Editors;
using DotNetNuke.Entities.Controllers;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Instrumentation;
using DotNetNuke.Services.Installer.Packages;

namespace Dnn.PersonaBar.Extensions.Components.Editors
{
    public class JsLibraryPackageEditor : IPackageEditor
    {
        private static readonly ILog Logger = LoggerSource.Instance.GetLogger(typeof(JsLibraryPackageEditor));

        #region IPackageEditor Implementation

        public PackageInfoDto GetPackageDetail(int portalId, PackageInfo package)
        {
            var usedBy = PackageController.Instance.GetPackageDependencies(d => 
                            d.PackageName == package.Name && d.Version <= package.Version).Select(d => d.PackageId);

            var usedByPackages = from p in PackageController.Instance.GetExtensionPackages(portalId)
                                 where usedBy.Contains(p.PackageID)
                                 select new UsedByPackage { Id = p.PackageID, Name = p.Name, Version = p.Version.ToString() };

            var library = JavaScriptLibraryController.Instance.GetLibrary(l => l.PackageID == package.PackageID);
            var detail = new JsLibraryPackageDetailDto(portalId, package)
            {
                Name = library.LibraryName,
                Version = library.Version.ToString(),
                ObjectName = library.ObjectName,
                DefaultCdn = library.CDNPath,
                FileName = library.FileName,
                Location = library.PreferredScriptLocation.ToString(),
                CustomCdn = HostController.Instance.GetString("CustomCDN_" + library.LibraryName),
                Dependencies = package.Dependencies.Select(d => new ListItemDto {Id = d.PackageId, Name = d.PackageName}),
                UsedBy = usedByPackages
            };

            return detail;
        }

        public bool SavePackageSettings(PackageSettingsDto packageSettings, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                var library = JavaScriptLibraryController.Instance.GetLibrary(l => l.PackageID == packageSettings.PackageId);

                if(packageSettings.EditorActions.ContainsKey("customCdn")
                    && !string.IsNullOrEmpty(packageSettings.EditorActions["customCdn"]))
                {
                    HostController.Instance.Update("CustomCDN_" + library.LibraryName, packageSettings.EditorActions["customCdn"]);
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                errorMessage = ex.Message;
                return false;
            }
        }

        #endregion
    }
}