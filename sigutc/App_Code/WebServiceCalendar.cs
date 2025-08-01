using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using ClassLibraryLaboratorios;
using ClassLibraryTesis;
using Newtonsoft.Json;

/// <summary>
/// Descripción breve de WebServiceCalendar
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
[ScriptService]
public class WebServiceCalendar : System.Web.Services.WebService
{
    Personal personal1 = new Personal();
    MATERIAS materia1 = new MATERIAS();
    SIG_HORAS horas1 = new SIG_HORAS();
    CURSO cuso1 = new CURSO();
    UB_CARRERAS carrera1 = new UB_CARRERAS();
    UNIDAD_TEMA unidad1 = new UNIDAD_TEMA();
    TEMA tema1 = new TEMA();
    LAB_SOFTWARE software1 = new LAB_SOFTWARE();
    LAB_LABORATORIOS laboratorio1 = new LAB_LABORATORIOS();
    LAB_RESERVAC reservac1 = new LAB_RESERVAC();
    LAB_RESERVAD reservad1 = new LAB_RESERVAD();
    LAB_EXCLUSIVO labEx1 = new LAB_EXCLUSIVO();
    LAB_USO uso1 = new LAB_USO();
    SIG_PERIODOS periodo1 = new SIG_PERIODOS();


    public WebServiceCalendar()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string ObtenerAsignaturas(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<MATERIAS> listMaterias = materia1.Load_MATERIAS(comodin, filtro1, filtro2, filtro3, filtro4);

        var resultado = listMaterias.Select(mat => new {
            strCod_mate = mat.strcod_mate,
            strNombre_mate = mat.strnombre_mate
        });

        // Serializamos manualmente
        return JsonConvert.SerializeObject(resultado);
    }

    [WebMethod]
    public string ObtenerHorario(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<SIG_HORAS> listHoras = horas1.Load_SG_HORAS(comodin, filtro1, filtro2, filtro3, filtro4);

        var resultado = listHoras.Select(horas => new
        {
            strCod_horas = horas.strCod_horas,
            strHoraInicio = horas.dtInicio_horas,
            strHoraFin = horas.dtFin_horas,
        });

        return JsonConvert.SerializeObject(resultado);
    }

    [WebMethod]
    public string ObtenerCiclo(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<CURSO> listCurso = cuso1.Load_CURSO(comodin, filtro1, filtro2, filtro3, filtro4);

        var resultado = listCurso.Select(ciclo => new
        {
            strcod_curso = ciclo.strcod_curso,
            strcod_per = ciclo.strcod_per,
            strnombre_curso = ciclo.strnombre_curso,
            strparalelo_curso = ciclo.strparalelo_curso
        });

        return JsonConvert.SerializeObject(resultado);
    }

    [WebMethod]
    public string ObtenerCarrera(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<UB_CARRERAS> listCarrera = carrera1.LoadUB_CARRERAS(comodin, filtro1, filtro2, filtro3, filtro4);

        var resultado = listCarrera.Select(carrera => new
        {
            strnombre_car = carrera.strnombre_car
        });

        return JsonConvert.SerializeObject(resultado);
    }

    [WebMethod]
    public string ObtenerEstudiantes(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<Personal> listPersonal = personal1.Load_PERSONAL(comodin, filtro1, filtro2, filtro3, filtro4);

        var resultado = listPersonal.Select(personal => new
        {
            cedula_alu = personal.cedula_alu,
            apellido_alu = personal.apellido_alu,
            apellidom_alu = personal.apellidom_alu,
            nombre_alu = personal.nombre_alu,
            correo_alu = personal.correo_alu
        });

        return JsonConvert.SerializeObject(resultado);
    }

    [WebMethod]
    public string ObtenerUnidad(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<UNIDAD_TEMA> listUnidad = unidad1.Load_UNIDAD_TEMA(comodin, filtro1, filtro2, filtro3, filtro4);

        var resultado = listUnidad.Select(unidad => new
        {
            strcod_unidtem = unidad.strcod_unidtem,
            strcod_silaboc = unidad.strcod_silaboc,
            strnum_unidtem = unidad.strnum_unidtem,
            strdesc_unidtem = unidad.strdesc_unidtem
        });

        return JsonConvert.SerializeObject(resultado);
    }

