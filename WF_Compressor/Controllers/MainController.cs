using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CL_Compressor;

namespace WF_Compressor.Controllers
{
    public class MainController
    {
        private readonly Form1 _view;
        private readonly HuffmanAlgorithm _model;
        private string? _selectedFilePath;

        /// <summary>
        /// Constructor que conecta el Modelo y la Vista.
        /// </summary>
        /// <param name="view">La instancia del formulario principal.</param>
        /// <param name="model">La instancia de la lógica de compresión.</param>
        public MainController(Form1 view, HuffmanAlgorithm model)
        {
            _view = view;
            _model = model;

            // --- ¡Aquí conectamos los cables! ---
            // Nos suscribimos a los eventos que la Vista dispara.
            _view.SelectFileButtonClick += OnSelectFileClick;
            _view.CompressButtonClick += OnCompressClick;
            _view.DecompressButtonClick += OnDecompressClick;
            _view.FileDropped += OnFileDropped;
        }

        /// <summary>
        /// Manejador para el evento de clic en el botón "Seleccionar Archivo".
        /// </summary>
        private void OnSelectFileClick(object? sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                // Actualizamos el filtro para aceptar ambos tipos de archivo
                dialog.Filter = "Archivos Soportados (*.txt, *.huff)|*.txt;*.huff|Todos los archivos (*.*)|*.*";
                dialog.Title = "Selecciona un archivo";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SetSelectedFile(dialog.FileName);
                }
            }
        }

        /// <summary>
        /// Manejador para el evento de soltar un archivo en el panel.
        /// </summary>
        private void OnFileDropped(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
            {
                // Tomamos solo el primer archivo si arrastran varios.
                SetSelectedFile(files[0]);
            }
        }

        /// <summary>
        /// Manejador para el evento de clic en el botón "Comprimir".
        /// Usamos async/await para no congelar la interfaz durante la compresión.
        /// </summary>
        private async void OnCompressClick(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedFilePath)) return;

            try
            {
                _view.ResetProgressAndStats();
                _view.UpdateStatus("Leyendo archivo...");

                long originalSize = new FileInfo(_selectedFilePath).Length;
                string fileContent = await File.ReadAllTextAsync(_selectedFilePath);

                if (string.IsNullOrEmpty(fileContent))
                {
                    MessageBox.Show("El archivo está vacío.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _view.UpdateStatus("Archivo vacío. Selecciona otro.");
                    return;
                }

                // --- INICIO DE CAMBIOS ---

                // 1. Preguntamos al usuario dónde guardar el archivo
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Archivo Huffman (*.huff)|*.huff";
                    saveDialog.Title = "Guardar archivo comprimido";
                    // 2. Sugerimos un nombre limpio: "archivo.txt" se convierte en "archivo.huff"
                    saveDialog.FileName = Path.GetFileNameWithoutExtension(_selectedFilePath) + ".huff";

                    // Si el usuario presiona "Guardar"
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        _view.UpdateStatus("Comprimiendo...");
                        var progress = new Progress<int>(percentage => _view.UpdateProgress(percentage));
                        byte[] compressedData = await Task.Run(() => _model.Compress(fileContent, progress));

                        // 3. Usamos la ruta que el usuario eligió
                        string compressedFilePath = saveDialog.FileName;
                        await File.WriteAllBytesAsync(compressedFilePath, compressedData);
                        long compressedSize = new FileInfo(compressedFilePath).Length;

                        // Calculamos y mostramos las estadísticas
                        double reduction = 100.0 - ((double)compressedSize / originalSize * 100.0);
                        string stats = $"Tamaño Original: {originalSize / 1024.0:F2} KB\n" +
                                       $"Tamaño Comprimido: {compressedSize / 1024.0:F2} KB\n" +
                                       $"Ahorro: {reduction:F2}%";
                        _view.DisplayStats(stats);

                        MessageBox.Show($"Compresión completada.\n{stats}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetSelection();
                    }
                    else // Si el usuario cancela, no hacemos nada
                    {
                        _view.UpdateStatus("Compresión cancelada por el usuario.");
                    }
                }
                // --- FIN DE CAMBIOS ---
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _view.UpdateStatus("Error durante la compresión.");
            }
        }

        /// <summary>
        /// Manejador para el evento de clic en el botón "Descomprimir".
        /// </summary>
        private async void OnDecompressClick(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedFilePath)) return;

            try
            {
                _view.ResetProgressAndStats();
                _view.UpdateStatus("Descomprimiendo...");

                long originalSize = new FileInfo(_selectedFilePath).Length;
                byte[] compressedBytes = await File.ReadAllBytesAsync(_selectedFilePath);

                // --- INICIO DE CAMBIOS ---

                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Archivo de Texto (*.txt)|*.txt";
                    saveDialog.Title = "Guardar archivo descomprimido";
                    // Sugerimos un nombre limpio para el archivo de salida
                    saveDialog.FileName = Path.GetFileNameWithoutExtension(_selectedFilePath) + "_descomprimido.txt";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        var progress = new Progress<int>(percentage => _view.UpdateProgress(percentage));
                        string decompressedContent = await Task.Run(() => _model.Decompress(compressedBytes, progress));

                        // Usamos la ruta elegida por el usuario
                        string decompressedFilePath = saveDialog.FileName;
                        await File.WriteAllTextAsync(decompressedFilePath, decompressedContent);
                        long decompressedSize = new FileInfo(decompressedFilePath).Length;

                        string stats = $"Tamaño Comprimido: {originalSize / 1024.0:F2} KB\n" +
                                       $"Tamaño Original: {decompressedSize / 1024.0:F2} KB";
                        _view.DisplayStats(stats);

                        MessageBox.Show($"Descompresión completada.\n{stats}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetSelection();
                    }
                    else // Si el usuario cancela
                    {
                        _view.UpdateStatus("Descompresión cancelada por el usuario.");
                    }
                }
                // --- FIN DE CAMBIOS ---
            }
            catch (Exception ex)
            {
                MessageBox.Show($"El archivo está corrupto o no es un archivo .huff válido.\nError: {ex.Message}", "Error de Descompresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _view.UpdateStatus("Error durante la descompresión.");
            }
        }

        /// <summary>
        /// Método centralizado para manejar un archivo recién seleccionado.
        /// </summary>
        private void SetSelectedFile(string filePath)
        {
            _selectedFilePath = filePath;
            string extension = Path.GetExtension(filePath).ToLower();

            // Desactivamos ambos botones antes de decidir
            _view.SetCompressButtonEnabled(false);
            _view.SetDecompressButtonEnabled(false); // Necesitarás añadir este método a tu MainForm

            if (extension == ".txt")
            {
                _view.SetCompressButtonEnabled(true);
                _view.UpdateStatus($"Archivo de texto seleccionado:\n{Path.GetFileName(_selectedFilePath)}");
            }
            else if (extension == ".huff")
            {
                _view.SetDecompressButtonEnabled(true);
                _view.UpdateStatus($"Archivo comprimido seleccionado:\n{Path.GetFileName(_selectedFilePath)}");
            }
            else
            {
                _view.UpdateStatus($"Archivo no soportado:\n{Path.GetFileName(_selectedFilePath)}");
            }
        }

        /// <summary>
        /// Reinicia el estado de la selección de archivo.
        /// </summary>
        private void ResetSelection()
        {
            _selectedFilePath = null;
            _view.SetCompressButtonEnabled(false);
            _view.SetDecompressButtonEnabled(false);
            _view.ResetProgressAndStats(); // Añadimos esto para limpiar la UI
            _view.UpdateStatus("Arrastra un archivo aquí o selecciónalo");
        }
    }
}

