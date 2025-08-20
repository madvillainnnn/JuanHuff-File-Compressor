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
            if (string.IsNullOrEmpty(_selectedFilePath))
            {
                MessageBox.Show("Por favor, selecciona un archivo primero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Actualizamos la vista para dar feedback
                _view.UpdateStatus("Leyendo archivo...");
                string fileContent = await File.ReadAllTextAsync(_selectedFilePath);

                if (string.IsNullOrEmpty(fileContent))
                {
                    MessageBox.Show("El archivo está vacío y no se puede comprimir.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _view.UpdateStatus("Archivo vacío. Selecciona otro.");
                    return;
                }

                _view.UpdateStatus("Comprimiendo... esto puede tardar un momento.");

                // Usamos Task.Run para ejecutar la lógica pesada en un hilo secundario
                byte[] compressedData = await Task.Run(() =>
                {
                    var frequencyTable = _model.BuildFrequencyTable(fileContent);
                    var huffmanTree = _model.BuildHuffmanTree(frequencyTable);
                    var codes = _model.GenerateCodes(huffmanTree);
                    // ¡Aquí podrías necesitar también el árbol para descomprimir!
                    // Por simplicidad, solo guardamos los datos comprimidos.
                    return _model.Compress(fileContent);
                });

                // Guardamos el archivo comprimido
                string compressedFilePath = _selectedFilePath + ".huff";
                await File.WriteAllBytesAsync(compressedFilePath, compressedData);

                // Mostramos mensaje de éxito
                _view.UpdateStatus($"¡Éxito! Archivo guardado en:\n{compressedFilePath}");
                MessageBox.Show($"Compresión completada con éxito.\nArchivo guardado como: {Path.GetFileName(compressedFilePath)}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetSelection();
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
            if (string.IsNullOrEmpty(_selectedFilePath))
            {
                MessageBox.Show("Por favor, selecciona un archivo primero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _view.UpdateStatus("Descomprimiendo... esto puede tardar.");

                // Leemos todos los bytes del archivo .huff
                byte[] compressedBytes = await File.ReadAllBytesAsync(_selectedFilePath);

                // Llamamos a nuestro nuevo método en el modelo
                string decompressedContent = await Task.Run(() => _model.Decompress(compressedBytes));

                // Guardamos el archivo descomprimido
                string decompressedFilePath = _selectedFilePath.Replace(".huff", "_descomprimido.txt");
                await File.WriteAllTextAsync(decompressedFilePath, decompressedContent);

                // Mensaje de éxito
                _view.UpdateStatus($"¡Éxito! Archivo guardado en:\n{decompressedFilePath}");
                MessageBox.Show($"Descompresión completada.\nArchivo guardado como: {Path.GetFileName(decompressedFilePath)}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetSelection();
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
            _view.UpdateStatus("Arrastra un archivo .txt aquí o selecciónalo");
        }
    }
}

