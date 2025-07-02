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
    LAB_TIPO tipo1 = new LAB_TIPO();
    Personal personal1 = new Personal();

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
            var listTipo = tipo1.LoadLAB_TIPO("ALL", "", "", "", "");

            var data = listLaboratorio.Select(lab => new
            {
                lab.strCod_lab,
                lab.strNombre_lab,
                lab.strFotografia1_lab,
                lab.strFotografia2_lab,
                lab.strUbicacion_lab,
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
                TipoLaboratorio = (from tipo in listTipo
                                   join labo in listLaboratorio on tipo.strCod_tipoLab equals labo.strCod_tipoLab
                                   where tipo.strCod_tipoLab == lab.strCod_tipoLab
                                   select new
                                   {
                                       nombre = tipo.strNombre_tipoLab
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

}

  