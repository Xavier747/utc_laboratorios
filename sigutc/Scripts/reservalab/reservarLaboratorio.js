var dia = "";
var horaFin = "";
var selectMateria = '';
var eventId = '';

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
            const fechaCompleta = info.dateStr;
            const fecha = fechaCompleta.substring(0, 10);
            $('#txtFecha').val(fecha);

            dia = obtenerDiaSemana(fecha);

            consultarAsignatura('xDia', dia, cedula, '', '', function(data){
                cargarMaterias(data);
                selectMateria = $('#selectAsignatura option').first().val();

                consultarHorario('xCodMat', selectMateria, dia, '', '', function(data){
                    const dropdown = $("#selectHoraInicio");
                    cargarHora(data, dropdown);
                });

                consultarCiclo('xAsignatura', selectMateria, '', '', '', function(data){
                    const txtCiclo = $("#txtCiclo");
                    const txtParalelo = $("#txtParalelo");
                    cargarCiclo(data, txtCiclo, txtParalelo);
                });

                consultarCarrera('xAsignatura', selectMateria, '', '', '', function(data){
                    const txtCarrera = $("#txtCarrera");
                    cargarCarrera(data, txtCarrera);
                });

                consultarAlumno('xAsignatura', selectMateria, '', '', '', function(data){
                    const txtNumeroAsistentes = $("#txtNumeroAsistentes");
                    cargarNumeroEstudiante(data, txtNumeroAsistentes);
                });
            });
        },
        events: function (fetchInfo, successCallback, failureCallback) {

            consultarEventos('xCodLab', codLab, '', '', '',function(data) {
                const eventos = [];

                // Iterar sobre los datos recibidos
                $.each(data, function(i, item) {
                    eventos.push({
                        id: item.strCod_reser,
                        title: item.strTema_reser,
                        start: convertirFechaForFullCalendar(item.dtFechainicio_reser),
                        end: convertirFechaForFullCalendar(item.dtFechaFin_reser),
                        backgroundColor: item.strColor_reser,
                    });
                });

                // Enviar eventos a FullCalendar
                successCallback(eventos);
            }, function(error) {
                // En caso de error
                console.error("Error consultando eventos", error);
                failureCallback(error);
            });
        },
        eventDidMount: function(info) {
            info.el.classList.add('evento-personalizado');
        },
        eventTimeFormat: {
            hour: 'numeric',
            hour12: true
        },
        eventClick: function(info){
            eventId = info.event.id;

            consultarEventos('xPK', eventId, '', '', '', function(data) {
                var reserva = data[0];
                var codAsignatura = data[0].strCod_Mate;
                var cedula = data[0].cedula_alu;
                var codUnidad = data[0].strCod_unidTem;

                $('#txtFechaDet').val(data[0].dtFechainicio_reser.split('T')[0]);
                $('#txtHoraInicioDet').val(data[0].dtFechainicio_reser.split('T')[1]);
                $('#txtHoraFinDet').val(data[0].dtFechaFin_reser.split('T')[1]);
                $('#txtAsistentes').val(data[0].intTotalAsistente_reser);
                $('#txtTemaDet').val(data[0].strTema_reser);
                $('#txtDescDet').val(data[0].strDescripcion_reser);
                $('#txtMaterialDet').val(data[0].strMateriales_reser);

                consultarAlumno('xCEDULA', cedula, '', '', '', function(data){
                    var nombre = data[0].apellido_alu + ' ' + data[0].apellidom_alu + ' ' + data[0].nombre_alu;

                    $('#txtCorreoDet').val(data[0].correo_alu);
                    $('#txtNombresDet').val(nombre);
                });

                consultarAsignatura('xPK', codAsignatura, '', '', '', function(data){
                    $('#txtAsigDet').val(data[0].strNombre_mate);
                });                

                consultarCiclo('xAsignatura', codAsignatura, '', '', '', function(data){
                    $('#txtCicloDet').val(data[0].strnombre_curso);
                    $('#txtParaleloDet').val(data[0].strparalelo_curso);
                });

                consultarCarrera('xAsignatura', codAsignatura, '', '', '', function(data){
                    $('#txtCarreraDet').val(data[0].strnombre_car);
                });

                //$('#txtTipoMotivoDet').val(data[0].);
                consultarUnidad('xPK', codUnidad, '', '', '', function(data){
                    $('#txtUnidadDet').val(data[0].strdesc_unidtem);
                });
            }, function(error) {
                // En caso de error
                console.error("Error consultando eventos", error);
                failureCallback(error);
            });

            $('#form_Detalle').modal('show');
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
                calendar.unselect();
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
        consultarHorario('xCodMat', asignaturaId, dia, '', '', function(data){
            const dropdown = $("#selectHoraInicio");
            cargarHora(data, dropdown);

            selectMateria = $('#selectAsignatura').val();

            consultarCiclo('xAsignatura', selectMateria, '', '', '', function(data){
                const txtCiclo = $("#txtCiclo");
                const txtParalelo = $("#txtParalelo");
                cargarCiclo(data, txtCiclo, txtParalelo);
            });

            consultarCarrera('xAsignatura', selectMateria, '', '', '', function(data){
                const txtCarrera = $("#txtCarrera");
                cargarCarrera(data, txtCarrera);
            });

            consultarAlumno('xAsignatura', selectMateria, '', '', '', function(data){
                const txtNumeroAsistentes = $("#txtNumeroAsistentes");
                cargarNumeroEstudiante(data, txtNumeroAsistentes);
            });
        });
    });

    $("#selectUnidad").on('change', function () {
        var unidadId = this.value; // Capturar el valor seleccionado
        consultarTema('xUnidad', unidadId, '', '', '', function(data){

            if(data.length > 0){
                $("#content_ddlTema").css("display", 'block');
                const dropdown = $("#selectTema");
                cargarTema(data, dropdown);
            }
            else{
                $("#content_ddlTema").css("display", 'none');
            }
        });
    });

    $('#selectHoraInicio').on('change', function () {
        const horaInicio = $(this).val();
        const op = 2;

        selectMateria = $('#selectAsignatura option').first().val();

        consultarHorario(selectMateria, dia, function(data){
            console.log(data);
            const dropdown = $("#selectHoraFin");

            cargarHoraFin(op, data, dropdown)
        });
    });

    $("#switchSoftware").on("change", function () {
        var selectSoftware = $('#countries');

        if ($(this).is(":checked")) {
            // Eliminar instancia previa del plugin
            $('.mult-select-tag').remove();

            // Llamada al backend
            consultarSoftware(codLab, function(data) {
                // Llenar el select con datos
                cargarSoftware(data, selectSoftware);

                // Re-inicializar el plugin con el nuevo select
                new MultiSelectTag('countries', {        
                    rounded: true,    // default true
                    shadow: true,      // default false
                    placeholder: 'Search',  // default Search...
                    onChange: function(values) {
                        console.log(values)
                    }
                });
            });

            // Mostrar contenedor
            $('#content-software').css('display', 'block');
            $('#lblSoftwareValidate').text('SI');

        } else {
            // Ocultar contenedor
            $('.multi-select-tag').remove();
            $('#content-software').css('display', 'none');
            $('#lblSoftwareValidate').text('NO');
        }
    });


    $("#switchEncontrado").on("change", function () {
        if ($(this).is(":checked")) {
            // Mostrar contenedor
            $('#list-software').css('display', 'block');
            $('#content_nombre').css('display', 'none');
            $('#lblSoftwareVal').text('SI');

        } else {
            // Ocultar contenedor
            $('#list-software').css('display', 'none');
            $('#content_nombre').css('display', 'block');
            $('#lblSoftwareVal').text('NO');
        }
    });

    $("#selectTipoMotivo").on("change", function () {
        if ($("#selectTipoMotivo").val() === "evento ocasional") {
            // Mostrar contenedor
            $('#content_txtTema').css('display', 'block');
            $('#content_unidad').css('display', 'none');
            $('#content_ddlTema').css('display', 'none');

        } else {
            // Ocultar contenedor
            $('#content_txtTema').css('display', 'none');
            $('#content_unidad').css('display', 'block');
            $('#content_ddlTema').css('display', 'none');
        }
    });

    $("#btnEnviar").click(function() {
        let reservacion = [];

        reservacion[0] = $('#selectAsignatura').val();
        reservacion[1] = $('#selectUnidad').val();
        reservacion[2] = $('#selectTema').val();
        reservacion[3] = $('#txtDescripcion').val();
        reservacion[4] = $('#txtMaterial').val();
        reservacion[5] = $('#txtFecha').val() + ' ' + $('#selectHoraInicio').val();
        reservacion[6] = $('#txtFecha').val() + ' ' + $('#selectHoraFin').val();
        reservacion[7] = $('#txtNumeroAsistentes').val();
        reservacion[8] = '';
        reservacion[9] = '#a4e4af';

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
                var icon = data.resultado == true ? 'success' : 'error';

                $('#form_registrar').modal('hide');

                mostrarMensageCRUD(mensaje, icon);
            },
            error: function (xhr, status, error) {
                console.log("Status: " + xhr.status);
                console.log("Response: " + xhr.responseText);                
            }
        });
    });

    $("#btnEditar").click(function() {
        $('#form_Detalle').modal('hide');

        // Espera a que termine de ocultarse el primero antes de abrir el segundo
        $('#form_Detalle').on('hidden.bs.modal', function () {
            consultarEventos('xPK', eventId, '', '', '', function(data) {
                var reserva = data[0];
                var codAsignatura = data[0].strCod_Mate;
                var cedula = data[0].cedula_alu;
                var codUnidad = data[0].strCod_unidTem;

                $('#txtFechaAct').val(data[0].dtFechainicio_reser.split('T')[0]);
                $('#txtHoraInicioAct').val(data[0].dtFechainicio_reser.split('T')[1]);
                $('#txtHoraFinAct').val(data[0].dtFechaFin_reser.split('T')[1]);
                $('#txtNumeroAsistentesAct').val(data[0].intTotalAsistente_reser);
                $('#txtDescripcionAct').val(data[0].strDescripcion_reser);
                $('#txtMaterialesAct').val(data[0].strMateriales_reser);

                consultarAlumno('xCEDULA', cedula, '', '', '', function(data){
                    var nombre = data[0].apellido_alu + ' ' + data[0].apellidom_alu + ' ' + data[0].nombre_alu;

                    $('#txtEmailAct').val(data[0].correo_alu);
                    $('#txtNombreAct').val(nombre);
                });

                consultarAsignatura('xPK', codAsignatura, '', '', '', function(data){
                    $('#txtAsignaturaAct').val(data[0].strNombre_mate);
                });                

                consultarCiclo('xAsignatura', codAsignatura, '', '', '', function(data){
                    $('#txtCicloAct').val(data[0].strnombre_curso);
                    $('#txtParaleloAct').val(data[0].strparalelo_curso);
                });

                consultarCarrera('xAsignatura', codAsignatura, '', '', '', function(data){
                    $('#txtCarreraAct').val(data[0].strnombre_car);
                });

                //$('#txtTipoMotivoDet').val(data[0].);

                consultarUnidad('xAsignatura', codAsignatura, '', '', '', function(dataUni){
                    const dropdown = $("#selectUnidadAct");
                    cargarUnidad(dataUni, dropdown);

                    $('#selectUnidadAct').val(data[0].strCod_unidTem);
                }); 


                consultarTema('xUnidad', codUnidad, '', '', '', function(data){

                    if(data.length > 0){
                        $("#content_ddlTema").css("display", 'block');
                        const dropdown = $("#selectTemaAct");
                        cargarTema(data, dropdown);

                        $('#selectTemaAct').val(data[0].strCod_tema);
                    }
                    else{
                        $("#content_ddlTema").css("display", 'none');
                    }
                });

            }, function(error) {
                // En caso de error
                console.error("Error consultando eventos", error);
                failureCallback(error);
            });






            $('#form_actualizar').modal('show');
        });
    });

    $("#btnActualizar").click(function() {
        /*let reservacion = [];

        reservacion[0] = $('#selectAsignatura').val();
        reservacion[1] = $('#selectUnidad').val();
        reservacion[2] = $('#selectTema').val();
        reservacion[3] = $('#txtDescripcion').val();
        reservacion[4] = $('#txtMaterial').val();
        reservacion[5] = $('#txtFecha').val() + ' ' + $('#selectHoraInicio').val();
        reservacion[6] = $('#txtFecha').val() + ' ' + $('#selectHoraFin').val();
        reservacion[7] = $('#txtNumeroAsistentes').val();
        reservacion[8] = '';
        reservacion[9] = '#a4e4af';

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
                var icon = data.resultado == true ? 'success' : 'error';

                $('#form_registrar').modal('hide');

                mostrarMensageCRUD(mensaje, icon);
            },
            error: function (xhr, status, error) {
                console.log("Status: " + xhr.status);
                console.log("Response: " + xhr.responseText);                
            }
        });*/
    });
});

