//
// Copyright (c) 2008-2011, Kenneth Bell
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
//

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly: AssemblyTitle("DiscUtils")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Kenneth Bell")]
[assembly: AssemblyProduct("DiscUtils")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM

[assembly: Guid("64a0f4ef-c057-4afe-a8fb-3152de41b143")]
[assembly: NeutralResourcesLanguage("en")]
[assembly: InternalsVisibleTo("DiscUtils, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.BootConfig, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Btrfs, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Containers, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Dmg, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Ext, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Fat, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.FileSystems, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.HfsPlus, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Iscsi, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Iso9660, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Net, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Nfs, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Ntfs, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.OpticalDiscSharing, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.OpticalDisk, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Registry, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Sdi, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.SquashFs, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Streams, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Swap, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Transports, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Udf, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Vdi, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Vfs, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Vhd, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Vhdx, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Vmdk, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Wim, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Xva, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Diagnostics, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Lvm, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("DiscUtils.Xfs, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
[assembly: InternalsVisibleTo("LibraryTests, PublicKey=002400000480000094000000060200000024000052534131000400000100010047ebec172a9831bb20fede77e17d784026ea7030d7055f2ae09576c71cebe77ebfab436d80580a4fcbba7242ff61bd52b686f5fe9d41fe7cd3e6c05b8a876eccf35b8ad7c5e3a6704295d7210b138d7280a6f72688419a65dd7a8612d66869f2e712c57c41fcc9196e4cb06d95d8e678f6967e65348c370405fb7eeb6aa1d3e8")]
