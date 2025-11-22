# Taller-Fundamentos

# Máquina de Turing Física — Suma y Resta en Unario

Representación física de una **máquina de Turing** que ejecuta **suma (A+B)** y **resta (A−B, A≥B)** en **unario** usando Arduino, LEDs y un cabezal móvil.

## Idea general

- **Controlador:** Arduino Uno que implementa la lógica de transición (leer símbolo, escribir símbolo, mover L/R, cambiar de estado).
- **Cinta:** hilera de LEDs (LED ENCENDIDO = `1`, LED APAGADO = `␣`).
- **Cabezal:** carrito con LDR que “lee” el LED actual; la “escritura” la realiza el Arduino encendiendo/apagando ese LED.
- **Movimiento:** motor paso a paso 28BYJ-48 + driver ULN2003 (desplazamiento celda a celda).
- **Límites:** 2 limit switches para homing y extremos.
- **Interacción:** botones (Step, Reset, Modo Suma/Resta, Izq/Der).
- **Alimentación:** 5 V externa (motor + LEDs), GND común.

## Representación formal utilizada

- **Alfabeto de entrada:** Σ = { `1`, `␣` }

## Propuesta de materiales

- Arduino Uno
- Protoboard + jumpers
- LEDs (10–15) + resistencias 220–330 Ω
- LDR + resistencia 10 kΩ
- 28BYJ-48 + ULN2003
- Riel (MGN12 u otro) + carrito
- 2 limit switches
- Botones/switches (4–6)
- LEDs extra para estados (4–6)
- Fuente 5 V externa
- Base de madera

## Propuesta de diseño (resumen)

- **Cinta:** fila de LEDs; cada LED es una celda (`1` = encendido, `␣` = apagado).
- **Cabezal:** carro con LDR que lee ON/OFF; escritura por Arduino sobre la celda actual.
- **Control y estados:** botones para Step/Reset/Modo; LEDs de estado para q0, q1, q2…
- **Alimentación y montaje:** 5 V externa, protoboard y base rígida (madera).

---

## Fases del proyecto

### Fase 1 — Informe teórico (31/08 - 11/09)

- Análisis teórico
- Definición del autómata: 7-tupla `M = (Q, Σ, Γ, δ, q₀, B, F)` y tablas de transición completas para **suma** y **resta (A≥B)**

**Encargados**

- Investigación de máquina de Turing: **Benjamín Cuello**
- Estados y 7-tupla: **Benjamín Salas**

### Fase 2 — Selección de materiales y diseño del sistema (11/09 - 30/09)

- Selección y justificación de materiales y componentes.
- Diseño de la arquitectura física: cinta, movimiento del cabezal, lectura/escritura.
- Diagramas técnicos con dimensiones y funcionamiento de cada componente.

**Encargados**

- Diseño de la arquitectura física en tinkercad: **Benjamín Cuello**
- Seleccion de Materiales y medidas: **Benjamín Salas**

### Fase 3 — Construcción, programación y pruebas

- Ensamblaje del hardware y montaje mecánico.
- Programación de la lógica .
- Pruebas de funcionamiento y demostraciones controladas.

**Encargados**

- Construccion en Unity: **Benjamín Cuello**
- Implementacion de logica y Diagrama en AUTOCAD: **Benjamín Salas**

---

## Simulación Digital en Unity

Esta simulación replica el prototipo físico: misma base de 30×30 cm, riel, fotoboards con LEDs, motor paso a paso con carrete y nylon, botones físicos, Arduino y cableado.

Permite ver con claridad cómo el cabezal recorre la cinta, qué símbolo lee/escribe en cada paso y en qué estado `qi` se encuentra la máquina.

---

### Interfaz y controles principales

| Botón / Control  | Función                                                                                                         |
| ---------------- | --------------------------------------------------------------------------------------------------------------- |
| **Left / Right** | Mueven manualmente el cabezal una celda a la izquierda o derecha.                                              |
| **Toggle**       | Cambia el símbolo de la celda actual (ciclo: `B` → `1` → `0` → `X` → `B`).                                      |
| **Step**         | Ejecuta una única transición de estado (una instrucción de la tabla δ).                                        |
| **Run / Stop**   | Inicia o detiene la ejecución automática continua.                                                              |
| **Speed Slider** | Controla la velocidad de ejecución durante el modo automático .                                                 |
| **Reset**        | Limpia toda la cinta (todas las celdas a `B`) y devuelve el cabezal a la posición inicial.                     |
| **Suma / Resta** | Cambia entre las tablas de transición δ para **suma (A+B)** o **resta (A−B)**.                                  |

Además de los botones de la interfaz, en la maqueta 3D hay **botones físicos**  que tienen la animacion cuando se presionan desde la UI, simulando la sensación de pulsar un botón real.

