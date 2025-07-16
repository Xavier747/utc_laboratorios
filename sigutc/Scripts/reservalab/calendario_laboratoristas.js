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
                const eventos = [];

                // Iterar sobre los datos recibidos
                $.each(data, function (i, item) {
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
            }, function (error) {
                // En caso de error
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
            let mensaje = '';
            let icon = '';

            now.setHours(0, 0, 0, 0);
            if (info.start < now) {
                mensaje = 'Las fechas pasadas no están disponibles para reservas.';
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
            selectMateria = $('#selectAsignatura option').first().val();

            cargarMaterias(data, dropdown);
            consultarInformacionParaReservacion(selectMateria);
        });

        $('#det_reservacionInt').css('display', 'none');
    })

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

                if($("#switchReserva").is(":checked")){
                    //Horas reservacion externa
                    inicioSelect = $('#selectHoraInicio');
                    finSelect = $('#selectHoraFin');
                }
                else {
                    //Horas reservacion interna
                    inicioSelect = $('#selectHoraInicioExt');
                    finSelect = $('#selectHoraFinExt');
                }


                llenarHoras(inicioSelect, finSelect);
                $('#form_registrar').modal('show');
            }
        });
    });

    $("#btnValidar").click(function () {
        var fechaHoy = $('#txtFecha').val(); // formato: YYYY-MM-DD

        validarReservacion(fechaHoy);
    });

    $("#btnEnviarInterno").click(function () {
        let isValid = true;

        // Validar si está visible el bloque principal
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

            // Validar tema (si está visible)
            if ($('#content_ddlTemaInt').is(':visible') && $('#selectTema').val() === "") {
                isValid = false;
                $('#selectTemaInt').addClass('is-invalid');
            } else {
                $('#selectTemaInt').removeClass('is-invalid');
            }

            if ($('#content_txtTemaInt').is(':visible') && $('#txtTema').val().trim() === "") {
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

            // Validar software si está habilitado
            if ($('#switchSoftwareInt').is(':checked')) {
                var valorSelect = $('#ddlSoftwareInt').val();

                if (!$('#switchEncontradoInt').is(':checked')) {
                    if ($('#txtMaterial').val().trim() === "") {
                        isValid = false;
                        $('#txtSoftwareInt').addClass('is-invalid');
                    } else {
                        $('#txtSoftwareInt').removeClass('is-invalid');
                    }
                }
                else {
                    if (valorSelect == null || valorSelect.length === 0) {
                        isValid = false;
                        $('#ddlSoftwareInt').addClass('is-invalid');
                    } else {
                        $('#ddlSoftwareInt').removeClass('is-invalid');
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
    $('#tbl_det_reservacion').on('click', '.btn-info', function (event) {
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
    $('#tbl_det_reservacion').on('click', '.btn-warning', function (event) {
        event.preventDefault();

        const idReserva = $(this).data('id');

        consultarEventos('xPK', idReserva, '', '', '', function (data) {
            var reserva = data[0];
            var codAsignatura = reserva.strCod_Mate;
            var cedula = reserva.cedula_alu;
            var codUnidad = reserva.strCod_unidTem;

            if (reserva.bitTipo_reser) {
                $('#switchReservaAct').prop('checked', true);

                $('det_reservacionIntAct').css('display', 'none');
                $('det_reservacionExtAct').css('display', 'block');
            }
            else {
                $('#switchReservaAct').prop('checked', false);

                $('det_reservacionIntAct').css('display', 'block');
                $('det_reservacionExtAct').css('display', 'none');
            }

            $('#txtFechaAct').val(reserva.dtFechainicio_reser.split('T')[0]);
            $('#txtHoraInicioAct').val(reserva.dtFechainicio_reser.split('T')[1]);
            $('#txtHoraFinAct').val(reserva.dtFechaFin_reser.split('T')[1]);
            $('#txtNumeroAsistentesAct').val(reserva.intTotalAsistente_reser);
            $('#txtDescripcionAct').val(reserva.strDescripcion_reser);
            $('#txtMaterialesAct').val(dreserva.strMateriales_reser);

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

            $('#txtTipoMotivoDet').val(data[0].strTipo_reser);

            consultarUnidad('xAsignatura', codAsignatura, '', '', '', function (dataUni) {
                const dropdown = $("#selectUnidadAct");
                cargarUnidad(dataUni, dropdown);

                $('#selectUnidadAct').val(data[0].strCod_unidTem);
            });

            consultarTema('xUnidad', codUnidad, '', '', '', function (data) {
                if (data.length > 0) {
                    $("#content_ddlTema").css("display", 'block');
                    const dropdown = $("#selectTemaAct");
                    cargarTema(data, dropdown);

                    $('#selectTemaAct').val(data[0].strCod_tema);
                }
                else {
                    $("#content_ddlTema").css("display", 'none');
                }
            });

            let fechaHoy = new Date();
            let fechaConvertida = convertirFechaForFullCalendar(data[0].dtFechaRegistro_reser);
            let fechaRegistro = new Date(fechaConvertida);

            fechaRegistro.setHours(fechaRegistro.getHours() + 3);

            if (fechaHoy > fechaRegistro) {
                // Mostrar el mensaje
                $('#txtMsgInfo').text('¡Has superado las tres horas límite para la reservación!');
                $('#txtMsgInfo').fadeIn(); // Aparece con una animación

                // Ocultarlo después de 3 segundos (3000 milisegundos)
                setTimeout(() => {
                    $('#txtMsgInfo').fadeOut(); // Desaparece con animación
                }, 2000);
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

function validarReservacion(fechaHoy) {
    // Construye correctamente las fechas de inicio y fin como objetos Date
    const nuevaInicio = new Date(fechaHoy + 'T' + $('#selectHoraInicio').val() + ':00');
    const nuevaFin = new Date(fechaHoy + 'T' + $('#selectHoraFin').val() + ':00');
    var horaInicioMenosTres = new Date(nuevaInicio.getTime() - 3 * 60 * 60 * 1000);
    var ahora = new Date();
    var lblMsg = $('#tooltipErrorInt');


    consultarEventos('xFecha', codLab, fechaHoy, '', '', function (data) {
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

            consultarUnidad('xAsignatura', selectMateria, '', '', '', function (dataAsig) {
                console.log(dataAsig);

                const dropdown = $("#selectUnidadInt");
                cargarUnidad(dataAsig, dropdown);

                var selectUnidad = $('#selectUnidadInt option').first().val();
            });

            $('#det_reservacionInt').css('display', 'block');
        }
    });
}

function guardarDatos() {
    let reservacion = [];
    let unidad = $('#selectUnidadInt');
    let proposito = $('#selectPropositoInt').val()

    reservacion[0] = $('#selectAsignatura').val();
    reservacion[1] = unidad.is(':visible') ? unidad.val() : '';
    reservacion[2] = $('#selectTemaInt').is(':visible') && proposito !== 'evento ocasional' ? $('#selectTemaInt').val() : unidad.val();

    if ($('#txtTemaInt').is(':visible')) {
        reservacion[2] = $('#txtTemaInt').val();
    }

    reservacion[3] = $('#txtDescripcionInt').val();
    reservacion[4] = $('#txtMaterialInt').val();
    reservacion[5] = $('#txtFecha').val() + ' ' + $('#selectHoraInicio').val();
    reservacion[6] = $('#txtFecha').val() + ' ' + $('#selectHoraFin').val();
    reservacion[7] = $('#txtNumeroAsistentes').val();
    reservacion[8] = $('#selectDocentes').val();
    reservacion[9] = '#a4e4af';
    reservacion[10] = proposito;
    reservacion[11] = codLab;
    reservacion[12] = false;

    guardarReservacion(reservacion);
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
        var codAsignatura = reserva.strCod_Mate;
        var cedula = data[0].cedula_alu;
        var codUnidad = data[0].strCod_unidTem;

        $('#txtFechaDet').val(data[0].dtFechainicio_reser.split('T')[0]);
        $('#txtHoraInicioDet').val(data[0].dtFechainicio_reser.split('T')[1]);
        $('#txtHoraFinDet').val(data[0].dtFechaFin_reser.split('T')[1]);
        $('#txtAsistentes').val(data[0].intTotalAsistente_reser);
        $('#txtTemaDet').val(data[0].strTema_reser);
        $('#txtDescDet').val(data[0].strDescripcion_reser);
        $('#txtMaterialDet').val(data[0].strMateriales_reser);
        $('#txtTipoMotivoDet').val(data[0].strProposito_reser.toUpperCase());

        consultarAlumno('xCEDULA', cedula, '', '', '', function (data) {
            var nombre = data[0].apellido_alu + ' ' + data[0].apellidom_alu + ' ' + data[0].nombre_alu;

            $('#txtCorreoDet').val(data[0].correo_alu);
            $('#txtNombresDet').val(nombre);
        });

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

        consultarUnidad('xPK', codUnidad, '', '', '', function (data) {
            $('#txtUnidadDet').val(data[0].strdesc_unidtem);
        });

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