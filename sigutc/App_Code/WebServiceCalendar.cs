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
    LAB_RESERVA reserva1 = new LAB_RESERVA();
    LAB_RESERSOFTWARE reserSoft1 = new LAB_RESERSOFTWARE();


    public WebServiceCalendar()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string ObtenerAsignaturas(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        string cedula = Context.User.Identity.Name;
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
        List<LAB_SOFTWARE> listSoftware = software1.LoadLAB_SOFTWARE(comodin, filtro1, filtro2, filtro3, filtro4);

        var resultado = listSoftware.Select(software => new
        {
            strCod_sof = software.strCod_sof,
            strNombre_sof = software.strNombre_sof,
        });

        return JsonConvert.SerializeObject(resultado);
    }

    [WebMethod(EnableSession = true)]
    public string GuardarReserva(List<string> reservacion)
    {
        reserva1.strCod_reser = Session["laboratorioId"].ToString() + "_" + reservacion[5];
        reserva1.strCod_lab = Session["laboratorioId"].ToString();
        reserva1.strCod_Mate = reservacion[0];
        reserva1.cedula_alu = reservacion[8] != "" ? reservacion[8] : Context.User.Identity.Name;
        reserva1.strCod_unidTem = reservacion[1];
        reserva1.strTema_reser = reservacion[2] ?? string.Empty;
        reserva1.strTipo_reser = reservacion[10];
        reserva1.strDescripcion_reser = reservacion[3];
        reserva1.strMateriales_reser = reservacion[4];
        reserva1.dtFechainicio_reser = DateTime.Parse(reservacion[5]);
        reserva1.dtFechaFin_reser = DateTime.Parse(reservacion[6]);
        reserva1.intTotalAsistente_reser = int.Parse(reservacion[7]);
        reserva1.strColor_reser = reservacion[9];
        reserva1.dtFechaRegistro_reser = DateTime.Now;
        reserva1.bitEstado_reser = true;
        reserva1.dtFecha_log = DateTime.Today;
        reserva1.strUser_log = Context.User.Identity.Name;
        reserva1.strObs1_reser = string.Empty;
        reserva1.strObs2_reser = string.Empty;
        reserva1.bitObs1_reser = false;
        reserva1.bitObs2_reser = false;
        reserva1.decObs1_reser = -1;
        reserva1.decObs2_reser = -1;
        reserva1.dtObs1_reser = DateTime.Parse("1900-01-01");
        reserva1.dtObs2_reser = DateTime.Parse("1900-01-01");

        reserva1.AddLAB_RESERVA(reserva1);

        return JsonConvert.SerializeObject(reserva1);
    }

    [WebMethod]
    public string GuardarSofReserva(string codSoft, string codReser)
    {
        if (codSoft != "")
        {
            var listSoftware = software1.LoadLAB_SOFTWARE("xPK", codSoft, "", "", "");
            string codResof = codReser + "_" + listSoftware[0].strCod_sof ?? codReser + "_" + codSoft;

            reserSoft1.strCod_resof = codResof;
            reserSoft1.strCod_sof = listSoftware[0].strCod_sof ?? string.Empty;
            reserSoft1.strCod_Sede = listSoftware[0].strCod_Sede ?? string.Empty;
            reserSoft1.strCod_Fac = listSoftware[0].strCod_Fac ?? string.Empty;
            reserSoft1.strCod_reser = codReser;
            reserSoft1.strNombre_resof = listSoftware[0].strCod_sof ?? codSoft;
            reserSoft1.dtRegistro_resof = DateTime.Now;
            reserSoft1.dtFecha_log = DateTime.Now;
            reserSoft1.strUser_log = Context.User.Identity.Name;
            reserSoft1.strObs1_resof = string.Empty;
            reserSoft1.strObs2_resof = string.Empty;
            reserSoft1.bitObs1_resof = false;
            reserSoft1.bitObs2_resof = false;
            reserSoft1.decObs1_resof = -1;
            reserSoft1.decObs2_resof = -1;
            reserSoft1.dtObs1_resof = DateTime.Parse("1900-01-01");
            reserSoft1.dtObs2_resof = DateTime.Parse("1900-01-01");
            reserSoft1.AddLAB_RESERSOFTWARE(reserSoft1);

            return JsonConvert.SerializeObject(reserSoft1);
        }
        return string.Empty;
    }

    [WebMethod]
    public string ObtenerReservacion(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<LAB_RESERVA> listReservacion = reserva1.LoadLAB_RESERVA(comodin, filtro1, filtro2, filtro3, filtro4);

        var resultado = listReservacion.Select(reservacion => new
        {
            strCod_reser = reservacion.strCod_reser,
            strCod_lab = reservacion.strCod_lab,
            strCod_Mate = reservacion.strCod_Mate,
            cedula_alu = reservacion.cedula_alu,
            strCod_unidTem = reservacion.strCod_unidTem,
            strCod_tema = reservacion.strTema_reser ?? "",
            strTema_reser = ObtenerNombreTema(reservacion.strTema_reser) != "" ? ObtenerNombreTema(reservacion.strTema_reser) : reservacion.strTema_reser,
            strTipo_reser = reservacion.strTipo_reser,
            strDescripcion_reser = reservacion.strDescripcion_reser,
            strMateriales_reser = reservacion.strMateriales_reser,
            dtFechainicio_reser = reservacion.dtFechainicio_reser,
            dtFechaFin_reser = reservacion.dtFechaFin_reser,
            intTotalAsistente_reser = reservacion.intTotalAsistente_reser,
            strColor_reser = reservacion.strColor_reser,
            dtFechaRegistro_reser = reservacion.dtFechaRegistro_reser,
            bitEstado_reser = reservacion.bitEstado_reser,
            dtFecha_log = reservacion.dtFecha_log,
            strUser_log = reservacion.strUser_log,
            strObs1_reser = reservacion.strObs1_reser,
            strObs2_reser = reservacion.strObs2_reser,
            bitObs1_reser = reservacion.bitObs1_reser,
            bitObs2_reser = reservacion.bitObs2_reser,
            decObs1_reser = reservacion.decObs1_reser,
            decObs2_reser = reservacion.decObs2_reser,
            dtObs1_reser = reservacion.dtObs1_reser,
            dtObs2_reser = reservacion.dtObs2_reser,
        });

        return JsonConvert.SerializeObject(resultado);
    }

    [WebMethod]
    public string ObtenerSoftwareReserva(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<LAB_RESERSOFTWARE> listSoftReserva = reserSoft1.LoadLAB_RESERSOFTWARE(comodin, filtro1, filtro2, filtro3, filtro4);

        return "";
    }

    public string ObtenerNombreTema(string idTema)
    {
        List<TEMA> listTema = tema1.Load_TEMA("xPK", idTema, "", "", "");

        string tema = listTema.Count > 0 ? listTema[0].strDesc_tema : string.Empty;

        return  tema ;
    }
}
