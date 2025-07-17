//Consulta las asignauras del docente
function consultarAsignatura(comodin, filtro1, filtro2, filtro3, filtro4, callback) {
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerAsignaturas",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4 }),
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

//Consulta el horario de los docentes por cada dia
function consultarHorario(comodin, filtro1, filtro2, filtro3, filtro4, callback) {
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerHorario",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4 }),
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

//Consultar el ciclo al que esta asignado esa asignatura
function consultarCiclo(comodin, filtro1, filtro2, filtro3, filtro4, callback) {
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerCiclo",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4 }),
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

//Consultar la carrera a la que pertenece la asignatura
function consultarCarrera(comodin, filtro1, filtro2, filtro3, filtro4, callback) {
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerCarrera",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4 }),
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

//Consultar las alumnos matriculados en esa materia
function consultarAlumno(comodin, filtro1, filtro2, filtro3, filtro4, callback) {
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerEstudiantes",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4 }),
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

function consultarUnidad(comodin, filtro1, filtro2, filtro3, filtro4, callback) {
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerUnidad",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4 }),
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

function consultarTema(comodin, filtro1, filtro2, filtro3, filtro4, callback) {
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerTema",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4 }),
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

//Consultar software
function consultarSoftware(comodin, filtro1, filtro2, filtro3, filtro4, callback) {
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerSoftware",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4 }),
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

//Consultar eventos
function consultarEventos(comodin, filtro1, filtro2, filtro3, filtro4, callback) {
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerReservacion",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4 }),
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

function guardarSoftware(codSoft, codReser, callback) {
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/GuardarSofReserva",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ codSoft: codSoft, codReser: codReser }),
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

function consultarExclusivo(comodin, filtro1, filtro2, filtro3, filtro4, callback) {
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerExclusivo",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4 }),
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

function eliminarReservacion(comodin, filtro1, filtro2, filtro3, filtro4, callback) {
    $.ajax({
        type: "POST",
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/EliminarSoftwareReserva",
        data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response1) {
            var validacion = JSON.parse(response1.d);

            // Validar el resultado devuelto por EliminarSoftwareReserva
            if (validacion.resultado) {
                // Si la validación es verdadera, ejecutar la segunda llamada
                $.ajax({
                    type: "POST",
                    url: "http://localhost:10873/ws/WebServiceCalendar.asmx/EliminarReserva",
                    data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4 }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response2) {
                        var data = JSON.parse(response2.d);

                        callback(data); // Ejecutar el callback final
                    },
                    error: function (xhr) {
                        console.log("Error en EliminarReserva:", xhr.responseText);
                        callback([]);
                    }
                });
            } else {
                // Si validación es falsa, no se hace la segunda llamada
                console.log("Validación fallida. No se eliminará la reserva.");
                callback([]);
            }
        },
        error: function (xhr) {
            console.log("Error en EliminarSoftwareReserva:", xhr.responseText);
            callback([]);
        }
    });
}

function eliminarSoftReservacion(comodin, filtro1, filtro2, filtro3, filtro4, callback) {
    $.ajax({
        type: "POST",
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/EliminarSoftwareReserva",
        data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response1) {
            var data = JSON.parse(response1.d);

            callback(data);
        },
        error: function (xhr) {
            console.log("Error en EliminarSoftwareReserva:", xhr.responseText);
            callback([]);
        }
    });
}

function guardarReservacion(reservacion) {
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/GuardarReserva",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ reservacion: reservacion }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = JSON.parse(response.d);
            var mensaje = data.msg;
            var icon = data.resultado ? 'success' : 'error';
            var codReser = data.strCod_reserc;
            var software = $('#txtSoftware');


            if (listSoftware.length > 0) {
                listSoftware.forEach(item => {
                    let codSoft = item.value;

                    guardarSoftware(codSoft, codReser, function (data) {
                        console.log(data.msg)
                    });
                });
            }
            else if (software.val() !== '' && software.is(':visible')) {
                guardarSoftware(software.val(), codReser, function (data) {
                    console.log(data.msg)
                });
            }

            $('#form_registrar').modal('hide');

            mostrarMensageCRUD(mensaje, icon);
        },
        error: function (xhr, status, error) {
            console.log("Status: " + xhr.status);
            console.log("Response: " + xhr.responseText);
        }
    });
}

function consultarPeriodoAcademico(comodin, filtro1, filtro2, filtro3, filtro4, callback) {
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerPeriodoAcademico",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4 }),
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

function actualizarReservacion(reservacion) {
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ActulizarReservacion",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ reservacion: reservacion }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = JSON.parse(response.d);
            var mensaje = data.msg;
            var icon = data.resultado == true ? 'success' : 'error';

            $('#form_actualizar').modal('hide');

            mostrarMensageCRUD(mensaje, icon);
        },
        error: function (xhr, status, error) {
            console.log("Status: " + xhr.status);
            console.log("Response: " + xhr.responseText);
        }
    });
}