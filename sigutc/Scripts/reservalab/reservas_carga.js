function cargarCiclo(data, input1, input2) {
    input1.empty();
    input2.empty();

    // Crear opción por cada elemento de la lista
    input1.val(data[0].strnombre_curso);
    input2.val(data[0].strparalelo_curso);
}

function cargarCarrera(data, input1) {
    input1.empty();

    // Crear opción por cada elemento de la lista
    input1.val(data[0].strnombre_car);
}

function cargarNumeroEstudiante(data, input1) {
    input1.empty();

    // Crear opción por cada elemento de la lista
    let numeroEstudiantes = data.length;
    input1.val(numeroEstudiantes);
}

//Carga los horario de inicio de clases por cada hora durante un periodo de tiempo
function cargarHora(data, dropdown) {
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
function cargarHoraFin(op, data, dropdown) {
    dropdown.empty();

    //Carga la hora de acuerdo a como llega de la base de datos
    if (op === 1) {
        // Crear opción por cada elemento de la lista
        data.forEach(item => {
            const opcion = document.createElement("option");
            opcion.value = convertirHora(item.strHoraFin);          // valor que se enviará
            opcion.textContent = convertirHora(item.strHoraFin); // lo que se muestra al usuario
            dropdown.append(opcion);
        });
    }
        //Carga el codigo deacuerdo al item y/o hora de inicio seleccionado
    else if (op === 2) {
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

function cargarMaterias(data, dropdown) {
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

function cargarUnidad(data, dropdown) {
    dropdown.empty();

    // Crear opción por cada elemento de la lista
    data.forEach(item => {
        const opcion = document.createElement("option");
        opcion.value = item.strcod_unidtem;          // valor que se enviará
        opcion.textContent = item.strdesc_unidtem;   // lo que se muestra al usuario
        dropdown.append(opcion);
    });
}

function cargarTema(data, dropdown) {
    dropdown.empty();
    // Crear opción por cada elemento de la lista
    data.forEach(item => {
        const opcion = document.createElement("option");
        opcion.value = item.strCod_tema;          // valor que se enviará
        opcion.textContent = item.strDesc_tema;   // lo que se muestra al usuario
        dropdown.append(opcion);
    });
}

function cargarSoftware(data, selectSoftware) {
    selectSoftware.empty(); // Limpia opciones anteriores

    $.each(data, function (i, item) {
        var opcion = $('<option>', {
            value: item.strCod_sof,
            text: item.strNombre_sof
        });
        selectSoftware.append(opcion);
    });
}