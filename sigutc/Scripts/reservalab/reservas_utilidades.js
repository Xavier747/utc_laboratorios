//Mostrar mensaje de informacion 
function mostrarMensage(mensaje, icon) {
    const Toast = Swal.mixin({
        toast: true,
        position: "top-end",
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer);
            toast.addEventListener('mouseleave', Swal.resumeTimer);
        }
    });

    Toast.fire({
        icon: icon,
        title: mensaje
    });
}

//Mostrar mensaje de informacion y recargar la pagina
function mostrarMensageCRUD(mensaje, icon) {
    const Toast = Swal.mixin({
        toast: true,
        position: "top-end",
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer);
            toast.addEventListener('mouseleave', Swal.resumeTimer);
        }
    });

    Toast.fire({
        icon: icon,
        title: mensaje
    }).then(() => {
        window.location.href = window.location.href;
    });
}

//Muestra un modal de confirmacion
function showAlertDelete(btn) {
    // Detiene el postback
    event.preventDefault(); 

    Swal.fire({
        title: "¿Estás seguro?",
        text: "¡No podrás revertir esta acción!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        if (result.isConfirmed) {
            __doPostBack(btn.name, '');
        }
    });

    // Siempre evitar el postback automático
    return false; 
}

//Recarga la pagina
function cerrar() {
    window.location.href = window.location.href;
}

//Muestea un mensaje por una fraccion de tiempo y desaparece
function mostrarTooltipSimple(msg, lblMsg) {
    lblMsg.text(msg).fadeIn();

    setTimeout(() => {
        lblMsg.fadeOut();
    }, 4000);
}

//Convierte el formato de hora a uno entendible para js
function convertirFechaForFullCalendar(fecha) {
    return fecha.replace(' ', 'T');
}

//Segun la fecha determina el numero de dia(Domingo = 1, Lunes = 2, ...)
function obtenerDiaSemana(fechaStr) {
    // Dividir la fecha
    const [anio, mes, dia] = fechaStr.split('-').map(Number);

    // Crear fecha con año, mes (0 indexado), día
    const fecha = new Date(anio, mes - 1, dia);

    // Obtener el día original: 0 (domingo) a 6 (sábado)
    const diaSemana = fecha.getDay();
    return diaSemana;
}

//Apartir de una fecha y hora obtiene solo la hora
function convertirHora(fechaCompleta){
    const fecha = new Date(fechaCompleta);

    const horas = fecha.getHours().toString().padStart(2, '0');
    const minutos = fecha.getMinutes().toString().padStart(2, '0');
    const segundos = fecha.getSeconds().toString().padStart(2, '0');

    const horaFinal = `${horas}:${minutos}`;
    return horaFinal;
}

function llenarHorasFin(horaInicio, finSelect) {
    const hora = parseInt(horaInicio.split(':')[0]); // Extrae la hora (ej: 7)
    finSelect.empty();

    for (let h = hora; h <= 21; h++) {
        const horaFin = h.toString().padStart(2, '0') + ':59';
        finSelect.append($('<option>', {
            value: horaFin,
            text: horaFin
        }));
    }
}