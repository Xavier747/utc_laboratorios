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
    

    public WebServiceCalendar()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hola a todos";
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string ObtenerAsignaturas(string dia)
    {
        string cedula = Context.User.Identity.Name;
        List<MATERIAS> listMaterias = materia1.Load_MATERIAS("xDia", dia, cedula, "", "");

        var resultado = listMaterias.Select(mat => new {
            strCod_mate = mat.strcod_mate,
            strNombre_mate = mat.strnombre_mate
        });

        // Serializamos manualmente
        return JsonConvert.SerializeObject(resultado);
    }

    [WebMethod]
    public string ObtenerHorario(string asignaturaId, string dia)
    {
        List<SIG_HORAS> listHoras = horas1.Load_SG_HORAS("xCodMat", asignaturaId, dia, "", "");

        var resultado = listHoras.Select(horas => new
        {
            strCod_horas = horas.strCod_horas,
            strHoraInicio = horas.dtInicio_horas,
            strHoraFin = horas.dtFin_horas,
        });

        return JsonConvert.SerializeObject(resultado);
    }

    [WebMethod]
    public string ObtenerCiclo(string asignaturaId)
    {
        List<CURSO> listCurso = cuso1.Load_CURSO("xAsignatura", asignaturaId, "", "", "");

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
    public string ObtenerCarrera(string asignaturaId)
    {
        List<UB_CARRERAS> listCarrera = carrera1.LoadUB_CARRERAS("xAsignatura", asignaturaId, "", "", "");

        var resultado = listCarrera.Select(carrera => new
        {
            strnombre_car = carrera.strnombre_car
        });

        return JsonConvert.SerializeObject(resultado);
    }


}
