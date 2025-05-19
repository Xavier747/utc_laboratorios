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

        }

            )

        return "";
    }

}
