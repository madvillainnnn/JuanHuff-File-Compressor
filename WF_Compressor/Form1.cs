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
            this.dragDropPanel.DragDrop += (s, e) => FileDropped?.Invoke(s, e);
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
        private void dragDropPanel_DragEnter(object sender, DragEventArgs e)
        {
            // Comprobamos si lo que se arrastra es un archivo
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // Muestra el cursor de "copiar"
            }
            else
            {
                e.Effect = DragDropEffects.None; // Muestra el cursor de "prohibido"
            }
        }
    }
}