//Funciones de Consulta
//Consulta las asignauras del docente
function consultarAsignatura(comodin, filtro1, filtro2, filtro3, filtro4, callback){
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerAsignaturas",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4}),
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
function consultarHorario(comodin, filtro1, filtro2, filtro3, filtro4, callback){
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
function consultarCiclo(comodin, filtro1, filtro2, filtro3, filtro4, callback){
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
function consultarCarrera(comodin, filtro1, filtro2, filtro3, filtro4, callback){
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
function consultarAlumno(comodin, filtro1, filtro2, filtro3, filtro4, callback){
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

function consultarUnidad(comodin, filtro1, filtro2, filtro3, filtro4, callback){
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

function consultarTema(comodin, filtro1, filtro2, filtro3, filtro4, callback){
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerTema",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ comodin: comodin, filtro1: filtro1, filtro2: filtro2, filtro3: filtro3, filtro4: filtro4}),
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
function consultarSoftware(codLab, callback){
    $.ajax({
        type: "POST",
        // Página y método del backend que procesará la solicitud
        url: "http://localhost:10873/ws/WebServiceCalendar.asmx/ObtenerSoftware",
        // Enviar la fecha como parámetro
        data: JSON.stringify({ codLab: codLab }),
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
function consultarEventos(comodin, filtro1, filtro2, filtro3, filtro4, callback){
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

//Funcion para mostrar en el formulario
//Carga los horario de inicio de clases por cada hora durante un periodo de tiempo
function cargarHora(data, dropdown){
    dropdown.empty();

    // Crear opción por cada elemento de la lista
    data.forEach(item => {
        const opcion = document.createElement("option");
        opcion.value = convertirHora(item.strHoraInicio);          // valor que se enviará
        opcion.textContent = convertirHora(item.strHoraInicio); // lo que se muestra al usuario
        dropdown.append(opcion);
        horaFin = item.strCod_horas;
    });

    var selectHoraInicio = dropdown.first().val();
    var selectHoraFin = $('#selectHoraFin')
    const op = 1;
    cargarHoraFin(op, data, selectHoraFin)
}

//Carga los horarios de fin de clases por cada hora reloj finalizada
function cargarHoraFin(op, data, dropdown){
    dropdown.empty();

    //Carga la hora de acuerdo a como llega de la base de datos
    if(op === 1){
        // Crear opción por cada elemento de la lista
        data.forEach(item => {
            const opcion = document.createElement("option");
            opcion.value = convertirHora(item.strHoraFin);          // valor que se enviará
            opcion.textContent = convertirHora(item.strHoraFin); // lo que se muestra al usuario
            dropdown.append(opcion);
        });
    }
    //Carga el codigo deacuerdo al item y/o hora de inicio seleccionado
    else if(op === 2){
        const hora = $("#selectHoraInicio").val();

        var horaInicio = parseInt(hora.replace('H', ''), 10);
        var horaF = parseInt(horaFin.replace('H', ''), 10);

        for (let i = horaInicio; i <= horaF; i++) {
            if (i < 10) {
                const option = `<option value="0${i}:59">0${i}:59</option>`;
                dropdown.append(option);
            }
            else {
                const option = `<option value="${i}:59">${i}:59</option>`;
                dropdown.append(option);
            }
        }
    }
}

//Cargar ciclo, donde el primer agumento es la data debuelta en la consulta
//el segundo y tercero son los componentes "input text"
function cargarCiclo(data, input1, input2){
    input1.empty();
    input2.empty();

    // Crear opción por cada elemento de la lista
    input1.val(data[0].strnombre_curso);
    input2.val(data[0].strparalelo_curso);
}

function cargarCarrera(data, input1){
    input1.empty();

    // Crear opción por cada elemento de la lista
    input1.val(data[0].strnombre_car);
}

function cargarNumeroEstudiante(data, input1){
    input1.empty();

    // Crear opción por cada elemento de la lista
    let numeroEstudiantes = data.length;
    input1.val(numeroEstudiantes);
}

function convertirHora(fechaCompleta){
    const fecha = new Date(fechaCompleta);

    const horas = fecha.getHours().toString().padStart(2, '0');
    const minutos = fecha.getMinutes().toString().padStart(2, '0');
    const segundos = fecha.getSeconds().toString().padStart(2, '0');

    const horaFinal = `${horas}:${minutos}`;
    return horaFinal;
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

function validarReservacion(){
    const fechaInicio = $('#txtFecha').val() + ' ' + $('#selectHoraInicio').val();
    const fechaFin = $('#txtFecha').val() + ' ' + $('#selectHoraFin').val();

    consultarUnidad('xAsignatura', selectMateria, '', '', '', function(data){
        const dropdown = $("#selectUnidad");
        cargarUnidad(data, dropdown);

        var selectUnidad = $('#selectUnidad option').first().val();
    }); 

    $('#det_reservacion').css('display', 'block');
}

function cargarUnidad(data, dropdown){
    dropdown.empty();

    // Crear opción por cada elemento de la lista
    data.forEach(item => {
        const opcion = document.createElement("option");
        opcion.value = item.strcod_unidtem;          // valor que se enviará
        opcion.textContent = item.strdesc_unidtem; // lo que se muestra al usuario
        dropdown.append(opcion);
    });
}

function cargarTema(data, dropdown){
    dropdown.empty();
    // Crear opción por cada elemento de la lista
    data.forEach(item => {
        const opcion = document.createElement("option");
        opcion.value = item.strCod_tema;          // valor que se enviará
        opcion.textContent = item.strDesc_tema; // lo que se muestra al usuario
        dropdown.append(opcion);
    });
}

function cargarSoftware(data, selectSoftware) {
    selectSoftware.empty(); // Limpia opciones anteriores
    console.log(data);
    $.each(data, function (i, item) {
        var opcion = $('<option>', {
            value: item.strCod_sof,
            text: item.strNombre_sof
        });
        selectSoftware.append(opcion);
    });
}

function convertirFechaForFullCalendar(fecha){
    return fecha.replace(' ', 'T');
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

function mostrarMensageCRUD(mensaje, icon){
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