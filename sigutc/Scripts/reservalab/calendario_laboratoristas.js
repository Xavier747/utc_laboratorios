var codLab = codLabCli;
var codSede = codSedeCli;
var codFacultad = codFacultadCli;
var dia = "";
var horaFin = "";
var selectMateria = '';
var listSoftware = [];

document.addEventListener('DOMContentLoaded', function () {
    // Múltiples rangos permitidos
    var calendarEl = document.getElementById('calendarLab');

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
        initialView: 'dayGridMonth', // vista mensual
        locale: 'es',                // idioma español
        height: altura,
        headerToolbar: {
            left: 'prev,next',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,listWeek'
        },
        dateClick: function (info) {
            var fechaCompleta = info.dateStr;
            var fecha = fechaCompleta.substring(0, 10);
            $('#fecha').text(fecha);
            $('#txtFecha').val(fecha);
            $('#txtFechaExt').val(fecha);

            dia = obtenerDiaSemana(fecha);
        },
        events: function (fetchInfo, successCallback, failureCallback) {
            consultarEventos('xCodLab', codLab, '', '', '', function (data) {
        
                const eventos = data.map(function (item) {
                    // .map crea un nuevo arreglo basado en lo que retorna esta función
                    return {
                        id: item.strCod_reser,
                        title: item.strTema_reser,
                        start: convertirFechaForFullCalendar(item.dtFechainicio_reser),
                        end: convertirFechaForFullCalendar(item.dtFechaFin_reser),
                        backgroundColor: item.strColor_reser,
                        borderColor: item.strColor_reser
                    };
                });

                successCallback(eventos); // Enviar eventos a FullCalendar

            }, function (error) {
                console.error("Error consultando eventos", error);
                failureCallback(error);
            });
        },
        eventDidMount: function (info) {
            info.el.classList.add('evento-personalizado');
        },
        eventTimeFormat: {
            hour: 'numeric',
            hour12: true
        },
        eventClick: function (info) {
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

            now.setHours(0, 0, 0, 0);
            if (info.start < now) {
                let mensaje = 'Las fechas pasadas no están disponibles para reservas.';
                let icon = 'warning';

                mostrarMensage(mensaje, icon);
                calendar.unselect();
            }
            else {
                var fecha = info.start.toISOString().split('T')[0];

                mostrarListado(fecha);
                $('#form_listReserva').modal('show');
            }
        },
    });

    calendar.render();
});

