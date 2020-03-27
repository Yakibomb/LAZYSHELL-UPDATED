using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class LevelsTemplate : NewForm
    {
        // variables
        //
        private Levels levels;
        private Overlay overlay;
        private LevelTemplate template; public LevelTemplate Template { get { return this.template; } }
        private LevelTemplate templateC;
        private ArrayList templates = new ArrayList();
        private Bitmap templateImage;
        public ToolStripButton TemplateTransfer { get { return templateTransfer; } set { templateTransfer = value; } }
        // functions
        public LevelsTemplate(Levels levels, Overlay overlay)
        {
            this.levels = levels;
            this.overlay = overlay;
            InitializeComponent();
            SetTemplateImage();
        }
        public void Reload(Levels levels, Overlay overlay)
        {
            this.levels = levels;
            this.overlay = overlay;
            SetTemplateImage();
        }
        private void SetTemplateImage()
        {
            if (template == null)
                return;
            pictureBoxTemplate.Size = template.Size;
            templateImage = Do.PixelsToImage(template.GetPixels(levels.Level, levels.Tileset),
                template.Size.Width, template.Size.Height);
            pictureBoxTemplate.Invalidate();
        }
        // event handlers
        private void templateTransfer_Click(object sender, EventArgs e)
        {
            if (overlay.Select.Empty || overlay.Select.Size == new Size(0, 0))
            {
                MessageBox.Show("Need to make a selection before creating a new template.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // now create the template from the selection
            template = new LevelTemplate();
            // can't have templates with the same name
            int ctr = 0;
            string name = "New template";
            foreach (LevelTemplate lt in templates)
            {
                if (lt.Name == name)
                {
                    name = name + ctr.ToString();
                    ctr++;
                }
            }
            template.Name = name;
            templates.Add(template);
            levels.Tilemap.Assemble();
            template.Transfer(levels.Tilemap.Tilemaps_Bytes, levels.LevelMap, levels.SolidityMap, overlay.Select.Location, overlay.Select.Terminal);
            // add to listbox
            templatesLoaded.BeginUpdate();
            templatesLoaded.Items.Add(template.Name);
            templatesLoaded.SelectedIndex = templatesLoaded.Items.Count - 1;
            templatesLoaded.EndUpdate();
        }
        private void templateImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Settings.Default.LastRomPath;
            openFileDialog1.Title = "Select files to import";
            openFileDialog1.Filter = "Data files (*.dat)|*.dat|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = true;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            templates.Clear();
            templatesLoaded.Items.Clear();
            foreach (string path in openFileDialog1.FileNames)
            {
                Stream s = File.OpenRead(path);
                BinaryFormatter b = new BinaryFormatter();
                LevelTemplate temp = (LevelTemplate)b.Deserialize(s);
                templates.Add(temp);
                templatesLoaded.Items.Add(temp.Name);
                s.Close();
            }
            if (templatesLoaded.Items.Count > 0)
                templatesLoaded.SelectedIndex = 0;
        }
        private void templateExport_Click(object sender, EventArgs e)
        {
            if (this.templates.Count == 0)
                return;
            // first, open and create directory
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.SelectedPath = Settings.Default.LastDirectory;
            folderBrowserDialog1.Description = "Select directory to export to";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                Settings.Default.LastDirectory = folderBrowserDialog1.SelectedPath;
            else
                return;
            string fullPath = folderBrowserDialog1.SelectedPath + "\\";
            FileInfo fi = new FileInfo(fullPath);
            DirectoryInfo di = new DirectoryInfo(fi.DirectoryName);
            if (!di.Exists)
                di.Create();
            foreach (LevelTemplate lt in templates)
                Do.Export(lt, null, fullPath + lt.Name + ".dat");
        }
        private void templatesLoaded_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (templatesLoaded.SelectedIndex == -1)
                return;
            template = (LevelTemplate)templates[templatesLoaded.SelectedIndex];
            templateRenameText.Text = template.Name;
            SetTemplateImage();
            templateExport.Enabled = true;
            templatesLoaded.Enabled = true;
            toolStrip1.Enabled = true;
        }
        private void templateRename_Click(object sender, EventArgs e)
        {
            templateRenameText.Visible = templateRename.Checked;
        }
        private void templateRenameText_TextChanged(object sender, EventArgs e)
        {
            if (templates.Count == 0)
                return;
            if (templateRenameText.Text == "")
            {
                MessageBox.Show("A template name cannot be empty.", "LAZY SHELL");
                return;
            }
            foreach (LevelTemplate lt in templates)
            {
                if (template != lt && templateRenameText.Text == lt.Name)
                {
                    MessageBox.Show("Cannot rename " + lt.Name + ". A template with the name you specified already exists.",
                       "LAZY SHELL");
                    return;
                }
                else if (template == lt && template.Name == templateRenameText.Text)
                    return;
            }
            template.Name = templateRenameText.Text;
            templatesLoaded.Items[templatesLoaded.SelectedIndex] = template.Name;
        }
        private void templateDelete_Click(object sender, EventArgs e)
        {
            if (template == null || templates.Count == 0)
                return;
            templates.Remove(template);
            int temp = templatesLoaded.SelectedIndex;
            templatesLoaded.BeginUpdate();
            templatesLoaded.Items.Clear();
            foreach (LevelTemplate lt in templates)
                templatesLoaded.Items.Add(lt.Name);
            templatesLoaded.EndUpdate();
            if (templates.Count == 0)
            {
                templateExport.Enabled = false;
                templatesLoaded.Enabled = false;
                templateRenameText.Text = "";
                toolStrip1.Enabled = false;
                templateImage = null;
                pictureBoxTemplate.Invalidate();
                template = null;
            }
            else if (templates.Count == temp)
                templatesLoaded.SelectedIndex = temp - 1;
            else
                templatesLoaded.SelectedIndex = temp;
        }
        private void templateCopy_Click(object sender, EventArgs e)
        {
            if (template == null || templates.Count == 0)
                return;
            templateC = template;
        }
        private void templatePaste_Click(object sender, EventArgs e)
        {
            if (templateC == null || templates.Count == 0)
                return;
            template = templateC;
            templates.Add(template);
            templatesLoaded.Items.Add(template.Name);
            templatesLoaded.SelectedIndex = templatesLoaded.Items.Count - 1;
            templatesLoaded.Enabled = true;
            templateRenameText.Enabled = true;
            templateRename.Enabled = true;
            templateRenameText.Text = template.Name;
        }
        private void pictureBoxTemplate_Paint(object sender, PaintEventArgs e)
        {
            if (templateImage != null)
                e.Graphics.DrawImage(templateImage, 0, 0);
        }
    }
}
