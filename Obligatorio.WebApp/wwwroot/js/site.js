/* ------------------------------------------
   1. Spinner global para formularios
------------------------------------------ */
document.querySelectorAll("form").forEach(form => {
    form.addEventListener("submit", () => {
        showGlobalSpinner();
    });
});

function showGlobalSpinner() {
    let spinner = document.createElement("div");
    spinner.className = "global-spinner";
    spinner.innerHTML = `
        <div class="spinner-circle"></div>
        <p>Cargando...</p>
    `;
    document.body.appendChild(spinner);
}

/* ------------------------------------------
   2. Auto-focus al primer input visible
------------------------------------------ */
window.addEventListener("load", () => {
    const firstField = document.querySelector("input:not([type=hidden]), select, textarea");
    if (firstField) firstField.focus();
});

/* ------------------------------------------
   3. Scroll suave hacia resultados
------------------------------------------ */
function smoothScrollTo(selector) {
    const el = document.querySelector(selector);
    if (el) {
        el.scrollIntoView({ behavior: "smooth", block: "start" });
    }
}

/* ------------------------------------------
   4. Modo Oscuro (Dark Mode)
------------------------------------------ */
// Para activarlo desde un botón:
// <button onclick="toggleDarkMode()">🌙</button>

function toggleDarkMode() {
    document.body.classList.toggle("dark-mode");

    localStorage.setItem("theme",
        document.body.classList.contains("dark-mode") ? "dark" : "light"
    );
}

// Cargar tema guardado
window.addEventListener("DOMContentLoaded", () => {
    if (localStorage.getItem("theme") === "dark") {
        document.body.classList.add("dark-mode");
    }
});

/* ------------------------------------------
   5. Animación Figma: Smooth Hover Levitation
------------------------------------------ */
document.querySelectorAll(".card, .pago-card, .equipo-card, .auditoria-item")
    .forEach(el => {
        el.addEventListener("mouseover", () => {
            el.style.transform = "translateY(-3px)";
            el.style.transition = "0.25s";
            el.style.boxShadow = "0 10px 22px rgba(0,0,0,0.12)";
        });

        el.addEventListener("mouseout", () => {
            el.style.transform = "translateY(0)";
            el.style.boxShadow = "0 6px 14px rgba(0,0,0,0.07)";
        });
    });

/* ------------------------------------------
   6. Helper: Mostrar mensajes tipo toast
------------------------------------------ */
function showToast(msg, type = "info") {
    const toast = document.createElement("div");
    toast.className = `figma-toast figma-toast-${type}`;
    toast.innerText = msg;

    document.body.appendChild(toast);

    setTimeout(() => {
        toast.style.opacity = "0";
        setTimeout(() => toast.remove(), 800);
    }, 3000);
}

window.addEventListener("load", function () {
    const loadingScreen = document.getElementById("loadingScreen");

    // Esperamos 500ms para dar tiempo a que el spinner se vea
    setTimeout(() => {
        loadingScreen.style.display = "none";  // Esconde el spinner
    }, 300);  // Ajusta el tiempo si lo necesitas
});