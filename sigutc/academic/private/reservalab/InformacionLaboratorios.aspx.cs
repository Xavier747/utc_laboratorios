using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using ClassLibraryLaboratorios;
using System.Web.Configuration;

public partial class academic_private_reservalab_InformacionLaboratorios : System.Web.UI.Page
{
    LAB_RESPONSABLE responsable1 = new LAB_RESPONSABLE();
    LAB_LABORATORIOS laboratorio2 = new LAB_LABORATORIOS();
    Personal personal1 = new Personal();
    LAB_SOFTWARE software1 = new LAB_SOFTWARE();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Context.User.Identity.Name == "") Response.Redirect("~/academic/public/Login.aspx");

        if (!IsPostBack)
        {
            SeguridadUTC sutc = new SeguridadUTC();

            if (Request.QueryString["In"] != null)
            {
                lblCrono.Text = sutc.Desencripta(Request.Params["In"].ToString());

                //Llamado a los metodos que se deben cargar con la pagina
                cargarTabla();
                cargarSoftware();
            }
            else
            {
                Response.Redirect("~/academic/private/reservalab/Laboratorios.aspx");
            }            
        }
    }

    public void cargarTabla()
    {
        string strCodLab = lblCrono.Text;

        try
        {
            var listLaboratorio = laboratorio2.LoadLAB_LABORATORIOS("xPK", strCodLab, "", "", "");
            var listResponsable = responsable1.LoadLAB_RESPONSABLE("ALL", "", "", "", "");
            var listPersonal = personal1.Load_PERSONAL("ALL", "", "", "", "");

            var data = listLaboratorio.Select(lab => new
            {
                lab.strCod_lab,
                lab.strNombre_lab,
                lab.strFotografia1_lab,
                lab.strFotografia2_lab,
                lab.strUbicacion_lab,
                lab.strDescripcion_lab,
                lab.strTipo_lab,
                ResponsableAcademico = (from resp in listResponsable
                                        join pers in listPersonal on resp.strCod_res equals pers.cedula_alu
                                        where resp.strCod_lab == lab.strCod_lab && resp.strTipo_respo == "Responsable Academico"
                                        select new
                                        {
                                            nombre = $"{pers.apellido_alu} {pers.apellidom_alu} {pers.nombre_alu}",
                                            FotoAcademico = pers.imagen_alu,
                                            correo = pers.correo_alu
                                        }).FirstOrDefault(),
                ResponsableAdministrativo = (from resp in listResponsable
                                             join pers in listPersonal on resp.strCod_res equals pers.cedula_alu
                                             where resp.strCod_lab == lab.strCod_lab && resp.strTipo_respo == "Responsable Administrativo"
                                             select new
                                             {
                                                 nombre = $"{pers.apellido_alu} {pers.apellidom_alu} {pers.nombre_alu}",
                                                 FotoAdministrativo = pers.imagen_alu,
                                                 correo = pers.correo_alu
                                             }).FirstOrDefault(),
            });

            rptLaboratorio.DataSource = data;
            rptLaboratorio.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: " + ex.Message);
        }
    }

    public void cargarSoftware()
    {
        string strCodLab = lblCrono.Text;

        try
        {
            var listSoftware = software1.LoadLAB_SOFTWARE("xLaboratorio", strCodLab, "", "", "");

            content_software.Visible = listSoftware.Count > 0 ? true : false;

            rptSoftware.DataSource = listSoftware;
            rptSoftware.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: " + ex.Message);
        }
    }

}

  