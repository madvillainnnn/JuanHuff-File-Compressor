namespace CL_Compressor
{
    public class HuffmanNode : IComparable<HuffmanNode>
    {
        /// El símbolo (carácter) que este nodo representa. Es nulo para nodos internos.
        public char Simbolo { get; }

        /// La frecuencia de aparición del símbolo en el texto.
        public int Frecuencia { get; }

        /// Referencia al hijo izquierdo en el árbol.
        public HuffmanNode? LeftChild { get; }

        /// Referencia al hijo derecho en el árbol.
        public HuffmanNode? RightChild { get; }

        /// Constructor para un nodo hoja.
        /// <param name="symbol">El carácter del nodo.</param>
        /// <param name="frequency">La frecuencia del carácter.</param>
        public HuffmanNode(char symbol, int frequency)
        {
            Simbolo = symbol;
            Frecuencia = frequency;
            LeftChild = null;
            RightChild = null;
        }

        /// Constructor para un nodo interno.
        /// <param name="leftChild">El hijo izquierdo del nuevo nodo.</param>
        /// <param name="rightChild">El hijo derecho del nuevo nodo.</param>
        public HuffmanNode(HuffmanNode leftChild, HuffmanNode rightChild)
        {
            Simbolo = '\0'; // Carácter nulo para nodos internos
            Frecuencia = leftChild.Frecuencia + rightChild.Frecuencia;
            LeftChild = leftChild;
            RightChild = rightChild;
        }

        /// Propiedad que devuelve verdadero si el nodo es una hoja (no tiene hijos).
        public bool IsLeaf => LeftChild == null && RightChild == null;

        /// Compara este nodo con otro basado en su frecuencia. Esencial para la cola de prioridad (menor frecuencia = mayor prioridad).
        /// <param name="other">El otro nodo Huffman con el que se va a comparar.</param>
        /// <returns>Un entero que indica si esta instancia precede, sigue o está en la misma posición en el orden de clasificación que el otro nodo.</returns>
        public int CompareTo(HuffmanNode? other)
        {
            if (other == null) return 1;
            return this.Frecuencia.CompareTo(other.Frecuencia);
        }
    }
}
