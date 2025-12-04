document.querySelector("#Dto_TipoPago").addEventListener('change', MostrarOpcionesSegunTipo);
MostrarOpcionesSegunTipo();

function MostrarOpcionesSegunTipo() {
    let op = document.querySelector("#Dto_TipoPago").value;

    // Si es "RECURRENTE", muestra las opciones
    if (op === "RECURRENTE") {
        document.querySelector("#opcionesRecurrente").style.display = "block";
    } else {
        // Si no es "RECURRENTE", oculta las opciones y limpia el campo de fecha de fin
        document.querySelector("#opcionesRecurrente").style.display = "none";
        document.querySelector("#Dto_FechaFin").value = "";
    }
}