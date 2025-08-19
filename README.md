# JuanHuff 🧙‍♂️

Un compresor de archivos minimalista y potente basado en el algoritmo de Huffman, construido con C# y Windows Forms.



## 📝 Descripción

**Byte Weaver** es una aplicación de escritorio creada como un proyecto para explorar los fundamentos de los algoritmos de compresión sin pérdida. Permite a los usuarios comprimir archivos de texto de forma rápida e intuitiva a través de una interfaz gráfica simple con funcionalidad de arrastrar y soltar.

---

## ✨ Características Actuales

* **Compresión Huffman:** Utiliza el algoritmo clásico de Huffman para una compresión eficiente.
* **Interfaz Gráfica Intuitiva:** Ventana simple con soporte para arrastrar y soltar (`Drag & Drop`).
* **Arquitectura MVC:** Código organizado siguiendo el patrón Modelo-Vista-Controlador para una máxima legibilidad y escalabilidad.
* **Lógica Modular:** El motor de compresión está en una biblioteca de clases separada, lista para ser reutilizada.

---

## 🚀 Roadmap de Mejoras (Plan a Futuro)

Esta es la lista de las próximas funcionalidades que se implementarán para llevar **Byte Weaver** al siguiente nivel.

- [ ] **Añadir Descompresión:** Implementar la capacidad de revertir el proceso y obtener el archivo original desde un `.huff`.
- [ ] **Barra de Progreso y Estadísticas:** Proporcionar feedback visual durante la compresión y mostrar el ratio de ahorro al finalizar.
- [ ] **Soporte Universal de Archivos:** Adaptar el motor para que funcione con cualquier tipo de archivo (imágenes, ejecutables, etc.) operando a nivel de `byte` en lugar de `char`.
- [ ] **Interfaz con Doble Modo:** Rediseñar la UI para tener un modo "Comprimir" y un modo "Descomprimir" claros.
- [ ] **Mejorar Feedback Visual:** Reemplazar los `MessageBox` con notificaciones integradas y más modernas.
- [ ] **Icono y Personalización:** Darle una identidad visual única a la aplicación.

---

## 💻 Tecnologías Utilizadas

* **C#**
* **.NET 8**
* **Windows Forms**
