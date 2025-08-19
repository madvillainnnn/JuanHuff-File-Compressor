using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_Compressor
{
    public class HuffmanAlgorithm
    {
        /// Construye una tabla de frecuencias a partir de un texto de entrada.
        /// <param name="text">El contenido del archivo a analizar.</param>
        /// <returns>Un diccionario donde la clave es el carácter y el valor es su frecuencia.</returns>
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

        /// Construye el árbol de Huffman a partir de la tabla de frecuencias.
        /// <param name="frequencyTable">El diccionario de frecuencias de los caracteres.</param>
        /// <returns>El nodo raíz del árbol de Huffman construido.</returns>
        public HuffmanNode BuildHuffmanTree(Dictionary<char, int> frequencyTable)
        {
            // .NET 6+ incluye una PriorityQueue nativa.
            var priorityQueue = new PriorityQueue<HuffmanNode, int>();

            foreach (var entry in frequencyTable)
            {
                priorityQueue.Enqueue(new HuffmanNode(entry.Key, entry.Value), entry.Value);
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

        /// Genera los códigos de Huffman para cada carácter recorriendo el árbol.
        /// <param name="root">El nodo raíz del árbol de Huffman.</param>
        /// <returns>Un diccionario con los códigos binarios para cada carácter.</returns>
        public Dictionary<char, string> GenerateCodes(HuffmanNode root)
        {
            var codes = new Dictionary<char, string>();
            GenerateCodesRecursive(root, "", codes);
            return codes;
        }

        /// Método auxiliar recursivo para generar los códigos.
        private void GenerateCodesRecursive(HuffmanNode? node, string currentCode, Dictionary<char, string> codes)
        {
            if (node == null) return;

            if (node.IsLeaf)
            {
                // Solo los nodos hoja tienen símbolos que nos interesan
                codes[node.Simbolo] = currentCode;
                return;
            }

            GenerateCodesRecursive(node.LeftChild, currentCode + "0", codes);
            GenerateCodesRecursive(node.RightChild, currentCode + "1", codes);
        }

        /// Comprime el texto original usando los códigos de Huffman generados.
        /// <param name="text">El texto original a comprimir.</param>
        /// <param name="codes">El diccionario de códigos de Huffman.</param>
        /// <returns>Un array de bytes que representa los datos comprimidos.</returns>
        public byte[] Compress(string text, Dictionary<char, string> codes)
        {
            var stringBuilder = new StringBuilder();
            foreach (char symbol in text)
            {
                stringBuilder.Append(codes[symbol]);
            }

            string bitString = stringBuilder.ToString();
            var compressedBytes = new List<byte>();

            // El primer byte almacenará el número de bits de relleno en el último byte.
            int padding = (8 - (bitString.Length % 8)) % 8;
            compressedBytes.Add((byte)padding);

            for (int i = 0; i < bitString.Length; i += 8)
            {
                string byteString = bitString.Length - i < 8
                    ? bitString.Substring(i).PadRight(8, '0')
                    : bitString.Substring(i, 8);

                compressedBytes.Add(Convert.ToByte(byteString, 2));
            }

            return compressedBytes.ToArray();
        }

        /// Descomprime un array de bytes usando el árbol de Huffman.
        /// <param name="compressedData">El array de bytes comprimidos.</param>
        /// <param name="root">La raíz del árbol de Huffman usado para la compresión.</param>
        /// <returns>El texto original descomprimido.</returns>
        public string Decompress(byte[] compressedData, HuffmanNode root)
        {
            if (compressedData.Length == 0) return "";

            byte padding = compressedData[0];
            var stringBuilder = new StringBuilder();

            for (int i = 1; i < compressedData.Length; i++)
            {
                stringBuilder.Append(Convert.ToString(compressedData[i], 2).PadLeft(8, '0'));
            }

            // Remove padding
            string bitString = stringBuilder.ToString();
            bitString = bitString.Substring(0, bitString.Length - padding);

            var decompressedText = new StringBuilder();
            var currentNode = root;

            foreach (char bit in bitString)
            {
                currentNode = bit == '0' ? currentNode.LeftChild : currentNode.RightChild;

                if (currentNode != null && currentNode.IsLeaf)
                {
                    decompressedText.Append(currentNode.Simbolo);
                    currentNode = root; // Volver a la raíz para el siguiente símbolo
                }
            }

            return decompressedText.ToString();
        }
    }
}

