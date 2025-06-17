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

function guardarSoftware(codReser, callback) {
    if (listSoftware.length > 0) {
        listSoftware.forEach(item => {
            let codSoft = item.value;

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
        });
    }
    else {
        let codSoft = $('#txtSoftware').val();

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