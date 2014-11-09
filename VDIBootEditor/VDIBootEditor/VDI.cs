using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VDIBootEditor
{
    enum VDIError
    { 
        NoError,
        InvalidHeader,
        InvalidFileLength,

        Not512Bytes,

        Exception
    }

    class VDI
    {
        private byte[] _vdiHeader = new byte[]
        {
            0x3C, 0x3C, 0x3C, 0x20, 0x4F, 0x72, 0x61, 0x63, 0x6C, 0x65, 0x20, 0x56, 0x4D, 0x20, 0x56, 0x69,
            0x72, 0x74, 0x75, 0x61, 0x6C, 0x42, 0x6F, 0x78, 0x20, 0x44, 0x69, 0x73, 0x6B, 0x20, 0x49, 0x6D, 
            0x61, 0x67, 0x65, 0x20, 0x3E, 0x3E, 0x3E
        };

        private int _mbrOffset;

        public Exception Exception;

        private string _path;

        public VDIError OpenFile(string path)
        {
            _path = path;

            try
            {
                VDIError ret = VDIError.NoError;
                using (FileStream fs = File.OpenRead(path))
                {
                    if (fs.Length > _vdiHeader.Length)
                    {
                        byte[] header = new byte[_vdiHeader.Length];
                        fs.Read(header, 0, header.Length);

                        if (CompareArrays(_vdiHeader, header))
                        {
                            byte[] mbrPosition = new byte[4];
                            fs.Position = 0x159;
                            fs.Read(mbrPosition, 0, mbrPosition.Length);

                            _mbrOffset = LittleEndianToInt(mbrPosition);
                        }
                        else
                        {
                            ret = VDIError.InvalidHeader;
                        }
                    }
                    else
                    {
                        ret = VDIError.InvalidFileLength;
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
            return VDIError.Exception;
        }

        private bool CompareArrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            for (int x = 0; x < a.Length; x++)
            {
                if (a[x] != b[x])
                    return false;
            }
            return true;
        }

        private int LittleEndianToInt(byte[] data)
        {
            return (data[0] << 24)
                 | (data[1] << 16)
                 | (data[2] << 8)
                 | data[3];
        }

        public bool ReadMBR(string binaryFile)
        {
            try
            {
                using (FileStream raw = File.OpenWrite(binaryFile))
                {
                    using (FileStream vdi = File.OpenRead(_path))
                    {
                        vdi.Position = _mbrOffset;
                        byte[] mbr = new byte[512];
                        vdi.Read(mbr, 0, mbr.Length);

                        raw.Write(mbr, 0, mbr.Length);
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                Exception = ex;
            }
            return false;
        }

        public VDIError WriteMBR(string binaryFile)
        {
            try
            {
                VDIError err = VDIError.NoError;
                using (FileStream raw = File.OpenRead(binaryFile))
                {
                    if (raw.Length == 512)
                    {
                        using (FileStream vdi = File.OpenWrite(_path))
                        {
                            byte[] mbr = new byte[512];
                            raw.Read(mbr, 0, mbr.Length);

                            vdi.Position = _mbrOffset;
                            vdi.Write(mbr, 0, mbr.Length);
                        }
                    }
                    else
                    {
                        err = VDIError.Not512Bytes;
                    }
                }

                return err;
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
            return VDIError.Exception;
        }
    }
}