$(document).ready(function () {
    $("#switchReserva").on("change", function () {
        if ($(this).is(":checked")) {
            // Mostrar contenedor
            $('#reservaInterna').css('display', 'none');
            $('#reservaExterna').css('display', 'block');

        } else {
            // Ocultar contenedor           
            $('#reservaInterna').css('display', 'block');
            $('#reservaExterna').css('display', 'none');
        }
    });

    $("#selectDocentes").on('change', function () {
        var cedula = this.value;

        consultarAlumno('xCEDULA', cedula, '', '', '', function (data) {
            $('#txtEmail').val(data[0].correo_alu);
        });

        consultarAsignatura('xDocente', cedula, '', '', '', function (data) {
            const dropdown = $("#selectAsignatura");
            cargarMaterias(data, dropdown);

            selectMateria = $('#selectAsignatura option').first().val();
            consultarInformacionParaReservacion(selectMateria);
        });

        $('#det_reservacionInt').css('display', 'none');
    })

    $("#selectAsistencia").on('change', function () {
        let valor = $(this).val();

        console.log(valor)

        if (valor === '0') {
            $('#contentAsistencias').hide();
        } else {
            $('#contentAsistencias').show();
        }
    });

    $("#selectAsignatura").on('change', function () {
        var asignaturaId = this.value; // Capturar el valor seleccionado
        selectMateria = $('#selectAsignatura').val();

        consultarInformacionParaReservacion(asignaturaId);

        $('#det_reservacionInt').css('display', 'none');
    });

    $("#selectUnidadInt").on('change', function () {
        // Capturar el valor seleccionado
        var unidadId = this.value; 

        consultarTema('xUnidad', unidadId, '', '', '', function (data) {
            if (data.length > 0) {
                $("#content_ddlTemaInt").css("display", 'block');
                const dropdown = $("#selectTemaInt");
                cargarTema(data, dropdown);
            }
            else {
                $("#content_ddlTemaInt").css("display", 'none');
            }
        });
    });

    $("#switchSoftwareInt").on("change", function () {
        var selectSoftware = $('#ddlSoftwareInt');

        if ($(this).is(":checked")) {
            // Eliminar instancia previa del plugin
            $('.mult-select-tag').remove();

            // Llamada al backend
            consultarSoftware('xLaboratorio', codLab, '', '', '', function (data) {
                // Llenar el select con datos
                cargarSoftware(data, selectSoftware);

                // Re-inicializar el plugin con el nuevo select
                new MultiSelectTag('ddlSoftwareInt', {
                    rounded: true,    // default true
                    shadow: true,      // default false
                    placeholder: 'Search',  // default Search...
                    onChange: function (values) {
                        listSoftware = values
                    }
                });
            });

            // Mostrar contenedor
            $('#content-softwareInt').css('display', 'block');
            $('#lblSoftwareValidateInt').text('SI');

        } else {
            // Ocultar contenedor
            $('.multi-select-tag').remove();
            $('#content-softwareInt').css('display', 'none');
            $('#lblSoftwareValidateInt').text('NO');
        }
    });

    $("#switchEncontradoInt").on("change", function () {
        if ($(this).is(":checked")) {
            // Mostrar contenedor
            $('#list-softwareInt').css('display', 'block');
            $('#content_nombreInt').css('display', 'none');
            $('#lblSoftwareValInt').text('SI');

        } else {
            // Ocultar contenedor
            $('#list-softwareInt').css('display', 'none');
            $('#content_nombreInt').css('display', 'block');
            $('#lblSoftwareValInt').text('NO');
        }
    });

    $("#switchSoftwareAct").on("change", function () {
        var selectSoftware = $('#ddlSoftwareAct');

        if ($(this).is(":checked")) {
            // Eliminar instancia previa del plugin
            $('.mult-select-tag').remove();

            // Llamada al backend
            consultarSoftware('xLaboratorio', codLab, '', '', '', function (data) {
                // Llenar el select con datos
                cargarSoftware(data, selectSoftware);

                // Re-inicializar el plugin con el nuevo select
                new MultiSelectTag('ddlSoftwareAct', {
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


    $("#selectPropositoInt").on("change", function () {
        if ($("#selectPropositoInt").val() === "evento ocasional") {
            // Mostrar contenedor
            $('#content_txtTemaInt').css('display', 'block');
            $('#content_unidadInt').css('display', 'none');
            $('#content_ddlTemaInt').css('display', 'none');

        } else {
            // Ocultar contenedor
            $('#content_txtTemaInt').css('display', 'none');
            $('#content_unidadInt').css('display', 'block');
            $('#content_ddlTemaInt').css('display', 'none');
        }
    });

    $("#btnNuevaReserv").click(function () {
        let fechaSelect = new Date($('#fecha').text() + "T00:00:00");

        $('#form_listReserva').modal('hide');

        $('#form_listReserva').on('hidden.bs.modal', function () {
            let fechaHoy = new Date();
            fechaHoy.setHours(0, 0, 0, 0);


            if (fechaHoy > fechaSelect) {
                let mensaje = 'Solo puedes reservar apartir de la fecha actual!';
                let icon = 'warning';

                mostrarMensage(mensaje, icon);
            }
            else if (fechaHoy <= fechaSelect) {

                consultarAlumno('xSedeFacultad', codSede, codFacultad, '', '', function (data) {
                    var selectDocentes = $('#selectDocentes');
                    var txtCorreo = $('#txtEmail');

                    cargarDocente(data, selectDocentes, txtCorreo);

                    let cedulaDocen = selectDocentes.find('option:first').val();

                    consultarAsignatura('xDocente', cedulaDocen, '', '', '', function (data) {
                        const dropdown = $("#selectAsignatura");
                        cargarMaterias(data, dropdown);

                        selectMateria = $('#selectAsignatura option').first().val();

                        consultarInformacionParaReservacion(selectMateria);
                    });
                });

                var inicioSelect = '';
                var finSelect = '';

                //Horas reservacion interna
                inicioSelect = $('#selectHoraInicio');
                finSelect = $('#selectHoraFin');

                llenarHoras(inicioSelect, finSelect);
                $('#form_registrar').modal('show');
            }
        });
    });

    $("#btnValidar").click(function () {
        var fechaHoy = $('#txtFecha').val(); // formato: YYYY-MM-DD

        validarReservacion(fechaHoy);
    });

    $("#btnEnviar").click(function () {
        let isValid = true;

        // Verifica si es reserva INTERNA
        if (!$('#switchReserva').is(":checked")) {
            if ($('#det_reservacionInt').is(':visible')) {

                // Validar tipo/motivo
                if ($('#selectPropositoInt').val() === "") {
                    isValid = false;
                    $('#selectPropositoInt').addClass('is-invalid');
                } else {
                    $('#selectPropositoInt').removeClass('is-invalid');
                }

                // Validar unidad
                if ($('#selectUnidadInt').val() === "") {
                    isValid = false;
                    $('#selectUnidadInt').addClass('is-invalid');
                } else {
                    $('#selectUnidadInt').removeClass('is-invalid');
                }

                // Validar tema (opcional según visibilidad)
                if ($('#content_ddlTemaInt').is(':visible') && $('#selectTemaInt').val() === "") {
                    isValid = false;
                    $('#selectTemaInt').addClass('is-invalid');
                } else {
                    $('#selectTemaInt').removeClass('is-invalid');
                }

                if ($('#content_txtTemaInt').is(':visible') && $('#txtTemaInt').val().trim() === "") {
                    isValid = false;
                    $('#txtTemaInt').addClass('is-invalid');
                } else {
                    $('#txtTemaInt').removeClass('is-invalid');
                }

                // Validar descripción
                if ($('#txtDescripcionInt').val().trim() === "") {
                    isValid = false;
                    $('#txtDescripcionInt').addClass('is-invalid');
                } else {
                    $('#txtDescripcionInt').removeClass('is-invalid');
                }

                // Validar materiales
                if ($('#txtMaterialInt').val().trim() === "") {
                    isValid = false;
                    $('#txtMaterialInt').addClass('is-invalid');
                } else {
                    $('#txtMaterialInt').removeClass('is-invalid');
                }

                // Validar software si está activado
                if ($('#switchSoftwareInt').is(':checked')) {
                    var valorSelect = $('#ddlSoftwareInt').val();

                    if (!$('#switchEncontradoInt').is(':checked')) {
                        if ($('#txtSoftwareInt').val().trim() === "") {
                            isValid = false;
                            $('#txtSoftwareInt').addClass('is-invalid');
                        } else {
                            $('#txtSoftwareInt').removeClass('is-invalid');
                        }
                    } else {
                        if (valorSelect == null || valorSelect.length === 0) {
                            isValid = false;
                            $('#ddlSoftwareInt').addClass('is-invalid');
                        } else {
                            $('#ddlSoftwareInt').removeClass('is-invalid');
                        }
                    }
                }

                if (!isValid) {
                    mostrarTooltipSimple("Por favor, complete todos los campos obligatorios.", $('#msg_registro'));
                    return;
                }

                guardarDatos(); // Ejecutar si todo es válido
            }
        }

            // Si es reserva EXTERNA
        else {
            if ($('#det_reservacionExt').is(':visible')) {

                // Aquí puedes replicar el mismo tipo de validaciones que para interna, pero con sufijo "Ext"
                if ($('#selectTipoMotivo').val() === "") {
                    isValid = false;
                    $('#selectTipoMotivo').addClass('is-invalid');
                } else {
                    $('#selectTipoMotivo').removeClass('is-invalid');
                }

                if ($('#txtDescripcion').val().trim() === "") {
                    isValid = false;
                    $('#txtDescripcion').addClass('is-invalid');
                } else {
                    $('#txtDescripcion').removeClass('is-invalid');
                }

                if ($('#txtMaterial').val().trim() === "") {
                    isValid = false;
                    $('#txtMaterial').addClass('is-invalid');
                } else {
                    $('#txtMaterial').removeClass('is-invalid');
                }

                if ($('#txtObservacion').val().trim() === "") {
                    isValid = false;
                    $('#txtObservacion').addClass('is-invalid');
                } else {
                    $('#txtObservacion').removeClass('is-invalid');
                }

                // Agrega aquí otras validaciones si aplica (por ejemplo software externo)

                if (!isValid) {
                    mostrarTooltipSimple("Por favor, complete todos los campos obligatorios.", $('#msg_registro'));
                    return;
                }

                guardarDatos(); // Ejecutar si todo es válido
            }
        }
    });

    // Detalle
    $('#tbl_det_reservacion').on('click', '.btn-info', function (event) {
        event.preventDefault();
        const idReserva = $(this).data('id');

        consultarEventos('xPK', idReserva, '', '', '', function (data) {
            if(data[0].bitEstado_reser){
                $('#form_listReserva').modal('hide');

                // Espera a que termine de ocultarse el primero antes de abrir el segundo
                $('#form_listReserva').on('hidden.bs.modal', function () {
                    mostrarDetalle(idReserva);

                    $('#form_Detalle').modal('show');
                });
            }
            else{
                let mensage = "¡Este evento ya se encuentra cerrado!"
                let lblMsg = $('#txtMsgInfo');

                mostrarTooltipSimple(mensage, lblMsg);
            }
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

            if(!reserva.bitEstado_reser){
                let msg = '¡La edición no está permitida. El evento ha sido finalizado.';
                let lblMsg =  $('#txtMsgInfo');

                mostrarTooltipSimple(msg, lblMsg)
            }
            else if (fechaHoy > fechaRegistro) {
                let mensage = "¡Has superado las tres horas límite para eliminar la reservación!"
                let lblMsg = $('#txtMsgInfo');

                mostrarTooltipSimple(mensage, lblMsg);
            }
            else {
                $('#form_listReserva').modal('hide');

                $('#form_listReserva').on('hidden.bs.modal', function () {
                    eliminarReservacion('xCodReserva', idReserva, '', '', '', function (data) {
                        let mensaje = '';
                        let icon = '';

                        if (data.resultado) {
                            mensaje = data.msg;
                            icon = 'success';
                        }
                        else {
                            mensaje = data.msg;
                            icon = 'error';
                        }

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

    // Editar
    $('#tbl_det_reservacion').on('click', '.btn-success', function (event) {
        event.preventDefault();

        const idReserva = $(this).data('id');
        $('#lblCodReserva').text(idReserva)

        consultarEventos('xPK', idReserva, '', '', '', function (data) {
            var reserva = data[0];

            $('#txtFechaAct').val(reserva.dtFechainicio_reser.split('T')[0]);
            $('#txtHoraInicioAct').val(reserva.dtFechainicio_reser.split('T')[1]);
            $('#txtHoraFinAct').val(reserva.dtFechaFin_reser.split('T')[1]);
            $('#txtAsistentesAct').val(reserva.intTotalAsistente_reser);

            if (!reserva.bitTipo_reser) {
                var codAsignatura = reserva.strCod_Mate;
                var cedulaId = reserva.cedula_alu;
                var codUnidad = reserva.strCod_unidTem;

                $('#txtDescripcionAct').val(reserva.strDescripcion_reser);
                $('#txtMaterialesAct').val(reserva.strMateriales_reser);
                $('#selectPropositoAct').val(reserva.strProposito_reser);

                consultarAlumno('xCEDULA', cedulaId, '', '', '', function (data) {
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

                if (reserva.strProposito_reser === 'evento ocasional') {
                    $('#txtTemaAct').val(reserva.strTema_reser);

                    $('#content_txtTemaAct').css('display', 'block');
                    $('#content_ddlTemaAct').css('display', 'none');
                    $('#content_unidadAct').css('display', 'none');
                }
                else {

                    consultarUnidad('xAsignatura', codAsignatura, '', '', '', function (dataUni) {
                        if (dataUni.length > 0) {
                            const dropdown = $("#selectUnidadAct");
                            cargarUnidad(dataUni, dropdown);
                            $("#content_unidadAct").css("display", 'block');
                            $('#selectUnidadAct').val(reserva.strCod_unidTem);
                        }
                        else {
                            $("#content_ddlTemaAct").css("display", 'none');
                        }
                    });

                    consultarTema('xUnidad', codUnidad, '', '', '', function (data) {
                        if (data.length > 0) {
                            $("#content_ddlTemaAct").css("display", 'block');
                            const dropdown = $("#selectTemaAct");
                            cargarTema(data, dropdown);

                            $('#selectTemaAct').val(data[0].strCod_tema);
                        }
                        else {
                            $("#content_ddlTema").css("display", 'none');
                        }
                    });
                }

                $('#switchReservaAct').prop('disabled', true);
                $('#switchReservaAct').prop('checked', false);
                $('#reservaInternaAct').css('display', 'block');
                $('#reservaExternaAct').css('display', 'none');
            }
            else {
                consultarAlumno('xCEDULA', cedula, '', '', '', function (data) {
                    var nombre = data[0].apellido_alu + ' ' + data[0].apellidom_alu + ' ' + data[0].nombre_alu;

                    $('#txtEmailActExt').val(data[0].correo_alu);
                    $('#txtNombreActExt').val(nombre);
                });

                $('#selectTipoMotivoExt').val(reserva.strProposito_reser);
                $('#txtTemaExt').val(reserva.strTema_reser);
                $('#txtObservacionExt').val(reserva.strObs2_reser);
                $('#txtDescripcionExt').val(reserva.strDescripcion_reser);
                $('#txtMaterialExt').val(reserva.strMateriales_reser);

                $('#switchReservaAct').prop('checked', true);
                $('#reservaInternaAct').css('display', 'none');
                $('#reservaExternaAct').css('display', 'block');
            }


            let fechaHoy = new Date();
            let fechaInicio = reserva.dtFechaRegistro_reser;

            if(!reserva.bitEstado_reser){
                let msg = '¡La edición no está permitida. El evento ha sido finalizado.';
                let lblMsg =  $('#txtMsgInfo');

                mostrarTooltipSimple(msg, lblMsg)
            }
            else if (fechaHoy > fechaInicio) {
                // Mostrar el mensaje
                let msg = '¡No es posible realizar cambios en la reservación porque el evento ya está en curso!';
                let lblMsg =  $('#txtMsgInfo');

                mostrarTooltipSimple(msg, lblMsg)
            }
            else {
                $('#form_listReserva').modal('hide');

                $('#form_listReserva').on('hidden.bs.modal', function () {
                    $('#form_actualizar').modal('show');
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
        reservacion[6] = '#a4e4af';

        if(!$("#switchReservaAct").is(":checked")){
            reservacion[1] = $('#selectUnidadAct').is(':visible') ? $('#selectUnidadAct').val() : '';
            reservacion[2] = $('#selectTemaAct').is(':visible') ? $('#selectTemaAct').val() : $('#selectUnidadAct').val();

            if ($('#txtTemaAct').is(':visible')) {
                reservacion[2] = $('#txtTemaAct').val();
            }

            reservacion[3] = $('#selectPropositoAct').val();
            reservacion[4] = $('#txtDescripcionAct').val();
            reservacion[5] = $('#txtMaterialesAct').val();
            reservacion[7] = 'Reservación interna';
            reservacion[8] = '';
            reservacion[9] = true;

            consultarEventos('xPK', idReserva, '', '', '', function (reserva) {
                let fechaHoy = new Date();
                let fechaConvertida = reserva.dtFechaRegistro_reser;
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
        }
        else{
            reservacion[1] = '';
            reservacion[2] = $('#txtTemaExt').val();
            reservacion[3] = $('#selectTipoMotivoExt').val();
            reservacion[4] = $('#txtDescripcionExt').val();
            reservacion[5] = $('#txtMaterialExt').val();
            reservacion[7] = 'Reservación externa';
            reservacion[8] = $('#txtObservacionExt').val();
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
        }
    });

    //Control de uso 
    $('#tbl_det_reservacion').on('click', '.btn-active', function (event) {
        event.preventDefault();

        const idReserva = $(this).data('id');

        $('#codReserva').val(idReserva);

        consultarEventos('xPK', idReserva, '', '', '', function (data) {
            var reserva = data[0];

            let fecha = reserva.dtFechainicio_reser.split('T')[0];
            let inicio = reserva.dtFechainicio_reser.split('T')[1];
            let fin = reserva.dtFechaFin_reser.split('T')[1];

            let $contenedor = $('#listaHoras');
            $contenedor.empty(); // Limpiar contenido anterior

            // Parsear horas
            const [hIni, mIni] = inicio.split(':').map(Number);
            const [hFin, mFin] = fin.split(':').map(Number);

            let horaActual = new Date();
            horaActual.setHours(hIni, 0, 0, 0); // Inicia en X:00

            let horaFin = new Date();
            horaFin.setHours(hFin, mFin, 0, 0); // Hora final

            while (horaActual <= horaFin) {
                $('#fechaReg').val(fecha)
                let inicioBloque = new Date(horaActual);
                let finBloque = new Date(horaActual);
                finBloque.setMinutes(59);

                // Formato HH:MM
                const formato = (hora) => hora.toTimeString().substring(0, 5);
                const bloque = `${formato(inicioBloque)}`;

                // Crear HTML del checkbox con jQuery
                let $div = $('<div class="form-check"></div>');
                let $checkbox = $(`
                <input class="form-check-input hora-check" style="margin-left: 0px;" type="checkbox" value="${bloque}" id="${bloque.replace(/[^0-9]/g, '')}">
                `);
                let $label = $(`
                <label class="form-check-label" for="${bloque.replace(/[^0-9]/g, '')}">
                    ${bloque}
                </label>
                `);

                $div.append($checkbox).append($label);
                $contenedor.append($div);

                // Avanza a la siguiente hora
                horaActual.setHours(horaActual.getHours() + 1);
                horaActual.setMinutes(0);
            }

            if(!reserva.bitEstado_reser){
                let msg = '¡El control de uso no está permitida. El evento ha sido finalizado.';
                let lblMsg =  $('#txtMsgInfo');

                mostrarTooltipSimple(msg, lblMsg)
            }else{
                $('#controlUso').modal('show');
            }


        }, function (error) {
            // En caso de error
            console.error("Error consultando eventos", error);
            failureCallback(error);
        });

    });

    $('#regUso').click(function (){
        var uso = []
        var codResv = $('#codReserva').val();

        if($('#selectAsistencia').val() === '1'){
            $('.hora-check').each(function () {
                uso[0] = codResv;
                uso[1] = $('#observacion_general').val();
                uso[2] = $('#fechaReg').val() + ' ' + $(this).val()
                uso[3] = $(this).is(':checked'); // true o false
                uso[4] = $('#codReserva').val() + '_' + $(this).val().split(':')[0];
                
                guardarUso(uso, function(data){
                    let mensaje = data.msg;
                    let icon = data.resultado ? 'success' : 'error' ;

                    $('#form_listReserva').modal('hide');
                    $('#controlUso').modal('hide');

                    if(data.resultado){
                        acatualizarReservaUso(data.strcod_reser);
                    }

                    mostrarMensageCRUD(mensaje, icon)
                });
            });
        }else{
            $('.hora-check').each(function () {
                uso[0] = $('#codReserva').val();
                uso[1] = $('#observacion_general').val();
                uso[2] = $('#fechaReg').val() + ' ' + $(this).val()
                uso[3] = false;
                uso[4] = $('#codReserva').val() + '_' + $(this).val().split(':')[0];
                
                guardarUso(uso, function(data){
                    let mensaje = data.msg;
                    let icon = data.resultado ? 'success' : 'error' ;

                    $('#form_listReserva').modal('hide');
                    $('#controlUso').modal('hide');

                    if(data.resultado){
                        acatualizarReservaUso(data.strcod_reser);
                    }

                    mostrarMensageCRUD(mensaje, icon)
                });
            });
        }
    })
});

function validarReservacion(fechaHoy) {
    const nuevaInicio = new Date(fechaHoy + 'T' + $('#selectHoraInicio').val() + ':00');
    const nuevaFin = new Date(fechaHoy + 'T' + $('#selectHoraFin').val() + ':00');

    const horaInicioMenosTres = new Date(nuevaInicio.getTime() - 3 * 60 * 60 * 1000);
    const ahora = new Date();

    const lblMsg = $('#tooltipErrorInt');
    const checkbox = document.getElementById("switchReserva");

    const esExterna = checkbox.checked;

    const horaLimite = horaInicioMenosTres;

    consultarEventos('xFecha', codLab, fechaHoy, '', '', function (data) {
        let hayConflicto = false;

        data.forEach(reser => {
            let resInicio = new Date(reser.dtFechainicio_reser);
            let resFin = new Date(reser.dtFechaFin_reser);

            if (
                (nuevaInicio >= resInicio && nuevaInicio < resFin) ||
                (nuevaFin > resInicio && nuevaFin <= resFin) ||
                (nuevaInicio <= resInicio && nuevaFin >= resFin)
            ) {
                hayConflicto = true;
            }
        });

        if (hayConflicto) {
            mostrarTooltipSimple('Ya existe una reservación en esta hora', lblMsg);
            $('#btnEnviar').attr('disabled', 'disabled');
            return;
        }

        if (ahora > horaLimite && ahora < nuevaFin) {
            mostrarTooltipSimple('Solo puede reservar hasta 3 horas antes del inicio de clase.', lblMsg);
            $('#btnEnviar').attr('disabled', 'disabled');
            return;
        }

        if (ahora >= nuevaFin) {
            mostrarTooltipSimple('No es posible reservar un laboratorio después de que la clase ha finalizado.', lblMsg);
            $('#btnEnviar').attr('disabled', 'disabled');
            return;
        }

        // Si todo está bien
        $('#btnEnviar').removeAttr('disabled');

        consultarUnidad('xAsignatura', selectMateria, '', '', '', function (dataAsig) {
            const dropdown = $("#selectUnidadInt");
            cargarUnidad(dataAsig, dropdown);
        });

        if (esExterna) {
            $('#det_reservacionExt').css('display', 'block');
        } else {
            $('#det_reservacionInt').css('display', 'block');
        }
    });
}

function guardarDatos() {
    let reservacion = [];

    const esExterna = $('#switchReserva').is(':checked');

    // DATOS COMUNES
    const fecha = $('#txtFecha').val();
    const hora_inicio = $('#selectHoraInicio').val();
    const hora_fin = $('#selectHoraFin').val();

    const color = '#a4e4af';

    if (!esExterna) {
        // === RESERVA INTERNA ===
        let unidad = $('#selectUnidadInt');
        let proposito = $('#selectPropositoInt').val();

        reservacion[0] = $('#selectAsignatura').val(); // asignatura
        reservacion[1] = unidad.is(':visible') ? unidad.val() : '';
        reservacion[2] = $('#selectTemaInt').is(':visible') && proposito !== 'evento ocasional'
            ? $('#selectTemaInt').val()
            : unidad.val();

        if ($('#txtTemaInt').is(':visible')) {
            reservacion[2] = $('#txtTemaInt').val();
        }

        reservacion[3] = $('#txtDescripcionInt').val(); // descripción
        reservacion[4] = $('#txtMaterialInt').val();    // materiales
        reservacion[5] = fecha + ' ' + hora_inicio; // inicio
        reservacion[6] = fecha + ' ' + hora_fin;    // fin
        reservacion[7] = $('#txtNumeroAsistentes').val();            // asistentes
        reservacion[8] = $('#selectDocentes').val();                 // docente
        reservacion[9] = color;
        reservacion[10] = proposito;
        reservacion[11] = codLab;
        reservacion[12] = false;
        reservacion[13] = 'Reservación interna';
        reservacion[14] = '';
        reservacion[15] = codReservacion(fecha, hora_inicio, hora_fin);
    } else {
        // === RESERVA EXTERNA ===
        let propositoExt = $('#selectTipoMotivo').val();

        reservacion[0] = 'NA';
        reservacion[1] = 'NA';
        reservacion[2] = $('#txtTema').val();;
        reservacion[3] = $('#txtDescripcion').val();    // descripción
        reservacion[4] = $('#txtMaterial').val();       // materiales
        reservacion[5] = fecha + ' ' + hora_inicio; // inicio
        reservacion[6] = fecha + ' ' + hora_fin;    // fin
        reservacion[7] = 0;  // sin asistentes internos
        reservacion[8] = cedula;
        reservacion[9] = color;
        reservacion[10] = propositoExt;
        reservacion[11] = codLab;
        reservacion[12] = true;
        reservacion[13] = 'Reservación externa';
        reservacion[14] = $('#txtObservacion').val();
        reservacion[15] = codReservacion(fecha, hora_inicio, hora_fin);
    }

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

function mostrarListado(fecha) {
    consultarEventos('xFecha', codLab, fecha, '', '', function (data) {
        const tbody = $('#tbl_det_reservacion');
        tbody.empty(); // Limpiar contenido anterior                    

        if (data.length > 0) {
            // Iterar sobre los datos recibidos
            $.each(data, function (i, item) {
                const tr = $('<tr></tr>');

                tr.append(`<td>${item.strTema_reser}</td>`);
                tr.append(`<td>${convertirHora(item.dtFechainicio_reser)}</td>`);
                tr.append(`<td>${convertirHora(item.dtFechaFin_reser)}</td>`);
                tr.append(`<td><div style="width:20px; height:20px; background-color:${item.strColor_reser}; border-radius:3px;"></div></td>`);

                // Botones de acción
                const btnDetalle = `<button class="btn btn-info" data-id="${item.strCod_reser}"><i class="fa fa-info-circle" aria-hidden="true"></i></button>`;
                const btnActualizar = `<button class="btn btn-success" data-id="${item.strCod_reser}"><i class="fa fa-upload" aria-hidden="true"></i></button>`;
                const btnEliminar = `<button class="btn btn-danger" data-id="${item.strCod_reser}"><i class="fa fa-trash" aria-hidden="true"></i></button>`;
                const btnUso = `<button class="btn btn-active" data-id="${item.strCod_reser}"><i class="fa fa-clock-o" aria-hidden="true"></i></button>`;

                consultarAlumno('xCEDULA', item.cedula_alu, '', '', '', function (data) {
                    var nombre = data[0].apellido_alu + ' ' + data[0].apellidom_alu + ' ' + data[0].nombre_alu;

                    tr.append(`<td>${nombre}</td>`);
                    tr.append(`<td>${btnDetalle} ${btnActualizar} ${btnEliminar} ${btnUso}</td>`);
                });
                tbody.append(tr);
            });
        }
        else {
            const tr = $('<tr></tr>');

            tr.append(`<td colspan="6" class='text-center'>No sé a encontrado reservaciones para el día de hoy</td>`);
            tbody.append(tr);
        }
    }, function (error) {
        // En caso de error
        console.error("Error consultando eventos", error);
    });
}

function mostrarDetalle(idReserva) {
    consultarEventos('xPK', idReserva, '', '', '', function (data) {
        var reserva = data[0];
        var cedula = reserva.cedula_alu;

        consultarAlumno('xCEDULA', cedula, '', '', '', function (data) {
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

            consultarAsignatura('xPK', codAsignatura, '', '', '', function (data) {
                $('#txtAsigDet').val(data[0].strNombre_mate);
            });

            consultarCiclo('xAsignatura', codAsignatura, '', '', '', function (data) {
                $('#txtCicloDet').val(data[0].strnombre_curso);
                $('#txtParaleloDet').val(data[0].strparalelo_curso);
            });

            consultarCarrera('xAsignatura', codAsignatura, '', '', '', function (data) {
                $('#txtCarreraDet').val(data[0].strnombre_car);
            });

            consultarSoftware('xCodReserva', idReserva, '', '', '', function (data) {
                // Llenar el select con datos
                let selectSoftware = $('#ddlSoftwareDet');
                cargarSoftware(data, selectSoftware);
            });

            if(codUnidad !== ''){
                consultarUnidad('xPK', codUnidad, '', '', '', function (data) {
                    $('#txtUnidadDet').val(data[0].strdesc_unidtem);
                });
            }

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

    }, function (error) {
        // En caso de error
        console.error("Error consultando eventos", error);
        failureCallback(error);
    });
}

function acatualizarReservaUso(codResv){
    var reservacion = [];

    consultarEventos('xPK', codResv, '', '', '', function (data) {
        let reserva = data[0];

        reservacion[0] = reserva.strCod_reser;
        reservacion[1] = reserva.strCod_unidTem;
        reservacion[2] = reserva.strTema_reser;
        reservacion[3] = reserva.strProposito_reser;
        reservacion[4] = reserva.strDescripcion_reser;
        reservacion[5] = reserva.strMateriales_reser;
        reservacion[6] = '#b2b2b2';
        reservacion[7] = reserva.strObs1_reser;
        reservacion[8] = reserva.strObs2_reser;
        reservacion[9] = false;

        actualizarReservacion(reservacion);

    }, function (error) {
        // En caso de error
        console.error("Error consultando eventos", error);
        failureCallback(error);
    });
}

function consultarInformacionParaReservacion(selectMateria) {
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
}