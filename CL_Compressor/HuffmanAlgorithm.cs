using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace CL_Compressor
{
    public class HuffmanAlgorithm
    {
        // --- MÉTODOS DE COMPRESIÓN (MODIFICADOS) ---

        /// <summary>
        /// Comprime el texto original y empaqueta la tabla de frecuencias para la futura descompresión.
        /// </summary>
        /// <param name="text">El texto original a comprimir.</param>
        /// <returns>Un array de bytes que representa el archivo .huff completo (mapa + datos).</returns>
        public byte[] Compress(string text)
        {
            // 1. Construir todo lo necesario como antes
            var frequencyTable = BuildFrequencyTable(text);
            var huffmanTree = BuildHuffmanTree(frequencyTable);
            var codes = GenerateCodes(huffmanTree);

            // 2. Codificar el texto a una cadena de bits
            var stringBuilder = new StringBuilder();
            foreach (char symbol in text)
            {
                stringBuilder.Append(codes[symbol]);
            }
            string bitString = stringBuilder.ToString();

            // 3. Convertir la tabla de frecuencias a JSON (nuestro "mapa")
            string headerJson = JsonSerializer.Serialize(frequencyTable);
            byte[] headerBytes = Encoding.UTF8.GetBytes(headerJson);
            byte[] headerLengthBytes = BitConverter.GetBytes(headerBytes.Length);

            // 4. Convertir la cadena de bits a bytes de datos
            var dataBytes = new List<byte>();
            int padding = (8 - (bitString.Length % 8)) % 8;
            dataBytes.Add((byte)padding); // El primer byte de datos nos dice el relleno
            for (int i = 0; i < bitString.Length; i += 8)
            {
                string byteString = bitString.Length - i < 8
                    ? bitString.Substring(i).PadRight(8, '0')
                    : bitString.Substring(i, 8);
                dataBytes.Add(Convert.ToByte(byteString, 2));
            }

            // 5. Unir todo en un solo array de bytes: [Tamaño del Mapa] + [Mapa] + [Datos]
            return headerLengthBytes.Concat(headerBytes).Concat(dataBytes).ToArray();
        }

        // --- MÉTODOS DE DESCOMPRESIÓN (NUEVOS) ---

        /// <summary>
        /// Descomprime un array de bytes de un archivo .huff.
        /// </summary>
        /// <param name="compressedFileBytes">El contenido completo del archivo .huff.</param>
        /// <returns>El texto original descomprimido.</returns>
        public string Decompress(byte[] compressedFileBytes)
        {
            // 1. Leer el tamaño del mapa (los primeros 4 bytes)
            int headerLength = BitConverter.ToInt32(compressedFileBytes, 0);

            // 2. Leer el mapa JSON y deserializarlo de vuelta a un Diccionario
            string headerJson = Encoding.UTF8.GetString(compressedFileBytes, 4, headerLength);
            var frequencyTable = JsonSerializer.Deserialize<Dictionary<char, int>>(headerJson);

            if (frequencyTable == null || frequencyTable.Count == 0)
            {
                return string.Empty; // Archivo vacío o corrupto
            }

            // 3. Reconstruir el árbol de Huffman EXACTO que se usó para comprimir
            var huffmanTree = BuildHuffmanTree(frequencyTable);

            // 4. Leer los datos comprimidos (todo lo que viene después del mapa)
            int dataStartIndex = 4 + headerLength;
            byte padding = compressedFileBytes[dataStartIndex];

            var stringBuilder = new StringBuilder();
            for (int i = dataStartIndex + 1; i < compressedFileBytes.Length; i++)
            {
                stringBuilder.Append(Convert.ToString(compressedFileBytes[i], 2).PadLeft(8, '0'));
            }

            // 5. Quitar el relleno para obtener la cadena de bits original
            string bitString = stringBuilder.ToString();
            bitString = bitString.Substring(0, bitString.Length - padding);

            // 6. Decodificar la cadena de bits usando el árbol
            var decompressedText = new StringBuilder();
            var currentNode = huffmanTree;
            foreach (char bit in bitString)
            {
                currentNode = bit == '0' ? currentNode.LeftChild : currentNode.RightChild;
                if (currentNode != null && currentNode.IsLeaf)
                {
                    decompressedText.Append(currentNode.Simbolo);
                    currentNode = huffmanTree; // Volver a la raíz para el siguiente símbolo
                }
            }

            return decompressedText.ToString();
        }

        // --- MÉTODOS AUXILIARES (SIN CAMBIOS) ---
        public Dictionary<char, int> BuildFrequencyTable(string text)
        {
            var frequencyTable = new Dictionary<char, int>();
            foreach (char symbol in text)
            {
                if (frequencyTable.ContainsKey(symbol))
                {
                    frequencyTable[symbol]++;
                }
                else
                {
                    frequencyTable[symbol] = 1;
                }
            }
            return frequencyTable;
        }

        public HuffmanNode BuildHuffmanTree(Dictionary<char, int> frequencyTable)
        {
            var priorityQueue = new PriorityQueue<HuffmanNode, int>();
            foreach (var entry in frequencyTable)
            {
                priorityQueue.Enqueue(new HuffmanNode(entry.Key, entry.Value), entry.Value);
            }

            // Si solo hay un tipo de caracter, la cola no entra al bucle.
            // Creamos un nodo padre para que el árbol siempre tenga raíz y al menos un hijo.
            if (priorityQueue.Count == 1)
            {
                var singleNode = priorityQueue.Dequeue();
                var parent = new HuffmanNode(singleNode, new HuffmanNode('\0', 0)); // Hijo derecho vacío
                priorityQueue.Enqueue(parent, parent.Frecuencia);
                return parent;
            }

            while (priorityQueue.Count > 1)
            {
                var leftChild = priorityQueue.Dequeue();
                var rightChild = priorityQueue.Dequeue();
                var parent = new HuffmanNode(leftChild, rightChild);
                priorityQueue.Enqueue(parent, parent.Frecuencia);
            }
            return priorityQueue.Dequeue();
        }

        public Dictionary<char, string> GenerateCodes(HuffmanNode root)
        {
            var codes = new Dictionary<char, string>();
            GenerateCodesRecursive(root, "", codes);
            return codes;
        }

        private void GenerateCodesRecursive(HuffmanNode? node, string currentCode, Dictionary<char, string> codes)
        {
            if (node == null) return;
            if (node.IsLeaf)
            {
                // Un código debe tener al menos un bit
                if (string.IsNullOrEmpty(currentCode))
                {
                    currentCode = "0";
                }
                codes[node.Simbolo] = currentCode;
                return;
            }
            GenerateCodesRecursive(node.LeftChild, currentCode + "0", codes);
            GenerateCodesRecursive(node.RightChild, currentCode + "1", codes);
        }
    }
}

