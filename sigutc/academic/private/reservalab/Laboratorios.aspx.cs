using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using ClassLibraryLaboratorios;
using ClassLibraryTesis;
using System.Web.Configuration;

public partial class academic_private_reservalab_Laboratorios : System.Web.UI.Page
{
    LAB_LABORATORIOS laboratorio2 = new LAB_LABORATORIOS();
    UB_FACULTADES facultad1 = new UB_FACULTADES();
    LAB_RESPONSABLE responsable1 = new LAB_RESPONSABLE();
    Personal personal1 = new Personal();
    AC_DISTRIBUTIVO distributivo1 = new AC_DISTRIBUTIVO();
    CURSO curso1 = new CURSO();
    SIG_PERIODOS periodoAcademico = new SIG_PERIODOS();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Context.User.Identity.Name == "") Response.Redirect("~/academic/public/Login.aspx");

        if (!IsPostBack)
        {
            cargarFacultad();
            cargarTabla();
        }
    }

    public void cargarFacultad()
    {
        string cedula = Context.User.Identity.Name;

        var listDistributivo = distributivo1.LoadAC_DISTRIBUTIVO("xCEDULA", cedula, "", "", "");
        var listCurso = curso1.Load_CURSO("ALL", "", "", "", "");
        var listPeriodo = periodoAcademico.LoadSIG_PERIODOS("ALL", "", "", "", "");

        var listaFacultad = (
            from d in listDistributivo
            join c in listCurso on d.strCod_curso equals c.strcod_curso
            join p in listPeriodo on c.strcod_per equals p.strCod_per
            select new
            {
                strCod_Fac = p.strCod_Fac,
                strCod_Sede = p.strCod_Sede
            }
        )
        .Distinct()
        .ToList();

        var listaFinal = listaFacultad
            .Select(item =>
            {
                // Obtener la facultad por su clave
                var facultad = facultad1.LoadUB_FACULTADES("xSedeFacultad", item.strCod_Sede, item.strCod_Fac, "", "").FirstOrDefault(); // en caso de que retorne lista

                // Devuelve datos anónimos si existe
                return facultad != null ? new
                {
                    strCod_Fac = item.strCod_Fac,
                    strCod_Sede = item.strCod_Sede,
                    strNombre_Fac = facultad.strnombre_fac
                } : null;
            })
            .Where(f => f != null) // eliminar nulos si alguna búsqueda falló
            .ToList();


        lblCodSede.Text = listaFinal[0].strCod_Sede;
        lblCodFacultad.Text = listaFinal[0].strCod_Fac;

        ddlFacultad.DataSource = listaFinal;
        rptFacultades.DataBind();
    }

    protected void rptFacultades_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "CargarLaboratorios")
        {
            lblCodFacultad.Text = Convert.ToString(e.CommandArgument);

            txtSearch.Text = "";
            cargarTabla();
        }
    }

    public void cargarTabla()
    {
        try
        {
            string codFac = lblCodFacultad.Text;
            string codSede = lblCodSede.Text;

            var listLaboratorios = laboratorio2.LoadLAB_LABORATORIOS("xSedeFacultad", codSede, codFac, "", "");
            var listResponsable = responsable1.LoadLAB_RESPONSABLE("ALL", "", "", "", "");
            var listPersonal = personal1.Load_PERSONAL("ALL", "", "", "", "");

            var data = listLaboratorios.Select(lab => new
            {
                lab.strCod_lab,
                lab.strNombre_lab,
                lab.strFotografia1_lab,
                ResponsableAcademico = (from resp in listResponsable
                                        join pers in listPersonal on resp.strCod_res equals pers.cedula_alu
                                        where resp.strCod_lab == lab.strCod_lab && resp.strTipo_respo == "Responsable Academico"
                                        select new
                                        {
                                            nombre = string.Concat(pers.apellido_alu, " ", pers.apellidom_alu, " ", pers.nombre_alu),
                                            FotoAcademico = pers.imagen_alu
                                        }).FirstOrDefault(),
                ResponsableAdministrativo = (from resp in listResponsable
                                        join pers in listPersonal on resp.strCod_res equals pers.cedula_alu
                                        where resp.strCod_lab == lab.strCod_lab && resp.strTipo_respo == "Responsable Administrativo"
                                        select new
                                        {
                                            nombre = string.Concat(pers.apellido_alu, " ", pers.apellidom_alu, " ", pers.nombre_alu),
                                            FotoAdministrativo = pers.imagen_alu
                                        }).FirstOrDefault()
            });

            listarLaboratorios.DataSource = data;
            listarLaboratorios.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: " + ex.Message);
        }
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        string filtro = txtSearch.Text.Trim();
        string strCodFac = lblCodFacultad.Text;

        cargarTablaFiltrada(strCodFac, filtro );
    }


    public void cargarTablaFiltrada(string strCodFac, string filtro)
    {
        if(filtro != "")
        {
            try
            {
                var listLaboratorios = laboratorio2.LoadLAB_LABORATORIOS("xFiltro", strCodFac, filtro, "", "");
                var listResponsable = responsable1.LoadLAB_RESPONSABLE("ALL", "", "", "", "");
                var listPersonal = personal1.Load_PERSONAL("ALL", "", "", "", "");

                var data = listLaboratorios.Select(lab => new
                {
                    lab.strCod_lab,
                    lab.strNombre_lab,
                    lab.strFotografia1_lab,

                    ResponsableAcademico = (from resp in listResponsable
                                            join pers in listPersonal on resp.strCod_res equals pers.cedula_alu
                                            where resp.strCod_lab == lab.strCod_lab && resp.strTipo_respo == "Responsable Academico"
                                            select new
                                            {
                                                nombre = string.Concat(pers.apellido_alu, " ", pers.apellidom_alu, " ", pers.nombre_alu),
                                                FotoAcademico = pers.imagen_alu
                                            }).FirstOrDefault(),
                    ResponsableAdministrativo = (from resp in listResponsable
                                                 join pers in listPersonal on resp.strCod_res equals pers.cedula_alu
                                                 where resp.strCod_lab == lab.strCod_lab && resp.strTipo_respo == "Responsable Administrativo"
                                                 select new
                                                 {
                                                     nombre = string.Concat(pers.apellido_alu, " ", pers.apellidom_alu, " ", pers.nombre_alu),
                                                     FotoAdministrativo = pers.imagen_alu
                                                 }).FirstOrDefault()
                });

                listarLaboratorios.DataSource = data;
                listarLaboratorios.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }
        else
        {
            cargarTabla();
        }
    }


    protected void listarLaboratorios_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string codLab = e.CommandArgument.ToString();

        if (e.CommandName == "Reservar")
        {
            SeguridadUTC sutc = new SeguridadUTC();
            Response.Redirect("~/academic/public/ReservaLaboratorioDocen.aspx?In= " + Server.UrlEncode(sutc.Encripta(codLab)));
        }
        else if (e.CommandName == "Informacion")
        {
            SeguridadUTC sutc = new SeguridadUTC();
            Response.Redirect("~/academic/private/reservalab/InformacionLaboratorios.aspx?In= " + Server.UrlEncode(sutc.Encripta(codLab)));
        }
    }
}