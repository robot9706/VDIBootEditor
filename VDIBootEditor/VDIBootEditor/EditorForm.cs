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
        private VDI _vdi;

        public EditorForm()
        {
            InitializeComponent();

            _vdi = new VDI();
        }

        private void openVDI_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "VirtualBox VDI files(*.vid)|*.vdi";
            if (opf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VDIError err = _vdi.OpenFile(opf.FileName);
                if (err == VDIError.NoError)
                {
                    vdiPath.Text = opf.FileName;
                }
                else
                {
                    vdiPath.Text = string.Empty;
                    MessageBoxError(err);
                }
            }
        }

        private void MessageBoxError(VDIError err)
        {
            if (err == VDIError.Exception)
            {
                MessageBox.Show("Error: " + _vdi.Exception.Message);
            }
            else
            {
                MessageBox.Show("Error: " + err.ToString());
            }
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
                if (_vdi.ReadMBR(svf.FileName))
                {
                    MessageBox.Show("Done!");
                }
                else
                {
                    MessageBox.Show("Error: " + _vdi.Exception.Message);
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
                VDIError err = _vdi.WriteMBR(opf.FileName);
                if (err == VDIError.NoError)
                {
                    MessageBox.Show("Done!");
                }
                else
                {
                    MessageBoxError(err);
                }
            }
        }
    }
}
