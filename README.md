# JuanHuff 🧙‍♂️

Un compresor de archivos minimalista y potente basado en el algoritmo de Huffman, construido con C# y Windows Forms.



## 📝 Descripción

**JuanHuff File Compressor** es una aplicación de escritorio creada como un proyecto para explorar los fundamentos de los algoritmos de compresión sin pérdida. Permite a los usuarios comprimir archivos de texto de forma rápida e intuitiva a través de una interfaz gráfica simple con funcionalidad de arrastrar y soltar.

---

## ✨ Características Actuales

* **Compresión Huffman:** Utiliza el algoritmo clásico de Huffman para una compresión eficiente.
* **Interfaz Gráfica Intuitiva:** Ventana simple con soporte para arrastrar y soltar (`Drag & Drop`).
* **Arquitectura MVC:** Código organizado siguiendo el patrón Modelo-Vista-Controlador para una máxima legibilidad y escalabilidad.
* **Lógica Modular:** El motor de compresión está en una biblioteca de clases separada, lista para ser reutilizada.

---

## 🚀 Roadmap de Mejoras (Plan a Futuro)

Esta es la lista de las próximas funcionalidades que se implementarán para llevar **JuanHuff File Compressor** al siguiente nivel.

- [x] **Añadir Descompresión:** Implementar la capacidad de revertir el proceso y obtener el archivo original desde un `.huff`.
- [x] **Barra de Progreso y Estadísticas:** Proporcionar feedback visual durante la compresión y mostrar el ratio de ahorro al finalizar.
- [ ] **Soporte Universal de Archivos:** Adaptar el motor para que funcione con cualquier tipo de archivo (imágenes, ejecutables, etc.) operando a nivel de `byte` en lugar de `char`.
- [x] **Interfaz con Doble Modo:** Rediseñar la UI para tener un modo "Comprimir" y un modo "Descomprimir" claros.
- [ ] **Mejorar Feedback Visual:** Reemplazar los `MessageBox` con notificaciones integradas y más modernas.
- [ ] **Icono y Personalización:** Darle una identidad visual única a la aplicación.

---

## 💻 Tecnologías Utilizadas

* **C#**
* **.NET 8**
* **Windows Forms**

---

## Pasos Para Ejecutar el Proyecto

## Paso 0: El Setup (Lo que necesitas tener) 🛠️

Antes de tirar una sola línea de código o clonar nada, asegúrate de que tu PC esté lista para la batalla.

1.  **Visual Studio 2022:** Es el IDE por excelencia para C# y .NET. Si no lo tienes, descárgalo desde la web oficial de Microsoft. La versión **Community** es gratuita y tiene todo el power que necesitas.
2.  **Workload de .NET:** Durante la instalación de Visual Studio, asegúrate de marcar la casilla que dice **"Desarrollo de escritorio de .NET"**. Esto instala todo lo necesario para Windows Forms y .NET 8.
3.  **Git:** Aunque Visual Studio ya lo trae integrado, nunca está de más tener Git instalado en tu sistema. Puedes bajarlo desde [git-scm.com](https://git-scm.com/).

## Paso 1: Clonar el Repositorio (Bajar el código) 🧬

Ahora sí, vamos a traernos ese código épico desde GitHub a tu máquina.

Tienes dos vibes para hacer esto:

## A. Con la terminal (La vieja confiable):

Abre tu terminal favorita (Git Bash, PowerShell, etc.), navega a la carpeta donde guardas tus proyectos y lanza este comando:
git clone https://github.com/madvillainnnn/JuanHuff-File-Compressor.git

## B. Desde Visual Studio (Modo chill):

1.  Abre Visual Studio 2022.
2.  En la ventana de inicio, selecciona **"Clonar un repositorio"**.
3.  Pega la URL del repositorio de GitHub en el campo correspondiente.
4.  Elige la ruta local donde quieres que se guarde el proyecto.
5.  Dale al botón **"Clonar"** y deja que la magia ocurra. ✨


## Paso 2: Abrir y Construir el Proyecto (El Momento de la Verdad) 🏗️

Una vez clonado, Visual Studio probablemente abrirá el proyecto automáticamente. Si no, no te estreses:

1.  Navega a la carpeta que acabas de clonar.
2.  Tendrás dos carpetas, CL_Compressor (Biblioteca de Clases) y WF_Compressor (Proyecto de WinForms en patrón MVC).
3.  Abre ambas carpetas en dos ventanas de VS separadas
4.  En la ventana de la carpeta "WF_Compressor" Busca el archivo mágico con la extensión **`.sln`** ("WF_Compressor.sln"). Ese es el archivo de la solución. Hazle doble clic.
5.  Visual Studio cargará todo el proyecto. En la parte derecha, en el **"Explorador de soluciones"**, verás toda la estructura de carpetas y archivos.
6.  Antes de ejecutar, hay que construir el proyecto. Esto compila el código y descarga las dependencias (paquetes NuGet) que necesite. Ve al menú superior en la ventana de la carpeta "CL_Compressor" y haz clic en **Compilar \> Compilar solución** (o simplemente presiona `Ctrl+Shift+B`).

La primera vez, puede que tarde un poquito mientras restaura los paquetes. Fíjate en la ventana "Resultados" de abajo para asegurarte de que todo salió bien (deberías ver un mensaje de "Compilación correcta").

## Paso 3: ¡Ejecutar y Comprimir\! ▶️

Si la compilación fue un éxito, ya estás a un solo clic de la gloria.

1.  En la barra de herramientas superior de Visual Studio, verás un **botón verde de Play (▶️)** que  dice el nombre del proyecto ("WF_Compressor").
2.  ¡Dale clic a ese botón\! (O presiona la tecla `F5`).

¡Y PUM\! 💥 Debería aparecer la ventana de tu compresor **JuanHuff**, lista para que le arrastres unos archivitos y veas cómo hace su magia.

-----

## Posibles Problemas (Por si las moscas) 🪰

  * **¿Errores de compilación sobre .NET 8?** Es posible que no tengas el SDK correcto. Abre el "Visual Studio Installer", dale a "Modificar" en tu instalación y asegúrate de que el SDK de .NET 8 esté seleccionado e instalado.
  * **¿No encuentras el archivo `.sln`?** Asegúrate de haber clonado el repositorio completo y no solo descargado un archivo suelto.

¡Listo\! 
