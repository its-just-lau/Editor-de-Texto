using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor_de_Texto
{
    public partial class Editor : Form
    {
        bool archivoGuardado = false;
        string filePath = null;
        bool cambiosRealizados = false;
        public Editor()
        {
            InitializeComponent();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbEditor.Text != "" || archivoGuardado) {
                DialogResult resultado;
                resultado = MessageBox.Show("Desea guardar antes de abrir un nuevo documento?",
                    "Sistema", MessageBoxButtons.YesNoCancel);
                switch (resultado) {                       
                    case DialogResult.Yes:
                        guardarToolStripMenuItem_Click(sender,e);
                        archivoGuardado = false;
                        filePath = null;
                        rtbEditor.Clear();
                        break;
                     case DialogResult.No:
                        archivoGuardado = false;
                        filePath = null;
                        rtbEditor.Clear();
                        break;

                }

            }
            else
            {
                archivoGuardado = false;
                filePath = null;
                rtbEditor.Clear();
            }

        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado;
            resultado = openFileDialogEditor.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                filePath = openFileDialogEditor.FileName;
                try
                {
                    string texto = File.ReadAllText(filePath);
                    rtbEditor.Text = texto;
                    archivoGuardado = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al abrir el archivo: " + ex.Message);
                }
            }

        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado;
            if (archivoGuardado == false)
            {
                resultado = saveFileDialogEditor.ShowDialog();
                if (resultado == DialogResult.OK)
                {
                    filePath = saveFileDialogEditor.FileName;
                    string texto = rtbEditor.Text;
                    try
                    {
                        File.WriteAllText(filePath, texto);
                        MessageBox.Show("Archivo guardado correctamente");
                        archivoGuardado = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al guardar el archivo: " + ex.Message);
                    }
                }
            }
            else
            {
                try
                {
                    string texto = rtbEditor.Text;
                    File.WriteAllText(filePath, texto);
                    MessageBox.Show("Archivo guardado correctamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el archivo: " + ex.Message);
                }
            }

        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado;
            filePath = "";
            resultado = saveFileDialogEditor.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                filePath = saveFileDialogEditor.FileName;
                string texto = rtbEditor.Text;
                try
                {
                    File.WriteAllText(filePath, texto);
                    MessageBox.Show("Archivo guardado correctamente");
                    archivoGuardado = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el archivo: " + ex.Message);
                }
            }

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (archivoGuardado && cambiosRealizados) {
                DialogResult resultado;
                resultado = MessageBox.Show("Desea guardar antes de salir?",
                    "Sistema", MessageBoxButtons.YesNoCancel);
                switch (resultado)
                {
                    case DialogResult.Yes:
                        guardarToolStripMenuItem_Click(sender, e);
                        this.Close();
                        break;
                    case DialogResult.No:
                        this.Close();
                        break;

                }
            }
            else if (cambiosRealizados && !archivoGuardado) 
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Desea guardar antes de salir?",
                    "Sistema", MessageBoxButtons.YesNoCancel);
                switch (resultado)
                {
                    case DialogResult.Yes:
                        guardarToolStripMenuItem_Click(sender, e);
                        this.Close();
                        break;
                    case DialogResult.No:
                        this.Close();
                        break;

                }
            }
            else
            {
                this.Close();
            }
        }

        private void rtbEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiosRealizados = true;
        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
