# Taller-Fundamentos
# Máquina de Turing Física — Suma y Resta en Unario

Representación física de una **máquina de Turing** que ejecuta **suma (A+B)** y **resta (A−B, A≥B)** en **unario** usando Arduino, LEDs y un cabezal móvil.

## Idea general
- **Controlador:** Arduino Uno que implementa la lógica de transición (leer símbolo, escribir símbolo, mover L/R, cambiar de estado).
- **Cinta:** hilera de LEDs (LED ENCENDIDO = `1`, LED APAGADO = `␣`).
- **Cabezal:** carrito con LDR que “lee” el LED actual; la “escritura” la realiza el Arduino encendiendo/apagando ese LED.
- **Movimiento:** motor paso a paso 28BYJ-48 + driver ULN2003 (desplazamiento celda a celda).
- **Límites:** 2 limit switches para homing y extremos.
- **Interacción:** botones (Step, Reset, Modo Suma/Resta, Izq/Der) y LEDs de estado (q0, q1, q2, …).
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

### Fase 1 — Informe teórico
- Análisis teórico
- Definición del autómata: 7-tupla `M = (Q, Σ, Γ, δ, q₀, B, F)` y tablas de transición completas para **suma** y **resta (A≥B)**

**Encargados**  
- Investigación de máquina de Turing: **Benjamín Cuello**  
- Estados y 7-tupla: **Benjamín Salas**

### Fase 2 — Selección de materiales y diseño del sistema
- Selección y justificación de materiales y componentes.  
- Diseño de la arquitectura física: cinta, movimiento del cabezal, lectura/escritura.  
- Diagramas técnicos con dimensiones y funcionamiento de cada componente.  

**Encargados**  
- *(por definir)*

### Fase 3 — Construcción, programación y pruebas
- Ensamblaje del hardware y montaje mecánico.  
- Programación de la lógica .  
- Pruebas de funcionamiento y demostraciones controladas.  

**Encargados**  
- *(por definir)*