    [WebMethod]
    public string ObtenerTema(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<TEMA> listTema = tema1.Load_TEMA(comodin, filtro1, filtro2, filtro3, filtro4);

        var resultado = listTema.Select(tema => new
        {
            strCod_tema = tema.strCod_tema,
            strCod_unidTem = tema.strCod_unidTem,
            strDesc_tema = tema.strDesc_tema,
            strNum_tema = tema.strNum_tema
        });

        return JsonConvert.SerializeObject(resultado);
    }

    [WebMethod]
    public string ObtenerSoftware(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        object resultado = null; // ← Declaración sin inicializar

        List<LAB_SOFTWARE> listSoftware = software1.LoadLAB_SOFTWARE(comodin, filtro1, filtro2, filtro3, filtro4);

        if (listSoftware.Count > 0)
        {
            resultado = listSoftware.Select(software => new
            {
                strCod_sof = software.strCod_sof,
                strNombre_sof = software.strNombre_sof,
            });
        }
        else
        {
            List<LAB_RESERVAD> listReserSoftware = reservad1.LoadLAB_RESERVAD(comodin, filtro1, filtro2, filtro3, filtro4);

            resultado = listReserSoftware.Select(software => new
            {
                strCod_sof = software.strCod_sof,
                strNombre_sof = software.strNombre_reserd,
            });
        }

        return JsonConvert.SerializeObject(resultado);
    }

    [WebMethod(EnableSession = true)]
    public string GuardarReserva(List<string> reservacion)
    {
        reservac1.strCod_reserc = reservacion[15];
        reservac1.strCod_lab = reservacion[11].ToString();
        reservac1.strCod_Mate = reservacion[0];
        reservac1.cedula_alu = reservacion[8] != "" ? reservacion[8] : Context.User.Identity.Name;
        reservac1.strCod_unidTem = reservacion[1] ?? "";
        reservac1.strTema_reserc = reservacion[2];
        reservac1.strProposito_reserc = reservacion[10];
        reservac1.bitTipo_reserc = bool.Parse(reservacion[12]);
        reservac1.strDescripcion_reserc = reservacion[3];
        reservac1.strMateriales_reserc = reservacion[4];
        reservac1.dtFechainicio_reserc = DateTime.Parse(reservacion[5]);
        reservac1.dtFechaFin_reserc = DateTime.Parse(reservacion[6]);
        reservac1.intTotalAsistente_reserc = int.Parse(reservacion[7]);
        reservac1.strColor_reserc = reservacion[9];
        reservac1.dtFechaRegistro_reserc = DateTime.Now;
        reservac1.bitEstado_reserc = true;
        reservac1.dtFecha_log = DateTime.Now;
        reservac1.strUser_log = Context.User.Identity.Name;
        reservac1.strObs1_reserc = reservacion[13];
        reservac1.strObs2_reserc = reservacion[14];
        reservac1.bitObs1_reserc = false;
        reservac1.bitObs2_reserc = false;
        reservac1.decObs1_reserc = -1;
        reservac1.decObs2_reserc = -1;
        reservac1.dtObs1_reserc = DateTime.Parse("1900-01-01");
        reservac1.dtObs2_reserc = DateTime.Parse("1900-01-01");

        reservac1.AddLAB_RESERVAC(reservac1);

        return JsonConvert.SerializeObject(reservac1);
    }

