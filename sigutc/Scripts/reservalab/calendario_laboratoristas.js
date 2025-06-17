var dia = "";
var horaFin = "";
var selectMateria = '';
var listSoftware = [];

document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendarLab');

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
        validRange: function () {
            // Múltiples rangos permitidos
            const data = [
                {
                    fechaInicio: '2024-10-21',
                    fechaFin: '2025-02-24',
                },
                {
                    fechaInicio: '2025-04-07',
                    fechaFin: '2025-08-22',
                },
            ];

            // Calcular el rango general (mínimo y máximo de todo)
            const fechasInicio = data.map(r => new Date(r.fechaInicio));
            const fechasFin = data.map(r => new Date(r.fechaFin));

            const min = new Date(Math.min.apply(null, fechasInicio));
            const max = new Date(Math.max.apply(null, fechasFin));

            return {
                start: min.toISOString().split('T')[0],
                end: max.toISOString().split('T')[0]
            };
        },
        dayCellDidMount: function (info) {
            const fecha = info.date.toISOString().split('T')[0];
            const data = [
                {
                    fechaInicio: '2024-10-21',
                    fechaFin: '2025-02-24',
                },
                {
                    fechaInicio: '2025-04-07',
                    fechaFin: '2025-08-22',
                },
            ];

            // Verificar si la fecha está dentro de algún tramo válido
            const esValida = data.some(r => {
                return fecha >= r.fechaInicio && fecha <= r.fechaFin;
            });

            if (!esValida) {
                info.el.classList.add('fc-day-disabled');
            }
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
            var fechaCompleta = info.dateStr;
            var fecha = fechaCompleta.substring(0, 10);
            $('#' + txtFecha).text(fecha);
            $('#fecha').text(fecha);

            dia = obtenerDiaSemana(fecha);

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
            else if (dayOfWeek === 0 || dayOfWeek === 6) {
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
            const data = [
                {
                    fechaInicio: '2024-10-21',
                    fechaFin: '2025-02-24',
                },
                {
                    fechaInicio: '2025-04-07',
                    fechaFin: '2025-08-22',
                },
            ];

            const start = selectInfo.startStr;
            const end = selectInfo.endStr;

            return data.some(rango => {
                return start >= rango.fechaInicio && end <= rango.fechaFin;
            });
        },
    });

    calendar.render();
});


$(document).ready(function () {
    $("#selectAsignatura").on('change', function () {
        var asignaturaId = this.value; // Capturar el valor seleccionado
        consultarHorario('xCodMat', asignaturaId, dia, '', '', function (data) {
            const dropdown = $("#selectHoraInicio");
            cargarHora(data, dropdown);

            selectMateria = $('#selectAsignatura').val();

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

        $('#det_reservacion').css('display', 'none');
    });

    $("#selectUnidad").on('change', function () {
        var unidadId = this.value; // Capturar el valor seleccionado
        consultarTema('xUnidad', unidadId, '', '', '', function (data) {

            if (data.length > 0) {
                $("#content_ddlTema").css("display", 'block');
                const dropdown = $("#selectTema");
                cargarTema(data, dropdown);
            }
            else {
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
            consultarSoftware('xLaboratorio', codLab, '', '', '', function (data) {
                // Llenar el select con datos
                cargarSoftware(data, selectSoftware);

                // Re-inicializar el plugin con el nuevo select
                new MultiSelectTag('countries', {
                    rounded: true,    // default true
                    shadow: true,      // default false
                    placeholder: 'Search',  // default Search...
                    onChange: function (values) {
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
            else if (fechaHoy === fechaSelect) {
                $('#form_registrar').modal('show');
            }
            else {
                $('#form_registrar').modal('show');
            }
        });
    });

    $("#btnValidar").click(function () {
        var fechaHoy = $('#txtFecha').val(); // formato: YYYY-MM-DD

        validarReservacion(fechaHoy);
    });

    $("#btnEnviar").click(function () {
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
        reservacion[10] = $('#selectTipoMotivo').val();

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
                var codReser = data.strCod_reser;

                guardarSoftware(codReser, function (data) {
                    console.log(data[0].msg)
                });

                $('#form_registrar').modal('hide');

                mostrarMensageCRUD(mensaje, icon);
            },
            error: function (xhr, status, error) {
                console.log("Status: " + xhr.status);
                console.log("Response: " + xhr.responseText);
            }
        });
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
            var codAsignatura = data[0].strCod_Mate;
            var cedula = data[0].cedula_alu;
            var codUnidad = data[0].strCod_unidTem;


            $('#txtFechaAct').val(data[0].dtFechainicio_reser.split('T')[0]);
            $('#txtHoraInicioAct').val(data[0].dtFechainicio_reser.split('T')[1]);
            $('#txtHoraFinAct').val(data[0].dtFechaFin_reser.split('T')[1]);
            $('#txtNumeroAsistentesAct').val(data[0].intTotalAsistente_reser);
            $('#txtDescripcionAct').val(data[0].strDescripcion_reser);
            $('#txtMaterialesAct').val(data[0].strMateriales_reser);

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
            mostrarTooltipSimple();
        } else {
            consultarUnidad('xAsignatura', selectMateria, '', '', '', function (data) {
                const dropdown = $("#selectUnidad");
                cargarUnidad(data, dropdown);
                var selectUnidad = $('#selectUnidad option').first().val();
            });

            $('#det_reservacion').css('display', 'block');
        }
    });
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
                const btnDetalle = `<button class="btn btn-info" data-id="${item.strCod_reser}">Detalle</button>`;
                const btnActualizar = `<button class="btn btn-warning" data-id="${item.strCod_reser}">Actualizar</button>`;
                const btnEliminar = `<button class="btn btn-danger" data-id="${item.strCod_reser}">Eliminar</button>`;

                consultarAlumno('xCEDULA', item.cedula_alu, '', '', '', function (data) {
                    var nombre = data[0].apellido_alu + ' ' + data[0].apellidom_alu + ' ' + data[0].nombre_alu;

                    tr.append(`<td>${nombre}</td>`);
                    tr.append(`<td>${btnDetalle} ${btnActualizar} ${btnEliminar}</td>`);
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
        $('#txtTipoMotivoDet').val(data[0].strTipo_reser.toUpperCase());

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