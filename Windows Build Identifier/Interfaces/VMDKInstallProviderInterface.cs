﻿using DiscUtils.Vmdk;
using DiscUtils.Ntfs;
using DiscUtils.Partitions;
using System.IO;

namespace WindowsBuildIdentifier.Interfaces
{
    public class VMDKInstallProviderInterface : WindowsInstallProviderInterface
    {
        private readonly string _vhdpath;
        private readonly Disk _vhd;
        private readonly NtfsFileSystem _ntfs;

        public VMDKInstallProviderInterface(string vhdpath)
        {
            _vhdpath = vhdpath;
            _vhd = new Disk(vhdpath, FileAccess.Read);

            PartitionInfo part = null;
            foreach (var partition in _vhd.Partitions.Partitions)
            {
                if (part == null)
                {
                    part = partition;
                }
                else
                {
                    if (partition.SectorCount > part.SectorCount)
                    {
                        part = partition;
                    }
                }
            }

            _ntfs = new NtfsFileSystem(part.Open());
        }

        public void Close()
        {
            _vhd.Dispose();
        }

        public string ExpandFile(string Entry)
        {
            var tmp = Path.GetTempFileName();
            using (var srcstrm = _ntfs.OpenFile(Entry, FileMode.Open))
            {
                using var dststrm = new FileStream(tmp, FileMode.Append);
                srcstrm.CopyTo(dststrm);
            }

            return tmp;
        }

        public string[] GetFileSystemEntries()
        {
            return _ntfs.GetFiles("", "*.*", SearchOption.AllDirectories);
        }
    }
}
