using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VDIBootEditor
{
    public partial class EditorForm : Form
    {
        private byte[] _vdiHeader = new byte[]
        {
            0x3C, 0x3C, 0x3C, 0x20, 0x4F, 0x72, 0x61, 0x63, 0x6C, 0x65, 0x20, 0x56, 0x4D, 0x20, 0x56, 0x69,
            0x72, 0x74, 0x75, 0x61, 0x6C, 0x42, 0x6F, 0x78, 0x20, 0x44, 0x69, 0x73, 0x6B, 0x20, 0x49, 0x6D, 
            0x61, 0x67, 0x65, 0x20, 0x3E, 0x3E, 0x3E
        };

        private int _mbrOffset;

        public EditorForm()
        {
            InitializeComponent();
        }

        private void openVDI_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "VirtualBox VDI files(*.vid)|*.vdi";
            if (opf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ValidImage(opf.FileName))
                {
                    vdiPath.Text = opf.FileName;
                }
                else
                {
                    vdiPath.Text = string.Empty;
                    MessageBox.Show("Invalid VDI!");
                }
            }
        }

        private bool ValidImage(string path)
        {
            try
            {
                bool ok = false;
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
                            ok = true;
                        }
                    }
                }

                return ok;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while checking VDI file: " + ex.Message);
            }
            return false;
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists(vdiPath.Text))
            {
                MessageBox.Show("File not found: " + vdiPath.Text);
                return;
            }

            SaveFileDialog svf = new SaveFileDialog();
            svf.Filter = "Bin (*.bin)|*.bin|Raw (*.raw)|*.raw|Img (*.img)|*.img";
            if (svf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    using (FileStream raw = File.OpenWrite(svf.FileName))
                    {
                        using (FileStream vdi = File.OpenRead(vdiPath.Text))
                        {
                            vdi.Position = _mbrOffset;
                            byte[] mbr = new byte[512];
                            vdi.Read(mbr, 0, mbr.Length);

                            raw.Write(mbr, 0, mbr.Length);
                        }
                    }
                    MessageBox.Show("Done!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to read MBR!\n" + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!File.Exists(vdiPath.Text))
            {
                MessageBox.Show("File not found: " + vdiPath.Text);
                return;
            }

            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "All binary files (*.bin,*.img,*.raw)|*.bin;*.img;*.raw|All files (*.*)|*.*";
            if (opf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    bool ok = true;

                    using (FileStream raw = File.OpenRead(opf.FileName))
                    {
                        if (raw.Length == 512)
                        {
                            using (FileStream vdi = File.OpenWrite(vdiPath.Text))
                            {
                                byte[] mbr = new byte[512];
                                raw.Read(mbr, 0, mbr.Length);

                                vdi.Position = _mbrOffset;
                                vdi.Write(mbr, 0, mbr.Length);
                            }
                        }
                        else
                        {
                            ok = false;
                            MessageBox.Show("Only 512 byte MBRs are supported!");
                        }
                    }
                    if (ok)
                    {
                        MessageBox.Show("Done!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to read MBR!\n" + ex.Message);
                }
            }
        }
    }
}