    [WebMethod]
    public string GuardarSofReserva(string codSoft, string codReser)
    {
        if (codSoft != "")
        {
            var listSoftware = software1.LoadLAB_SOFTWARE("xPK", codSoft, "", "", "");
            string codResof = listSoftware.Count > 0 ? codReser + "_" + listSoftware[0].strCod_sof : codReser + "_" + codSoft;

            reservad1.strCod_reserd = codResof;
            reservad1.strCod_sof = listSoftware.Count > 0 ? listSoftware[0].strCod_sof : string.Empty;
            reservad1.strCod_Sede = listSoftware.Count > 0 ? listSoftware[0].strCod_Sede : string.Empty;
            reservad1.strCod_Fac = listSoftware.Count > 0 ? listSoftware[0].strCod_Fac : string.Empty;
            reservad1.strCod_reserc = codReser;
            reservad1.strNombre_reserd = listSoftware.Count > 0 ? string.Empty : codSoft;
            reservad1.dtRegistro_reserd = DateTime.Now;
            reservad1.dtFecha_log = DateTime.Now;
            reservad1.strUser_log = Context.User.Identity.Name;
            reservad1.strObs1_reserd = string.Empty;
            reservad1.strObs2_reserd = string.Empty;
            reservad1.bitObs1_reserd = false;
            reservad1.bitObs2_reserd = false;
            reservad1.decObs1_reserd = -1;
            reservad1.decObs2_reserd = -1;
            reservad1.dtObs1_reserd = DateTime.Parse("1900-01-01");
            reservad1.dtObs2_reserd = DateTime.Parse("1900-01-01");
            reservad1.AddLAB_RESERVAD(reservad1);

            return JsonConvert.SerializeObject(reservad1);
        }
        return string.Empty;
    }

    [WebMethod]
    public string ObtenerReservacion(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<LAB_RESERVAC> listReservacion = reservac1.LoadLAB_RESERVAC(comodin, filtro1, filtro2, filtro3, filtro4);

        var resultado = listReservacion.Select(reservacion => new
        {
            strCod_reser = reservacion.strCod_reserc,
            strCod_lab = reservacion.strCod_lab,
            strCod_Mate = reservacion.strCod_Mate,
            cedula_alu = reservacion.cedula_alu,
            strCod_unidTem = reservacion.strCod_unidTem,
            strCod_tema = reservacion.strTema_reserc ?? "",
            strTema_reser = ObtenerNombreTema(reservacion.strTema_reserc) != "" ? ObtenerNombreTema(reservacion.strTema_reserc) : reservacion.strTema_reserc,
            strProposito_reser = reservacion.strProposito_reserc,
            bitTipo_reser = reservacion.bitTipo_reserc,
            strDescripcion_reser = reservacion.strDescripcion_reserc,
            strMateriales_reser = reservacion.strMateriales_reserc,
            dtFechainicio_reser = reservacion.dtFechainicio_reserc,
            dtFechaFin_reser = reservacion.dtFechaFin_reserc,
            intTotalAsistente_reser = reservacion.intTotalAsistente_reserc,
            strColor_reser = reservacion.strColor_reserc,
            dtFechaRegistro_reser = reservacion.dtFechaRegistro_reserc,
            bitEstado_reser = reservacion.bitEstado_reserc,
            dtFecha_log = reservacion.dtFecha_log,
            strUser_log = reservacion.strUser_log,
            strObs1_reser = reservacion.strObs1_reserc,
            strObs2_reser = reservacion.strObs2_reserc,
            bitObs1_reser = reservacion.bitObs1_reserc,
            bitObs2_reser = reservacion.bitObs2_reserc,
            decObs1_reser = reservacion.decObs1_reserc,
            decObs2_reser = reservacion.decObs2_reserc,
            dtObs1_reser = reservacion.dtObs1_reserc,
            dtObs2_reser = reservacion.dtObs2_reserc,
        });

        return JsonConvert.SerializeObject(resultado);
    }

    [WebMethod]
    public string ObtenerExclusivo(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<LAB_EXCLUSIVO> listExclusivo = labEx1.LoadLAB_EXCLUSIVO(comodin, filtro1, filtro2, filtro3, filtro4);

        var resultado = listExclusivo.Select(exlusivo => new
        {
            strCod_labEx = exlusivo.strCod_labEx,
            strCod_Sede = exlusivo.strCod_Sede,
            strCod_Fac = exlusivo.strCod_Fac,
            strCod_Car = exlusivo.strCod_Car,
            strCod_lab = exlusivo.strCod_lab,
            dtFechaRegistro_labEx = exlusivo.dtFechaRegistro_labEx,
            bitEstado_labEx = exlusivo.bitEstado_labEx,
            dtFecha_log = exlusivo.dtFecha_log,
            strUser_log = exlusivo.strUser_log,
            strObs1_labEx = exlusivo.strObs1_labEx,
            strObs2_labEx = exlusivo.strObs2_labEx,
            bitObs1_labEx = exlusivo.bitObs1_labEx,
            bitObs2_labEx = exlusivo.bitObs2_labEx,
            decObs1_labEx = exlusivo.decObs1_labEx,
            decObs2_labEx = exlusivo.decObs2_labEx,
            dtObs1_labEx = exlusivo.dtObs1_labEx,
            dtObs2_labEx = exlusivo.dtObs2_labEx
        });

        return JsonConvert.SerializeObject(resultado);
    }

