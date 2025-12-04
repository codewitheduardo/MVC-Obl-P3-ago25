const alerta = document.getElementById("alertaHome");
if (alerta) {
    setTimeout(() => {
        alerta.style.transition = "opacity 0.8s";  // Solo transición de opacidad
        alerta.style.opacity = "0";  // Desvanece la alerta
        setTimeout(() => alerta.remove(), 900);  // Elimina la alerta después de la animación
    }, 5000);  // La alerta se mantendrá visible durante 5 segundos antes de desvanecerse
}
