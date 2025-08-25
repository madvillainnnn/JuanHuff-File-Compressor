namespace WF_Compressor
{
    public partial class Form1 : Form
    {
        // Propiedades para que el Controlador pueda acceder a los eventos
        public event EventHandler? SelectFileButtonClick;
        public event EventHandler? CompressButtonClick;
        public event EventHandler? DecompressButtonClick;
        public event DragEventHandler? FileDropped;

        // UNICO CONSTRUCTOR EXISTETE
        public Form1()
        {
            InitializeComponent();
            this.Text = "JuanHuff";
            // Conectamos los eventos del diseñador a nuestros eventos públicos para que el Controlador los escuche.
            this.selectFileButton.Click += (s, e) => SelectFileButtonClick?.Invoke(s, e);
            this.compressButton.Click += (s, e) => CompressButtonClick?.Invoke(s, e);
            this.dragDropPanel.DragDrop += dragDropPanel_DragDrop;
            this.decompressButton.Click += (s, e) => DecompressButtonClick?.Invoke(s, e);
        }

        // --- Métodos públicos para que el Controlador actualice la Vista ---

        /// <summary>
        /// Actualiza el texto de la etiqueta de estado.
        /// Este método debe ser seguro para llamadas desde otros hilos.
        /// </summary>
        /// <param name="message">El mensaje a mostrar.</param>
        public void UpdateStatus(string message)
        {
            if (statusLabel.InvokeRequired)
            {
                statusLabel.Invoke(new Action(() => statusLabel.Text = message));
            }
            else
            {
                statusLabel.Text = message;
            }
        }

        /// <summary>
        /// Habilita o deshabilita el botón de compresión.
        /// </summary>
        /// <param name="isEnabled">Verdadero para habilitar, falso para deshabilitar.</param>
        public void SetCompressButtonEnabled(bool isEnabled)
        {
            if (compressButton.InvokeRequired)
            {
                compressButton.Invoke(new Action(() => compressButton.Enabled = isEnabled));
            }
            else
            {
                compressButton.Enabled = isEnabled;
            }
        }

        public void SetDecompressButtonEnabled(bool isEnabled)
        {
            if (decompressButton.InvokeRequired)
            {
                decompressButton.Invoke(new Action(() => decompressButton.Enabled = isEnabled));
            }
            else
            {
                decompressButton.Enabled = isEnabled;
            }
        }

        // --- Manejadores de eventos de Drag & Drop (solo para feedback visual) ---

        /// <summary>
        /// Evento que se dispara cuando un archivo entra en el área del panel.
        /// Cambia el cursor para dar feedback visual al usuario.
        /// </summary>

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Actualiza el valor de la barra de progreso.
        /// </summary>
        public void UpdateProgress(int percentage)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action(() => progressBar.Value = percentage));
            }
            else
            {
                progressBar.Value = percentage;
            }
        }

        /// <summary>
        /// Muestra el texto de las estadísticas y hace visible la barra de progreso.
        /// </summary>
        public void DisplayStats(string statsText)
        {
            // Hacemos visible la barra y la etiqueta de stats
            if (statsLabel.InvokeRequired)
            {
                statsLabel.Invoke(new Action(() =>
                {
                    statsLabel.Text = statsText;
                    statsLabel.Visible = true;
                }));
            }
            else
            {
                statsLabel.Text = statsText;
                statsLabel.Visible = true;
            }
        }

        /// <summary>
        /// Reinicia y oculta los controles de progreso y stats.
        /// </summary>
        public void ResetProgressAndStats()
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action(() =>
                {
                    progressBar.Value = 0;
                    progressBar.Visible = false;
                    statsLabel.Visible = false;
                    statsLabel.Text = "";
                }));
            }
            else
            {
                progressBar.Value = 0;
                progressBar.Visible = false;
                statsLabel.Visible = false;
                statsLabel.Text = "";
            }
        }

        // MÉTODOS PARA EFECTOS VISUALES

        private void dragDropPanel_DragEnter(object sender, DragEventArgs e)
        {

            // Comprobamos si lo que se arrastra es un archivo
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                dragDropPanel.BackColor = Color.LightGreen;
                e.Effect = DragDropEffects.Copy; // Muestra el cursor de "copiar"
            }
            else
            {
                dragDropPanel.BackColor = Color.LightCoral;
                e.Effect = DragDropEffects.None; // Muestra el cursor de "prohibido"
            }
        }

        private void dragDropPanel_DragDrop(object? sender, DragEventArgs e)
        {
            // 1. Primero, nos encargamos de la parte visual: reseteamos el color.
            dragDropPanel.BackColor = SystemColors.Control;

            // 2. Después, le avisamos al Controller que el archivo fue soltado.
            FileDropped?.Invoke(sender, e);
        }

        private void dragDropPanel_DragLeave(object sender, EventArgs e)
        {
            dragDropPanel.BackColor = SystemColors.Control;
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            compressButton.BackColor = Color.LightBlue;
            this.Cursor = Cursors.Hand;
        }

        private void compressButton_MouseEnter(object sender, EventArgs e)
        {
            selectFileButton.BackColor = Color.LightBlue;
            this.Cursor = Cursors.Hand;
        }

        private void compressButton_MouseLeave(object sender, EventArgs e)
        {
            compressButton.BackColor = SystemColors.Control;
            this.Cursor = Cursors.Default;
        }

        private void selectFileButton_MouseEnter(object sender, EventArgs e)
        {
            selectFileButton.BackColor = Color.LightBlue;
            this.Cursor = Cursors.Hand;
        }

        private void selectFileButton_MouseLeave(object sender, EventArgs e)
        {
            selectFileButton.BackColor = SystemColors.Control;
            this.Cursor = Cursors.Default;
        }

        private void decompressButton_MouseEnter(object sender, EventArgs e)
        {
            decompressButton.BackColor = Color.LightBlue;
            this.Cursor = Cursors.Hand;
        }

        private void decompressButton_MouseLeave(object sender, EventArgs e)
        {
            decompressButton.BackColor = SystemColors.Control;
            this.Cursor = Cursors.Default;
        }
    }
}