    public string ObtenerNombreTema(string idTema)
    {
        List<TEMA> listTema = tema1.Load_TEMA("xPK", idTema, "", "", "");

        string tema = "";

        if (listTema.Count > 0)
        {
            tema = listTema[0].strDesc_tema;
        }
        else
        {
            List<UNIDAD_TEMA> listUnidad = unidad1.Load_UNIDAD_TEMA("xPK", idTema, "", "", "");

            tema = listUnidad.Count > 0 ? listUnidad[0].strdesc_unidtem : idTema;
        }

        return  tema;
    }

    [WebMethod]
    public string EliminarSoftwareReserva(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        reservad1.DelLAB_RESERVAD(comodin, filtro1, "", "", "");
        return JsonConvert.SerializeObject(reservad1);
    }

    [WebMethod]
    public string EliminarReserva(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        reservac1.DeleteLAB_RESERVAC(comodin, filtro1, "", "", "");
        return JsonConvert.SerializeObject(reservac1);
    }

    [WebMethod]
    public string ObtenerPeriodoAcademico(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<SIG_PERIODOS> listPerido = periodo1.LoadSIG_PERIODOS(comodin, filtro1, filtro2, filtro3, filtro4);

        var resultado = listPerido.Select(periodoAcad => new
        {
            strCod_per = periodoAcad.strCod_per,
            intNum_per = periodoAcad.intNum_per,
            intNumSemanas_per = periodoAcad.intNumSemanas_per,
            strCod_Sede = periodoAcad.strCod_Sede,
            strCod_Fac = periodoAcad.strCod_Fac,
            strCod_Car = periodoAcad.strCod_Car,
            dtFechaIni_per = periodoAcad.dtFechaIni_per,
            dtFechaFin_per = periodoAcad.dtFechaFin_per
        });

        return JsonConvert.SerializeObject(resultado);
    }

    [WebMethod]
    public string ActulizarReservacion(List<string> reservacion)
    {
        reservac1.strCod_reserc = reservacion[0];
        reservac1.strCod_unidTem = reservacion[1];
        reservac1.strTema_reserc = reservacion[2];
        reservac1.strProposito_reserc = reservacion[3];
        reservac1.strDescripcion_reserc = reservacion[4];
        reservac1.strMateriales_reserc = reservacion[5];
        reservac1.strColor_reserc = reservacion[6];
        reservac1.bitEstado_reserc = bool.Parse(reservacion[9]);
        reservac1.dtFecha_log = DateTime.Now;
        reservac1.strUser_log = Context.User.Identity.Name;
        reservac1.strObs1_reserc = reservacion[7];
        reservac1.strObs2_reserc = reservacion[8];

        reservac1.UpdateLAB_RESERVAC(reservac1);

        return JsonConvert.SerializeObject(reservac1);
    }

    [WebMethod]
    public string GuardarUso(List<string> uso)
    {
        uso1.strcod_uso = uso[4];
        uso1.strcod_reser = uso[0];
        uso1.dthorainicio_uso = DateTime.Parse(uso[2]);
        uso1.strobservacion_uso = uso[1];
        uso1.dtfecharegistro_uso = DateTime.Now;
        uso1.bitestado_uso = bool.Parse(uso[3]);
        uso1.dtfecha_log = DateTime.Now;
        uso1.struser_log = Context.User.Identity.Name;
        uso1.strobs1_uso = string.Empty;
        uso1.strobs2_uso = string.Empty;
        uso1.bitobs1_uso = false;
        uso1.bitobs2_uso = false;
        uso1.decobs1_uso = -1;
        uso1.decobs2_uso = -1;
        uso1.dtobs1_uso = DateTime.Parse("1900-01-01");
        uso1.dtobs2_uso = DateTime.Parse("1900-01-01");

        uso1.AddLAB_USO(uso1);

        return JsonConvert.SerializeObject(uso1);
    }
}
