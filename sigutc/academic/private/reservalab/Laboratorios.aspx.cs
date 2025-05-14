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
    string cadenaConexion;
    SqlConnection conexion;

    LAB_LABORATORIOS laboratorio2 = new LAB_LABORATORIOS();
    UB_FACULTADES facultad1 = new UB_FACULTADES();
    LAB_RESPONSABLE responsable1 = new LAB_RESPONSABLE();
    Personal personal1 = new Personal();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.cadenaConexion = ConfigurationManager.ConnectionStrings["conexionBddProductos"].ConnectionString;
        this.conexion = new SqlConnection(this.cadenaConexion);

        if (Context.User.Identity.Name == null) Response.Redirect("~/academic/private/Login.aspx");

        if (!IsPostBack)
        {
            cargarFacultad();
            cargarTabla();
        }
    }

    public void cargarFacultad()
    {
        string cedula = Context.User.Identity.Name;
        var listFacultad = facultad1.LoadUB_FACULTADES("xIdPersonal", cedula, "", "", "");

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
            var listPersonal = personal1.LoadPersonal("ALL", "", "", "", "");

            var data = listLaboratorios.Select(lab => new
            {
                lab.strCod_lab,
                lab.strNombre_lab,
                lab.strFotografia1_lab,
                ResponsableAcademico = (from resp in listResponsable
                                        join pers in listPersonal on resp.strCod_res equals pers.CEDULA_ALU
                                        where resp.strCod_lab == lab.strCod_lab && resp.strTipo_respo == "Responsable Academico"
                                        select new
                                        {
                                            nombre = pers.NOMBRE_ALU,
                                            FotoAcademico = pers.IMAGEN_ALU
                                        }).FirstOrDefault(),
                ResponsableAdministrativo = (from resp in listResponsable
                                             join pers in listPersonal on resp.strCod_res equals pers.CEDULA_ALU
                                             where resp.strCod_lab == lab.strCod_lab && resp.strTipo_respo == "Responsable Administrativo"
                                             select new
                                             {
                                                 nombre = pers.NOMBRE_ALU,
                                                 FotoAdministrativo = pers.IMAGEN_ALU
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
                var listPersonal = personal1.LoadPersonal("ALL", "", "", "", "");

                var data = listLaboratorios.Select(lab => new
                {
                    lab.strCod_lab,
                    lab.strNombre_lab,
                    lab.strFotografia1_lab,
                    ResponsableAcademico = (from resp in listResponsable
                                            join pers in listPersonal on resp.strCod_res equals pers.CEDULA_ALU
                                            where resp.strCod_lab == lab.strCod_lab && resp.strTipo_respo == "Responsable Academico"
                                            select new
                                            {
                                                nombre = pers.NOMBRE_ALU,
                                                FotoAcademico = pers.IMAGEN_ALU
                                            }).FirstOrDefault(),
                    ResponsableAdministrativo = (from resp in listResponsable
                                                 join pers in listPersonal on resp.strCod_res equals pers.CEDULA_ALU
                                                 where resp.strCod_lab == lab.strCod_lab && resp.strTipo_respo == "Responsable Administrativo"
                                                 select new
                                                 {
                                                     nombre = pers.NOMBRE_ALU,
                                                     FotoAdministrativo = pers.IMAGEN_ALU
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
        if (e.CommandName == "Reservar")
        {
            Session["laboratorioId"] = e.CommandArgument.ToString();
            Response.Redirect("ReservaLaboratorio.aspx");
        }
        else if (e.CommandName == "Informacion")
        {
            Session["laboratorioId"] = e.CommandArgument.ToString();
            Response.Redirect("InformacionLaboratorios.aspx");
        }
    }
}