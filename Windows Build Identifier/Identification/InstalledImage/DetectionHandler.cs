﻿/*
 * Copyright (c) 2020, Gustave Monce - gus33000.me - @gus33000
 *
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using WindowsBuildIdentifier.Interfaces;

namespace WindowsBuildIdentifier.Identification.InstalledImage
{
    public class DetectionHandler
    {
        public static Report IdentifyWindowsNT(WindowsInstallProviderInterface installProvider)
        {
            Report report = new Report();

            var fileentries = installProvider.GetFileSystemEntries();

            //
            // We need a few files from the install to gather enough information
            // These files are:
            //
            // - \ntkrnlmp.exe
            // or \ntoskrnl.exe
            // - windows\system32\config\software
            // - windows\system32\config\system
            //

            string kernelPath = "";
            string softwareHivePath = "";
            string systemHivePath = "";

            var kernelEntry = fileentries.FirstOrDefault(x => 
            x.EndsWith(
                @"\ntkrnlmp.exe", StringComparison.InvariantCultureIgnoreCase) && 
                !x.Contains("WinSxS", StringComparison.InvariantCultureIgnoreCase)) ?? 
            fileentries.FirstOrDefault(x => 
                x.EndsWith(@"\ntoskrnl.exe", StringComparison.InvariantCultureIgnoreCase) && 
                !x.Contains("WinSxS", StringComparison.InvariantCultureIgnoreCase));
            if (kernelEntry != null)
            {
                kernelPath = installProvider.ExpandFile(kernelEntry);
            }

            var softwareHiveEntry = fileentries.FirstOrDefault(x => x.Equals(@"windows\system32\config\software", StringComparison.InvariantCultureIgnoreCase));
            if (softwareHiveEntry != null)
            {
                softwareHivePath = installProvider.ExpandFile(softwareHiveEntry);
            }

            var systemHiveEntry = fileentries.FirstOrDefault(x => x.Equals(@"windows\system32\config\system", StringComparison.InvariantCultureIgnoreCase));
            if (systemHiveEntry != null)
            {
                systemHivePath = installProvider.ExpandFile(systemHiveEntry);
            }

            bool IsUnstaged = !fileentries.Any(x => x.Contains(@"system32\ntoskrnl.exe", StringComparison.InvariantCultureIgnoreCase)) &&
                !fileentries.Any(x => x.Contains(@"system32\ntkrnlmp.exe", StringComparison.InvariantCultureIgnoreCase));

            VersionInfo1 info = new VersionInfo1();

            if (!string.IsNullOrEmpty(kernelPath))
            {
                Console.WriteLine("Extracting version information from the image 1");
                info = ExtractVersionInfo(kernelPath);

                File.Delete(kernelPath);

                report.Architecture = info.Architecture;
                report.BuildType = info.BuildType;
            }

            if (IsUnstaged)
            {
                report.Sku = "Unstaged";

                Console.WriteLine("Image detected as unstaged, gathering target editions available in the image");

                // parse editions
                report.Editions = GatherUnstagedEditions(installProvider);
            }
            else
            {
                Console.WriteLine("Extracting additional edition information");
                report.Sku = ExtractEditionInfo(systemHivePath);
            }

            Console.WriteLine("Extracting version information from the image 2");
            VersionInfo2 info2 = ExtractVersionInfo2(softwareHivePath, systemHivePath);

            File.Delete(softwareHivePath);
            File.Delete(systemHivePath);

            report.Tag = info2.Tag;
            report.Licensing = info2.Licensing;
            report.LanguageCodes = info2.LanguageCodes;

            WindowsVersion version1 = new WindowsVersion
            {
                MajorVersion = info.MajorVersion,
                MinorVersion = info.MinorVersion,
                BuildNumber = info.BuildNumber,
                DeltaVersion = info.DeltaVersion,
                BranchName = info.BranchName,
                CompileDate = info.CompileDate
            };

            WindowsVersion version2 = new WindowsVersion
            {
                MajorVersion = info2.MajorVersion,
                MinorVersion = info2.MinorVersion,
                BuildNumber = info2.BuildNumber,
                DeltaVersion = info2.DeltaVersion,
                BranchName = info2.BranchName,
                CompileDate = info2.CompileDate
            };

            WindowsVersion correctVersion = Common.GetGreaterVersion(version1, version2);

            report.MajorVersion = correctVersion.MajorVersion;
            report.MinorVersion = correctVersion.MinorVersion;
            report.BuildNumber = correctVersion.BuildNumber;
            report.DeltaVersion = correctVersion.DeltaVersion;
            report.BranchName = correctVersion.BranchName;
            report.CompileDate = correctVersion.CompileDate;

            if (report.BuildNumber > 2195)
            {
                if (report.Sku.Equals("personal", StringComparison.InvariantCultureIgnoreCase))
                {
                    report.Sku = "Home";
                }
            }

            if (report.BuildNumber >= 1911)
            {
                if (report.Sku.Equals("workstation", StringComparison.InvariantCultureIgnoreCase))
                {
                    report.Sku = "Professional";
                }
            }

            if (IsUnstaged && report.Editions != null)
            {
                foreach (var skuunstaged in report.Editions)
                {
                    if ((skuunstaged.Contains("server", StringComparison.InvariantCultureIgnoreCase) && skuunstaged.EndsWith("hyperv", StringComparison.InvariantCultureIgnoreCase)) ||
                        (skuunstaged.Contains("server", StringComparison.InvariantCultureIgnoreCase) && skuunstaged.EndsWith("v", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        if (!report.Type.Contains(Type.ServerV))
                        {
                            report.Type.Add(Type.ServerV);
                        }
                    }
                    else if (skuunstaged.Contains("server", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (!report.Type.Contains(Type.Server))
                        {
                            report.Type.Add(Type.Server);
                        }
                    }
                    else
                    {
                        if (!report.Type.Contains(Type.Client))
                        {
                            report.Type.Add(Type.Client);
                        }
                    }
                }
            }
            else if (!string.IsNullOrEmpty(report.Sku))
            {
                if (report.Sku.Equals("ads", StringComparison.InvariantCultureIgnoreCase))
                {
                    report.Sku = "AdvancedServer";
                }

                if (report.Sku.Equals("pro", StringComparison.InvariantCultureIgnoreCase))
                {
                    report.Sku = "Professional";
                }

                if ((report.Sku.Contains("server", StringComparison.InvariantCultureIgnoreCase) && report.Sku.EndsWith("hyperv", StringComparison.InvariantCultureIgnoreCase)) ||
                    (report.Sku.Contains("server", StringComparison.InvariantCultureIgnoreCase) && report.Sku.EndsWith("v", StringComparison.InvariantCultureIgnoreCase)))
                {
                    if (!report.Type.Contains(Type.ServerV))
                    {
                        report.Type.Add(Type.ServerV);
                    }
                }
                else if (report.Sku.Contains("server", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!report.Type.Contains(Type.Server))
                    {
                        report.Type.Add(Type.Server);
                    }
                }
                else
                {
                    if (!report.Type.Contains(Type.Client))
                    {
                        report.Type.Add(Type.Client);
                    }
                }
            }

            return report;
        }

        private static VersionInfo1 ExtractVersionInfo(string kernelPath)
        {
            VersionInfo1 result = new VersionInfo1();

            FileVersionInfo info = FileVersionInfo.GetVersionInfo(kernelPath);

            result.Architecture = Common.GetMachineTypeFromFile(new FileStream(kernelPath, FileMode.Open));

            WindowsVersion verinfo = Common.ParseBuildString(info.FileVersion);

            result.MajorVersion = verinfo.MajorVersion;
            result.MinorVersion = verinfo.MinorVersion;
            result.BuildNumber = verinfo.BuildNumber;
            result.DeltaVersion = verinfo.DeltaVersion;
            result.BranchName = verinfo.BranchName;
            result.CompileDate = verinfo.CompileDate;

            result.BuildType = info.IsDebug ? BuildType.chk : BuildType.fre;

            return result;
        }

        private static VersionInfo2 ExtractVersionInfo2(string softwareHivePath, string systemHivePath)
        {
            VersionInfo2 result = new VersionInfo2();

            using (var hiveStream = new FileStream(softwareHivePath, FileMode.Open, FileAccess.Read))
            using (DiscUtils.Registry.RegistryHive hive = new DiscUtils.Registry.RegistryHive(hiveStream))
            {
                try
                {
                    DiscUtils.Registry.RegistryKey subkey = hive.Root.OpenSubKey(@"Microsoft\Windows NT\CurrentVersion");

                    string buildLab = (string)subkey.GetValue("BuildLab");
                    string buildLabEx = (string)subkey.GetValue("BuildLabEx");

                    string releaseId = (string)subkey.GetValue("ReleaseId");

                    int? UBR = (int?)subkey.GetValue("UBR");
                    int? Major = (int?)subkey.GetValue("CurrentMajorVersionNumber");
                    int? Minor = (int?)subkey.GetValue("CurrentMinorVersionNumber");

                    if (!string.IsNullOrEmpty(buildLab) && buildLab.Count(x => x == '.') == 2)
                    {
                        var splitLab = buildLab.Split('.');

                        result.BranchName = splitLab[1];
                        result.CompileDate = splitLab[2];
                        result.BuildNumber = ulong.Parse(splitLab[0]);
                    }

                    if (!string.IsNullOrEmpty(buildLabEx) && buildLabEx.Count(x => x == '.') == 4)
                    {
                        var splitLabEx = buildLabEx.Split('.');

                        result.BranchName = splitLabEx[3];
                        result.CompileDate = splitLabEx[4];
                        result.DeltaVersion = ulong.Parse(splitLabEx[1]);
                        result.BuildNumber = ulong.Parse(splitLabEx[0]);
                    }

                    if (UBR.HasValue)
                    {
                        result.DeltaVersion = (ulong)UBR.Value;
                    }

                    if (Major.HasValue)
                    {
                        result.MajorVersion = (ulong)Major.Value;
                    }

                    if (Minor.HasValue)
                    {
                        result.MinorVersion = (ulong)Minor.Value;
                    }

                    if (!string.IsNullOrEmpty(releaseId))
                    {
                        result.Tag = releaseId;
                    }
                }
                catch { };

                try
                {
                    string productId = "";

                    DiscUtils.Registry.RegistryKey subkey = hive.Root.OpenSubKey(@"Microsoft\Windows NT\CurrentVersion\DefaultProductKey");
                    if (subkey != null)
                    {
                        productId = (string)subkey.GetValue("ProductId");
                    }
                    else
                    {
                        subkey = hive.Root.OpenSubKey(@"Microsoft\Windows\CurrentVersion");
                        if (subkey != null)
                        {
                            productId = (string)subkey.GetValue("ProductId");
                        }
                    }

                    if (!string.IsNullOrEmpty(productId))
                    {
                        result.Licensing = productId.Contains("OEM") ? Licensing.OEM : Licensing.Retail;
                    }
                }
                catch { };
            }

            using (var hiveStream = new FileStream(systemHivePath, FileMode.Open, FileAccess.Read))
            using (DiscUtils.Registry.RegistryHive hive = new DiscUtils.Registry.RegistryHive(hiveStream))
            {
                try
                {
                    DiscUtils.Registry.RegistryKey subkey = hive.Root.OpenSubKey(@"ControlSet001\Control\MUI\UILanguages");

                    result.LanguageCodes = subkey.GetSubKeyNames();
                }
                catch { };

                try
                {
                    DiscUtils.Registry.RegistryKey subkey = hive.Root.OpenSubKey(@"ControlSet001\Control\Nls\Language");

                    string langid = (string)subkey.GetValue("Default");

                    var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

                    if (cultures.Any(x => x.LCID == int.Parse(langid, NumberStyles.HexNumber, CultureInfo.CurrentCulture)))
                    {
                        var name = cultures.First(x => x.LCID == int.Parse(langid, NumberStyles.HexNumber, CultureInfo.CurrentCulture)).Name;
                        if (result.LanguageCodes == null ||
                            result.LanguageCodes != null && !result.LanguageCodes.Any(x => x.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            if (result.LanguageCodes == null)
                            {
                                result.LanguageCodes = new string[] { name };
                            }
                            else
                            {
                                result.LanguageCodes = result.LanguageCodes.Append(name).ToArray();
                            }
                        }
                    }
                }
                catch { };
            }

            return result;
        }

        private static string ExtractEditionInfo(string systemHivePath)
        {
            string result = "";

            using (var hiveStream = new FileStream(systemHivePath, FileMode.Open, FileAccess.Read))
            using (DiscUtils.Registry.RegistryHive hive = new DiscUtils.Registry.RegistryHive(hiveStream))
            {
                try
                {
                    DiscUtils.Registry.RegistryKey subkey = hive.Root.OpenSubKey(@"ControlSet001\Control\ProductOptions");

                    string[] list = (string[])subkey.GetValue("ProductSuite");

                    result = list.Length > 0 ? list[0] : "";
                    if (string.IsNullOrEmpty(result))
                    {
                        result = "Workstation";
                    }

                    result = result.Replace(" ", "");

                    switch (result.ToLower())
                    {
                        case "enterprise":
                        case "backoffice":
                        case "datacenter":
                        case "securityappliance":
                            {
                                result = result + "Server";
                                break;
                            }
                        case "whserver":
                            {
                                result = "HomeServer";
                                break;
                            }
                        case "smallbusiness":
                            {
                                result = "SmallBusinessServer";
                                break;
                            }
                        case "smallbusiness(restricted)":
                            {
                                result = "SmallBusinessServerRestricted";
                                break;
                            }
                        case "blade":
                            {
                                result = "ServerWeb";
                                break;
                            }
                        case "embeddednt":
                            {
                                result = "Embedded";
                                break;
                            }
                        case "embedded(restricted)":
                            {
                                result = "EmbeddedRestricted";
                                break;
                            }
                    }
                }
                catch { };
            }

            return result;
        }

        private static string[] GatherUnstagedEditions(WindowsInstallProviderInterface installProvider)
        {
            SortedSet<string> report = new SortedSet<string>();
            foreach (var x in installProvider.GetFileSystemEntries())
            {
                bool foundsomething = false;

                var filename = x.ToLower();
                if (filename.StartsWith("packages") &&
                    filename.Contains("sku") &&
                    filename.IndexOf("sku") > ("packages").Length &&
                    filename.Contains("security-licensing-slc-component-sku") &&
                    filename.Contains("pl") &&
                    filename.Contains("xrm"))
                {
                    foundsomething = true;

                    var split = x.Split('\\');
                    var packagename = split[^2];
                    var lastpart = packagename.Split('-').Last();
                    var skufound = lastpart.Split('_')[0];

                    if (!report.Contains(skufound))
                    {
                        report.Add(skufound);
                    }
                }

                if (!foundsomething)
                {
                    foundsomething = false;
                    var filenamelast = filename.Split('\\').Last();

                    if (filename.StartsWith("packages") &&
                        filenamelast.StartsWith("update") &&
                        filenamelast.Contains("update.mum"))
                    {
                        foundsomething = true;

                        var split = x.Split('\\');
                        var packagename = split[^2];

                        if (!report.Contains(packagename))
                        {
                            report.Add(packagename);
                        }
                    }

                    if (!foundsomething)
                    {
                        if (filename.StartsWith("packages") &&
                        filenamelast.StartsWith("shellbrd") &&
                        filenamelast.EndsWith("dll"))
                        {
                            var split = x.Split('\\');
                            var packagename = split[^2];
                            var splitpkg = packagename.Split('-');
                            var lastpart = splitpkg.Last();
                            var skufound = lastpart.Split('_')[0];
                            if (skufound.Equals("edition", StringComparison.InvariantCultureIgnoreCase))
                            {
                                skufound = splitpkg[^2];
                            }

                            if (!report.Contains(skufound))
                            {
                                report.Add(skufound);
                            }
                        }
                    }
                }
            }

            return report.ToArray();
        }
    }
}