---

### Representación de la cinta

- La **cinta** se compone de **24 celdas** (LEDs), cada una representando un símbolo del alfabeto `{B, 1, 0, X}`.

| Color    | Símbolo | Significado                                               |
| -------- | ------- | ----------------------------------------------------------|
| Gris     | `B`     | Celda en blanco / sin valor.                              |
| Rojo     | `1`     | Unidad en unario (número).                                |
| Azul     | `0`     | Separador (solo en resta).                                |
| Naranjo  | `X`     | Celda tachada o restada (valor eliminado, solo en resta). |


El cabezal es un carrito que se desplaza sobre el riel. Debajo lleva el sensor LDR virtual y por encima se ve el nylon y el cable negro que se estiran cuando se mueve, igual que en la máquina física.

---

### Funcionamiento general de la máquina

1. Al iniciar la escena, el sistema parte en el estado inicial `q0` y la cinta está en blanco (`B...B`).
2. El usuario escribe la entrada moviendo el cabezal con **Left / Right** y usando **Toggle** para cambiar el símbolo de la celda actual.
3. Según el modo elegido (**Suma** o **Resta**), se carga la tabla de transición δ correspondiente.
4. Con **Step** se ejecuta paso a paso: se lee el símbolo, se escribe el nuevo símbolo, se mueve el cabezal y se cambia el estado `qi`.
5. Con **Run** la máquina recorre la entrada automáticamente hasta que no haya más reglas, momento en que se detiene.
6. El resultado queda en la cinta:
   - En suma, la cantidad total de `1` representa `A+B`.
   - En resta, si el resultado es 0 la cinta queda en blanco; si no, la cantidad de `1` representa `A−B`.


### Controles de cámara

La cámara tiene dos modos:

#### 1) Cámara libre (modo normal)

- **Click derecho + mover mouse** → rotar la vista.
- **W / A / S / D** → moverse adelante/atrás/izquierda/derecha.
- **Q / E** → bajar/subir.
- El movimiento está limitado a un area aproximada.

#### 2) Cámara siguiendo al cabezal (solo en RUN)

- Al presionar **Run**, la cámara pasa a un modo de seguimiento: se coloca sobre el cabezal y lo sigue mientras recorre la cinta.
- Al presionar **Run / Stop** de nuevo, o cuando la máquina termina, se vuelve automáticamente al modo de cámara libre.

---

### Lógica interna (scripts principales)

- **`Cell.cs`**  
  Representa cada celda de la cinta. Almacena el símbolo (`B, 1, 0, X`) y actualiza el material (color) del LED.

- **`HeadController.cs`**  
  Controla el movimiento del cabezal entre las celdas y las operaciones de lectura/escritura.

- **`TuringMachine.cs`**  
  Implementa la máquina de Turing:
  - Estados `qi` para suma y resta.
  - Tablas de transición δ para ambos modos.
  - Modo automático (Run/Stop).
  - Actualización del texto de estado y control de la cámara.

- **`UIController.cs`**  
  Conecta los botones de la UI con la lógica de la máquina y acciona la animación de los botones físicos (Script `ButtonFisico`).

- **`NylonController.cs`**  
  Actualiza el carrete del motor y estira el nylon/cable cuando el cabezal se mueve.

- **`FreeCamera.cs` y `FollowHead.cs`**  
  Gestionan el movimiento libre de la cámara y el seguimiento del cabezal cuando la máquina está en ejecución.

---

### Cómo usar la simulación

- **Suma (ej. 1 + 1):**
  1. Presiona el botón **Suma**.
  2. Desde el inicio de la cinta, enciende LEDs rojos con **Toggle** y muévete con **Left / Right** para escribir algo como:  
     `Rojo | Gris | Rojo` (primer número, separador en gris, segundo número).
  3. Vuelve con el cabezal al primer LED rojo.
  4. Presiona **Run** para que la máquina haga toda la operación, o **Step** varias veces para ver cada transición.

- **Resta (ej. 2 − 1):**
  1. Presiona el botón **Resta**.
  2. Deja el **primer LED apagado (gris)** como borde izquierdo.  
  3. Escribe los `1` del minuendo en rojo, por ejemplo: `Gris | Rojo | Rojo | ...`
  4. Para el separador, sigue usando **Toggle** hasta que el LED se ponga **azul**.
  5. Luego escribe los `1` del sustraendo en rojo. Ejemplo completo:  
     `Gris | Rojo | Rojo | Azul | Rojo`
  6. Vuelve con el cabezal al primer LED rojo (el segundo LED, porque el primero es borde gris).
  7. Presiona **Run** o **Step** para ver el proceso de resta.  
     - Si el resultado es 0, la cinta queda en blanco (`B`).
     - Si no, quedan `1` rojos equivalentes a `A−B`.


