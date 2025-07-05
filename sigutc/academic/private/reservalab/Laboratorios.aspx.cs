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
        var listFacultad = new List<UB_FACULTADES>();
        string cedula = Context.User.Identity.Name;

        if (Session["ROL"].ToString() == "DOCENTE")
        {
            listFacultad = facultad1.LoadUB_FACULTADES("xDocente", cedula, "", "", "");
        }
        else
        {
            listFacultad = facultad1.LoadUB_FACULTADES("xAlumno", cedula, "", "", "");
        }

        if (listFacultad.Count > 0)
        {
            lblCodFacultad.Text = listFacultad[0].strcod_fac;
            rptFacultades.DataSource = listFacultad;
            rptFacultades.DataBind();
        }
    }

    protected void rptFacultades_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "CargarLaboratorios")
        {
            lblCodFacultad.Text = Convert.ToString(e.CommandArgument);

            ViewState["FacultadSeleccionada"] = Convert.ToString(e.CommandArgument);

            txtSearch.Text = "";
            cargarTabla();
        }
    }

    public void cargarTabla()
    {
        try
        {
            string codFac = lblCodFacultad.Text;
            var listLaboratorios = laboratorio2.LoadLAB_LABORATORIOS("xFacultad", codFac, "", "", "");
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
                                            nombre = $"{pers.apellido_alu} {pers.apellidom_alu} {pers.nombre_alu}",
                                            FotoAcademico = pers.imagen_alu
                                        }).FirstOrDefault(),
                ResponsableAdministrativo = (from resp in listResponsable
                                        join pers in listPersonal on resp.strCod_res equals pers.cedula_alu
                                        where resp.strCod_lab == lab.strCod_lab && resp.strTipo_respo == "Responsable Administrativo"
                                        select new
                                        {
                                            nombre = $"{pers.apellido_alu} {pers.apellidom_alu} {pers.nombre_alu}",
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
                                                nombre = $"{pers.apellido_alu} {pers.apellidom_alu} {pers.nombre_alu}",
                                                FotoAcademico = pers.imagen_alu
                                            }).FirstOrDefault(),
                    ResponsableAdministrativo = (from resp in listResponsable
                                                 join pers in listPersonal on resp.strCod_res equals pers.cedula_alu
                                                 where resp.strCod_lab == lab.strCod_lab && resp.strTipo_respo == "Responsable Administrativo"
                                                 select new
                                                 {
                                                     nombre = $"{pers.apellido_alu} {pers.apellidom_alu} {pers.nombre_alu}",
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