document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        buttonText: {
            month: 'Mes',
            week: 'Semana',
            list: 'Lista'
        },
        initialView: 'dayGridMonth', // vista mensual
        locale: 'es',                // idioma español
        headerToolbar: {
            left: 'prev,next',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,listWeek'
        },
        dayHeaderContent: function (arg) {
            switch (arg.date.getUTCDay()) {
                case 0: return 'Domingo';
                case 1: return 'Lunes';
                case 2: return 'Martes';
                case 3: return 'Miércoles';
                case 4: return 'Jueves';
                case 5: return 'Viernes';
                case 6: return 'Sábado';
            }
        },
        dateClick: function (info) {
            var fecha = info.dateStr;
            $('#txtFecha').val(fecha);

            let dia = obtenerDiaSemana(fecha);

            consultarAsignatura(dia, function(data){
                cargarMaterias(data);
            });

            var selectMateria = $("#selectAsignatura").val();

            consultarHorario(selectMateria, dia, function(data){
                
                const dropdown = $("#selectHoraInicio");
                cargarHora(data, dropdown);
            });

        },
        selectable: true,
        select: function (info) {
            var now = new Date();
            var dayOfWeek = info.start.getUTCDay();
            let mensaje = '';
            let icon = '';

            now.setHours(0, 0, 0, 0);
            if (info.start < now) {
                mensaje = 'Las fechas pasadas no están disponibles para reservas.';
                icon = 'warning';

                mostrarMensage(mensaje, icon);
                calendar.unselect();
            }
            else if(dayOfWeek === 0 || dayOfWeek === 6){
                mensaje = 'No se permiten reservas los fines de semana. Por favor, selecciona un día laborable.';
                icon = 'warning';

                mostrarMensage(mensaje, icon);
            }
            else {
                $('#form_registrar').modal('show');
            }

        },
    });

    calendar.render();
});


$(document).ready(function () {
    $("#selectAsignatura").on('change', function () {
        var asignaturaId = this.value; // Capturar el valor seleccionado
        consultarHorario(asignaturaId, dia);
    });
});

function consultarAsignatura(dia, callback){
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerAsignaturas",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ dia: dia }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = JSON.parse(response.d);
            callback(data);
        },
        error: function (xhr, status, error) {
            console.log("Status: " + xhr.status);
            console.log("Response: " + xhr.responseText);
            callback([]);
        }
    });
}

function consultarHorario(asignaturaId, dia, callback){
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerHorario",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ asignaturaId: asignaturaId, dia: dia }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = JSON.parse(response.d);
            callback(data);
        },
        error: function (xhr, status, error) {
            console.log("Status: " + xhr.status);
            console.log("Response: " + xhr.responseText);
            callback([]);
        }
    });
}

function cargarHora(data, dropdown){
    dropdown.empty();

    // Crear opción por cada elemento de la lista
    data.forEach(item => {
        const opcion = document.createElement("option");
        opcion.value = item.strCod_horas;          // valor que se enviará
        opcion.textContent = convertirHora(item.strHoraInicio); // lo que se muestra al usuario
        dropdown.append(opcion);
    });
}

function convertirHora(fechaCompleta){
    const fecha = new Date(fechaCompleta);

    const horas = fecha.getHours().toString().padStart(2, '0');
    const minutos = fecha.getMinutes().toString().padStart(2, '0');
    const segundos = fecha.getSeconds().toString().padStart(2, '0');

    const horaFinal = `${horas}:${minutos}:${segundos}`;
}

function obtenerDiaSemana(fechaStr) {
    // Dividir la fecha
    const [anio, mes, dia] = fechaStr.split('-').map(Number);

    // Crear fecha con año, mes (0 indexado), día
    const fecha = new Date(anio, mes - 1, dia);

    // Obtener el día original: 0 (domingo) a 6 (sábado)
    const diaSemana = fecha.getDay();
    return diaSemana;
}

function cargarMaterias(data){
    const dropdown = $("#selectAsignatura");

    // Limpiar opciones anteriores
    dropdown.empty();

    // Crear opción por cada elemento de la lista
    data.forEach(item => {
        const opcion = document.createElement("option");
        opcion.value = item.strCod_mate;          // valor que se enviará
        opcion.textContent = item.strNombre_mate; // lo que se muestra al usuario
        dropdown.append(opcion);
    });
}

function mostrarMensage(mensaje, icon){
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