﻿using System;

namespace Dnn.PersonaBar.Extensions.Components.Editors
{
    public sealed class PackageEditorFactory
    {
        public static IPackageEditor GetPackageEditor(string packageType)
        {
            PackageTypes pkgType;
            Enum.TryParse<PackageTypes>(packageType, true, out pkgType);

            switch (pkgType)
            {
                case PackageTypes.Module:
                    return new ModulePackageEditor();
                case PackageTypes.AuthSystem:
                    return new AuthSystemPackageEditor();
                case PackageTypes.JavascriptLibrary:
                    return new JsLibraryPackageEditor();
                case PackageTypes.CoreLanguagePack:
                case PackageTypes.ExtensionLanguagePack:
                    return new LanguagePackageEditor();
                case PackageTypes.Container:
                case PackageTypes.Skin:
                    return new SkinPackageEditor();
                case PackageTypes.SkinObject:
                    return new SkinObjectPackageEditor();
                case PackageTypes.DashboardControl:
                    break;
                case PackageTypes.Language:
                    break;
                case PackageTypes.Library:
                    break;
                case PackageTypes.Provider:
                    break;
                case PackageTypes.Widget:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(packageType));
            }
            return null;
        }
    }
}