var dia = "";
var horaFin = "";
var selectMateria = '';
var listSoftware = [];
var codLab = $('#' + codLabCli).text();

document.addEventListener('DOMContentLoaded', function () {
    // Múltiples rangos permitidos
    consultarPeriodoAcademico('xGeneral', 'MUTC', 'NA', 'NA', '', function (data) {
        // Guardar los rangos válidos
        rangosPermitidos = data.map(r => ({
            fechaInicio: r.dtFechaIni_per.split('T')[0],
            fechaFin: r.dtFechaFin_per.split('T')[0]
        }));

        // Calcular min y max para el validRange general
        const fechasInicio = rangosPermitidos.map(r => new Date(r.fechaInicio));
        const fechasFin = rangosPermitidos.map(r => new Date(r.fechaFin));

        const min = new Date(Math.min.apply(null, fechasInicio));
        const max = new Date(Math.max.apply(null, fechasFin));

        const rangoValido = {
            start: min.toISOString().split('T')[0],
            end: max.toISOString().split('T')[0]
        };

        var calendarEl = document.getElementById('calendar');

        var ancho = window.innerWidth;

        // Definir altura según el ancho del dispositivo
        var altura =
            ancho <= 480 ? 'auto' :
            ancho <= 1024 ? 650 :
            1200; // para pantallas grandes

        var calendar = new FullCalendar.Calendar(calendarEl, {
            buttonText: {
                month: 'Mes',
                week: 'Semana',
                list: 'Lista'
            },
            locale: 'es',                // idioma español
            height: altura,
            headerToolbar: {
                left: 'prev,next',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,listWeek'
            },
            validRange: rangoValido,
            dayCellDidMount: function(info) {
                const fecha = info.date.toISOString().split('T')[0];
                const dayOfWeek = info.date.getUTCDay(); // 0 = Domingo, 6 = Sábado

                const esDiaValido = rangosPermitidos.some(r => {
                    return fecha >= r.fechaInicio && fecha <= r.fechaFin;
                });

                if (!esDiaValido) {
                    info.el.classList.add('fc-day-disabled');
                }
            },
            dateClick: function (info) {
                var fechaCompleta = info.dateStr;
                var fecha = fechaCompleta.substring(0, 10);
                $('#txtFecha').val(fecha);
                $('#fecha').text(fecha);

                dia = obtenerDiaSemana(fecha);
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
                let fecha = info.event.start.toISOString().split('T')[0];
                $('#fecha').text(fecha);

                mostrarListado(fecha);
                $('#form_listReserva').modal('show');
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
                    var fecha = info.start.toISOString().split('T')[0];

                    mostrarListado(fecha);
                    $('#form_listReserva').modal('show');
                }
            },
            selectAllow: function (selectInfo) {
                const start = selectInfo.startStr;
                const end = selectInfo.endStr;

                return rangosPermitidos.some(r => {
                    return start >= r.fechaInicio && end <= r.fechaFin;
                });
            }
        });

        calendar.render();
    });
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

        $('#det_reservacion').css('display', 'none');
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

        selectHoraFin = $('#selectHoraFin');
        cargarHoraFin(op, '', selectHoraFin)
    });

    $("#switchSoftware").on("change", function () {
        var selectSoftware = $('#countries');

        if ($(this).is(":checked")) {
            // Eliminar instancia previa del plugin
            $('.mult-select-tag').remove();

            // Llamada al backend
            consultarSoftware('xLaboratorio', codLab, '', '', '', function(data) {
                // Llenar el select con datos
                cargarSoftware(data, selectSoftware);

                // Re-inicializar el plugin con el nuevo select
                new MultiSelectTag('countries', {        
                    rounded: true,    // default true
                    shadow: true,      // default false
                    placeholder: 'Search',  // default Search...
                    onChange: function(values) {
                        listSoftware = values
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


    $("#switchSoftwareAct").on("change", function () {
        var selectSoftware = $('#softwareSelect');

        if ($(this).is(":checked")) {
            // Eliminar instancia previa del plugin
            $('.mult-select-tag').remove();

            // Llamada al backend
            consultarSoftware('xLaboratorio', codLab, '', '', '', function (data) {
                // Llenar el select con datos
                cargarSoftware(data, selectSoftware);

                // Re-inicializar el plugin con el nuevo select
                new MultiSelectTag('softwareSelect', {
                    rounded: true,    // default true
                    shadow: true,      // default false
                    placeholder: 'Search',  // default Search...
                    onChange: function (values) {
                        listSoftware = values
                    }
                });
            });

            // Mostrar contenedor
            $('#content-softwareAct').css('display', 'block');
            $('#lblSoftwareValidateAct').text('SI');

        } else {
            // Ocultar contenedor
            $('.multi-select-tag').remove();
            $('#content-softwareAct').css('display', 'none');
            $('#lblSoftwareValidateAct').text('NO');
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

    $("#switchEncontradoAct").on("change", function () {
        if ($(this).is(":checked")) {
            // Mostrar contenedor
            $('#list-softwareAct').css('display', 'block');
            $('#content_nombreAct').css('display', 'none');
            $('#lblSoftwareValAct').text('SI');

        } else {
            // Ocultar contenedor
            $('#list-softwareAct').css('display', 'none');
            $('#content_nombreAct').css('display', 'block');
            $('#lblSoftwareValAct').text('NO');
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

    $("#selectTipoMotivoAct").on("change", function () {
        if ($("#selectTipoMotivoAct").val() === "evento ocasional") {
            // Mostrar contenedor
            $('#content_txtTemaAct').css('display', 'block');
            $('#content_unidadAct').css('display', 'none');
            $('#content_ddlTemaAct').css('display', 'none');

        } else {
            // Ocultar contenedor
            $('#content_txtTemaAct').css('display', 'none');
            $('#content_unidadAct').css('display', 'block');
            $('#content_ddlTemaAct').css('display', 'none');
        }
    });

    //Nueva reservacio
    $("#btnNuevaReserv").click(function() {
        let fechaSelect = new Date($('#fecha').text() + "T00:00:00");
        $('#btnEnviar').attr('disabled', 'disabled');

        $('#form_listReserva').modal('hide');

        $('#form_listReserva').on('hidden.bs.modal', function () {
            let fechaHoy = new Date();
            fechaHoy.setHours(0, 0, 0, 0);

            if(fechaHoy > fechaSelect){
                let mensaje = 'Solo puedes reservar apartir de la fecha actual!';
                let icon = 'warning';

                mostrarMensage(mensaje, icon);
            }
            else if (fechaHoy <= fechaSelect) {
                consultarAsignatura('xDia', dia, cedula, '', '', function (data) {
                    const dropdown = $("#selectAsignatura");
                    cargarMaterias(data, dropdown);

                    selectMateria = $('#selectAsignatura option').first().val();

                    consultarHorario('xCodMat', selectMateria, dia, '', '', function (data) {
                        const dropdown = $("#selectHoraInicio");
                        cargarHora(data, dropdown);
                    });

                    consultarCiclo('xAsignatura', selectMateria, '', '', '', function (data) {
                        const txtCiclo = $("#txtCiclo");
                        const txtParalelo = $("#txtParalelo");
                        cargarCiclo(data, txtCiclo, txtParalelo);
                    });

                    consultarCarrera('xAsignatura', selectMateria, '', '', '', function (data) {
                        const txtCarrera = $("#txtCarrera");
                        cargarCarrera(data, txtCarrera);
                    });

                    consultarAlumno('xAsignatura', selectMateria, '', '', '', function (data) {
                        const txtNumeroAsistentes = $("#txtNumeroAsistentes");
                        cargarNumeroEstudiante(data, txtNumeroAsistentes);
                    });
                });

                $('#form_registrar').modal('show');
            }
        });
    });

    
    $("#btnValidar").click(function () {
        var fechaHoy = $('#txtFecha').val(); // formato: YYYY-MM-DD

        validarReservacion(fechaHoy);
    });

    //Guardar reservacion
    $('#btnEnviar').click(function () {
        let isValid = true;

        // Validar si está visible el bloque principal
        if ($('#det_reservacion').is(':visible')) {

            // Validar tipo/motivo
            if ($('#selectTipoMotivo').val() === "") {
                isValid = false;
                $('#selectTipoMotivo').addClass('is-invalid');
            }
            else {
                $('#selectTipoMotivo').removeClass('is-invalid');
            }

            // Validar unidad
            if ($('#selectUnidad').val() === "") {
                isValid = false;
                $('#selectUnidad').addClass('is-invalid');
            }
            else {
                $('#selectUnidad').removeClass('is-invalid');
            }

            // Validar tema (si está visible)
            if ($('#content_ddlTema').is(':visible') && $('#selectTema').val() === "") {
                isValid = false;
                $('#selectTema').addClass('is-invalid');
            }
            else {
                $('#selectTema').removeClass('is-invalid');
            }

            if ($('#content_txtTema').is(':visible') && $('#txtTema').val().trim() === "") {
                isValid = false;
                $('#txtTema').addClass('is-invalid');
            }
            else {
                $('#txtTema').removeClass('is-invalid');
            }

            // Validar descripción
            if ($('#txtDescripcion').val().trim() === "") {
                isValid = false;
                $('#txtDescripcion').addClass('is-invalid');
            } 
            else {
                $('#txtDescripcion').removeClass('is-invalid');
            }

            // Validar materiales
            if ($('#txtMaterial').val().trim() === "") {
                isValid = false;
                $('#txtMaterial').addClass('is-invalid');
            }
            else {
                $('#txtMaterial').removeClass('is-invalid');
            }

            // Validar software si está habilitado
            if ($('#switchSoftware').is(':checked')) {
                var valorSelect = $('#countries').val();

                if (!$('#switchEncontrado').is(':checked')) {
                    if ($('#txtMaterial').val().trim() === "") {
                        isValid = false;
                        $('#txtSoftware').addClass('is-invalid');
                    }
                    else {
                        $('#txtSoftware').removeClass('is-invalid');
                    }
                }
                else{
                    if (valorSelect == null || valorSelect.length === 0) {
                        isValid = false;
                        $('#countries').addClass('is-invalid');
                    }
                    else {
                        $('#countries').removeClass('is-invalid');
                    }
                }
            }

            if (!isValid) {
                let mensage = "Por favor, complete todos los campos obligatorios."
                let lblMsg = $('#msg_registro');

                mostrarTooltipSimple(mensage, lblMsg);
                return;
            }

            guardarDatos();
        }
    });

    // Detalle
    $('#tbl_det_reservacion').on('click', '.btn-info', function(event) {
        event.preventDefault();

        $('#form_listReserva').modal('hide');
        const idReserva = $(this).data('id');
        
        // Espera a que termine de ocultarse el primero antes de abrir el segundo
        $('#form_listReserva').on('hidden.bs.modal', function () {
            mostrarDetalle(idReserva);

            $('#form_Detalle').modal('show');
        });
    });

    // Editar
    $('#tbl_det_reservacion').on('click', '.btn-success', function(event) {
        event.preventDefault();

        const idReserva = $(this).data('id');
        $('#lblCodReserva').text(idReserva);

        consultarEventos('xPK', idReserva, '', '', '', function(data) {
            var reserva = data[0];
            let fechaHoy = new Date();
            let fechaRegistro = new Date(reserva.dtFechaRegistro_reser);

            fechaRegistro.setHours(fechaRegistro.getHours() + 3);

            if(fechaHoy >= fechaRegistro){
                let mensage = "¡Has superado las tres horas límite para editar la reservación!"
                let lblMsg = $('#txtMsgInfo');

                mostrarTooltipSimple(mensage, lblMsg);
            }
            else{
                $('#form_listReserva').modal('hide');

                var codAsignatura = reserva.strCod_Mate;
                var cedula = reserva.cedula_alu;
                var codUnidad = reserva.strCod_unidTem;

                $('#txtFechaAct').val(reserva.dtFechainicio_reser.split('T')[0]);
                $('#txtHoraInicioAct').val(reserva.dtFechainicio_reser.split('T')[1]);
                $('#txtHoraFinAct').val(reserva.dtFechaFin_reser.split('T')[1]);
                $('#txtNumeroAsistentesAct').val(reserva.intTotalAsistente_reser);
                $('#txtDescripcionAct').val(reserva.strDescripcion_reser);
                $('#txtMaterialesAct').val(reserva.strMateriales_reser);

                consultarAlumno('xCEDULA', cedula, '', '', '', function (data) {
                    var nombre = data[0].apellido_alu + ' ' + data[0].apellidom_alu + ' ' + data[0].nombre_alu;

                    $('#txtEmailAct').val(data[0].correo_alu);
                    $('#txtNombreAct').val(nombre);
                });

                consultarAsignatura('xPK', codAsignatura, '', '', '', function (data) {
                    $('#txtAsignaturaAct').val(data[0].strNombre_mate);
                });

                consultarCiclo('xAsignatura', codAsignatura, '', '', '', function (data) {
                    $('#txtCicloAct').val(data[0].strnombre_curso);
                    $('#txtParaleloAct').val(data[0].strparalelo_curso);
                });

                consultarCarrera('xAsignatura', codAsignatura, '', '', '', function (data) {
                    $('#txtCarreraAct').val(data[0].strnombre_car);
                });

                $('#selectTipoMotivoAct').val(reserva.strProposito_reser);

                if (reserva.strProposito_reser === 'evento ocasional') {
                    $('#content_txtTemaAct').css('display', 'block');
                    $('#content_unidadAct').css('display', 'none');
                    $('#content_ddlTemaAct').css('display', 'none');

                    $('#txtTemaAct').val(reserva.strTema_reser)
                } else {
                    // Ocultar contenedor
                    $('#content_txtTemaAct').css('display', 'none');
                    $('#content_unidadAct').css('display', 'block');
                    $('#content_ddlTemaAct').css('display', 'none');
                }

                consultarUnidad('xAsignatura', codAsignatura, '', '', '', function (dataUni) {
                    const dropdown = $("#selectUnidadAct");
                    cargarUnidad(dataUni, dropdown);

                    $('#selectUnidadAct').val(reserva.strCod_unidTem);

                    consultarTema('xUnidad', codUnidad, '', '', '', function (dataTema) {
                        if (dataTema.length > 0) {
                            $("#content_ddlTemaAct").css("display", 'block');
                            const dropdown = $("#selectTemaAct");
                            cargarTema(dataTema, dropdown);

                            $('#selectTemaAct').val(reserva.strCod_tema);
                        }
                        else {
                            $("#content_ddlTemaAct").css("display", 'none');
                        }
                    });
                });

                consultarSoftware('xCodReserva', idReserva, '', '', '', function (data) {
                    // Llenar el select con datos
                    let selectSoftware = $('#ddlSoftwareAct');
                    cargarSoftware(data, selectSoftware);
                });

                $('#form_listReserva').on('hidden.bs.modal', function () {
                    $('#form_actualizar').modal('show');
                });
            }
        }, function(error) {
            // En caso de error
            console.error("Error consultando eventos", error);
            failureCallback(error);
        });
    });

    //Eliminar reservacion
    $('#tbl_det_reservacion').on('click', '.btn-danger', function (event) {
        event.preventDefault();

        const idReserva = $(this).data('id');

        consultarEventos('xPK', idReserva, '', '', '', function (data) {
            var reserva = data[0];
            let fechaHoy = new Date();
            let fechaRegistro = new Date(reserva.dtFechaRegistro_reser);

            fechaRegistro.setHours(fechaRegistro.getHours() + 3);

            if (fechaHoy > fechaRegistro) {
                let mensage = "¡Has superado las tres horas límite para eliminar la reservación!"
                let lblMsg = $('#txtMsgInfo');

                mostrarTooltipSimple(mensage, lblMsg);
            }
            else {
                $('#form_listReserva').modal('hide');

                $('#form_listReserva').on('hidden.bs.modal', function () {
                    eliminarReservacion('xCodReserva', idReserva, '', '', '', function (data) {
                        let mensaje = data.resultado ? data.msg : data.msg;
                        let icon = data.resultado ? 'success' : 'error';

                        $('#form_listReserva').modal('hide');
                        mostrarMensageCRUD(mensaje, icon)
                    });                    
                });
            }
        }, function (error) {
            // En caso de error
            console.error("Error consultando eventos", error);
            failureCallback(error);
        });
    });

    $("#btnActualizar").click(function () {
        let reservacion = [];
        let idReserva = $('#lblCodReserva').text();

        reservacion[0] = idReserva;
        reservacion[1] = $('#selectUnidadAct').is(':visible') ? $('#selectUnidadAct').val() : '';
        reservacion[2] = $('#selectTemaAct').is(':visible') ? $('#selectTemaAct').val() : $('#selectUnidadAct').val();

        if ($('#txtTemaAct').is(':visible')) {
            reservacion[2] = $('#txtTemaAct').val();
        }

        reservacion[3] = $('#selectTipoMotivoAct').val();
        reservacion[4] = $('#txtDescripcionAct').val();
        reservacion[5] = $('#txtMaterialesAct').val();
        reservacion[6] = '#a4e4af';
        reservacion[7] = '';
        reservacion[8] = '';
        reservacion[9] = true;

        consultarEventos('xPK', idReserva, '', '', '', function (reserva) {
            let fechaHoy = new Date();
            let fechaConvertida = reserva[0].dtFechaRegistro_reser;
            let fechaRegistro = new Date(fechaConvertida);

            fechaRegistro.setHours(fechaRegistro.getHours() + 3);

            if (fechaHoy > fechaRegistro) {
                let mensage = "¡Has superado las tres horas límite para eliminar la reservación!"
                let lblMsg = $('#msg_actualizar');

                mostrarTooltipSimple(mensage, lblMsg);
            }
            else {
                var software = $('#txtSoftwareAct');

                if (listSoftware.length > 0) {
                    eliminarSoftReservacion('xCodReserva', idReserva, '', '', '', function (data) {
                        console.log(data.msg);
                    });

                    listSoftware.forEach(item => {
                        let codSoft = item.value;

                        guardarSoftware(codSoft, idReserva, function (data) {
                            console.log(data.msg)
                        });
                    });
                }
                else if (software.val() !== '' && software.is(':visible')) {
                    eliminarSoftReservacion('xCodReserva', idReserva, '', '', '', function (data) {
                        console.log(data.msg);
                    });

                    guardarSoftware(software.val(), idReserva, function (data) {
                        console.log(data.msg)
                    });
                }

                actualizarReservacion(reservacion);
            }
        }, function (error) {
            // En caso de error
            console.error("Error consultando eventos", error);
            failureCallback(error);
        });
    });
});

function guardarDatos() {
    let reservacion = [];
    let unidad = $('#selectUnidad');
    let tipo = $('#selectTipoMotivo').val()
    let fecha = $('#txtFecha').val();
    let hora_inicio = $('#selectHoraInicio').val();
    let hora_fin = $('#selectHoraFin').val();

    reservacion[0] = $('#selectAsignatura').val();
    reservacion[1] = unidad.is(':visible') ? unidad.val() : '';        
    reservacion[2] = $('#selectTema').is(':visible') && tipo !== 'evento ocasional' ? $('#selectTema').val() : unidad.val();

    if ($('#txtTema').is(':visible')) {
        reservacion[2] = $('#txtTema').val();
    }

    reservacion[3] = $('#txtDescripcion').val();
    reservacion[4] = $('#txtMaterial').val();
    reservacion[5] = fecha + ' ' + hora_inicio;
    reservacion[6] = fecha + ' ' + hora_fin;
    reservacion[7] = $('#txtNumeroAsistentes').val();
    reservacion[8] = cedula;
    reservacion[9] = '#a4e4af';
    reservacion[10] = tipo;
    reservacion[11] = codLab;
    reservacion[12] = false;
    reservacion[13] = 'Reservacion interna';
    reservacion[14] = "";
    reservacion[15] = codReservacion(fecha, hora_inicio, hora_fin);

    guardarReservacion(reservacion);
}

function codReservacion(fecha, hora_inicio, hora_fin) {
    // Procesar cada parte
    let horaInicioSinPuntos = hora_inicio.replace(':', ''); // "0700"
    let horaFinSinPuntos = hora_fin.replace(':', '');       // "0759"

    // Concatenar con guiones bajos
    let resultado = `${codLab}_${fecha.split('-')[2]}${fecha.split('-')[1]}${fecha.split('-')[0]}_${horaInicioSinPuntos}_${horaFinSinPuntos}`;

    return resultado
}

function validarReservacion(fechaHoy) {
    // Construye correctamente las fechas de inicio y fin como objetos Date
    const nuevaInicio = new Date(fechaHoy + 'T' + $('#selectHoraInicio').val() + ':00');
    const nuevaFin = new Date(fechaHoy + 'T' + $('#selectHoraFin').val() + ':00');
    var horaInicioMenosTres = new Date(nuevaInicio.getTime() - 3 * 60 * 60 * 1000);
    var ahora = new Date();
    var lblMsg = $('#tooltipError');


    consultarEventos('xFecha', codLab, fechaHoy, '', '', function(data) {
        let hayConflicto = false;

        data.forEach(reser => {
            let resInicio = new Date(reser.dtFechainicio_reser);
            let resFin = new Date(reser.dtFechaFin_reser);

            // Comparación real entre objetos Date
            if (
                (nuevaInicio >= resInicio && nuevaInicio < resFin) ||
                (nuevaFin > resInicio && nuevaFin <= resFin) ||
                (nuevaInicio <= resInicio && nuevaFin >= resFin)
            ) {
                hayConflicto = true;
            }
        });


        if (hayConflicto) {
            let msg = 'Ya existe una reservacion en esta hora';
            let lblMsg = $('#tooltipError');

            mostrarTooltipSimple(msg, lblMsg);
            $('#btnEnviar').attr('disabled', 'disabled');
            return
        }
        else if (ahora > horaInicioMenosTres && ahora < nuevaFin) {
            let msg = 'Solo puede reservar hasta 3 horas antes del inicio de clase.';
            mostrarTooltipSimple(msg, lblMsg);
            $('#btnEnviar').attr('disabled', 'disabled');
            return
        }
        else if (ahora >= nuevaFin) {
            let msg = 'No es posible reservar un laboratorio después de que la clase ha finalizado.';
            mostrarTooltipSimple(msg, lblMsg);
            $('#btnEnviar').attr('disabled', 'disabled');
            return
        }
        else {
            $('#btnEnviar').removeAttr('disabled');

            consultarExclusivo('xLabExclusivo', codLab, '', '', '', function (data) {
                consultarUnidad('xAsignatura', selectMateria, '', '', '', function(dataAsig) {
                    const dropdown = $("#selectUnidad");
                    cargarUnidad(dataAsig, dropdown);

                    var selectUnidad = $('#selectUnidad option').first().val();

                    if (data.length === 0){
                        $('#det_reservacion').css('display', 'block');
                    }
                    else{
                        let listCarreras = [];
                        let nombreCarForm = $('#txtCarrera').val();
                        let permitida = false;
                        let pendientes = data.length;

                        data.forEach(excl => {
                            consultarCarrera('xPK', excl.strCod_Car, '', '', '', function (dataCarrera) {
                                if (dataCarrera.length > 0) {
                                    let nombreCarConsulta = dataCarrera[0].strnombre_car;

                                    if (nombreCarConsulta === nombreCarForm) {
                                        permitida = true;
                                    }
                                    else {
                                        listCarreras.push(nombreCarConsulta);
                                    }
                                }

                                // Cuando se termina de procesar cada elemento:
                                pendientes--;
                                if (pendientes === 0) {
                                    // Aquí recién puedes hacer el if cuando todos han terminado
                                    if (permitida) {
                                        $('#det_reservacion').css('display', 'block');
                                    }
                                    else {
                                        let msg = 'El laboratorio es exclusivo de ';
                                        let lblMsg = $('#tooltipError');

                                        if (listCarreras.length === 1) {
                                            msg += listCarreras[0];
                                        }
                                        else if (listCarreras.length === 2) {
                                            msg += listCarreras[0] + ' y ' + listCarreras[1];
                                        }
                                        else {
                                            let todosMenosUltimo = listCarreras.slice(0, -1).join(', ');
                                            let ultimo = listCarreras[listCarreras.length - 1];
                                            msg += todosMenosUltimo + ' y ' + ultimo;
                                        }
                                        mostrarTooltipSimple(msg, lblMsg);
                                    }
                                }
                            });
                        });
                    }
                });
            });
        }
    });
}

function mostrarListado(fecha){
    consultarEventos('xFecha', codLab, fecha, '', '',function(data) {
        const tbody = $('#tbl_det_reservacion');
        tbody.empty(); // Limpiar contenido anterior                    

        if(data.length > 0){
            // Iterar sobre los datos recibidos
            $.each(data, function(i, item) {
                const tr = $('<tr></tr>');

                tr.append(`<td>${item.strTema_reser}</td>`);
                tr.append(`<td>${convertirHora(item.dtFechainicio_reser)}</td>`);
                tr.append(`<td>${convertirHora(item.dtFechaFin_reser)}</td>`);
                tr.append(`<td><div style="width:20px; height:20px; background-color:${item.strColor_reser}; border-radius:3px;"></div></td>`);

                // Botones de acción
                const btnDetalle = `<button class="btn btn-info" data-id="${item.strCod_reser}"><i class="fa fa-info-circle" aria-hidden="true"></i></button>`;
                const btnActualizar = `<button class="btn btn-success" data-id="${item.strCod_reser}"><i class="fa fa-upload" aria-hidden="true"></i></button>`;
                const btnEliminar = `<button class="btn btn-danger" data-id="${item.strCod_reser}"><i class="fa fa-trash" aria-hidden="true"></i></button>`;

                consultarAlumno('xCEDULA', item.cedula_alu, '', '', '', function(data){
                    var nombre = data[0].apellido_alu + ' ' + data[0].apellidom_alu + ' ' + data[0].nombre_alu;

                    tr.append(`<td>${nombre}</td>`);

                    if (cedula === item.cedula_alu) {
                        tr.append(`<td>${btnDetalle} ${btnActualizar} ${btnEliminar}</td>`);
                    }
                    else {
                        tr.append(`<td>${btnDetalle}</td>`);
                    }
                });
                tbody.append(tr);
            });
        }
        else{
            const tr = $('<tr></tr>');

            tr.append(`<td colspan="6" class='text-center'>No sé a encontrado reservaciones para el día de hoy</td>`);
            tbody.append(tr);
        }
    }, function(error) {
        // En caso de error
        console.error("Error consultando eventos", error);
    });
}

function mostrarDetalle(idReserva){
    consultarEventos('xPK', idReserva, '', '', '', function(data) {
        var reserva = data[0];
        var cedula = reserva.cedula_alu;

        consultarAlumno('xCEDULA', cedula, '', '', '', function(data){
            var nombre = data[0].apellido_alu + ' ' + data[0].apellidom_alu + ' ' + data[0].nombre_alu;

            $('#txtCorreoDet').val(data[0].correo_alu);
            $('#txtNombresDet').val(nombre);
        });

        if (!reserva.bitTipo_reser) {
            var codAsignatura = reserva.strCod_Mate;
            var codUnidad = reserva.strCod_unidTem;

            $('#txtFechaDet').val(reserva.dtFechainicio_reser.split('T')[0]);
            $('#txtHoraInicioDet').val(reserva.dtFechainicio_reser.split('T')[1]);
            $('#txtHoraFinDet').val(reserva.dtFechaFin_reser.split('T')[1]);
            $('#txtAsistentes').val(reserva.intTotalAsistente_reser);
            $('#txtTemaDet').val(reserva.strTema_reser);
            $('#txtDescDet').val(reserva.strDescripcion_reser);
            $('#txtMaterialDet').val(reserva.strMateriales_reser);
            $('#txtTipoMotivoDet').val(reserva.strProposito_reser.toUpperCase());

            if (reserva.strProposito_reser === 'evento ocasional') {
                $('#content_unidadDet').css('display', 'none');
            } else {
                $('#content_unidadDet').css('display', 'block');
            }

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
        
            consultarSoftware('xCodReserva', idReserva, '', '', '', function(data) {
                // Llenar el select con datos
                let selectSoftware = $('#ddlSoftwareDet');
                cargarSoftware(data, selectSoftware);
            });

            consultarUnidad('xPK', codUnidad, '', '', '', function (data) {
                $('#txtUnidadDet').val(data[0].strdesc_unidtem);
            });

            $('#detInterno').css('display', 'block');
            $('#detExterno').css('display', 'none');
        }
        else {
            $('#txtFechaExtDet').val(reserva.dtFechainicio_reser.split('T')[0]);
            $('#txtHoraInicioExtDet').val(reserva.dtFechainicio_reser.split('T')[1]);
            $('#txtHoraFinExtDet').val(reserva.dtFechaFin_reser.split('T')[1]);
            $('#txtTipoMotivoExtDet').val(reserva.strProposito_reser);
            $('#txtTemaExtDet').val(reserva.strTema_reser);
            $('#txtObExtDet').val(reserva.strObs1_reser);
            $('#txtDescExtDet').val(reserva.strDescripcion_reser);
            $('#txtMaterialExtDet').val(reserva.strMateriales_reser);

            $('#detExterno').css('display', 'block');
            $('#detInterno').css('display', 'none');
        }

    }, function(error) {
        // En caso de error
        console.error("Error consultando eventos", error);
        failureCallback(error);
    });
}